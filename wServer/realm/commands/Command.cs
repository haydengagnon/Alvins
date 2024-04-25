using DiscordWebhook;
using System;
using System.Collections.Generic;
using wServer.realm.entities;

namespace wServer.realm.commands
{
    public abstract class Command
    {
        public string CommandName { get; private set; }
        public string Alias { get; private set; }
        public int PermissionLevel { get; private set; }
        public bool ListCommand { get; private set; }
        public string WorldLevel { get; private set; }

        public class Perms
        {
            public static int Owner = 100;
            public static int TrustedDev = 99;
            public static int Dev = 90;
            public static int HeadMod = 80;
            public static int Mod = 41;
            public static int Content = 30;
            public static int VIP = 20;
            public static int Donor = 10;
        }
        //public Webhook webhook = new Webhook("https://discordapp.com/api/webhooks/628287220222263306/mC-3j1pXbp0EEYoP9Rs4d-LFObCpAP_VemJZz1LLC7rW26d7bIxCD7HXEyEVLebTBEY5");
        //public Webhook givelog = new Webhook("https://discordapp.com/api/webhooks/671553336272486400/_WQkQ9lQsktcyohHqOk7XCxt75-DPXFj4kFmVmgzOtj_otCKc2MYX2DWf_ro9khrtoGg");
        //public Webhook secretlog = new Webhook("https://discordapp.com/api/webhooks/692187298103820288/zHO9OjL5tqlsrDEE_gQ5WmMCGxgT3m-Q4b6RbCEjOwuypS2Hem-RuOz7g05pfhreZ_NP");

        protected Command(string name, int permLevel = 0, string alias = null, bool listCommand = true, string world = null)
        {
            CommandName = name;
            PermissionLevel = permLevel;
            ListCommand = listCommand;
            Alias = alias;
            WorldLevel = world;
        }

        protected abstract bool Process(Player player, string args);

        public bool HasPermission(Player player)
        {
            if (WorldLevel != null && player.Owner.Name.Equals(WorldLevel)) return true;

            return player.Client.Account.Rank >= PermissionLevel;
        }

        public bool Execute(Player player, string args, bool bypassPermission = false)
        {
            if (!bypassPermission && !HasPermission(player))
            {
                player.SendError("No permission!");
                return false;
            }

            try
            {
                return Process(player, args);
            }
            catch (Exception e)
            {
                player.SendError("Error when executing the command.");
                Program.Debug(typeof(Command), e.ToString(), warn: true);
                return false;
            }
        }

        //public void Secret2Discord(string username, string text)
        //{
        //    secretlog.PostData(new WebhookObject()
        //    {
        //        username = username,
        //        content = text,
        //    });
        //}
        //public void Give2Discord(string username, string text)
        //{
        //    if (!common.ServerConfig.EnableDebug)
        //    {
        //        givelog.PostData(new WebhookObject()
        //        {
        //            username = username,
        //            content = text,
        //        });F
        //    }
        //}
        //    public void LogToDiscord(string name, string content)
        //    {
        //        if (!common.ServerConfig.EnableDebug)
        //        {
        //            webhook.PostData(new WebhookObject()
        //            {
        //                username = name,
        //                content = content
        //            });
        //        }
        //    }
        //}

        public class CommandManager
        {
            private readonly RealmManager _manager;
            private readonly Dictionary<string, Command> _cmds;
            public IDictionary<string, Command> Commands { get { return _cmds; } }

            public CommandManager(RealmManager manager)
            {
                _manager = manager;
                _cmds = new Dictionary<string, Command>(StringComparer.InvariantCultureIgnoreCase);
                var t = typeof(Command);
                foreach (var i in t.Assembly.GetTypes())
                    if (t.IsAssignableFrom(i) && i != t)
                    {
                        var instance = (i.GetConstructor(new Type[] { typeof(RealmManager) }) == null) ?
                            (Command)Activator.CreateInstance(i) :
                            (Command)Activator.CreateInstance(i, manager);

                        try { _cmds.Add(instance.CommandName, instance); }
                        catch (Exception e)
                        { Program.Debug(typeof(Command), $"[Command Exception]: command already implemented!\n- Command: {instance.CommandName};\n- Exception: {e}", true); }

                        if (instance.Alias != null)
                        {
                            _cmds.Add(instance.Alias, instance);
                        }
                    }
            }

            public bool Execute(Player player, string text)
            {
                var index = text.IndexOf(' ');
                var cmd = text.Substring(1, index == -1 ? text.Length - 1 : index - 1);
                var args = index == -1 ? "" : text.Substring(index + 1);

                if (!_cmds.TryGetValue(cmd, out var command))
                {
                    player.SendError("Unknown command!");
                    return false;
                }

                return command.Execute(player, args);
            }
        }
    }
}
