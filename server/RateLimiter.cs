using System;
using System.Collections.Generic;
using common;

namespace server
{
    public class RateLimiter
    {
        private const double LAMBDA = 0.56931471805599453;
        private const double RATE_LIMIT = 1.2;
        private readonly Database database;
        private readonly List<String> whitelist = new List<string> { "/account/verify", "/app/getplayercount" };

        public RateLimiter(Database db)
        {
            database = db;
        }

        private double Evaluate(double n, int deltaT)
        {
            return n * LAMBDA * Math.Exp(LAMBDA * deltaT);
        }

        private void Update(string nkey, string tkey, double n, int deltaT)
        {
            database.Conn.StringSet(nkey, 1 + n * Math.Exp(LAMBDA * deltaT), TimeSpan.FromSeconds(30));
            database.Conn.StringSet(tkey, DateTime.Now.ToUnixTimestamp(), TimeSpan.FromSeconds(30));
        }

        public bool IsRateLimited(string ip, string localPath)
        {
            //Only put rate limiting on specified paths
            if (!whitelist.Contains(localPath.ToLower()))
                return false;

            var nkey = ip + ":N";
            var tkey = ip + ":T";

            var nValue = database.Conn.StringGet(nkey);
            var n = nValue.IsNullOrEmpty ? 0 : double.Parse(nValue);
            var tValue = database.Conn.StringGet(tkey);
            var t = tValue.IsNullOrEmpty ? 0 : int.Parse(tValue);

            var deltaT = t - DateTime.Now.ToUnixTimestamp();

            var estimatedRate = Evaluate(n, deltaT);
            var limited = estimatedRate > RATE_LIMIT;
            Update(nkey, tkey, n, deltaT);
            //Console.WriteLine(estimated_rate);
            return limited;
        }
    }
}