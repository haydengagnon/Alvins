using System;
using System.Collections.Generic;

namespace wServer.realm
{
    class ForgeList
    {
        public static readonly string FailedForge = "NULL";

        public enum ForgeData
        {
            Other,
            Sacred,
            Rusted,
            Godly,
            Nightmare,
            HeartSC,
            HeartLG
        }

        public static readonly Dictionary<string[], Tuple<string, string>> OtherList = new Dictionary<string[], Tuple<string, string>>
        {
            #region Dreadcull Upgrades
        { new [] { "Dreadcull Water T1" }, new Tuple<string, string>("Dreadcull Water", "Dreadcull T1 Upgrade") },
        { new [] { "Dreadcull Fire T1" }, new Tuple<string, string>("Dreadcull Fire", "Dreadcull T1 Upgrade") },
        { new [] { "Dreadcull Air T1" }, new Tuple<string, string>("Dreadcull Air ", "Dreadcull T1 Upgrade") },
        { new [] { "Dreadcull Earth T1" }, new Tuple<string, string>("Dreadcull Earth", "Dreadcull T1 Upgrade") },

        { new [] { "Dreadcull Water T2" }, new Tuple<string, string>("Dreadcull Water T1", "Dreadcull T2 Upgrade") },
        { new [] { "Dreadcull Fire T2" }, new Tuple<string, string>("Dreadcull Fire T1", "Dreadcull T2 Upgrade") },
        { new [] { "Dreadcull Air T2" }, new Tuple<string, string>("Dreadcull Air T1", "Dreadcull T2 Upgrade") },
        { new [] { "Dreadcull Earth T2" }, new Tuple<string, string>("Dreadcull Earth T1", "Dreadcull T2 Upgrade") },

        { new [] { "Dreadcull Water T3" }, new Tuple<string, string>("Dreadcull Water T2", "Dreadcull T3 Upgrade") },
        { new [] { "Dreadcull Fire T3" }, new Tuple<string, string>("Dreadcull Fire T2", "Dreadcull T3 Upgrade") },
        { new [] { "Dreadcull Air T3" }, new Tuple<string, string>("Dreadcull Air T2", "Dreadcull T3 Upgrade") },
        { new [] { "Dreadcull Earth T3" }, new Tuple<string, string>("Dreadcull Earth T2", "Dreadcull T3 Upgrade") },
        
        { new [] { "Dreadcull Water T4" }, new Tuple<string, string>("Dreadcull Water T3", "Dreadcull T4 Upgrade") },
        { new [] { "Dreadcull Fire T4" }, new Tuple<string, string>("Dreadcull Fire T3", "Dreadcull T4 Upgrade") },
        { new [] { "Dreadcull Air T4" }, new Tuple<string, string>("Dreadcull Air T3", "Dreadcull T4 Upgrade") },
        { new [] { "Dreadcull Earth T4" }, new Tuple<string, string>("Dreadcull Earth T3", "Dreadcull T4 Upgrade") },

        { new [] { "Dreadcull Water T5" }, new Tuple<string, string>("Dreadcull Water T4", "Dreadcull T5 Upgrade") },
        { new [] { "Dreadcull Fire T5" }, new Tuple<string, string>("Dreadcull Fire T4", "Dreadcull T5 Upgrade") },
        { new [] { "Dreadcull Air T5" }, new Tuple<string, string>("Dreadcull Air T4", "Dreadcull T5 Upgrade") },
        { new [] { "Dreadcull Earth T5" }, new Tuple<string, string>("Dreadcull Earth T4", "Dreadcull T5 Upgrade") },

        { new [] { "Dreadcull Water T6" }, new Tuple<string, string>("Dreadcull Water T5", "Dreadcull T6 Upgrade") },
        { new [] { "Dreadcull Fire T6" }, new Tuple<string, string>("Dreadcull Fire T5", "Dreadcull T6 Upgrade") },
        { new [] { "Dreadcull Air T6" }, new Tuple<string, string>("Dreadcull Air T5", "Dreadcull T6 Upgrade") },
        { new [] { "Dreadcull Earth T6" }, new Tuple<string, string>("Dreadcull Earth T5", "Dreadcull T6 Upgrade") },

        { new [] { "Dreadcull Water T7" }, new Tuple<string, string>("Dreadcull Water T6", "Dreadcull T7 Upgrade") },
        { new [] { "Dreadcull Fire T7" }, new Tuple<string, string>("Dreadcull Fire T6", "Dreadcull T7 Upgrade") },
        { new [] { "Dreadcull Air T7" }, new Tuple<string, string>("Dreadcull Air T6", "Dreadcull T7 Upgrade") },
        { new [] { "Dreadcull Earth T7" }, new Tuple<string, string>("Dreadcull Earth T6", "Dreadcull T7 Upgrade") },

        { new [] { "Dreadcull Water T8" }, new Tuple<string, string>("Dreadcull Water T7", "Dreadcull T8 Upgrade") },
        { new [] { "Dreadcull Fire T8" }, new Tuple<string, string>("Dreadcull Fire T7", "Dreadcull T8 Upgrade") },
        { new [] { "Dreadcull Air T8" }, new Tuple<string, string>("Dreadcull Air T7", "Dreadcull T8 Upgrade") },
        { new [] { "Dreadcull Earth T8" }, new Tuple<string, string>("Dreadcull Earth T7", "Dreadcull T8 Upgrade") },

        { new [] { "Dreadcull Water T9" }, new Tuple<string, string>("Dreadcull Water T8", "Dreadcull T9 Upgrade") },
        { new [] { "Dreadcull Fire T9" }, new Tuple<string, string>("Dreadcull Fire T8", "Dreadcull T9 Upgrade") },
        { new [] { "Dreadcull Air T9" }, new Tuple<string, string>("Dreadcull Air T8", "Dreadcull T9 Upgrade") },
        { new [] { "Dreadcull Earth T9" }, new Tuple<string, string>("Dreadcull Earth T8", "Dreadcull T9 Upgrade") },
        #endregion Dreadcull Upgrades
        
        { new [] { "revives two" }, new Tuple<string, string>("revives free", "revives free") },
        { new [] { "revives three" }, new Tuple<string, string>("revives two", "revives two") },
        //{ new [] { "welding5" }, new Tuple<string, string>("welding", "welding") },
        { new [] { "Necron's Blade" }, new Tuple<string, string>("Necron's Handle", "Wither Catalyst") },
        { new [] { "Wither Void" }, new Tuple<string, string>("Shadow Warp", "Wither Shield") },
        { new [] { "The Scroll" }, new Tuple<string, string>("Implosion", "Wither Void") },
        { new [] { "Hyperion" }, new Tuple<string, string>("Necron's Blade", "The Scroll") },
        { new [] { "cock ruler" }, new Tuple<string, string>("pencil staff", "SLAVE BOW") },
        { new [] { "piss bckuet" }, new Tuple<string, string>("patfot 2.0", "remains of finland") },
        { new [] { "welding3" }, new Tuple<string, string>("welding128", "welding128") },
        { new [] { "ender daggers" }, new Tuple<string, string>("blaze rod", "ender pearl") },
        { new [] { "Dragonslayer" }, new Tuple<string, string>("toniard of fatly retribution", "the dragon egg") },
        { new [] { "eye ender" }, new Tuple<string, string>("blaze powder", "ender pearl") },
        { new [] { "Bloodstone of Enmity" }, new Tuple<string, string>("welding", "MONKEY MONEY") },
        { new [] { "quiggin" }, new Tuple<string, string>("cock ruler", "welding") },
        { new [] { "welding2" }, new Tuple<string, string>("T3 UNLOCKER", "quiggin") },
        { new [] { "Ahnecdoic Potion" }, new Tuple<string, string>("the NUT", "Lumeneneyil") },
        { new [] { "T3 UNLOCKER" }, new Tuple<string, string>("quiggin", "Bloodstone of Enmity") },
        { new [] { "Golden Medal" }, new Tuple<string, string>("STAFF OF EPIC KILL", "HANDCANNON") },
        { new [] { "neco arc gun" }, new Tuple<string, string>("HANDCANNON", "welding3") },
        { new [] { "the TACKS" }, new Tuple<string, string>("monkey shuriken OG", "MONKEY MONEY") },
        { new [] { "mysteriuous blue tablet" }, new Tuple<string, string>("ortars LEAN", "fat stack of fuckin cash") },
        { new [] { "SNIPER MONEY MONEY MONEY HELMET" }, new Tuple<string, string>("SNIPER MONKEY HELMET", "fat stack of fuckin cash") },
        { new [] { "PLASMA GOGGLE" }, new Tuple<string, string>("fire BLASTER", "MONKEY MONEY") },
        { new [] { "money pimp staff" }, new Tuple<string, string>("PLASMA GOGGLE", "fat stack of fuckin cash") },
        { new [] { "remains of finland"}, new Tuple<string, string>("FINLAND SCEPTER", "NUT SWORD") },
        { new [] { "cuckson gun" }, new Tuple<string, string>("poggy gun", "choina stave") },
        { new [] { "luminite BAR" }, new Tuple<string, string>("luminite", "luminite") },
        { new [] { "vortex boopster lol", "stardust helmets", "nebula arcanuym", }, new Tuple<string, string>("luminite BAR", "luminite BAR") },
        { new [] { "Roft Neccopolis PRIVATE Key" }, new Tuple<string, string>("Roft Neccopolis Key", "raindow of Maxy") },
        { new [] { "Spaceship PRIVATE Key" }, new Tuple<string, string>("Spaceship Token", "raindow of Maxy") },
        { new [] { "Asgard PRIVATE Key" }, new Tuple<string, string>("ASGARD Token", "raindow of Maxy") },
        { new [] { "bronze catseye" }, new Tuple<string, string>("pink catseye", "pink catseye") },
        { new [] { "SILVER catseye" }, new Tuple<string, string>("bronze catseye", "bronze catseye") },
        { new [] { "GOLD catseye" }, new Tuple<string, string>("SILVER catseye", "SILVER catseye") },
        { new [] { "RAINBOW catseye" }, new Tuple<string, string>("GOLD catseye", "GOLD catseye") },
        { new [] { "CHAOS catseye" }, new Tuple<string, string>("RAINBOW catseye", "RAINBOW catseye") },
        { new [] { "Deep Sea PRIVATE Key" }, new Tuple<string, string>("Deep Sea Token", "raindow of Maxy") },
        { new [] { "FAVELA PRIVATE Key" }, new Tuple<string, string>("FAVELA Token", "raindow of Maxy") },
        { new [] { "FALLEN PRIVATE Key" }, new Tuple<string, string>("A Fallen Light (Token)", "raindow of Maxy") },
        { new [] { "bone club" }, new Tuple<string, string>("sons sparlking", "pizzer slice") },
        { new [] { "cold helmets" }, new Tuple<string, string>("xue hua tome tome", "coal amror") },
        { new [] { "Golden Medal" }, new Tuple<string, string>("Kernel of the Gods", "swag swag") },
        { new [] { "neco arc gun" }, new Tuple<string, string>("HANDCANNON", "Lumeneneyil") },
        { new [] { "Wrath of Calamity" }, new Tuple<string, string>("Spiritclaw", "core of claamity") },
        { new [] { "patfot 2.0", "patfot 2.0" ,"patfot 2.0", "patfot 2.0", "patfot 2.0", "patfot 2.0", "patfot 2.0", "patfot 2.0", "patfot 2.0" ,"swag swag" }, new Tuple<string, string>("cold helmets", "bone club") },
        { new [] { "MONKEY MONEY", "MONKEY MONEY", "MONKEY MONEY", "MONKEY MONEY", "fat stack of fuckin cash" }, new Tuple<string, string>("MONKEY MONEY", "MONKEY MONEY") },
        { new [] { "patfot 2.0", "patfot 2.0" ,"patfot 2.0", "patfot 2.0", "patfot 2.0", "patfot 2.0", "patfot 2.0", "patfot 2.0", "patfot 2.0" ,"swag swag" }, new Tuple<string, string>("patfot 2.0", "patfot 2.0") },
        { new [] { "balls HD", "balls HD", "balls HD", "balls HD", "balls HD", "balls HD", "balls HD", "balls HD", "balls HD", "balls HD", "balls HD", "balls HD", "balls HD", "balls HDD" }, new Tuple<string, string>("creamy armor rack", "eggoblade") },
        { new [] { "stone sword" }, new Tuple<string, string>("wither skeleton skull", "wither skeleton skull") },
        { new [] { "FINNISH STEM NUMBER ONE" }, new Tuple<string, string>("FINLAND PRIDE", "stupid asshole this is same rarity as handcannon you fucking suck") },
        { new [] { "two fac3d baby" }, new Tuple<string, string>("spamton skull", "Glombulus Rex") },
        { new [] { "STAFF OF EPIC KILL" }, new Tuple<string, string>("the Jar", "the rod") },
        { new [] { "END PRIVATE Key" }, new Tuple<string, string>("eye ender", "raindow of Maxy")},
        { new [] { "MANIC PRIVATE Key" }, new Tuple<string, string>("key cat lol", "raindow of Maxy")},
        
        #region rotf craftables
        { new [] { "CUCKSON SLASHER ULTIMATE" }, new Tuple<string, string>("CUCKSON SLASHER (welsh)", "primal THUSULA Upgrade") },
        { new [] { "[ROTF] The Winybaby New Account Experience" }, new Tuple<string, string>("[ROTF] Mad loot montage", "primal BEL Upgrade") },
        { new [] { "Wand of the Cocked Balls" }, new Tuple<string, string>("Wand of the Cock Balls", "primal DIAGON Upgrade") },
        { new [] { "poland SLAYER" }, new Tuple<string, string>("kitty cat UwU :3", "primal LYCAON Upgrade") },
        { new [] { "Reaper's Bow" }, new Tuple<string, string>("fleshy ten don", "primal ELITHOR Upgrade") },
        { new [] { "Valorant" }, new Tuple<string, string>("apex Legends", "primal NOVUS Upgrade") },
        { new [] { "Grungle" }, new Tuple<string, string>("boobie plate", "primal BEL Upgrade") },
        { new [] {  "Slobulon" }, new Tuple<string, string>("zolmex server breastplate", "primal THUSULA Upgrade") },
        { new [] { "stupid hat" }, new Tuple<string, string>("cucky helmet", "primal BEL Upgrade") },
        { new [] { "if you  wear this seal you suck and smell like piss" }, new Tuple<string, string>("seal balls nuts", "primal THUSULA Upgrade") },
        { new [] { "bonebonebonebone" }, new Tuple<string, string>("Robe of the Deez Nuts", "primal DIAGON Upgrade") },
        { new [] { "da ribz" }, new Tuple<string, string>("no no no no", "primal DIAGON Upgrade") },
        { new [] { "ANGER HEART GRRR" }, new Tuple<string, string>("smile heart cute :)", "primal LYCAON Upgrade") },
        { new [] { "elongated muskrat cloth" }, new Tuple<string, string>("fursuit owo", "primal LYCAON Upgrade") },
        { new [] { "necklace of FANGZ uwu" }, new Tuple<string, string>("rotmg predator Necklace", "primal LYCAON Upgrade") },
        { new [] {  "destiny moderator" }, new Tuple<string, string>("destiny modenator", "primal ELITHOR Upgrade" )},
        { new [] { "i hate this name" }, new Tuple<string, string>("eligor goul goglge", "primal ELITHOR Upgrade") },
        { new [] { "Blessing of the Moon Presence" }, new Tuple<string, string>("Twilight Grommit", "primal ELITHOR Upgrade") },
        { new [] {  "TURKISH FLAG LOVE TURKEY" }, new Tuple<string, string>("godes primal ultimate RING", "primal NORGHUS Upgrade") },
        { new [] { "THE STORM 2" }, new Tuple<string, string>("THE STORM", "primal NORGHUS Upgrade") },
        { new [] { "another dimension another dimension PEW" }, new Tuple<string, string>("tome of Femoid Attention", "primal NORGHUS Upgrade") },
        { new [] { "fuck you rom" }, new Tuple<string, string>("AR quiver", "primal NORGHUS Upgrade") },
        { new [] { "unimaginable Tilapia Plantation" }, new Tuple<string, string>("fleshy brown", "primal NORGHUS Upgrade") },
        { new [] { "scepter of big blast bam" }, new Tuple<string, string>("extreme prejuice of ssceptr?", "primal NOVUS Upgrade") },
        { new [] { "Glombulus Rex" }, new Tuple<string, string>("polish skull", "primal NOVUS Upgrade") },
            #endregion rotf craftables
            #region rotf upgraders
        { new [] { "primal THUSULA Upgrade", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades"  }, new Tuple<string, string>("CUCKSON SLASHER (welsh)", "CUCKSON SLASHER (welsh)") },
        { new [] { "primal BEL Upgrade", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades" }, new Tuple<string, string>("[ROTF] Mad loot montage", "[ROTF] Mad loot montage") },
        { new [] { "primal DIAGON Upgrade", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades" }, new Tuple<string, string>("Wand of the Cock Balls", "Wand of the Cock Balls") },
        { new [] { "primal LYCAON Upgrade", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades" }, new Tuple<string, string>("kitty cat UwU :3", "kitty cat UwU :3") },
        { new [] { "primal ELITHOR Upgrade", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades" }, new Tuple<string, string>("fleshy ten don", "fleshy ten don") },
        { new [] { "primal NOVUS Upgrade", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades" }, new Tuple<string, string>("apex Legends", "apex Legends") },
        { new [] { "primal BEL Upgrade", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades" }, new Tuple<string, string>("boobie plate", "boobie plate") },
        { new [] { "primal THUSULA Upgrade", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades" }, new Tuple<string, string>("zolmex server breastplate", "zolmex server breastplate") },
        { new [] { "primal BEL Upgrade", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades" }, new Tuple<string, string>("cucky helmet", "cucky helmet") },
        { new [] { "primal THUSULA Upgrade", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades" }, new Tuple<string, string>("seal balls nuts", "seal balls nuts") },
        { new [] { "primal DIAGON Upgrade", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades" }, new Tuple<string, string>("Robe of the Deez Nuts", "Robe of the Deez Nuts") },
        { new [] { "primal DIAGON Upgrade", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades" }, new Tuple<string, string>("no no no no", "no no no no") },
        { new [] { "primal LYCAON Upgrade", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades" }, new Tuple<string, string>("smile heart cute :)", "smile heart cute :)") },
        { new [] { "primal LYCAON Upgrade", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades" }, new Tuple<string, string>("fursuit owo", "fursuit owo") },
        { new [] { "primal LYCAON Upgrade", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades" }, new Tuple<string, string>("rotmg predator Necklace", "rotmg predator Necklace") },
        { new [] { "primal ELITHOR Upgrade", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades" }, new Tuple<string, string>("destiny modenator", "destiny modenator" )},
        { new [] { "primal ELITHOR Upgrade", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades" }, new Tuple<string, string>("eligor goul goglge", "eligor goul goglge") },
        { new [] { "primal ELITHOR Upgrade", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades" }, new Tuple<string, string>("Twilight Grommit", "Twilight Grommit") },
        { new [] { "primal NORGHUS Upgrade", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades" }, new Tuple<string, string>("godes primal ultimate RING", "godes primal ultimate RING") },
        { new [] { "primal NORGHUS Upgrade", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades" }, new Tuple<string, string>("THE STORM", "THE STORM") },
        { new [] { "primal NORGHUS Upgrade", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades" }, new Tuple<string, string>("tome of Femoid Attention", "tome of Femoid Attention") },
        { new [] { "primal NORGHUS Upgrade", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades" }, new Tuple<string, string>("AR quiver", "AR quiver") },
        { new [] { "primal NORGHUS Upgrade", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades" }, new Tuple<string, string>("fleshy brown", "fleshy brown") },
        { new [] { "primal NOVUS Upgrade", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades" }, new Tuple<string, string>("extreme prejuice of ssceptr?", "extreme prejuice of ssceptr?") },
        { new [] { "primal NOVUS Upgrade", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades", "Darkin Blades" }, new Tuple<string, string>("polish skull", "polish skull") },

            #endregion rotf upgraders
            
            { new [] { "mario hat", "mario hat", "mario hat", "mario hat", "mario hat", "mario hat", "waluigi hat" }, new Tuple<string, string>("sealed candy skull", "mushroom helm") },
        { new [] { "mario hat", "mario hat", "mario hat", "mario hat", "mario hat", "mario hat", "waluigi hat" }, new Tuple<string, string>("mario hat", "mario hat") }
        };

        public static readonly Dictionary<string[], Tuple<string, string>> GodlyList = new Dictionary<string[], Tuple<string, string>>
        {
            { new [] { "Poseidon's Gauntlet" }, new Tuple<string, string>("Ring of the Seven Seas", "Godly Remnant") },
            { new [] { "Shade of the Phoenix" }, new Tuple<string, string>("Blazed Bow", "Godly Remnant") },
            { new [] { "Blade of the Angel's Honor" }, new Tuple<string, string>("Dagger of Gilded Pride", "Godly Remnant") },
            { new [] { "Drannol's Redemption" }, new Tuple<string, string>("Drannol's Judgement", "Godly Remnant") },
            { new [] { "Revenant Claw" }, new Tuple<string, string>("Spiritclaw", "Godly Remnant") },
            { new [] { "Omnipotence Ring" }, new Tuple<string, string>("Omnipotent Ring", "Godly Remnant") }
        };

        public static readonly Dictionary<string[], Tuple<string, string>> SacredList = new Dictionary<string[], Tuple<string, string>>
        {
            { new [] { "Alien Tech", "Molten Bow", "Hornet Sting" }, new Tuple<string, string>("Rusted Bow", "Sacred Essence") },
            { new [] { "The Dragon's Breath", "Lance of the King's Successor", "Bloodseeker's Redemption" }, new Tuple<string, string>("Rusted Lance", "Sacred Essence") },
            { new [] { "Axe of Demise", "Warlord's Sword", "Avaricious Javelin" }, new Tuple<string, string>("Rusted Sword", "Sacred Essence") },
            { new [] { "Ancient Wood Cane", "Extraterrestrial Wand", "Wyvern's Touch" }, new Tuple<string, string>("Rusted Wand", "Sacred Essence") },
            { new [] { "Against the World", "Toxin Infused Blade", "Honeycomb Dagger" }, new Tuple<string, string>("Rusted Dagger", "Sacred Essence") },
            { new [] { "Staff of Corrupt Demons", "Cane of the Devil", "A Wizard's Precious Treasure" }, new Tuple<string, string>("Rusted Staff", "Sacred Essence") },
            { new [] { "A Harpy's Fury", "Gold and Silver Blades", "Hot Chilli Blades" }, new Tuple<string, string>("Rusted Blades", "Sacred Essence") },
            { new [] { "Sunrise Katana", "Raven's Claw", "Unsheathed Fury of Yukimura" }, new Tuple<string, string>("Rusted Katana", "Sacred Essence") },
            { new [] { "Overseer Robe", "Inferno Garments", "Skullsplitter Robe" }, new Tuple<string, string>("Rusted Robe", "Sacred Essence") },
            { new [] { "Angelic Platemail", "Bloodshed Armor", "Beyond the Nature's Power" }, new Tuple<string, string>("Rusted Platemail", "Sacred Essence") },
            { new [] { "Tree-sap Armor", "Hide of the Defender", "Awakening of Bravery" }, new Tuple<string, string>("Rusted Leather Hide", "Sacred Essence") },
            { new [] { "Spectral Globe", "Creation of Anubis" }, new Tuple<string, string>("Rusted Prism", "Sacred Essence") },
            { new [] { "Demonic Siphon", "Advanced Tech Siphon"}, new Tuple<string, string>("Rusted Siphon", "Sacred Essence") },
            { new [] { "Drenched Gowns of Attila", "Spectres Shroud" }, new Tuple<string, string>("Rusted Cloak", "Sacred Essence") },
            { new [] { "Marvellous Concoction", "Vial of Corrosive Venom" }, new Tuple<string, string>("Rusted Poison", "Sacred Essence") },
            { new [] { "Tarnished Cloths", "Blood-Stained Jacket" }, new Tuple<string, string>("Rusted Jacket", "Sacred Essence") },
            { new [] { "Dice of the Scriptures", "Dice of Resistance" }, new Tuple<string, string>("Rusted Dice", "Sacred Essence") },
            { new [] { "Grasp of the Gods", "Poison-Tip Quiver" }, new Tuple<string, string>("Rusted Quiver", "Sacred Essence") },
            { new [] { "Dragon-Snap Trap", "Devil's Snare Trap" }, new Tuple<string, string>("Rusted Trap", "Sacred Essence") },
            { new [] { "Haunted Talisman", "Angelic Talisman" }, new Tuple<string, string>("Rusted Talisman", "Sacred Essence") },
            { new [] { "Earthquake Tome", "Eternal Glory" }, new Tuple<string, string>("Rusted Tome", "Sacred Essence") },
            { new [] { "Rejoiced Rod of Victory", "Weeping Magicians Calling" }, new Tuple<string, string>("Rusted Scepter", "Sacred Essence") },
            { new [] { "Shooting Star", "Weighted Star" }, new Tuple<string, string>("Rusted Shuriken", "Sacred Essence") },
            { new [] { "Sheath of Dark Deeds", "Inscribed Sheath" }, new Tuple<string, string>("Rusted Sheath", "Sacred Essence") },
            { new [] { "Hellfire Charm", "Purity Charm" }, new Tuple<string, string>("Rusted Charm", "Sacred Essence") },
            { new [] { "Ancient Runestone", "Platonic Lifeline" }, new Tuple<string, string>("Rusted Spell", "Sacred Essence") },
            { new [] { "Nightcrawler's Head", "Ancient Aztec Skull" }, new Tuple<string, string>("Rusted Skull", "Sacred Essence") },
            { new [] { "Cursed Anomaly Orb", "Galactic Arrival" }, new Tuple<string, string>("Rusted Orb", "Sacred Essence") },
            { new [] { "Helm of the Unholy Warrior", "Lucifer's Demise" }, new Tuple<string, string>("Rusted Helmet", "Sacred Essence") },
            { new [] { "Godfrey's Battalion", "Rusted Ancient Shield" }, new Tuple<string, string>("Rusted Shield", "Sacred Essence") },
            { new [] { "Plaguebearer Seal", "Universal Seal" }, new Tuple<string, string>("Rusted Seal", "Sacred Essence") },
            { new [] { "Banner of the Ages", "Banner of Champions" }, new Tuple<string, string>("Rusted Banner", "Sacred Essence") },
            { new [] { "Eden's Apple", "Supportive Spirit", "Guardian Necklace", "Holy Grail", "Glass Pendant", "Fortified Jewel", "Ring of Youthful Unity", "Treasure of the Cosmos", "Continuum", "Sunset Gemstone" }, new Tuple<string, string>("Rusted Ring", "Sacred Essence") }

        };

        public static readonly Dictionary<string[], Tuple<string, string>> NightmareList = new Dictionary<string[], Tuple<string, string>>
        {
            { new [] { "Tormentor of the Deep", "Wand of Violent Mana", "Lance of Honour", "Judgement's Call", "Soul's Guidance", "The Bewildering", "Protective Psalm",
                "Khonsu's Breastplate", "Enchanted Leather Hide", "An Alien's Blessing", "Immunity Gauntlet", "Evanescence", "Jackpot", "Piercing Silence",
                "Unknown Unstable Ballista", "Psychotic Attacks", "Bolstered Helm", "Blades of Majesty", "Steel-Woven Hide" }, new Tuple<string, string>("Nightmare Shard", "Legendary Essence") },

            { new [] { "Gem of Corruption", "Trap of Darkened Souls", "Hide of Horrors", "Death's Vow", "Ascension", "Endless Destruction","Waterlogged Blade",
                "Skull of Dark Spirits", "Eternal Deficit", "Split-Shot Crossbow", "Interstellar Force", "Path to the Underworld", "Whispers from Beyond", "Spell of 1000 Strikes", "Overlord's Crown",
                "Dagger of Dark Descent", "Eradication Bow", "The Forth-Calling" }, new Tuple<string, string>("Extinction Shard", "Legendary Essence") },

            { new [] { "Band of Exoneration" ,"Flash-Fire" ,"Demonic Resonance" ,"Volcanic Vengeance" ,"Book of Burning Souls" ,"Blades of Denial" ,"Dragon Tail Lance",
                "Scarlet Battle-Axe" ,"Ring of Shining Light" ,"The Blood Wager" ,"Anguish of Drannol" ,"Sky-Striker" ,"Soul of the Titan" ,"Extermination" ,"Inferno of Truth",
                "Blood-Fang" ,"Bloodstar" ,"Detrimental Destiny" ,"Cloak of the Ancients" ,"Kunai" }, new Tuple<string, string>("Crimson Shard", "Legendary Essence") },

            { new [] { "Royal Dagger", "The Monarch's Grasp", "Sword of Elegance", "Gem of True Eminence", "Grand Curse", "The Kingdom's Pride",
                "Scutum of the Legionnaire", "Stride of the Multitude", "Asteroid", "Purity's Call", "Cloth of the Battle-Mage", "The Storm Blanket", "Gratuitous Gift", "Herculean Amulet",
                "Beastly Remains", "Infused Spike", "The Ranger's Grace", "Platemail of the Stalwart", "Soulshredder" }, new Tuple<string, string>("Luxury Shard", "Legendary Essence") },

            { new [] { "Bow of the Void", "Quiver of the Shadows", "Armor of Nil", "Sourcestone", "Sword of the Colossus", "Marble Seal", "Breastplate of New Life", "Magical Lodestone",
                       "Staff of Unholy Sacrifice", "Skull of Corrupted Souls", "Ritual Robe", "Bloodshed Ring" }, new Tuple<string, string>("Lost Halls Shard", "Legendary Essence") }
        };

        public static readonly Dictionary<string[], Tuple<string, string>> LGHeart = new Dictionary<string[], Tuple<string, string>>
        {
            { new [] { "Tormentor of the Deep", "Wand of Violent Mana", "Lance of Honour", "Judgement's Call", "Soul's Guidance", "The Bewildering", "Protective Psalm",
                "Khonsu's Breastplate", "Enchanted Leather Hide", "An Alien's Blessing", "Immunity Gauntlet", "Evanescence", "Jackpot", "Piercing Silence",
                "Unknown Unstable Ballista", "Psychotic Attacks", "Bolstered Helm", "Blades of Majesty", "Steel-Woven Hide",
                "Gem of Corruption", "Trap of Darkened Souls", "Hide of Horrors", "Dagger of Dark Descent", "The Forth-Calling", "Eradication Bow", "Death's Vow", "Ascension", "Endless Destruction","Waterlogged Blade",
                "Skull of Dark Spirits", "Eternal Deficit", "Split-Shot Crossbow", "Interstellar Force", "Path to the Underworld", "Whispers from Beyond", "Spell of 1000 Strikes", "Overlord's Crown",
                "Band of Exoneration" ,"Flash-Fire" ,"Demonic Resonance" ,"Volcanic Vengeance" ,"Book of Burning Souls" ,"Blades of Denial" ,"Dragon Tail Lance",
                "Scarlet Battle-Axe" ,"Ring of Shining Light" ,"The Blood Wager" ,"Anguish of Drannol" ,"Sky-Striker" ,"Soul of the Titan" ,"Extermination" ,"Inferno of Truth",
                "Blood-Fang" ,"Bloodstar" ,"Detrimental Destiny" ,"Cloak of the Ancients" ,"Kunai", "Royal Dagger", "The Monarch's Grasp", "Sword of Elegance", "Gem of True Eminence", "Grand Curse", "The Kingdom's Pride",
                "Scutum of the Legionnaire", "Stride of the Multitude", "Asteroid", "Purity's Call", "Cloth of the Battle-Mage", "The Storm Blanket", "Gratuitous Gift", "Herculean Amulet",
                "Beastly Remains", "Infused Spike", "The Ranger's Grace", "Platemail of the Stalwart", "Soulshredder", "The Forth-Calling" }, new Tuple<string, string>("", "Change of Heart") }
        };

        public static readonly Dictionary<string[], Tuple<string, string>> SCHeart = new Dictionary<string[], Tuple<string, string>>
        {
            { new [] { "Axe of Demise", "Warlord's Sword", "Royalty Blade", "Against the World", "Toxin Infused Blade", "Honeycomb Dagger", "Alien Tech", "Hornet Sting",
                "Molten Bow", "Wyvern's Touch", "Extraterrestrial Wand", "Ancient Wood Cane", "A Wizard's Precious Treasure", "Cane of the Devil", "Staff of Corrupt Demons",
                "Unsheathed Fury of Yukimura", "Raven's Claw", "Sunrise Katana", "A Harpy's Fury", "Gold and Silver Blades", "Hot Chilli Blades", "The Dragon's Breath", "Lance of the King's Successor",
                "Bloodseeker's Redemption", "Drenched Gowns of Attila", "Spectres Shroud", "Spectral Globe", "Creation of Anubis", "Marvellous Concoction", "Vial of Corrosive Venom", "Tarnished Cloths",
                "Blood-Stained Jacket", "Dice of the Scriptures", "Dice of Resistance", "Grasp of the Gods", "Poison-Tip Quiver", "Dragon-Snap Trap", "Devil's Snare Trap", "Haunted Talisman", "Angelic Talisman",
                "Earthquake Tome", "Eternal Glory", "Rejoiced Rod of Victory", "Weeping Magicians Calling", "Demonic Siphon", "Advanced Tech Siphon", "Shooting Star", "Weighted Star", "Inscribed Sheath", "Sheath Of Dark Deeds",
                "Hellfire Charm", "Purity Charm", "Ancient Runestone", "Platonic Lifeline", "Nightcrawler's Head", "Ancient Aztec Skull", "Cursed Anomaly Orb", "Galactic Arrival", "Helm of the Unholy Warrior", "Lucifer's Demise",
                "Godfrey's Battalion", "Rusted Ancient Shield", "Plaguebearer Seal", "Universal Seal", "Banner of the Ages", "Banner of Champions", "Tree-sap Armor", "Hide of the Defender", "Awakening of Bravery", "Angelic Platemail",
                "Bloodshed Armor", "Beyond the Nature's Power", "Skullsplitter Robe", "Inferno Garments", "Overseer Robe", "Supportive Spirit", "Guardian Necklace", "Holy Grail", "Glass Pendant", "Fortified Jewel", "Ring of Youthful Unity",
                "Treasure of the Cosmos", "Continuum", "Sunset Gemstone", "Eden's Apple" }, new Tuple<string, string>("", "Change of Heart") }
        };
    }
}