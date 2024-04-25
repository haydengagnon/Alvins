using System;
using System.Linq;
using common.resources;
using wServer.realm;
using wServer.realm.worlds;

namespace wServer.logic.behaviors
{
    class QuakeOnDeath : Behavior
    {
        private readonly string _worldName;

        public QuakeOnDeath(string worldName)
        {
            _worldName = worldName;
        }

        protected internal override void Resolve(State parent)
        {
            parent.Death += (sender, e) =>
            {
                var worldNameProper =
                e.Host.Manager.Resources.Worlds.Data.FirstOrDefault(
                    p => p.Key.Equals(_worldName, StringComparison.InvariantCultureIgnoreCase)).Key;

                ProtoWorld proto;
                if (worldNameProper == null || (proto = e.Host.Manager.Resources.Worlds.Data[worldNameProper]).setpiece)
                    return;

                World world;
                if (proto.persist || proto.id == World.Arena || proto.id == World.DeathArena)
                    world = e.Host.Manager.Worlds[proto.id];
                else
                    world = e.Host.Manager.AddWorld(new World(proto));              

                e.Host.Owner.QuakeToWorld(world);
            };
        }

        protected override void TickCore(Entity host, ref object state)
        {
        }
    }
}