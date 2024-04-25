using System;
using System.Linq;
using common.resources;
using wServer.realm;
using wServer.realm.entities;

namespace wServer.logic.behaviors
{
    internal class GodlyScale : Behavior
    {
        private readonly int amount;
        private readonly int maxARs;

        private int countedRelics;
        private int lastCountedRelics = 0;
        //currently global - so only good for raid bosses / instances

        public GodlyScale(int amount, int maxARs = 4)
        {
            this.amount = amount;
            this.maxARs = maxARs;
        }

        protected override void TickCore(Entity host, ref object state)
        {

            foreach (var player in host.Owner.Players)
            {
                var i = 0;
                var inv = player.Value.Inventory;
                Item item = inv[i];
                for (i = 0; i < 4; i++)
                {
                    if (item.Godly)
                        countedRelics++;
                }
            }

            if (lastCountedRelics == countedRelics)
                return;

            if (countedRelics > maxARs)
                countedRelics = maxARs;


            var enemy = host as Enemy;

            enemy.MaximumDEF = enemy.DEF + (amount * countedRelics);

            lastCountedRelics = countedRelics;
            countedRelics = 0;
        }
    }
}