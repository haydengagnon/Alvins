﻿using common.resources;
using System;
using System.Collections.Generic;
using wServer.logic;
using wServer.networking.packets.outgoing;
using wServer.realm.cores;
using wServer.realm.terrain;
using wServer.realm.worlds;

namespace wServer.realm.entities
{
    public class Enemy : Character
    {
        private readonly bool stat;
        public Enemy ParentEntity;

        public Enemy(RealmManager manager, ushort objType)
            : base(manager, objType)
        {
            stat = ObjectDesc.MaxHP == 0;
            DamageCounter = new DamageCounter(this);
        }

        public DamageCounter DamageCounter { get; private set; }

        public WmapTerrain Terrain { get; set; }

        private Position? pos;
        public Position SpawnPoint => pos ?? new Position { X = X, Y = Y };

        public override void Init(World owner)
        {
            base.Init(owner);

            if (ObjectDesc.StasisImmune)
                ApplyConditionEffect(ConditionEffectIndex.StasisImmune, -1);

            if (stat)
                ApplyConditionEffect(ConditionEffectIndex.Invincible, -1);
        }

        public void SetDamageCounter(DamageCounter counter, Enemy enemy)
        {
            DamageCounter = counter;
            DamageCounter.UpdateEnemy(enemy);
        }

        public event EventHandler<BehaviorEventArgs> OnDeath;

        public void Death()
        {
            lastPlayerNum = -1;
            DamageCounter.Death();
            CurrentState?.OnDeath(new BehaviorEventArgs(this));
            OnDeath?.Invoke(this, new BehaviorEventArgs(this));
            if (Owner != null) Owner.LeaveWorld(this);
        }

        public int Damage(IPlayer from, int dmg, bool noDef, params ConditionEffect[] effs)
        {
            if (stat) return 0;
            if (HasConditionEffect(ConditionEffects.Invincible)) return 0;
            if (!HasConditionEffect(ConditionEffects.Paused) && !HasConditionEffect(ConditionEffects.Stasis))
            {
                var def = ObjectDesc.Defense;

                if (noDef) def = def / 5;

                dmg = (int)StatsManager.GetDefenseDamage(this, dmg, def);

                if (from is Player) DamageCounter.HitBy(from as Player, null, dmg);

                var effDmg = dmg;

                if (effDmg > HP) effDmg = HP;
                if (!HasConditionEffect(ConditionEffects.Invulnerable)) HP -= dmg;

                ApplyConditionEffect(effs);

                if (Owner != null && !Owner.Deleted) Owner.BroadcastPacketConditional(new Damage()
                {
                    TargetId = Id,
                    Effects = 0,
                    DamageAmount = (ushort)dmg,
                    Kill = HP < 0,
                    BulletId = 0,
                    ObjectId = (from as Entity).Id
                }, p => ((from is Player) && (from as Player).AccountId == p.AccountId) || (!p.Client.IgnoreAllyDamageText && p.DistSqr(this) < Player.RadiusSqr));
                if (HP < 0 && Owner != null) Death();

                return effDmg;
            }
            return 0;
        }

        public int DamagePoison(IPlayer from, int dmg, bool noDef, params ConditionEffect[] effs)
        {
            if (stat) return 0;
            if (HasConditionEffect(ConditionEffects.Invincible)) return 0;
            if (!HasConditionEffect(ConditionEffects.Paused) && !HasConditionEffect(ConditionEffects.Stasis))
            {

                if (from is Player) DamageCounter.HitBy(from as Player, null, dmg);

                var effDmg = dmg;

                if (effDmg > HP) effDmg = HP;
                HP -= effDmg;

                ApplyConditionEffect(effs);

                if (Owner != null && !Owner.Deleted) Owner.BroadcastPacketConditional(new Damage()
                {
                    TargetId = Id,
                    Effects = 0,
                    DamageAmount = (ushort)dmg,
                    Kill = HP < 0,
                    BulletId = 0,
                    ObjectId = (from as Entity).Id
                }, p => ((from is Player) && (from as Player).AccountId == p.AccountId) || (!p.Client.IgnoreAllyDamageText && p.DistSqr(this) < Player.RadiusSqr));
                if (HP < 0 && Owner != null) Death();

                return effDmg;
            }
            return 0;
        }

        public override bool HitByProjectile(Projectile projectile)
        {
            if (stat) return false;
            if (HasConditionEffect(ConditionEffects.Invincible)) return false;
            if (projectile.ProjectileOwner is IPlayer ip && !HasConditionEffect(ConditionEffects.Paused) && !HasConditionEffect(ConditionEffects.Stasis))
            {
                Player p = null;

                if (ip is Player) p = ip as Player;

                var def = ObjectDesc.Defense;

                if (projectile.ProjDesc.ArmorPiercing) def = 0;

                var dmg = (int)StatsManager.GetDefenseDamage(this, projectile.Damage, def);

                if (p != null) DamageCounter.HitBy(p, projectile, dmg);
                if (!HasConditionEffect(ConditionEffects.Invulnerable)) HP -= dmg;

                ConditionEffect[] effs = null;

                foreach (var pair in projectile.ProjDesc.CondChance)
                {
                    if (pair.Value == 0 || pair.Key == default(ConditionEffect)) continue;
                    if (pair.Value / 100d > new Random().NextDouble())
                    {
                        var effList = new List<ConditionEffect>(projectile.ProjDesc.Effects) { pair.Key };
                        effs = effList.ToArray();
                    }
                }

                ApplyConditionEffect(effs ?? projectile.ProjDesc.Effects);

                Owner.BroadcastPacketConditional(new Damage
                {
                    TargetId = Id,
                    Effects = projectile.ConditionEffects,
                    DamageAmount = (ushort)dmg,
                    Kill = HP < 0,
                    BulletId = projectile.ProjectileId,
                    ObjectId = projectile.ProjectileOwner.Self.Id
                }, o => o.AccountId != p.AccountId && (!o.Client.IgnoreAllyDamageText && o.DistSqr(this) < Player.RadiusSqr));

                if (p != null && p.StealAmount != null && !projectile._used)
                {
                    if (p.StealAmount[0] != 0 && !p.HasConditionEffect(ConditionEffects.Sick))
                    {
                        var maxHP = p.Stats[0];
                        double lifesteal = p.StealAmount[0] / Math.Sqrt(p.Inventory[0].NumProjectiles) / p.Inventory[0].RateOfFire;
                        p._hpStealCounter += lifesteal;
                        if (p._hpStealCounter >= 1 && p.HP < maxHP)
                        {
                            p.HP = Math.Min(maxHP,Math.Max(p.HP, p.HP + (int)p._hpStealCounter));
                            p._hpStealCounter -= (int)p._hpStealCounter;
                        }
                        else if(p.HP >= maxHP)
                        {
                            p._hpStealCounter = 0;
                        }
                    }
                    if (p.StealAmount[1] != 0 && !p.HasConditionEffect(ConditionEffects.Quiet))
                    {
                        var maxMP = p.Stats[1];
                        double manasteal = p.StealAmount[1] / Math.Sqrt(p.Inventory[0].NumProjectiles) / p.Inventory[0].RateOfFire;
                        p._mpStealCounter += manasteal;
                        if (p._mpStealCounter >= 1 && p.MP < maxMP)
                        {
                            p.MP = Math.Min(maxMP, Math.Max(p.MP, p.MP + (int)p._mpStealCounter));
                            p._mpStealCounter -= (int)p._mpStealCounter;
                        }
                        else if (p.HP >= maxMP)
                        {
                            p._mpStealCounter = 0;
                        }
                    }
                }
                if (HP < 0 && Owner != null) Death();

                return true;
            }
            return false;
        }

        public override void Tick()
        {
            if (pos == null) pos = new Position() { X = X, Y = Y };
            if (!stat && HasConditionEffect(ConditionEffects.Bleeding)) HP -= (int)(MaximumHP / 1500f * CoreConstant.worldTickMs / 1000f);

            base.Tick();
        }
    }
}
