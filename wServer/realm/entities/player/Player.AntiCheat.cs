using common.resources;
using log4net;
using System;
using wServer.realm.entities;
using wServer.networking.packets.incoming;

namespace wServer.realm.entities
{
    public enum PlayerShootStatus
    {
        Ok,
        ItemMismatch,
        CooldownStillActive,
        NumProjectileMismatch,
        ClientTooSlow,
        ClientTooFast
    }

    public class TimeCop
    {
        private readonly int[] _clientDeltaLog;
        private readonly int[] _serverDeltaLog;
        private readonly int _capacity;
        private int _index;
        private int _clientElapsed;
        private int _serverElapsed;
        private int _lastClientTime;
        private int _lastServerTime;
        private int _count;

        public TimeCop(int capacity = 20)
        {
            _capacity = capacity;
            _clientDeltaLog = new int[_capacity];
            _serverDeltaLog = new int[_capacity];
        }

        public void Push(int clientTime, int serverTime)
        {
            var dtClient = 0;
            var dtServer = 0;
            if (_count != 0)
            {
                dtClient = clientTime - _lastClientTime;
                dtServer = serverTime - _lastServerTime;
            }
            _count++;
            _index = (_index + 1) % _capacity;
            _clientElapsed += dtClient - _clientDeltaLog[_index];
            _serverElapsed += dtServer - _serverDeltaLog[_index];
            _clientDeltaLog[_index] = dtClient;
            _serverDeltaLog[_index] = dtServer;
            _lastClientTime = clientTime;
            _lastServerTime = serverTime;
        }

        public float LastClientTime()
        {
            return _lastClientTime;
        }

        public int LastServerTime()
        {
            return _lastServerTime;
        }

        /*
            a return value of 1 means client time is in sync with server time
            less than 1 means client time is slower than server time
            greater than 1 means client time is faster than server
        */

        public float TimeDiff()
        {
            if (_count < _capacity)
                return 1;

            return (float)_clientElapsed / _serverElapsed;
        }
    }

    partial class Player
    {
        private static readonly ILog CheatLog = LogManager.GetLogger("CheatLog");
        private const float MaxTimeDiff = 1.08f;
        private const float MinTimeDiff = 0.92f;
        private readonly TimeCop _time = new TimeCop();
        private int _shotsLeft;
        private int _lastShootTime;

        public PlayerShootStatus ValidatePlayerShoot(Item item, int time)
        {
            if (item != Inventory[0])
                return PlayerShootStatus.ItemMismatch;

            var dt = 1 / Stats.GetAttackFrequency() * (1 / item.RateOfFire);
            if (time < _time.LastClientTime() + dt)
                return PlayerShootStatus.CooldownStillActive;

            if (time != _lastShootTime)
            {
                _lastShootTime = time;

                if (_shotsLeft != 0 && _shotsLeft < item.NumProjectiles)
                {
                    _shotsLeft = 0;
                    _time.Push(time, Environment.TickCount);
                    return PlayerShootStatus.NumProjectileMismatch;
                }
                _shotsLeft = 0;
            }

            _shotsLeft++;
            if (_shotsLeft >= item.NumProjectiles)
                _time.Push(time, Environment.TickCount);

            var timeDiff = _time.TimeDiff();
            //Log.Info($"timeDiff: {timeDiff}");
            if (timeDiff < MinTimeDiff)
                return PlayerShootStatus.ClientTooSlow;
            if (timeDiff > MaxTimeDiff)
                return PlayerShootStatus.ClientTooFast;

            return PlayerShootStatus.Ok;
        }

        public bool IsNoClipping()
        {
            if (Owner == null || !TileOccupied(RealX, RealY) && !TileFullOccupied(RealX, RealY))
                return false;

            CheatLog.Info($"{Name} is walking on an occupied tile.");
            return true;
        }

        public bool ValidateEnemyHit(Projectile p, Entity e, long t)
        {
            Position pos = p.GetPosition(t - p.CreationTime);
            var x = Math.Abs(pos.X - e.X);
            var y = Math.Abs(pos.Y - e.Y);
            if (x < 1.5)
                if (y < 1.5)
                    return true;
            return false;
        }

        public bool IsKA(EnemyHit pkt)
        {
            var prj = (this as IProjectileOwner).Projectiles[pkt.BulletId];
            var entity = Owner.GetEntity(pkt.TargetId);

            var enemyAngle = FixAngle(Math.Atan2(prj.StartY - entity.Y, prj.StartX - entity.X) + Math.PI); // pi is a difference in angle in server and client
            var projAngle = FixAngle(prj.Angle);

            var distCli = MathsUtils.Dist(prj.StartX, prj.StartY, entity.X, entity.Y);
            var distSvr = prj.ProjDesc.Speed / 10d * prj.ProjDesc.LifetimeMS / 1000d; // max dist, s = v * t

            //Console.WriteLine($"ProjAngle: {projAngle} ToEnemyAngle: {enemyAngle} Difference: {GetDifferenceBetweenAngles(projAngle, enemyAngle)}");
            var angDiff = GetDifferenceBetweenAngles(projAngle, enemyAngle);

            if (distCli > 2 && angDiff > Math.PI / 4) // 45 degrees ~ 0.8 rad
                return true;

            if (distCli > 4 && angDiff > Math.PI / 6) // 30 degrees ~ 0.5 rad
                return true;

            if (distCli > 8 && angDiff > Math.PI / 9) // 20 degrees ~ 0.35 rad
                return true;

            if (prj.ProjDesc.Boomerang)
                distSvr /= 2;

            //Console.WriteLine($"DistCli: {distCli} DistSvr: {distSvr} Divide: {distCli/ distSvr}");
            if (distCli / distSvr > 1.2)
                return true;

            return false;
        }

        private double FixAngle(double ang)
        {
            double newAng = ang;
            while (newAng > 2 * Math.PI || newAng < 0)
            {
                newAng += newAng < 0 ? 2 * Math.PI : -2 * Math.PI;
            }
            return newAng;
        }

        private double GetDifferenceBetweenAngles(double ang1, double ang2)
        {
            var diff = Math.Abs(ang1 - ang2);
            if (diff > Math.PI) // max is pi ( 180 degrees )
                diff = Math.Abs(diff - 2 * Math.PI);

            return diff;
        }
    }
}