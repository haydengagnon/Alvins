using common.resources;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using wServer.networking.packets;
using wServer.networking.packets.outgoing;
using wServer.realm.cores;

namespace wServer.realm.entities
{
    partial class Player
    {
        private float _healing;
        private float _healing2;
        private float _healing3;
        private float _healing4;
        private float _bleeding;
        private int _newbieTime;
        private int _canTpCooldownTime;
        private double _protDelay = 0;
        private double _protRegen = 0;

        private void permdebufftest(ConditionEffect condeff)
        {
            if (condeff.DurationMS <= 0)
            {
                new ConditionEffect
                {
                    Effect = ConditionEffectIndex.Slowed,

                    DurationMS = 0
                };
                new ConditionEffect
                {
                    Effect = ConditionEffectIndex.Paralyzed,
                    DurationMS = 0
                };
                new ConditionEffect
                {
                    Effect = ConditionEffectIndex.Weak,
                    DurationMS = 0
                };
                new ConditionEffect
                {
                    Effect = ConditionEffectIndex.Stunned,
                    DurationMS = 0
                };
                new ConditionEffect
                {
                    Effect = ConditionEffectIndex.Confused,
                    DurationMS = 0
                };
                new ConditionEffect
                {
                    Effect = ConditionEffectIndex.Blind,
                    DurationMS = 0
                };
                new ConditionEffect
                {
                    Effect = ConditionEffectIndex.Quiet,
                    DurationMS = 0
                };
                new ConditionEffect
                {
                    Effect = ConditionEffectIndex.ArmorBroken,
                    DurationMS = 0
                };
                new ConditionEffect
                {
                    Effect = ConditionEffectIndex.Bleeding,
                    DurationMS = 0
                };
                new ConditionEffect
                {
                    Effect = ConditionEffectIndex.Dazed,
                    DurationMS = 0
                };
                new ConditionEffect
                {
                    Effect = ConditionEffectIndex.Sick,
                    DurationMS = 0
                };
                new ConditionEffect
                {
                    Effect = ConditionEffectIndex.Drunk,
                    DurationMS = 0
                };
                new ConditionEffect
                {
                    Effect = ConditionEffectIndex.Hallucinating,
                    DurationMS = 0
                };
                new ConditionEffect
                {
                    Effect = ConditionEffectIndex.Hexed,
                    DurationMS = 0
                };
                new ConditionEffect
                {
                    Effect = ConditionEffectIndex.Unstable,
                    DurationMS = 0
                };
                new ConditionEffect
                {
                    Effect = ConditionEffectIndex.Darkness,
                    DurationMS = 0
                };
                new ConditionEffect
                {
                    Effect = ConditionEffectIndex.Curse,
                    DurationMS = 0
                };
                new ConditionEffect
                {
                    Effect = ConditionEffectIndex.Nullified,
                    DurationMS = 0
                };
            }
        }

        private void HandleEffects() 
        {
            Player player = null;
            if (_client == null || _client.Account == null) return;
            if (Manager.Core.getTickCount() % 50 == 0)
            {
                if (CheckAxe())
                {
                    Stats.Boost.ActivateBoost[0].Push(300, true);
                    Stats.ReCalculateValues();
                }
                else
                {
                    Stats.Boost.ActivateBoost[0].Pop(300, true);
                    Stats.ReCalculateValues();
                }

                if (CheckSunMoon())
                {
                    Stats.Boost.ActivateBoost[1].Push(100);
                    Stats.ReCalculateValues();
                }
                else
                {
                    Stats.Boost.ActivateBoost[1].Pop(100);
                    Stats.ReCalculateValues();
                }

                if (CheckAnubis())
                {
                    Stats.Boost.ActivateBoost[1].Push(60);
                    Stats.ReCalculateValues();
                }
                else
                {
                    Stats.Boost.ActivateBoost[1].Pop(60);
                    Stats.ReCalculateValues();
                }

                if (CheckSeal())
                {
                    var pkts = new List<Packet>();
                    Owner.Timers.Add(new WorldTimer(4000, (w) =>
                    {
                        if (w == null || w.Deleted) return;
                        Stats.Boost.ActivateBoost[2].Push(35, true);
                        Stats.ReCalculateValues();
                    }));

                    Owner.Timers.Add(new WorldTimer(10000, (w) =>
                    {
                        if (w == null || w.Deleted) return;
                        Stats.Boost.ActivateBoost[2].Pop(35, true);
                        Stats.ReCalculateValues();
                    }));
                }
                else
                {
                    Stats.Boost.ActivateBoost[2].Pop(35, false);
                    Stats.ReCalculateValues();
                }
                
                if (CheckT3Vit())
                {
                    Stats.Boost.ActivateBoost[14].Push(12, true);

                }
                else
                {
                    Stats.Boost.ActivateBoost[14].Pop(12, true);

                }
                /*  
                  if (CheckAnyWelding())
                  {
                      int fortune = 0;
                      if (CheckWelding())
                          fortune += 1;
                      if (CheckWelding2())
                          fortune += 1;
                      if (CheckWelding3())
                          fortune += 1;
                      if (CheckWelding4())
                          fortune += 1;
                      if (CheckWelding5())
                          fortune += 1;
                      if (CheckWelding6())
                          fortune += 1;
                      if (CheckWelding7())
                          fortune += 1;
                      if (CheckWelding8())
                          fortune += 1;
                      if (CheckWelding9())
                          fortune += 1;
                      if (CheckWelding10())
                          fortune += 1;
                      if (CheckWelding11())
                          fortune += 1;
                      if (CheckWelding12())
                          fortune += 1;
                      if (CheckWelding13())
                          fortune += 1;
                      if (CheckWelding14())
                          fortune += 1;
                      if (CheckWelding15())
                          fortune += 1;
                      if (CheckAnyWelding5())
                      {
                          if (CheckWelding50())
                              fortune += 2;
                          if (CheckWelding52())
                              fortune += 2;
                          if (CheckWelding53())
                              fortune += 2;
                          if (CheckWelding54())
                              fortune += 2;
                          if (CheckWelding55())
                              fortune += 2;
                          if (CheckWelding56())
                              fortune += 2;
                          if (CheckWelding57())
                              fortune += 2;
                          if (CheckWelding58())
                              fortune += 2;
                          if (CheckWelding59())
                              fortune += 2;
                          if (CheckWelding510())
                              fortune += 2;
                          if (CheckWelding511())
                              fortune += 2;
                          if (CheckWelding512())
                              fortune += 2;
                          if (CheckWelding513())
                              fortune += 2;
                          if (CheckWelding514())
                              fortune += 2;
                          if (CheckWelding515())
                              fortune += 2;
                      }
                      if (CheckAnyWelding6())
                      {
                          if (CheckWelding60())
                              fortune += 3;
                          if (CheckWelding62())
                              fortune += 3;
                          if (CheckWelding63())
                              fortune += 3;
                          if (CheckWelding64())
                              fortune += 3;
                          if (CheckWelding65())
                              fortune += 3;
                          if (CheckWelding66())
                              fortune += 3;
                          if (CheckWelding67())
                              fortune += 3;
                          if (CheckWelding68())
                              fortune += 3;
                          if (CheckWelding69())
                              fortune += 3;
                          if (CheckWelding610())
                              fortune += 3;
                          if (CheckWelding611())
                              fortune += 3;
                          if (CheckWelding612())
                              fortune += 3;
                          if (CheckWelding613())
                              fortune += 3;
                          if (CheckWelding614())
                              fortune += 3;
                          if (CheckWelding615())
                              fortune += 3;
                      }
                      if (CheckAnyWelding7())
                      {
                          if (CheckWelding70())
                              fortune += 4;
                          if (CheckWelding72())
                              fortune += 4;
                          if (CheckWelding73())
                              fortune += 4;
                          if (CheckWelding74())
                              fortune += 4;
                          if (CheckWelding75())
                              fortune += 4;
                          if (CheckWelding76())
                              fortune += 4;
                          if (CheckWelding77())
                              fortune += 4;
                          if (CheckWelding78())
                              fortune += 4;
                          if (CheckWelding79())
                              fortune += 4;
                          if (CheckWelding710())
                              fortune += 4;
                          if (CheckWelding711())
                              fortune += 4;
                          if (CheckWelding712())
                              fortune += 4;
                          if (CheckWelding713())
                              fortune += 4;
                          if (CheckWelding714())
                              fortune += 4;
                          if (CheckWelding715())
                              fortune += 4;
                      }
                      if (CheckAnyWelding8())
                      {
                          if (CheckWelding80())
                              fortune += 5;
                          if (CheckWelding82())
                              fortune += 5;
                          if (CheckWelding83())
                              fortune += 5;
                          if (CheckWelding84())
                              fortune += 5;
                          if (CheckWelding85())
                              fortune += 5;
                          if (CheckWelding86())
                              fortune += 5;
                          if (CheckWelding87())
                              fortune += 5;
                          if (CheckWelding88())
                              fortune += 5;
                          if (CheckWelding89())
                              fortune += 5;
                          if (CheckWelding810())
                              fortune += 5;
                          if (CheckWelding811())
                              fortune += 5;
                          if (CheckWelding812())
                              fortune += 5;
                          if (CheckWelding813())
                              fortune += 5;
                          if (CheckWelding814())
                              fortune += 5;
                          if (CheckWelding815())
                              fortune += 5;
                      }
                      if (CheckAnyWelding9())
                      {
                          if (CheckWelding90())
                              fortune += 6;
                          if (CheckWelding92())
                              fortune += 6;
                          if (CheckWelding93())
                              fortune += 6;
                          if (CheckWelding94())
                              fortune += 6;
                          if (CheckWelding95())
                              fortune += 6;
                          if (CheckWelding96())
                              fortune += 6;
                          if (CheckWelding97())
                              fortune += 6;
                          if (CheckWelding98())
                              fortune += 6;
                          if (CheckWelding99())
                              fortune += 6;
                          if (CheckWelding910())
                              fortune += 6;
                          if (CheckWelding911())
                              fortune += 6;
                          if (CheckWelding912())
                              fortune += 6;
                          if (CheckWelding913())
                              fortune += 6;
                          if (CheckWelding914())
                              fortune += 6;
                          if (CheckWelding915())
                              fortune += 6;
                      }
                      if (CheckAnyWelding10())
                      {
                          if (CheckWelding100())
                              fortune += 7;
                          if (CheckWelding102())
                              fortune += 7;
                          if (CheckWelding103())
                              fortune += 7;
                          if (CheckWelding104())
                              fortune += 7;
                          if (CheckWelding105())
                              fortune += 7;
                          if (CheckWelding106())
                              fortune += 7;
                          if (CheckWelding107())
                              fortune += 7;
                          if (CheckWelding108())
                              fortune += 7;
                          if (CheckWelding109())
                              fortune += 7;
                          if (CheckWelding1010())
                              fortune += 7;
                          if (CheckWelding1011())
                              fortune += 7;
                          if (CheckWelding1012())
                              fortune += 7;
                          if (CheckWelding1013())
                              fortune += 7;
                          if (CheckWelding1014())
                              fortune += 7;
                          if (CheckWelding1015())
                              fortune += 7;
                      }
                      if (CheckAnyWelding11())
                      {
                          if (CheckWelding110())
                              fortune += 8;
                          if (CheckWelding112())
                              fortune += 8;
                          if (CheckWelding113())
                              fortune += 8;
                          if (CheckWelding114())
                              fortune += 8;
                          if (CheckWelding115())
                              fortune += 8;
                          if (CheckWelding116())
                              fortune += 8;
                          if (CheckWelding117())
                              fortune += 8;
                          if (CheckWelding118())
                              fortune += 8;
                          if (CheckWelding119())
                              fortune += 8;
                          if (CheckWelding1110())
                              fortune += 8;
                          if (CheckWelding1111())
                              fortune += 8;
                          if (CheckWelding1112())
                              fortune += 8;
                          if (CheckWelding1113())
                              fortune += 8;
                          if (CheckWelding1114())
                              fortune += 8;
                          if (CheckWelding1115())
                              fortune += 8;
                      }
                      if (CheckAnyWelding12())
                      {
                          if (CheckWelding120())
                              fortune += 9;
                          if (CheckWelding122())
                              fortune += 9;
                          if (CheckWelding123())
                              fortune += 9;
                          if (CheckWelding124())
                              fortune += 9;
                          if (CheckWelding125())
                              fortune += 9;
                          if (CheckWelding126())
                              fortune += 9;
                          if (CheckWelding127())
                              fortune += 9;
                          if (CheckWelding128())
                              fortune += 9;
                          if (CheckWelding129())
                              fortune += 9;
                          if (CheckWelding1210())
                              fortune += 9;
                          if (CheckWelding1211())
                              fortune += 9;
                          if (CheckWelding1212())
                              fortune += 9;
                          if (CheckWelding1213())
                              fortune += 9;
                          if (CheckWelding1214())
                              fortune += 9;
                          if (CheckWelding1215())
                              fortune += 9;
                      }


                      Stats.Boost.ActivateBoost[14].Push(fortune, false);
                  }
                  */
                
                if (CheckStone())
                {
                    var pkts = new List<Packet>();
                    Owner.Timers.Add(new WorldTimer(5000, (w) =>
                    {
                        if (w == null || w.Deleted) return;
                        Stats.Boost.ActivateBoost[2].Push(25, true);
                        Stats.ReCalculateValues();
                    }));

                    Owner.Timers.Add(new WorldTimer(10000, (w) =>
                    {
                        if (w == null || w.Deleted) return;
                        Stats.Boost.ActivateBoost[2].Pop(25, true);
                        Stats.ReCalculateValues();
                    }));
                }
                else
                {
                    Stats.Boost.ActivateBoost[2].Pop(35, false);
                    Stats.ReCalculateValues();
                }
                
                if (CheckTear())
                {
                    var pkts = new List<Packet>();
                    Owner.Timers.Add(new WorldTimer(5000, (w) =>
                    {
                        if (w == null || w.Deleted) return;
                        Stats.Boost.ActivateBoost[6].Push(100, true);
                        Stats.ReCalculateValues();
                    }));

                    Owner.Timers.Add(new WorldTimer(10000, (w) =>
                    {
                        if (w == null || w.Deleted) return;
                        Stats.Boost.ActivateBoost[6].Pop(100, true);
                        Stats.ReCalculateValues();
                    }));
                }
                else
                {
                    Stats.Boost.ActivateBoost[6].Pop(100, false);
                    Stats.ReCalculateValues();
                }

                if (CheckMocking()) ApplyConditionEffect(ConditionEffectIndex.Relentless);
                else ApplyConditionEffect(ConditionEffectIndex.Relentless, 0);

                if (CheckCrescent()) ApplyConditionEffect(ConditionEffectIndex.SlowedImmune);
                else ApplyConditionEffect(ConditionEffectIndex.SlowedImmune, 0);

                if (CheckForce()) ApplyConditionEffect(ConditionEffectIndex.ArmorBreakImmune);
                else ApplyConditionEffect(ConditionEffectIndex.ArmorBreakImmune, 0);

                if (CheckRoyal()) ApplyConditionEffect(ConditionEffectIndex.HealthRecovery);
                else ApplyConditionEffect(ConditionEffectIndex.HealthRecovery, 0);

                if (CheckResistance()) ApplyConditionEffect(ConditionEffectIndex.SlowedImmune);
                else ApplyConditionEffect(ConditionEffectIndex.SlowedImmune, 0);
                if (CheckAegis()) ApplyConditionEffect(ConditionEffectIndex.Vengeance);
                else ApplyConditionEffect(ConditionEffectIndex.Vengeance, 0);

                if (CheckGilded()) ApplyConditionEffect(ConditionEffectIndex.Alliance);
                else ApplyConditionEffect(ConditionEffectIndex.Alliance, 0);
            }
            if (HasConditionEffect(ConditionEffects.Nullified)) ApplyConditionEffect(PositiveEffs);

            if (_client.Account.Hidden && !HasConditionEffect(ConditionEffects.Hidden))
            {
                ApplyConditionEffect(ConditionEffectIndex.Hidden);
                ApplyConditionEffect(ConditionEffectIndex.Invincible);
                ApplyConditionEffect(ConditionEffectIndex.Swiftness);

                Manager.Clients[Client].Hidden = true;
            }
            if (!_client.Account.Hidden && HasConditionEffect(ConditionEffects.Hidden))
            {
                ApplyConditionEffect(ConditionEffectIndex.Hidden, 0);
                ApplyConditionEffect(ConditionEffectIndex.Invincible, 0);
                ApplyConditionEffect(ConditionEffectIndex.Swiftness, 0);

                Manager.Clients[Client].Hidden = false;
            }
            if (Muted && !HasConditionEffect(ConditionEffects.Muted)) ApplyConditionEffect(ConditionEffectIndex.Muted);
            if (HasConditionEffect(ConditionEffects.Healing) && !HasConditionEffect(ConditionEffects.Sick) && !HasConditionEffect(ConditionEffects.DrakzixCharging))
            {
                if (_healing > 1)
                {
                    HP = Math.Min(Stats[0], HP + (int)_healing);
                    _healing -= (int)_healing;
                }

                _healing += 28 * (CoreConstant.worldTickMs / 1000f);
            }
            if (Mark == 4 && Surge >= 25)
            {
                if (_healing3 > 1)
                {
                    HP = Math.Min(Stats[0], HP + (int)_healing3);
                    _healing3 -= (int)_healing3;
                }

                _healing3 += 24 * (CoreConstant.worldTickMs / 1000f);
            }
            if (CheckAxe())
            {
                if (_healing4 > 1)
                {
                    HP = Math.Min(Stats[0], HP + (int)_healing4);
                    _healing4 -= (int)_healing4;
                }

                _healing4 += 24 * (CoreConstant.worldTickMs / 1000f);
            }
            if (HasConditionEffect(ConditionEffects.HealthRecovery)
                && !HasConditionEffect(ConditionEffects.Sick)
                && !HasConditionEffect(ConditionEffects.DrakzixCharging))
            {
                if (_healing2 > 1)
                {
                    HP = Math.Min(Stats[0], HP + (int)_healing2);
                    _healing2 -= (int)_healing2;
                }

                _healing2 += 36 * (CoreConstant.worldTickMs / 1000f);
            }
            if (HasConditionEffect(ConditionEffects.Quiet) && MP > 0) 
                MP = 0;

            if (HasConditionEffect(ConditionEffects.Bleeding) && HP > 1)
            {
                if (_bleeding > 1)
                {
                    HP -= (int)_bleeding;
                    if (HP < 1)
                        HP = 1;
                    _bleeding -= (int)_bleeding;
                }

                _bleeding += 28 * (CoreConstant.worldTickMs / 1000f);
            }
            if (HasConditionEffect(ConditionEffects.Hexed))
            {
                HP = -1;
            }

            
            if (HasConditionEffect(ConditionEffects.NinjaSpeedy))
            {
                MP = Math.Max(0, (int)(MP - 10 * CoreConstant.worldTickMs / 1000f));

                if (MP == 0)
                {
                    ApplyConditionEffect(ConditionEffectIndex.NinjaSpeedy, 0);
                    abilityOngoing = false;
                }
            }
            if (HasConditionEffect(ConditionEffects.SamuraiBerserk))
            {
                MP = Math.Max(0, (int)(MP - 10 * CoreConstant.worldTickMs / 1000f));

                if (MP == 0)
                {
                    ApplyConditionEffect(ConditionEffectIndex.SamuraiBerserk, 0);
                    abilityOngoing = false;
                }
            }
            if (HasConditionEffect(ConditionEffects.DrakzixCharging))
                Owner.Timers.Add(new WorldTimer(100, (w) =>
                {
                    if (w == null || w.Deleted || this == null) return;

                    var drained_temp = DrainedHP;
                    MP -= 25;
                    HP -= 1;
                    DrainedHP += 6;
                    if (DrainedHP >= 100)
                    {
                        MP -= 25;
                        HP -= 1;
                        DrainedHP += 6;
                        if (DrainedHP >= 400)
                        {
                            MP -= 25;
                            HP -= 2;
                            DrainedHP += 6;
                        }
                    }
                    if (MP <= Stats[1] * .05)
                    {
                        MP += 75;
                        HP -= 30;
                        DrainedHP += 5;
                    }
                }));
            if (_newbieTime > 0)
            {
                _newbieTime -= (int)CoreConstant.worldTickMs;

                if (_newbieTime < 0) _newbieTime = 0;
            }
            if (_canTpCooldownTime > 0)
            {
                _canTpCooldownTime -= (int)CoreConstant.worldTickMs + 10;

                if (_canTpCooldownTime < 0) _canTpCooldownTime = 0;
            }
        }

        private void HandleProt()
        {
            ProtectionMax = (int)(((Math.Pow(Stats[11], 2)) * 0.04) + (Stats[0] / 50)) + 5;
            if (Protection > ProtectionMax) Protection = ProtectionMax;
            if (HasConditionEffect(ConditionEffects.Surged) && _protDelay > 0) _protDelay = 0;
            if (_protDelay <= 0 && !(Protection >= ProtectionMax))
            {
                if (Protection < 0) Protection = 0;

                _protRegen += ProtectionMax / 10 * (CoreConstant.worldTickMs / 1000f);

                if (_protRegen > 1)
                {
                    Protection += (int)_protRegen;
                    if (Protection > ProtectionMax)
                        Protection = ProtectionMax;
                    _protRegen -= (int)_protRegen;
                }
            }
            if (_protDelay > 0)
            {
                _protDelay -= (CoreConstant.worldTickMs / 1000f);

                if (_protDelay < 0) _protDelay = 0;
            }
        }

        private bool CanHpRegen()
        {
            if (HasConditionEffect(ConditionEffects.Sick) ||
                    HasConditionEffect(ConditionEffects.Bleeding) ||
                        HasConditionEffect(ConditionEffects.Corrupted) ||
                        HasConditionEffect(ConditionEffects.DrakzixCharging))

                return false;

            return true;
        }

        private bool CanMpRegen()
        {
            if (HasConditionEffect(ConditionEffects.Quiet) ||
                    HasConditionEffect(ConditionEffects.NinjaSpeedy) ||
                        HasConditionEffect(ConditionEffects.SamuraiBerserk) ||
                            HasConditionEffect(ConditionEffects.Corrupted)
                                 ||HasConditionEffect(ConditionEffects.DrakzixCharging))
                return false;

            return true;
        }

        internal void SetNewbiePeriod()
        {
            _newbieTime = 3000;
        }

        internal void SetTPDisabledPeriod()
        {
            _canTpCooldownTime = 8 * 1000; // 8 seconds
        }

        internal void SetProtRegenCooldown()
        {
            _protDelay = 10.0;
        }

        public bool IsVisibleToEnemy()
        {
            if (HasConditionEffect(ConditionEffects.Paused))
                return false;
            if (HasConditionEffect(ConditionEffects.Invisible))
                return false;
            if (HasConditionEffect(ConditionEffects.Hidden))
                return false;
            if (_newbieTime > 0)
                return false;
            return true;
        }

        public bool TPCooledDown()
        {
            return _canTpCooldownTime <= 0;
        }
    }
}
