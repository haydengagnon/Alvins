using System;
using System.Configuration;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Threading;

namespace UTAutoRestarter
{
	class Program
    {
        static string serverIP = ConfigurationManager.AppSettings.Get("serverIP");
        static int serverPort = Int32.Parse(ConfigurationManager.AppSettings.Get("serverPort"));
        static string wServerIP = ConfigurationManager.AppSettings.Get("wServerIP");
        static int wServerPort = Int32.Parse(ConfigurationManager.AppSettings.Get("wServerPort"));
        static string serverPath = ConfigurationManager.AppSettings.Get("serverPath");
        static string wServerPath = ConfigurationManager.AppSettings.Get("wServerPath");
        static int timeOut = Int32.Parse(ConfigurationManager.AppSettings.Get("connectionTimeout"));

        static async Task Main(string[] args)
        {
            // Set up main loop task
            ManualResetEventSlim resetEvent = new ManualResetEventSlim(false);
            Task loopTask = ServerRestarter(resetEvent);

            // Wait for either exit task (key press) or loop task to end
            Task exitTask = Task.WhenAny(loopTask, Task.Run(() => WaitForKey(ConsoleKey.X)));
            await exitTask;

            // Clean up and exit
            resetEvent.Set();
            await loopTask;
            resetEvent.Dispose();
            Console.WriteLine("Stopped Restarter");
        }

        static void WaitForKey(ConsoleKey key)
        {
            Console.WriteLine($"Press '{key}' to exit...");
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var consoleKey = Console.ReadKey(intercept: true).Key;
                    if (consoleKey == key)
                    {
                        Console.WriteLine("Stopping...");
                        break;
                    }
                }
                Thread.Sleep(100);
            }
        }

        static async Task ServerRestarter(ManualResetEventSlim resetEvent)
        {
            while (!resetEvent.IsSet)
            {
                using (Socket server = new Socket(SocketType.Stream, ProtocolType.Tcp),
                    wServer = new Socket(SocketType.Stream, ProtocolType.Tcp))
				{
                    if (!await TryConnect(server, serverPath, serverIP, serverPort, timeOut))
                    {
                        Console.WriteLine("Server connection timed out. Attempting to restart...");
                        System.Diagnostics.Process.Start(serverPath);
                    }

                    if (!await TryConnect(wServer, wServerPath, wServerIP, wServerPort, timeOut))
                    {
                        Console.WriteLine("wServer connection timed out. Attempting to restart...");
                        System.Diagnostics.Process.Start(wServerPath);
                    }

                    server.Dispose();
                    wServer.Dispose();
                }

                await Task.Delay(10000);
            }
        }

        static async Task<bool> TryConnect(Socket server, string path, string ip, int port, int timeOut)
        {
            Console.WriteLine("Attempting to connect to " + path);

            DateTime dt = DateTime.Now;
            while ((DateTime.Now - dt).TotalSeconds < timeOut)
            {
                try
                {
                    server.Connect(ip, port);
                    Console.WriteLine("Successfully connected to " + path);
                    return true;
                }
                catch (SocketException ex) when (ex.ErrorCode == 10056) // Socket is already connected
                {
                    Console.WriteLine(path + " is already running.");
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to connect to " + path + ", error:" + ex.ToString());
                }

                await Task.Delay(1000);
            }

            return false;
        }

        public static bool IsConnected(Socket socket)
        {
            return !(socket.Poll(1000000, SelectMode.SelectRead) && socket.Available == 0);
        }
    }
}
