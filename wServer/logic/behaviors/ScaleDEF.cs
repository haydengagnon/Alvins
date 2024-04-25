using System;
using wServer.realm;
using wServer.realm.cores;
using wServer.realm.entities;
using System.Linq;
using common.resources;

namespace wServer.logic.behaviors
{
    internal class ScaleDEF : Behavior
    {
        private readonly double _amount;
        private readonly bool _checkMax;

        public ScaleDEF(double amount, bool checkMax = true)
        {
            _amount = amount;
            _checkMax = checkMax;
        }

        protected override void TickCore(Entity host, ref object state)
        {
            var cool = (int?)state ?? -1;

            if (cool <= 0)
            {
                var enemy = host as Enemy;
                int playerNum = enemy.Owner.Players.Values.Count(p => enemy.Dist(p) < 50 && !p.HasConditionEffect(ConditionEffects.Hidden));
                if (enemy.lastPlayerNum == playerNum) return;
                /* old hp scale stuff for reference
                float hpPerc = enemy.HP / (float)enemy.MaximumHP;

                if (_cachedMaxHp == -1)
                    _cachedMaxHp = enemy.MaximumHP;
                enemy.MaximumHP = (int)(_cachedMaxHp + _amount * enemy.ObjectDesc.MaxHP * Math.Max(playerNum - 1, 0));
                enemy.HP = (int)(enemy.MaximumHP * hpPerc);
                */

                enemy.DEF = (int)(enemy.DEF * (_amount *Math.Max(playerNum - 1, 0)));
                cool = 3000;
                enemy.lastPlayerNum = playerNum;
            }
            else cool -= (int)CoreConstant.worldLogicTickMs;

            state = cool;
        }
    }
}
