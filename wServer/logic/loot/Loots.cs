using common.resources;
using System;
using System.Collections.Generic;
using System.Linq;
using wServer.networking.packets.outgoing;
using wServer.realm;
using wServer.realm.entities;
using wServer.realm.worlds.logic;
using DiscordWebhook;
using common;




namespace wServer.logic.loot
{
    public struct LootDef
    {
        public LootDef(Item item, double probabilty)
        {
            Item = item;
            Probability = probabilty;
        }

        public readonly Item Item;
        public readonly double Probability;
    }

    public class Loot : List<ILootDef>
    {
        public static Dictionary<int, int> ValuableBags = new Dictionary<int, int>
        {
            {9, 0xb353cc},
            {8, 0x3ea048},
            {11, 0xba1d1d},
            {12, 0xe8cb5a},
            {13, 0xd2b48c},
            {14, 0xfff059}
        };

        public Loot()
        {
        }
        public Loot(params ILootDef[] lootDefs)
        {  //For independent loots(e.g. chests)
            AddRange(lootDefs);
        }

        private static readonly Random Rand = new Random();

        private static readonly string[] ValuableItems = {
            "Nightmare Shard",
            "Luxury Shard",
            "Crimson Shard",
            "Extinction Shard",

            "The Zol Awakening (Token)",
            "Calling of the Titan (Token)",
            "Insignia of Valor",
        };

            public IEnumerable<Item> GetLoots(RealmManager manager, int min, int max)
        {  //For independent loots(e.g. chests)
            var consideration = new List<LootDef>();
            foreach (var i in this)
                i.Populate(manager, null, null, Rand, consideration);

            var retCount = Rand.Next(min, max);
            foreach (var i in consideration)
            {
                if (Rand.NextDouble() < i.Probability)
                {
                    yield return i.Item;
                    retCount--;
                }
                if (retCount == 0)
                    yield break;
            }
        }

        public void Handle(Enemy enemy)
        {
            var consideration = new List<LootDef>();

            //var sharedLoots = new List<Item>(); //no more kendo sticks or fire swords, ha
            foreach (var i in this)
                i.Populate(enemy.Manager, enemy, null, Rand, consideration);
            /*foreach (var i in consideration)
            {
                if (Rand.NextDouble() < i.Probability)
                    sharedLoots.Add(i.Item);
            }*/

            var dats = enemy.DamageCounter.GetPlayerData();
            var loots = enemy.DamageCounter.GetPlayerData().ToDictionary(
                d => d.Item1, d => (IList<Item>)new List<Item>());

            //foreach (var loot in sharedLoots.Where(item => item.Soulbound))
            //     loots[dats[Rand.Next(dats.Length)].Item1].Add(loot);

            foreach (var dat in dats)
            {
                consideration.Clear();
                foreach (var i in this)
                    i.Populate(enemy.Manager, enemy, dat, Rand, consideration);
                var donorBoost = 0;
                
                if (dats.Rank >= 10 && dats.Rank <= 19)
                {
                    donorBoost = 10/100;
                }
                if (dats.Rank >= 20 && dats.Rank <= 29)
                {
                    donorBoost = 20 / 100;
                }
                if (dats.Rank == 30)
                {
                    donorBoost = 30 / 100;
                }
                if (dats.Rank == 31)
                {
                    donorBoost = 30 / 100;
                }


                //double lootDropBoost = dat.Item1.LDBoostTime > 0 ? 0.5 : 0;
                double luckStatBoost = (dat.Item1.Stats.Boost[14] / 100.0) * (enemy.Manager.Config.serverSettings.fortuneMultiplier / 100);
                
                double qualifyBoost = Math.Min(1, Math.Max(0, (double)dat.Item2 * dats.Length / enemy.DamageCounter.TotalDamage));

                double lootDamageBoost = Math.Min(0.15, Math.Max(0, dat.Item2 / (double)enemy.DamageCounter.TotalDamage * dats.Length * 0.025));

                /*
                int TH;
                if (dat.Item1.RollSacredEffect(SacredEffects.TreasureHunter, 8))
                {
                    TH = 2;
                    dat.Item1.Owner.BroadcastPacketPrivate(new Notification
                    {
                        Color = new ARGB(0xDEC612),
                        ObjectId = dat.Item1.Id,
                        Message = "Treasure Hunter"
                    }, dat.Item1);

                } else
                {
                    TH = 1;
                }
                */
                double dropBoost = (0.5 + qualifyBoost / 2) * (1 + lootDamageBoost) * (1 + luckStatBoost);

                var playerLoot = loots[dat.Item1];
                foreach (var i in consideration)
                {
                    var lootBoost =(1.5 + lootDamageBoost + donorBoost) * (1 + luckStatBoost);
                    if (Rand.NextDouble() < (i.Probability * enemy.Manager.Config.serverSettings.lootMultiplier * lootBoost))
                        playerLoot.Add(i.Item);

                }
            }

            AddBagsToWorld(enemy, /*sharedLoots*/ new List<Item>(), loots);
        }

        private static void AddBagsToWorld(Enemy enemy, IList<Item> shared, IDictionary<Player, IList<Item>> soulbound)
        {
            var pub = new List<Player>();  //only people not getting soulbound
            foreach (var i in soulbound)
            {
                if (i.Value.Count > 0)
                    ShowBags(enemy, i.Value, i.Key);
                //else
                //    pub.Add(i.Key);
            }
            if (pub.Count > 0 && shared.Count > 0)
                ShowBags(enemy, shared, pub.ToArray());
        }
        public static void Chatlog(Webhook hook, string username, string text)
            {
                hook.PostData(new WebhookObject()
                {
                    username = username,
                    content = text,
                });
            }
        private static void ShowBags(Enemy enemy, IEnumerable<Item> loots, params Player[] owners)
        {
            var loot = new Webhook("https://discordapp.com/api/webhooks/732578697156100126/SuSYRs8cYRPW56RpXZRMbDH6WW_343Uqfm40mES6I1PkOqRKgiWxIsOegnrp8KJVCTC7");
            var ownerIds = owners.Select(x => x.AccountId).ToArray();
            var bagType = 0;
            var items = new Item[8];
            var idx = 0;
            foreach (var i in loots)
            {
                if (i.BagType > bagType) bagType = i.BagType;
                items[idx] = i;
                idx++;
                if (i.BagType >= 9)
                {
                    int color = 0;
                    switch (i.BagType)
                    {
                        case 9:
                            color = 0xb353cc;
                            break;
                        case 13:
                            color = 0xe8cb5a;
                            break;
                        case 14:
                            color = 0xe8cb5a;
                            break;
                    }

                    

                    string PlayerName = owners[0].Name;
                    var playerFortune = owners[0].Stats[14];
                    var guildBoost = 0;
                    ///*
                    if (owners[0].Rank >= 10 && owners[0].Rank <= 19)
                    {
                        playerFortune = owners[0].Stats[14] + 10;
                    }
                    if (owners[0].Rank >= 20 && owners[0].Rank <= 29)
                    {
                        playerFortune = owners[0].Stats[14] + 20;
                    }
                    if (owners[0].Rank == 30)
                    {
                        playerFortune = owners[0].Stats[14] + 30;
                    }
                    if (owners[0].Rank == 31)
                    {
                        playerFortune = owners[0].Stats[14] + 30;
                    }
                    if (owners[0].GuildRank >= 0)
                    {
                        var guild = owners[0].Manager.Database.GetGuild(owners[0].Client.Account.GuildId);
                        if (guild.Level >= 2)
                            playerFortune += 10;
                    }
                    
                    /*
                    switch (owners[0].Rank)
                    {
                        case 10:
                            playerFortune = owners[0].Stats[14] + 50;
                            break;
                        case 20:
                            playerFortune = owners[0].Stats[14] + 70;
                            break;
                        case 30:
                            playerFortune = owners[0].Stats[14] + 85;
                            break;
                    }
                    */
                    string ItemName = i.ObjectId;
                    double damagePercent = Math.Round(100.0 * (enemy.DamageCounter.DamageFrom(owners[0]) / (double)enemy.DamageCounter.TotalDamage), 2);
                    bool a_or_an = ItemName.ToLower().IndexOfAny("aeiou".ToCharArray()) == 0;
                    Text Valuable = new Text()
                    {
                        Name = "LOOT MAN",
                        NumStars = -1,
                        Txt = $"{PlayerName} LOOTED A{(a_or_an ? "n" : "")} {ItemName}, with {damagePercent}% DAMAGER dealt and {playerFortune} fortune! :trollface:",
                        BubbleTime = 0,
                        NameColor = 0x88ddef,
                        TextColor = color == 0 ? 0x88ef8a : color
                    };
                    if (i.BagType <= 9 || enemy.Owner is Test)
                        enemy.CurrWorld(Valuable);
                    else if (i.BagType == 11)
                    {
                        enemy.Manager.GlobalBroadcast(Valuable);
                        owners[0].Client.SendPacket(new GlobalNotification() { Text = "lgdrop" });
                    }
                    else if (i.BagType == 14)
                    {
                        enemy.Manager.GlobalBroadcast(Valuable);
                        owners[0].Client.SendPacket(new GlobalNotification() { Text = "primaldrop" });
                    }
                }

                if (idx == 8)
                {
                    ShowBag(enemy, ownerIds, bagType, items);

                    bagType = 0;
                    items = new Item[8];
                    idx = 0;
                }
            }

            if (idx > 0)
                ShowBag(enemy, ownerIds, bagType, items);
        }

        private static void ShowBag(Enemy enemy, int[] owners, int bagType, Item[] items)
        {
            ushort bag = 0x0503; // 0x0500; for brown bag default
            switch (bagType)
            {
                case 0:
                    //bag = 0x500;
                    break;

                case 1:
                    //bag = 0x506;
                    break;

                case 2:
                    bag = 0x503;
                    break;

                case 3:
                    bag = 0x508;
                    break;

                case 4:
                    bag = 0x509;
                    break;

                case 5:
                    bag = 0x050B;
                    break;

                case 6:
                    bag = 0x169a;
                    break;

                case 7:
                    bag = 0xfff;
                    break;

                case 8:
                    bag = 0x1861;
                    break;

                case 9:
                    bag = 0x050C;
                    break;

                case 10:
                    bag = 0x506f;
                    break;

                case 11:
                    bag = 0x44d4;
                    break;

                case 12:
                    bag = 0x44d5;
                    break;

                case 13:
                    bag = 0x7246;
                    break;

                case 14:
                    bag = 0x7245;
                    break;
            }
            var container = new Container(enemy.Manager, bag, 1000 * 60, true);
            for (var j = 0; j < 8; j++)
                container.Inventory[j] = items[j];
            container.BagOwners = owners;
            container.Move(
                enemy.X + (float)((Rand.NextDouble() * 2 - 1) * 0.5),
                enemy.Y + (float)((Rand.NextDouble() * 2 - 1) * 0.5));
            container.SetDefaultSize(bagType == 11 ? 120 : (bagType > 3 ? 110 : 80));
            enemy.Owner.EnterWorld(container);
        }
    }
}
