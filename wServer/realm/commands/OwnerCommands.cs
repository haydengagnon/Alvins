using common;
using System;
using System.Text;
using System.Linq;
using wServer.realm.entities;

namespace wServer.realm.commands
{
    internal class KillPlayerCommand : Command
    {
        public KillPlayerCommand() : base("killPlayer", Perms.Owner)
        {
        }

        protected override bool Process(Player player, string args)
        {
            foreach (var i in player.Manager.Clients.Keys)
            {
                if (i.Account.Name.EqualsIgnoreCase(args))
                {
                    i.Player.HP = 0;
                    i.Player.Death(player.Name);
                    player.SendInfo("Player killed!");
                    return true;
                }
            }
            player.SendError($"Player '{args}' could not be found!");
            return false;
        }
    }
    internal class ChangeBoostCommand : Command
    {
        public ChangeBoostCommand() : base("changeboost", permLevel: 100) { }

        protected override bool Process(Player player, string args)
        {
            if (string.IsNullOrEmpty(args))
            {
                player.SendError("/changeboost <boost>");
                return false;
            }
            var boost = int.Parse(args);

            if (player.Rank < 100 & boost > 30)
            {
                player.SendError("Mod's cant go this high! Sorry!");
                return false;
            }

            player.SendInfo("Boost changed.");
            player.Manager.Config.serverSettings.lootMultiplier = 1f + boost / 100f;
            return true;
        }
    }

    internal class ChangeFortuneModCommand : Command
    {
        public ChangeFortuneModCommand() : base("changefortune", permLevel: 100) { }

        protected override bool Process(Player player, string args)
        {
            if (string.IsNullOrEmpty(args))
            {
                player.SendError("/changefortune <modifier amount (multiplied by 100)>");
                return false;
            }
            var boost = int.Parse(args);

            if (player.Rank < 100)
            {
                player.SendError("Mod's cant go this high! Sorry!");
                return false;
            }

            player.SendInfo("Modifier changed.");
            player.Manager.Config.serverSettings.fortuneMultiplier = boost;
            return true;
        }
    }
    internal class RankCommand : Command
    {
        public RankCommand() : base("pintobeans" , Perms.Owner)
        {
        }

        protected override bool Process(Player player, string args)
        {
            var index = args.IndexOf(' ');
            if (string.IsNullOrEmpty(args) || index == -1)
            {
                player.SendInfo("this command does nothing");
                return false;
            }

            var name = args.Substring(0, index);
            var rank = int.Parse(args.Substring(index + 1));

            if (Database.GuestNames.Contains(name, StringComparer.InvariantCultureIgnoreCase))
            {
                player.SendError("Cannot rank unnamed accounts.");
                return false;
            }


            var id = player.Manager.Database.ResolveId(name);
            /*
            if (id == player.AccountId)
            {
                player.SendError("Cannot rank self.");
                return false;
            }
            */

            var acc = player.Manager.Database.GetAccount(id);
            if (id == 0 || acc == null)
            {
                player.SendError("Account not found!");
                return false;
            }

            // kick player from server to set rank
            foreach (var i in player.Manager.Clients.Keys)
                if (i.Account.Name.EqualsIgnoreCase(name))
                    i.Disconnect();
            
            if (acc.Admin && rank < 40)
            {
                // reset account
                player.Manager.Database.WipeAccount(
                    acc, player.Manager.Resources.GameData, player.Name);
                acc.Reload();
            } 

            acc.Admin = rank >= 80;
            acc.LegacyRank = rank;
            acc.Hidden = false;
            acc.FlushAsync();
            //Secret2Discord("Rank Log", $"Rank Log: [{player.Name}] has ranked [{acc.Name}] to [{acc.LegacyRank}]");
            player.SendInfo($"{acc.Name} given legacy rank {acc.LegacyRank}{((acc.Admin) ? " and now has admin status" : "")}.");
            return true;
        }
    }

    internal class OverrideAccountCommand : Command
    {
        public OverrideAccountCommand() : base("override", Perms.Owner)
        {
        }

        protected override bool Process(Player player, string name)
        {
            var acc = player.Client.Account;

            if (string.IsNullOrWhiteSpace(name))
            {
                player.SendError("Usage: /override <player name>");
                return false;
            }

            var id = player.Manager.Database.ResolveId(name);
            if (id == 0)
            {
                player.SendError("Account not found!");
                return false;
            }

            acc.AccountIdOverride = id;
            acc.FlushAsync();
            player.SendInfo("Account override set.");
            //Secret2Discord("Override Log", $"Override Log: [{player.Name}] has overrided [{acc.Name}]");
            return true;
        }
    }

    internal class UnnameCommand : Command
    {
        public UnnameCommand() : base("unname", Perms.Owner)
        {
        }

        protected override bool Process(Player player, string args)
        {
            if (string.IsNullOrWhiteSpace(args))
            {
                player.SendInfo("Usage: /unname <player name>");
                return false;
            }

            var playerName = args;

            var id = player.Manager.Database.ResolveId(playerName);
            if (id == 0)
            {
                player.SendError("Player account not found!");
                return false;
            }

            string lockToken = null;
            var key = Database.NAME_LOCK;
            var db = player.Manager.Database;

            try
            {
                while ((lockToken = db.AcquireLock(key)) == null) ;

                var acc = db.GetAccount(id);
                if (acc == null)
                {
                    player.SendError("Account doesn't exist.");
                    return false;
                }

                using (var l = db.Lock(acc))
                    if (db.LockOk(l))
                    {
                        while (!db.UnnameIGN(acc, lockToken)) ;
                        player.SendInfo("Account succesfully unnamed.");
                    }
                    else
                        player.SendError("Account in use.");
            }
            finally
            {
                if (lockToken != null)
                    db.ReleaseLock(key, lockToken);
            }

            return true;
        }
    }

    internal class ChangePassCommand : Command
    {
        public ChangePassCommand() : base("changepass", Perms.Dev)
        {
        }

        protected override bool Process(Player player, string args)
        {
            if (string.IsNullOrWhiteSpace(args))
            {
                player.SendInfo("Usage: /changepass <playerID>");
                return false;
            }

            bool success = Int32.TryParse(args, out int x);
            int id = 0;

            if (success)
            {
                id = x;
            } else
            {
                player.SendError("Player account not found!");
                return false;
            }
            
            var Account = player.Manager.Database.GetAccount(id);
            var playerName = Account.Name;
            int length = 7;

            // creating a StringBuilder object()
            StringBuilder str_build = new StringBuilder();
            Random random = new Random();

            char letter;

            for (int i = 0; i < length; i++)
            {
                double flt = random.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * flt));
                letter = Convert.ToChar(shift + 65);
                str_build.Append(letter);
            }
            player.Manager.Database.ChangePassword(Account.UUID, str_build.ToString());

            String winning = "Success " + playerName + " (" + id + ") " + " password changed to " + str_build.ToString();
            player.SendInfo(winning);

            return true;
        }
    }
}
