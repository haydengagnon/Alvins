using System.Collections.Concurrent;
using wServer.networking.packets.outgoing;
using wServer.realm;
using wServer.realm.worlds;

namespace wServer.logic.behaviors
{
    public class GlobalSound : Behavior
    {
        private readonly int soundId;
        public readonly ConcurrentDictionary<int, World> Worlds = new ConcurrentDictionary<int, World>();
        public GlobalSound(int soundId = 0)
        {
            this.soundId = soundId;
        }

        protected override void OnStateEntry(Entity host, ref object state)
        {
            var owner = host.Owner;
            foreach (var w in Worlds)
            {
                foreach (var plr in owner.Players.Values)
                {
                    plr.Client.SendPacket(new PlaySound
                    {
                        OwnerId = host.Id,
                        SoundId = soundId
                    });
                }
            }
        }

        protected override void TickCore(Entity host, ref object state) { }
    }
}
