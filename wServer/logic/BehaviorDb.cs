using common.resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using wServer.logic.loot;
using wServer.networking.packets.outgoing;
using wServer.realm;
using wServer.realm.entities;

namespace wServer.logic
{
    public partial class BehaviorDb
    {
        public RealmManager Manager { get; }

        private static int _initializing;
        private static Random rand = new Random();
        internal static BehaviorDb InitDb;
        internal static XmlData InitGameData => InitDb.Manager.Resources.GameData;

        public BehaviorDb(RealmManager manager)
        {
            Manager = manager;

            Definitions = new Dictionary<ushort, Tuple<State, Loot>>();

            if (Interlocked.Exchange(ref _initializing, 1) == 1)
            {
                throw new InvalidOperationException("Attempted to initialize multiple BehaviorDb at the same time.");
            }
            InitDb = this;

            var fields = GetType()
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(field => field.FieldType == typeof(_))
                .ToArray();
            for (var i = 0; i < fields.Length; i++)
            {
                var field = fields[i];
                ((_)field.GetValue(this))();
                field.SetValue(this, null);
            }

            InitDb = null;
            _initializing = 0;
        }

        public void ResolveBehavior(Entity entity)
        {
            if (Definitions.TryGetValue(entity.ObjectType, out var def))
                entity.SwitchTo(def.Item1);
        }

        private delegate ctor _();

        private struct ctor
        {
            public ctor Init(string objType, State rootState, params ILootDef[] defs)
            {
                var d = new Dictionary<string, State>();
                rootState.Resolve(d);
                rootState.ResolveChildren(d);
                var dat = InitDb.Manager.Resources.GameData;
                rootState.Death += (sender, e) => { if (e.Host is Enemy) handleExtraDrops(((Enemy)e.Host).DamageCounter.GetPlayerData(), (Enemy)e.Host); };
                if (defs.Length > 0)
                {
                    var loot = new Loot(defs);
                    rootState.Death += (sender, e) => { if (e.Host is Enemy) loot.Handle((Enemy)e.Host); };
                    if (dat.IdToObjectType.ContainsKey(objType))
                        InitDb.Definitions.Add(dat.IdToObjectType[objType], new Tuple<State, Loot>(rootState, loot));
                }
                else
                {
                    if (dat.IdToObjectType.ContainsKey(objType))
                        InitDb.Definitions.Add(dat.IdToObjectType[objType], new Tuple<State, Loot>(rootState, null));
                }
                return this;
            }
        }

        private static ctor Behav()
        {
            return new ctor();
        }

        public Dictionary<ushort, Tuple<State, Loot>> Definitions { get; }

        private static void handleExtraDrops(Tuple<Player, int>[] playerDamage, Enemy e)
        {
            double val;

            if (e.Owner is realm.worlds.logic.Test)
                return;

            foreach (var playerInfo in playerDamage)
            {
                if (e.GivesNoXp || e.Spawned)
                    continue;

                Player player = playerInfo.Item1;
                val = rand.NextDouble();
                if (player.Client.Account.ChanceDenom <= 0 || player.ChanceDenom <= 0)
                {
                    player.Client.Account.ChanceDenom = player.ChanceDenom = 5000;
                    player.ForceUpdate(player.ChanceDenom);
                }
                if (val <= 1.0 / player.ChanceDenom)
                {

                    player.Client.Account.ChanceDenom = player.ChanceDenom = 5000;
                    player.ForceUpdate(player.ChanceDenom);
                    player.Client.Manager.Database.UpdateAlertToken(player.Client.Account, 1);
                    player.AlertToken++;
                    player.ForceUpdate(player.AlertToken);
                    var packet = new Text
                    {
                        BubbleTime = 0,
                        NumStars = -1,
                        TextColor = 0x8700d6,
                        Name = "sex",
                        Txt = "you have gained ONE sex point."
                    };
                    player.Client.SendPacket(packet);
                } else
                {
                    int amount = player.ChanceDenom;
                    amount--;
                    player.Client.Account.ChanceDenom = player.ChanceDenom = Math.Max(1, amount);
                    player.ForceUpdate(player.ChanceDenom);
                }
            }

            if (e.ObjectDesc.AirDrop > 0)
            {
                foreach (var playerInfo in playerDamage)
                {
                    Player player = playerInfo.Item1;
                    player.Client.Manager.Database.UpdateAirStorage(player.Client.Account, e.ObjectDesc.AirDrop);
                    player.AirStorage += e.ObjectDesc.AirDrop;
                    player.ForceUpdate(player.AirStorage);
                }
            }
            if (e.ObjectDesc.WaterDrop > 0)
            {
                foreach (var playerInfo in playerDamage)
                {
                    Player player = playerInfo.Item1;
                    player.Client.Manager.Database.UpdateWaterStorage(player.Client.Account, e.ObjectDesc.WaterDrop);
                    player.WaterStorage += e.ObjectDesc.WaterDrop;
                    player.ForceUpdate(player.WaterStorage);
                }
            }

            if (e.ObjectDesc.EarthDrop > 0)
            {
                foreach (var playerInfo in playerDamage)
                {
                    Player player = playerInfo.Item1;
                    player.Client.Manager.Database.UpdateEarthStorage(player.Client.Account, e.ObjectDesc.EarthDrop);
                    player.EarthStorage += e.ObjectDesc.EarthDrop;
                    player.ForceUpdate(player.EarthStorage);
                }
            }

            if (e.ObjectDesc.FireDrop > 0)
            {
                foreach (var playerInfo in playerDamage)
                {
                    Player player = playerInfo.Item1;
                    player.Client.Manager.Database.UpdateFireStorage(player.Client.Account, e.ObjectDesc.FireDrop);
                    player.FireStorage += e.ObjectDesc.FireDrop;
                    player.ForceUpdate(player.FireStorage);
                }
            }

            if (e.ObjectDesc.Lootdrop)
            {
                foreach (var playerInfo in playerDamage)
                {
                    val = rand.NextDouble();
                    Player player = playerInfo.Item1;
                    if (val <= 0.05)
                    {
                        player.Client.Manager.Database.UpdateGoldLootbox(player.Client.Account, 1);
                        player.GoldLootbox++;
                        player.ForceUpdate(player.GoldLootbox);
                        var packet = new Text
                        {
                            BubbleTime = 0,
                            NumStars = -1,
                            TextColor = 0xFFD700,
                            Name = "#LootBox Notifier",
                            Txt = "You have obtained a Gold Lootbox Key!"
                        };
                        //player.Owner.BroadcastPacketConditional(packet, p => p.Name == player.Name);
                        player.Client.SendPacket(packet);
                        //player.BroadcastSync(packet);
                        //player.SendHelp("You have obtained a Gold Lootbox drop! Go to the Nexus to open it!");
                    }
                    val = rand.NextDouble();
                    if (val <= 0.1)
                    {
                        player.Client.Manager.Database.UpdateSilverLootbox(player.Client.Account, 1);
                        player.SilverLootbox++;
                        player.ForceUpdate(player.SilverLootbox);
                        var packet = new Text
                        {
                            BubbleTime = 0,
                            NumStars = -1,
                            TextColor = 0xC0C0C0,
                            Name = "#Lootbox Notifier",
                            Txt = "You have obtained a Silver Lootbox Key!"
                        };
                        player.Client.SendPacket(packet);
                        //player.Owner.BroadcastPacketConditional(packet, p => p.Name == player.Name);
                        //player.BroadcastSync(packet);
                        //player.SendHelp("You have obtained a Silver Lootbox drop! Go to the Nexus to open it!");
                    }
                    val = rand.NextDouble();
                    if (val <= 0.15)
                    {
                        player.Client.Manager.Database.UpdateBronzeLootbox(player.Client.Account, 1);
                        player.BronzeLootbox++;
                        player.ForceUpdate(player.BronzeLootbox);
                        var packet = new Text
                        {
                            BubbleTime = 0,
                            NumStars = -1,
                            TextColor = 0xCD7F32,
                            Name = "#Lootbox Notifier",
                            Txt = "You have obtained a Bronze Lootbox Key!"
                        };
                        player.Client.SendPacket(packet);
                        //player.Owner.BroadcastPacketConditional(packet, p => p.Name == player.Name);
                        //player.BroadcastSync(packet);
                        //player.SendHelp("You have obtained a Bronze Lootbox drop! Go to the Nexus to open it!");
                    }
                }
            }

            if (e.ObjectDesc.Elitedrop)
            {
                foreach (var playerInfo in playerDamage)
                {
                    val = rand.NextDouble();
                    double baseEliteBoxChance = 0.6;
                    double changePerBoxReceived = 0.4;
                    Player player = playerInfo.Item1;
                    int count = 0;
                    while (val <= baseEliteBoxChance)
                    {
                        count++;
                        baseEliteBoxChance *= changePerBoxReceived;
                        val = rand.NextDouble();
                    }
                    if (count > 0)
                    {
                        player.Client.Manager.Database.UpdateEliteLootbox(player.Client.Account, count);
                        player.EliteLootbox += count;
                        player.ForceUpdate(player.EliteLootbox);
                        var packet = new Text
                        {
                            BubbleTime = 0,
                            NumStars = -1,
                            TextColor = 0x04ff89,
                            Name = "#Elite Lootbox Notifier",
                            Txt = $"You have received {(count == 1 ? "an Elite Lootbox Key" : $"{count} Elite Lootbox Keys")}!"
                        };
                        player.Client.SendPacket(packet);
                    }
                }
            }

            //if (e.ObjectDesc.UElitedrop)
            //{
            //    foreach (var playerInfo in playerDamage)
            //    {
            //        val = rand.NextDouble();
            //        double baseEliteBoxChance = 1;
            //        double changePerBoxReceived = 0.5;
            //        Player player = playerInfo.Item1;
            //        int count = 0;
            //        while (val <= baseEliteBoxChance)
            //        {
            //            count++;
            //            baseEliteBoxChance *= changePerBoxReceived;
            //            val = rand.NextDouble();
            //        }
            //        if (count > 0)
            //        {
            //            player.Client.Manager.Database.UpdateEliteLootbox(player.Client.Account, count);
            //            player.EliteLootbox += count;
            //            player.ForceUpdate(player.EliteLootbox);
            //            var packet = new Text
            //            {
            //                BubbleTime = 0,
            //                NumStars = -1,
            //                TextColor = 0x04ff89,
            //                Name = "#Elite Lootbox Notifier",
            //                Txt = $"You have received {(count == 1 ? "an Elite Lootbox Key" : $"{count} Elite Lootbox Keys")}!"
            //            };
            //            player.Client.SendPacket(packet);
            //        }
            //    }
            //}

            if (e.ObjectDesc.EventLootboxDrop)
            {
                foreach (var playerInfo in playerDamage)
                {
                    val = rand.NextDouble();
                    double baseEventBoxChance = 0.7;
                    double changePerBoxReceived = 0.3;
                    Player player = playerInfo.Item1;
                    int count = 0;
                    while (val <= baseEventBoxChance)
                    {
                        count++;
                        baseEventBoxChance *= changePerBoxReceived;
                        val = rand.NextDouble();
                    }
                    if (count > 0)
                    {
                        player.Client.Manager.Database.UpdateEventLootbox(player.Client.Account, count);
                        player.EventLootbox += count;
                        player.ForceUpdate(player.EventLootbox);
                        var packet = new Text
                        {
                            BubbleTime = 0,
                            NumStars = -1,
                            TextColor = 0x72ff66,
                            Name = "#Event Lootbox Notifier",
                            Txt = $"You have received {(count == 1 ? "an Event Lootbox Key" : $"{count} Event Lootbox Keys")}!"
                        };
                        player.Client.SendPacket(packet);
                    }
                }
            }
        }
    }
}
