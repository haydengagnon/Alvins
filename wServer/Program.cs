using common;
using common.resources;
using log4net;
using log4net.Config;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using wServer.networking;
using wServer.networking.server;
using wServer.realm;

namespace wServer
{
    internal static class Program
    {
        internal static Stopwatch Uptime;
        internal static ServerConfig Config;
        internal static Resources Resources;
        internal static DiscordLogging DL;
        internal static ClientProtection CP;
        internal static RealmManager manager;
        internal static readonly ILog Log = LogManager.GetLogger("wServer");
        internal static int NewItems = 0;

        private static readonly ManualResetEvent Shutdown = new ManualResetEvent(false);

        private static void Main(string[] args)
        {
            Uptime = Stopwatch.StartNew();

            AppDomain.CurrentDomain.UnhandledException += LogUnhandledException;

            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.Name = "Entry";

            Config = args.Length > 0 ?
                ServerConfig.ReadFile(args[0]) :
                ServerConfig.ReadFile("wServer.json");

            DL = new DiscordLogging();
            CP = new ClientProtection(Config.serverSettings.checkClient, Config.serverSettings.tokens);

            Environment.SetEnvironmentVariable("ServerLogFolder", Config.serverSettings.logFolder);
            GlobalContext.Properties["ServerName"] = "GameServer";
            GlobalContext.Properties["ServerType"] = Config.serverInfo.type.ToString();

            XmlConfigurator.ConfigureAndWatch(new FileInfo(Config.serverSettings.log4netConfig));

            using (Resources = new Resources(Config.serverSettings.resourceFolder, true))
            using (var db = new Database(
                Config.dbInfo.host,
                Config.dbInfo.port,
                Config.dbInfo.auth,
                Config.dbInfo.index,
                Resources))
            {
                manager = new RealmManager(Resources, db, Config);
                manager.Run();

                var policy = new PolicyServer();
                policy.Start();

                var server = new Server(manager,
                    Config.serverInfo.port,
                    Config.serverSettings.maxConnections,
                    StringUtils.StringToByteArray(Config.serverSettings.key));
                server.Start();

                StartRestartTimers(Config.serverSettings.minutesToRestart,
                   Config.serverSettings.minutesBeforeRestartFirstWarning,
                   Config.serverSettings.minutesBeforeRestartSecondWarning);

                Console.CancelKeyPress += delegate { Shutdown.Set(); };

                Shutdown.WaitOne();

                manager.Stop();
                server.Stop();
                policy.Stop();
            }
        }

        public static void Debug(Type t, string message, bool error = false, bool fatal = false, bool warn = false)
        {
            if (ServerConfig.EnableDebug)
            {
                ILog log = LogManager.GetLogger(t);
                if (error)
                    log.Error(message);
                else if (fatal)
                    log.Fatal(message);
                else if (warn)
                    log.Warn(message);
                else
                    log.Info(message);
            }
        }

        public static void Stop()
        {
            Shutdown.Set();
        }

        private static void StartRestartTimers(int minutesToRestart, int minutesToFirstWarning, int minutesToSecondWarning)
		{
            //TODO: Allow arbitrary number of warnings and set them all up here
            RestartTimerState restartState = new RestartTimerState(RestartTimerTypes.Restart, minutesToRestart);
            RestartTimerState firstWarningState = new RestartTimerState(RestartTimerTypes.Warning, minutesToFirstWarning);
            RestartTimerState secondWarningState = new RestartTimerState(RestartTimerTypes.Warning, minutesToSecondWarning);

            Timer restartTimer = new Timer(TimerElapsed, restartState, TimeSpan.FromMinutes(minutesToRestart), TimeSpan.Zero);
            Timer firstWarningTimer = new Timer(TimerElapsed, firstWarningState, TimeSpan.FromMinutes(minutesToRestart - minutesToFirstWarning), TimeSpan.Zero);
            Timer secondWarningTimer = new Timer(TimerElapsed, secondWarningState, TimeSpan.FromMinutes(minutesToRestart - minutesToSecondWarning), TimeSpan.Zero);
        }

        private static void TimerElapsed(object state)
		{
            RestartTimerState timerState = state as RestartTimerState;
            if (timerState.Type == RestartTimerTypes.Restart)
			{
                Stop();
			}
            else if (timerState.Type == RestartTimerTypes.Warning)
			{
				manager.Chat.Announce("Server restart in " + timerState.SecondsBeforeRestart.ToString() + " minute(s)");
			}
		}

        private static void LogUnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
        }
    }
}
