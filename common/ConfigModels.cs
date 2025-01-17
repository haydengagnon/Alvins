﻿using Newtonsoft.Json;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;

namespace common
{
    public class ServerConfig
    {
        public DbInfo dbInfo { get; set; } = new DbInfo();
        public ServerInfo serverInfo { get; set; } = new ServerInfo();
        public EmailSettings emailSettings { get; set; } = new EmailSettings();
        public ServerSettings serverSettings { get; set; } = new ServerSettings();
        public const bool EnableDebug = false;
	
		public static ServerConfig ReadFile(string fileName)
        {
            using (var r = new StreamReader(fileName))
            {
                return ReadJson(r.ReadToEnd());
            }
        }

        public static ServerConfig ReadJson(string json)
        {
            return JsonConvert.DeserializeObject<ServerConfig>(json);
        }
    }

    public class DbInfo
    {
        public string host { get; set; } = "127.0.0.1";
        public int port { get; set; } = 6379;
        public string auth { get; set; } = "";
        public int index { get; set; } = 2;
    }

    public class ServerInfo
    {
        public ServerType type { get; set; } = ServerType.World;
        public string name { get; set; } = "Localhost";
        public string address { get; set; } = "127.0.0.1";
        public string bindAddress { get; set; } = "127.0.0.1";
        public int port { get; set; } = 2050;
        public Coordinates coordinates { get; set; } = new Coordinates();
        public int players { get; set; } = 0;
        public int maxPlayers { get; set; } = 100;
        public int queueLength { get; set; } = 0;
        public bool adminOnly { get; set; } = false;
        public int minRank { get; set; } = 0;
        public string instanceId { get; set; } = "";
        public PlayerList playerList { get; set; } = new PlayerList();
    }

    public class ServerSettings
    {
        public string logFolder { get; set; } = "./logs";
        public string resourceFolder { get; set; } = "./resources";
        public string log4netConfig { get; set; } = "log4net.config";
        public string version { get; set; } = "1.5.2";
        public int tps { get; set; } = 5;
        public ServerMode mode { get; set; } = ServerMode.Single;
        public string key { get; set; } = "B1A5ED";
        public float lootMultiplier { get; set; } = 1;
        public float fortuneMultiplier { get; set; } = 100;
        public int maxConnections { get; set; } = 256;
        public int maxPlayers { get; set; } = 100;
        public int maxPlayersWithPriority { get; set; } = 120;
        public string sendGridApiKey { get; set; } = "";
        public bool enableMarket { get; set; } = true;
        public bool checkClient { get; set; } = false;
        public string[] tokens { get; set; } = { "0" };
        public int minutesToRestart { get; set; } = 30;
        public int minutesBeforeRestartFirstWarning { get; set; } = 5;
        public int minutesBeforeRestartSecondWarning { get; set; } = 1;
    }

    public class EmailSettings
    {
        public string email { get; set; } = "";
        public int port { get; set; } = 587;
        public string stmp { get; set; } = "";
        public string password { get; set; } = "";
    }

    public enum ServerType
    {
        Account,
        World
    }

    public enum ServerMode
    {
        Single,
        Nexus,
        Realm,
        Marketplace
    }

    public class Coordinates
    {
        public float latitude { get; set; } = 0;
        public float longitude { get; set; } = 0;
    }

    public class PlayerInfo
    {
        public int AccountId;
        public int GuildId;
        public int PartyId;
        public bool Admin;
        public string Name;
        public string WorldName;
        public int WorldInstance;
        public bool Hidden;
    }

    public class PlayerList : IEnumerable<PlayerInfo>
    {
        private readonly ConcurrentDictionary<PlayerInfo, int> PlayerInfo;

        public PlayerList(IEnumerable<PlayerInfo> playerList = null)
        {
            PlayerInfo = new ConcurrentDictionary<PlayerInfo, int>();

            if (playerList == null)
                return;

            foreach (var plr in playerList)
                Add(plr);
        }

        public void Add(PlayerInfo playerInfo)
        {
            PlayerInfo.TryAdd(playerInfo, 0);
        }

        public void Remove(PlayerInfo playerInfo)
        {
            if (playerInfo == null)
                return;

            PlayerInfo.TryRemove(playerInfo, out int ignored);
        }

        IEnumerator<PlayerInfo> IEnumerable<PlayerInfo>.GetEnumerator() => PlayerInfo.Keys.GetEnumerator();

        public IEnumerator GetEnumerator() => PlayerInfo.Keys.GetEnumerator();
    }
}
