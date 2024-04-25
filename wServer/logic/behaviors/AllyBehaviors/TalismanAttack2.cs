using common.resources;
using wServer.networking.packets.outgoing;
using wServer.realm;
using wServer.realm.entities;
using wServer.realm.cores;
namespace wServer.logic.behaviors
{
    internal class TalismanAttack2 : Behavior
    {
        private readonly int _damage;
        private readonly int _duration;
        ConditionEffectIndex _effect;

        public TalismanAttack2(int damage, ConditionEffectIndex effect, int duration = 0)
        {
            _damage = damage;
            _duration = duration;
            _effect = effect;
        }
        protected override void OnStateEntry(Entity host, ref object state)
        {
            state = 0;
        }

        protected override void TickCore(Entity host, ref object state)
        {
            int cool = (int)state;
            if (cool <= 0)
            {
                var player = host.GetPlayerOwner();

                if (player == null)
                    return;

                if (host.GetNearestEntity(15, false, _ => _ is Enemy && _.GetPlayerOwner() == null && _.ObjectDesc.MaxHP != 0) is Enemy en)
                {
                    
                    en.Damage(player, _damage, true);
                    en.ApplyConditionEffect(new ConditionEffect
                    {
                        Effect = _effect,
                        DurationMS = _duration
                    });
                }
                cool = 200;
            }
            else
                cool -= (int)CoreConstant.worldLogicTickMs;

            state = cool;
        }
    }
}