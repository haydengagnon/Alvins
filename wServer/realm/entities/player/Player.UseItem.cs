using common;
using common.resources;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using wServer.networking.packets;
using wServer.networking.packets.outgoing;
using wServer.realm.worlds;
using wServer.realm.worlds.logic;

namespace wServer.realm.entities
{
    partial class Player
    {
        public const int MaxAbilityDist = 14;
        public int DrainedHP;
        public int DrakzixHPDrain;
        public int BMToggle;
        public List<Tuple<int, int, bool>> BMEffects;
        public bool abilityOngoing;
        public int SupportScore;

        public int PetID = 0;

        private Random rng = new Random();

        public ushort[] Rewards = {
             //LIST 1
            0x57e0, //cash robe
            0x57aa, //aniki prism
            0x5779, //diamond sword
            0x57d6, //baldi head
            0x5904, //DANGER PASKA
            0x57ed, //lazer gun real
            0x57f5, //triangle helmet
            0x5933, //sun bottle
            0x5925, //invulnerability shield
            0x5948, //ghast tear
            0x594e, //slimy brim brim
            0x593c, //netherite hlomet
            0x5918, //sparkling shialds
            0x5920, //cosmos is gay
            0x5b07, //LEGS
            0x595e, //coal amror
            0x57e1 //orendands ring
            //0x5b39 //roft neccopolis key 
            
            /* // LIST 2
            0x5b07, //LEGS
            0x5779, //diamond sword
            0x5925, //invulnerability shield
            0x573b, //pizzer slice
            0x5739, //xue hua tome tome
            0x48fb, //spiritclaw
            0x599f, //peace of mind
            0x53aa, //raindow of maxy
            0x5a13, //alvin realms is gonna disappear
            0x5987, //BLOWN THE FUCK OUT
            0x5904, //DANGER PASKA
            0x5b07, //LEGS
            0x5779, //diamond sword
            0x5925, //invulnerability shield
            0x573b, //pizzer slice
            0x5739, //xue hua tome tome
            0x48fb, //spiritclaw
            0x599f, //peace of mind
            0x53aa, //raindow of maxy
            0x5a13, //alvin realms is gonna disappear
            0x5987, //BLOWN THE FUCK OUT
            0x5904, //DANGER PASKA
            //0x5bcd, // PVZ PLANT KEY
            0x5951 //nether fire orb
            */

        };
        public ushort[] PrimalTokens = {
             //LIST 1
            0x5b13, //SLASHER
            0x5b17, //decap
            0x5b1b, //bonewand
            0x5b10, //kittycat
            0x5b22, //ten don
            0x5b2f, //apex
            0x5b15, //boobie plat
            0x5b16, //zolmex bp
            0x5b19, //cucky hjelm
            0x5b1a, //ball nuts
            0x5b21, //rotmg predator neck
            0x5b31, //scepter
            0x5b1d, //deez nuts rob
            0x5b1e, //nononono
            0x5b1f, //smile hart
            0x5b20, //fursuit
            0x5b25, //destiny modenator
            0x5b28, //gDOES PRIOMAL RGIN
            0x5b29, //the strorm
            0x5b2b, //fem attention tom
            0x5b2c, //ar quiver
            0x5b2e, //fleshy brow
            0x5b32, //polish skull
            0x5b26, //goul gogles
            0x5b27  //GROMMIT
        };
        public ushort[] NecrospoilisTokens = {
             //LIST 1
           0x5b13, //SLASHER
            0x5b17, //decap
            0x5b1b, //bonewand
            0x5b10, //kittycat
            0x5b22, //ten don
            0x5b2f, //apex
            0x5b15, //boobie plat
            0x5b16, //zolmex bp
            0x5b19, //cucky hjelm
            0x5b1a, //ball nuts
            0x5b21, //rotmg predator neck
            0x5b31, //scepter
            0x5b1d, //deez nuts rob
            0x5b1e, //nononono
            0x5b1f, //smile hart
            0x5b20, //fursuit
            0x5b25, //destiny modenator
            0x5b28, //gDOES PRIOMAL RGIN
            0x5b29, //the strorm
            0x5b2b, //fem attention tom
            0x5b2c, //ar quiver
            0x5b2e, //fleshy brow
            0x5b32, //polish skull
            0x5b26, //goul gogles
            0x5b27  //GROMMIT
        };
        public ushort[] Welding128 = {
             //LIST 1
            0x5bbf //welding3
        };
        public static readonly ConditionEffect[] NegativeEffs = {
            new ConditionEffect
            {
                Effect = ConditionEffectIndex.Slowed,
                DurationMS = 0
            },
            new ConditionEffect
            {
                Effect = ConditionEffectIndex.Paralyzed,
                DurationMS = 0
            },
            new ConditionEffect
            {
                Effect = ConditionEffectIndex.Weak,
                DurationMS = 0
            },
            new ConditionEffect
            {
                Effect = ConditionEffectIndex.Stunned,
                DurationMS = 0
            },
            new ConditionEffect
            {
                Effect = ConditionEffectIndex.Confused,
                DurationMS = 0
            },
            new ConditionEffect
            {
                Effect = ConditionEffectIndex.Blind,
                DurationMS = 0
            },
            new ConditionEffect
            {
                Effect = ConditionEffectIndex.Quiet,
                DurationMS = 0
            },
            new ConditionEffect
            {
                Effect = ConditionEffectIndex.ArmorBroken,
                DurationMS = 0
            },
            new ConditionEffect
            {
                Effect = ConditionEffectIndex.Bleeding,
                DurationMS = 0
            },
            new ConditionEffect
            {
                Effect = ConditionEffectIndex.Dazed,
                DurationMS = 0
            },
            new ConditionEffect
            {
                Effect = ConditionEffectIndex.Sick,
                DurationMS = 0
            },
            new ConditionEffect
            {
                Effect = ConditionEffectIndex.Drunk,
                DurationMS = 0
            },
            new ConditionEffect
            {
                Effect = ConditionEffectIndex.Hallucinating,
                DurationMS = 0
            },
            new ConditionEffect
            {
                Effect = ConditionEffectIndex.Hexed,
                DurationMS = 0
            },
            new ConditionEffect
            {
                Effect= ConditionEffectIndex.Unstable,
                DurationMS = 0
            },
            new ConditionEffect
            {
                Effect= ConditionEffectIndex.Darkness,
                DurationMS = 0
            },
            new ConditionEffect
            {
                Effect= ConditionEffectIndex.Curse,
                DurationMS = 0
            },
            new ConditionEffect
            {
                Effect= ConditionEffectIndex.Nullified,
                DurationMS = 0
            }
        };

        public static readonly ConditionEffect[] PositiveEffs = {
            new ConditionEffect
            {
                Effect = ConditionEffectIndex.Invisible,
                DurationMS = 0
            },
            new ConditionEffect
            {
                Effect = ConditionEffectIndex.Speedy,
                DurationMS = 0
            },
            new ConditionEffect
            {
                Effect = ConditionEffectIndex.Healing,
                DurationMS = 0
            },
            new ConditionEffect
            {
                Effect = ConditionEffectIndex.Damaging,
                DurationMS = 0
            },
            new ConditionEffect
            {
                Effect = ConditionEffectIndex.Berserk,
                DurationMS = 0
            },
            new ConditionEffect
            {
                Effect = ConditionEffectIndex.Armored,
                DurationMS = 0
            },
            new ConditionEffect
            {
                Effect = ConditionEffectIndex.NinjaSpeedy,
                DurationMS = 0
            },
            new ConditionEffect
            {
                Effect = ConditionEffectIndex.Swiftness,
                DurationMS = 0
            },
            new ConditionEffect
            {
                Effect = ConditionEffectIndex.Empowered,
                DurationMS = 0
            },
            new ConditionEffect
            {
                Effect = ConditionEffectIndex.Bravery,
                DurationMS = 0
            },
            new ConditionEffect
            {
                Effect = ConditionEffectIndex.SamuraiBerserk,
                DurationMS = 0
            },
            new ConditionEffect
            {
                Effect = ConditionEffectIndex.Relentless,
                DurationMS = 0
            },
            new ConditionEffect
            {
                Effect = ConditionEffectIndex.Relentless,
                DurationMS = 0
            },
            new ConditionEffect
            {
                Effect = ConditionEffectIndex.Invulnerable,
                DurationMS = 0
            },
            new ConditionEffect
            {
                Effect = ConditionEffectIndex.ManaRecovery,
                DurationMS = 0
            },            
            new ConditionEffect
            {
                Effect= ConditionEffectIndex.HealthRecovery,
                DurationMS = 0
            }
        };

        private readonly object _useLock = new object();

        public void UseItem(int objId, int slot, Position pos)
        {
            if (Monitor.TryEnter(_useLock, new TimeSpan(0, 0, 1)))
                try
                {
                    var entity = Owner.GetEntity(objId);

                    if (entity == null)
                    {
                        Client.SendPacket(new InvResult { Result = 1 });
                        return;
                    }

                    if (entity is Player && objId != Id)
                    {
                        Client.SendPacket(new InvResult { Result = 1 });
                        return;
                    }

                    var container = entity as IContainer;

                    if (this.Dist(entity) > 3)
                    {
                        Client.SendPacket(new InvResult { Result = 1 });
                        return;
                    }

                    var cInv = container?.Inventory.CreateTransaction();

                    // get item
                    Item item = null;
                    foreach (var stack in Stacks.Where(stack => stack.Slot == slot))
                    {
                        item = stack.Pull();

                        if (item == null)
                            return;

                        break;
                    }

                    if (item == null)
                    {
                        if (container == null)
                            return;

                        item = cInv[slot];
                    }

                    if (item == null)
                        return;

                    if (!(item.Consumable || item.InvUse || item.PetUse) && item.MultiPhase == abilityOngoing)
                        if (item.Cooldown != 0)
                            LastAltAttack = item.Cooldown;
                        else
                            LastAltAttack = 550;

                    // make sure not trading and trying to consume item
                    if (tradeTarget != null && item.Consumable)
                        return;

                    if (MP < item.MpCost && !item.MultiPhase)
                    {
                        Client.SendPacket(new InvResult { Result = 1 });
                        return;
                    }

                    if(item.ObjectType == 0x62a9 && Owner.Name != "Nexus")
                    {
                        SendError("You can only use this item in the Nexus.");
                        return;
                    }

                    // use item
                    var slotType = 10;
                    if (slot < cInv.Length)
                    {
                        slotType = container.SlotTypes[slot];

                        if (item.Consumable)
                        {
                            var gameData = Manager.Resources.GameData;
                            var db = Manager.Database;

                            Item successor = null;
                            if (item.SuccessorId != null)
                                successor = gameData.Items[gameData.IdToObjectType[item.SuccessorId]];
                            cInv[slot] = successor;


                            if(entity is Player)
                            {
                                var p = entity as Player;
                                p.SaveToCharacter();
                            }

                            var trans = db.Conn.CreateTransaction();
                            if (container is GiftChest)
                                if (successor != null)
                                    db.SwapGift(Client.Account, item.ObjectType, successor.ObjectType, trans);
                                else
                                    db.RemoveGift(Client.Account, item.ObjectType, trans);

                            var task = trans.ExecuteAsync();
                            task.ContinueWith(t =>
                            {
                                var success = !t.IsCanceled && t.Result;
                                if (!success || !Inventory.Execute(cInv))
                                {
                                    entity.ForceUpdate(slot);
                                    return;
                                }

                                if (slotType > 0)
                                {
                                    FameCounter.UseAbility();
                                }
                                else
                                {
                                    if (item.ActivateEffects.Any(eff =>
                                        eff.Effect == ActivateEffects.Heal || eff.Effect == ActivateEffects.HealNova || eff.Effect == ActivateEffects.Magic || eff.Effect == ActivateEffects.MagicNova))
                                        FameCounter.DrinkPot();
                                }

                                Activate(item, pos);
                            });
                            task.ContinueWith(e =>
                                Program.Debug(typeof(Player), e.Exception.ToString(), true),
                                TaskContinuationOptions.OnlyOnFaulted);
                           
                            return;
                        }

                        if (slotType > 0) FameCounter.UseAbility();
                    }
                    else FameCounter.DrinkPot();

                    if (item.Consumable || item.SlotType == slotType || item.InvUse || item.PetUse) Activate(item, pos);
                    else Client.SendPacket(new InvResult { Result = 1 });
                }
                finally { Monitor.Exit(_useLock); }
        }

        private void Activate(Item item, Position target)
        {
            bool usable = true;


            if ((HP < item.HpCost) || (Math.Max(0, MP) < item.MpCost)) // surge is set to 0 when a player enters a world
                usable = false;

            if (item.MultiPhase)
            {
                foreach (var eff in item.ActivateEffects)
                {
                    switch (eff.Effect)
                    {
                        case ActivateEffects.ShurikenAbility:
                            ApplyConditionEffect(ConditionEffectIndex.NinjaSpeedy, 0);
                            if (usable)
                                AEShurikenAbility(item, target, eff);
                            abilityOngoing = !abilityOngoing;
                            break;

                        case ActivateEffects.SamuraiAbility:
                            ApplyConditionEffect(ConditionEffectIndex.SamuraiBerserk, 0);
                            if (usable)
                                AESamuraiAbility(item, target, eff);
                            abilityOngoing = !abilityOngoing;
                            break;

                        case ActivateEffects.SiphonAbility:
                            ApplyConditionEffect(ConditionEffectIndex.DrakzixCharging, 0);
                            if (usable)
                                AESiphonAbility(item, target, eff);
                            abilityOngoing = !abilityOngoing;
                            break;
                    }
                }
            }

            if (!usable || (abilityOngoing && !item.Consumable))
                return;
            else
            {
                HP -= item.HpCost;
                MP -= item.MpCost;
            }

            if (RollSacredEffect(SacredEffects.ArcaneGrace, 7) && item.SlotType > 0 && item.MultiPhase == abilityOngoing)
                MP += item.MpCost;

            if (RollSacredEffect(SacredEffects.HelpingHand, 7))
            {
                var range = (float)(2 + SacredBoost(SacredEffects.HelpingHand, 1));
                var atk = StatsManager.GetStatIndex(StatsType.Attack);
                var def = StatsManager.GetStatIndex(StatsType.Defense);
                var amount = (int)SacredBoost(SacredEffects.HelpingHand, 10);
                var duration = 5;

                this.AOE(range, true, entity =>
                {
                    var player = entity as Player;

                    player.Stats.Boost.ActivateBoost[atk].Push(amount, true);
                    player.Stats.Boost.ActivateBoost[def].Push(amount, true);
                    player.Stats.ReCalculateValues();

                    Owner.Timers.Add(new WorldTimer(duration, (w) =>
                    {
                        if (w == null || w.Deleted || player == null) return;

                        player.Stats.Boost.ActivateBoost[atk].Pop(amount, true);
                        player.Stats.Boost.ActivateBoost[def].Pop(amount, true);
                        player.Stats.ReCalculateValues();
                    }));
                });

                BroadcastSync(new ShowEffect
                {
                    EffectType = EffectType.AreaBlast,
                    TargetObjectId = Id,
                    Color = new ARGB(0xffffffff),
                    Pos1 = new Position { X = range }
                }, p => this.DistSqr(p) < RadiusSqr);
            }

            if (CheckDJudgement())
            {
                HP -= item.MpCost * 2;
            }

            if (CheckMerc())
            {
                MP += item.MpCost / 2;
            }

            if (CheckMoonlight())
            {
                int x = rng.Next(2);
                if (x == 1)
                Protection += ProtectionMax / 5;

                if (Protection > ProtectionMax)
                {
                    Protection = ProtectionMax;
                }
            }

            if (CheckIoksRelief())
            {
                ApplyConditionEffect(NegativeEffs);
                BroadcastSync(new ShowEffect
                {
                    EffectType = EffectType.AreaBlast,
                    TargetObjectId = Id,
                    Color = new ARGB(0xffffffff),
                    Pos1 = new Position { X = 1 }
                }, p => this.DistSqr(p) < RadiusSqr);
            }

            if (CheckBleedingFang())
            {
                ApplyConditionEffect(ConditionEffectIndex.Armored, HP * 4);
            }

            if (CheckStarmind())
            {
                WeakBlast(item, target);
            }

            if (CheckDranbielGarbs())
            {
                AEHealNoRest(item, target, 60);
            }

            if (CheckStarCrashRing())
            {
                MP += Inventory[1].MpCost / 4;
            }

            if (CheckTheInfernus())
            {
                BurstFire(item, target);
            }

            if (CheckMeteor())
            {
                DamageGrenade(target);
            }

            if (CheckDimensionalPrism())
            {
                MP += item.MpCost;
            }

            if (CheckUrumi())
            {
                AEHealNoRest(item, target, 2 * Surge + 20);
            }

            if (Mark == 12)
            {
                if (item.MpCost > 0)
                    AEMagicNoRest(item, target, 10);

                if (item.MpCost > 0)
                    AEHealNoRest(item, target, 75);
            }

            foreach (var eff in item.ActivateEffects)
            {
                switch (eff.Effect)
                {
                    case ActivateEffects.GenericActivate:
                        AEGenericActivate(item, target, eff);
                        break;

                    case ActivateEffects.BulletNova:
                        AEBulletNova(item, target, eff);
                        break;

                    case ActivateEffects.Shoot:
                        AEShoot(item, target, eff);
                        break;

                    case ActivateEffects.StatBoostSelf:
                        AEStatBoostSelf(item, target, eff);
                        break;

                    case ActivateEffects.StatBoostAura:
                        AEStatBoostAura(item, target, eff);
                        break;

                    case ActivateEffects.ConditionEffectSelf:
                        AEConditionEffectSelf(item, target, eff);
                        break;

                    case ActivateEffects.ConditionEffectAura:
                        AEConditionEffectAura(item, target, eff);
                        break;

                    case ActivateEffects.ClearConditionEffectAura:
                        AEClearConditionEffectAura(item, target, eff);
                        break;

                    case ActivateEffects.Heal:
                        AEHeal(item, target, eff);
                        break;

                    case ActivateEffects.HealNova:
                        AEHealNova(item, target, eff);
                        break;

                    case ActivateEffects.Magic:
                        AEMagic(item, target, eff);
                        break;

                    case ActivateEffects.MagicNova:
                        AEMagicNova(item, target, eff);
                        break;

                    case ActivateEffects.DamageNova:
                        AEDamageNova(item, target, eff);
                        break;

                    case ActivateEffects.Teleport:
                        AETeleport(item, target, eff);
                        break;

                    case ActivateEffects.VampireBlast:
                        AEVampireBlast(item, target, eff);
                        break;

                    case ActivateEffects.Trap:
                        AETrap(item, target, eff);
                        break;

                    case ActivateEffects.StasisBlast:
                        StasisBlast(item, target, eff);
                        break;

                    case ActivateEffects.Decoy:
                        AEDecoy(item, target, eff);
                        break;

                    case ActivateEffects.Lightning:
                        AELightning(item, target, eff);
                        break;

                    case ActivateEffects.PoisonGrenade:
                        AEPoisonGrenade(item, target, eff);
                        break;

                    case ActivateEffects.RemoveNegativeConditions:
                        AERemoveNegativeConditions(item, target, eff);
                        break;

                    case ActivateEffects.RemoveNegativeConditionsSelf:
                        AERemoveNegativeConditionSelf(item, target, eff);
                        break;

                    case ActivateEffects.FixedStat:
                        AEFixedStat(item, target, eff);
                        break;

                    case ActivateEffects.IncrementStat:
                        AEIncrementStat(item, target, eff);
                        break;

                    case ActivateEffects.EonActivate:
                        AEEonActivate(item, target, eff);
                        break;

                    case ActivateEffects.Create:
                        AECreate(item, target, eff);
                        break;

                    case ActivateEffects.Dye:
                        AEDye(item, target, eff);
                        break;

                    case ActivateEffects.ShootEff:
                        AEShootEff(item, target, eff);
                        break;

                    case ActivateEffects.Shoot2:
                        AEMiscShoot(item, target, eff);
                        break;

                    case ActivateEffects.Fame:
                        AEAddFame(item, target, eff);
                        break;

                    case ActivateEffects.Backpack:
                        AEBackpack(item, target, eff);
                        break;

                    case ActivateEffects.XPBoost:
                        AEXPBoost(item, target, eff);
                        break;

                    case ActivateEffects.LDBoost:
                        AELDBoost(item, target, eff);
                        break;

                    case ActivateEffects.LTBoost:
                        AELTBoost(item, target, eff);
                        break;

                    case ActivateEffects.UnlockPortal:
                        AEUnlockPortal(item, target, eff);
                        break;

                    case ActivateEffects.UnlockEmote:
                        AEUnlockEmote(item, eff);
                        break;

                    case ActivateEffects.HealingGrenade:
                        AEHealingGrenade(item, target, eff);
                        break;

                    case ActivateEffects.SorForge:
                        AESorForge(item, target, eff);
                        break;

                    case ActivateEffects.TreasureActivate:
                        AETreasureActivate(item, target, eff);
                        break;

                    case ActivateEffects.FameActivate:
                        AEFameActivate(item, target, eff);
                        break;

                    case ActivateEffects.OnraneActivate:
                        AEOnraneActivate(item, target, eff);
                        break;

                    case ActivateEffects.RandomOnrane:
                        AERandomOnrane(item, target, eff);
                        break;

                    case ActivateEffects.URandomOnrane:
                        AEURandomOnrane(item, target, eff);
                        break;

                    case ActivateEffects.RandomGold:
                        AERandomGold(item, target, eff);
                        break;

                    case ActivateEffects.Banner:
                        AEBanner(item, target, eff);
                        break;

                    case ActivateEffects.Heal2:
                        AEHeal2(item, target, eff);
                        break;

                    case ActivateEffects.Magic2:
                        AEMagic2(item, target, eff);
                        break;

                    case ActivateEffects.DiceActivate:
                        AEDice(item, target, eff);
                        break;

                    case ActivateEffects.BigStasisBlast:
                        BigStasisBlast(item, target, eff);
                        break;

                    case ActivateEffects.UnlockSkin:
                        AEUnlockSkin(item, target, eff);
                        break;

                    case ActivateEffects.SorConstruct:
                        AESorConstruct(item, target, eff);
                        break;

                    case ActivateEffects.ActivateFragment:
                        AEActivateFragment(item, target, eff);
                        break;

                    case ActivateEffects.BurstInferno:
                        AEBurstInferno(item, target, eff);
                        break;

                    case ActivateEffects.DDiceActivate:
                        AEDDiceActivate(item, target, eff);
                        break;

                    case ActivateEffects.RDiceActivate:
                        AERDiceActivate(item, target, eff);
                        break;

                    case ActivateEffects.AbbyConstruct:
                        AEAbbyConstruct(item, target, eff);
                        break;

                    case ActivateEffects.Torii:
                        AETorii(item, target, eff);
                        break;

                    case ActivateEffects.JacketAbility:
                        AEJacketAbility(item, target, eff);
                        break;

                    case ActivateEffects.JacketAbility2:
                        break;

                    case ActivateEffects.MarksActivate:
                        AEMarksActivate(item, target, eff);
                        break;

                    case ActivateEffects.AscensionActivate:
                        AEAscensionActivate(item, target, eff);
                        break;

                    case ActivateEffects.PowerStat:
                        AEPowerStat(item, target, eff);
                        break;

                    case ActivateEffects.EffectRandom:
                        AEEffectRandom(item, target, eff);
                        break;

                    case ActivateEffects.SacredActivate:
                        AESacredActivate(item, target, eff);
                        break;

                    case ActivateEffects.BulletNova2:
                        AEBulletNova2(item, target, eff);
                        break;

                    case ActivateEffects.AstonAbility:
                        AEAstonAbility(item, target, eff);
                        break;

                    case ActivateEffects.InsigniaActivate:
                        AEInsigniaActivate(item, target, eff);
                        break;
                    
                    case ActivateEffects.ReaperUse:
                        AEReaperUse(item, target, eff);
                        break;
                    
                    case ActivateEffects.AsgardUse:
                        AEAsgardUse(item, target, eff);
                        break;
                    case ActivateEffects.FAVELASUse:
                        AEFAVELASUse(item, target, eff);
                        break;
                    case ActivateEffects.FallenUse:
                        AEFallenUse(item, target, eff);
                        break;
                    case ActivateEffects.EndUse:
                        AEEndUse(item, target, eff);
                        break;
                    case ActivateEffects.CatUse:
                        AECatUse(item, target, eff);
                        break;

                    case ActivateEffects.DreamEssenceActivate:
                        AEDreamEssenceActivate(item, target, eff);
                        break;
                        
                    //case ActivateEffects.AdminShoot:
                    //    AEAdminShoot(item, target, eff);
                    //    break;

                    case ActivateEffects.Gift:
                        AEGift(item, target, eff);
                        break;
                    case ActivateEffects.GiftNecrospoilis:
                        AEGiftNecrospoilis(item, target, eff);
                        break;

                    case ActivateEffects.DualShoot:
                        break;

                    case ActivateEffects.BurningLightning:
                        break;

                    case ActivateEffects.Drake:
                        break;

                    case ActivateEffects.PermaPet:
                        break;

                    case ActivateEffects.DazeBlast:
                        break;

                    case ActivateEffects.ClearConditionEffectSelf:
                        break;

                    case ActivateEffects.TomeDamage:
                        break;

                    case ActivateEffects.MultiDecoy:
                        break;

                    case ActivateEffects.Mushroom:
                        break;

                    case ActivateEffects.PearlAbility:
                        break;

                    case ActivateEffects.BuildTower:
                        break;

                    case ActivateEffects.MonsterToss:
                        break;

                    case ActivateEffects.PartyAOE:
                        break;

                    case ActivateEffects.MiniPot:
                        break;

                    case ActivateEffects.Halo:
                        break;

                    case ActivateEffects.Summon:
                        break;

                    case ActivateEffects.ChristmasPopper:
                        break;

                    case ActivateEffects.Belt:
                        break;

                    case ActivateEffects.Totem:
                        break;

                    case ActivateEffects.VaultUnlocker:
                        AEUnlockVault(item, target, eff);
                        break;

                    case ActivateEffects.Pet:
                        {
                            if (Database.GuestNames.Contains(Name))
                            {
                                SendInfo("Players without registered names cannot use this item.");
                                return;
                            }
                            string message = null;

                            if (Owner.Name == "Vault")
                            {
                                message = "You have recieved cosmetic pet : " + eff.ObjectId;
                                Entity CosmeticPet = Resolve(Manager, eff.ObjectId);
                                CosmeticPet.Move(X, Y);
                                CosmeticPet.SetPlayerOwner(this);
                                Owner.EnterWorld(CosmeticPet);

                                SendInfo(message);
                            }
                            else
                            {
                                SendInfo("You need to be in Vault to use this item!");
                                return;
                            }
                            SaveToCharacter();
                            UpdateCount++;
                        }
                        break;

                    case ActivateEffects.MysteryPortal:
                        break;

                    case ActivateEffects.ChangeSkin:
                        break;

                    case ActivateEffects.Unlock:
                        break;

                    case ActivateEffects.MysteryDyes:
                        break;

                    case ActivateEffects.UnScroll:
                        break;

                    case ActivateEffects.BlackScroll:
                        break;

                    case ActivateEffects.RenamePet:
                        break;

                    case ActivateEffects.IdScroll:
                        break;

                    case ActivateEffects.BrownScroll:
                        break;

                    case ActivateEffects.HealNovaSigil:
                        break;

                    case ActivateEffects.RevivementBox:
                        break;

                    case ActivateEffects.NeonBox:
                        break;

                    case ActivateEffects.DareFistBox:
                        break;

                    case ActivateEffects.VorvBox:
                        break;

                    case ActivateEffects.GPBox:
                        break;

                    case ActivateEffects.MayhemBox:
                        break;

                    case ActivateEffects.SunshineBox:
                        break;

                    case ActivateEffects.BlizzardBox:
                        break;

                    case ActivateEffects.WigWeekBox:
                        break;

                    case ActivateEffects.LootboxActivate:
                        break;

                    case ActivateEffects.PLootboxActivate:
                        break;

                    case ActivateEffects.SorMachine:
                        break;

                    case ActivateEffects.RandomKantos:
                        break;

                    case ActivateEffects.PoZPage:
                        break;

                    case ActivateEffects.AsiHeal:
                        break;

                    case ActivateEffects.AsiimovBox:
                        break;

                    case ActivateEffects.NewCharSlot:
                        break;

                    case ActivateEffects.RageReapBox:
                        break;

                    case ActivateEffects.BronzeLockbox:
                        break;

                    case ActivateEffects.SpiderTrap:
                        break;

                    case ActivateEffects.RoyalTrap:
                        break;

                    case ActivateEffects.OPBUFF:
                        break;

                    case ActivateEffects.WARPAWNBUFF:
                        break;

                    case ActivateEffects.SilentBox:
                        break;

                    case ActivateEffects.CrimsonBox:
                        break;

                    case ActivateEffects.FUnlockPortal:
                        break;

                    case ActivateEffects.CreateGauntlet:
                        break;

                    case ActivateEffects.TalismanAbility:
                        AETalismanAbility(item, target, eff);
                        break;

                    case ActivateEffects.DreadcullAbility:
                        AEDreadcullAbility(item, target, eff);
                        break;

                    case ActivateEffects.ShurikenAbility:
                        break;

                    case ActivateEffects.SamuraiAbility:
                        break;

                    case ActivateEffects.SiphonAbility:
                        break;

                    default:
                        Program.Debug(typeof(Player), $"Activate effect {eff.Effect} not implemented.", warn: true);
                        break;
                }
            }
        }

        private void AEDDiceActivate(Item item, Position target, ActivateEffect eff)
        {
            ConditionEffectIndex[] gamblerEffs = {
                ConditionEffectIndex.Weak,
                ConditionEffectIndex.Bravery,
                ConditionEffectIndex.Damaging
            };
            var roll = new Random().Next(0, 3);
            if (roll != 3)
                ApplyConditionEffect(gamblerEffs[roll], eff.DurationMS);
        }

        private void AERDiceActivate(Item item, Position target, ActivateEffect eff)
        {
            ConditionEffectIndex[] gamblerEffs = {
                ConditionEffectIndex.Berserk,
                ConditionEffectIndex.Armored,
                ConditionEffectIndex.Stunned
            };
            var roll = new Random().Next(0, 3);
            if (roll != 3)
                ApplyConditionEffect(gamblerEffs[roll], eff.DurationMS);
        }

        private void AEUnlockEmote(Item item, ActivateEffect eff)
        {
            if (Client.Player.Owner == null || Client.Player.Owner is Test)
            {
                SendInfo("Can't use emote unlocks in test worlds.");
                return;
            }

            var emotes = Client.Account.Emotes;
            if (!emotes.Contains(eff.Id))
                emotes.Add(eff.Id);
            Client.Account.Emotes = emotes;
            Client.Account.FlushAsync();
            SendInfo($"{eff.Id} Emote unlocked successfully");
        }

        private void AEUnlockPortal(Item item, Position target, ActivateEffect eff)
        {
            var gameData = Manager.Resources.GameData;

            // find locked portal
            var portals = Owner.StaticObjects.Values
                .Where(s => s is Portal && s.ObjectDesc.ObjectId.Equals(eff.LockedName) && s.DistSqr(this) <= 9)
                .Select(s => s as Portal);
            if (!portals.Any())
                return;
            var portal = portals.Aggregate(
                (curmin, x) => (curmin == null || x.DistSqr(this) < curmin.DistSqr(this) ? x : curmin));
            if (portal == null)
                return;

            // get proto of world
            if (!Manager.Resources.Worlds.Data.TryGetValue(eff.DungeonName, out var proto))
            {
                return;
            }

            if (proto.portals == null || proto.portals.Length < 1)
            {
                return;
            }

            // create portal of unlocked world
            var portalType = (ushort)proto.portals[0];

            if (!(Resolve(Manager, portalType) is Portal uPortal))
            {
                Program.Debug(typeof(Player), $"Error creating portal: {portalType}", true);
                return;
            }

            var portalDesc = gameData.Portals[portal.ObjectType];
            var uPortalDesc = gameData.Portals[portalType];

            // create world
            World world;
            if (proto.id < 0)
                world = Manager.GetWorld(proto.id);
            else
            {
                DynamicWorld.TryGetWorld(proto, Client, out world);
                world = Manager.AddWorld(world ?? new World(proto));
            }
            uPortal.WorldInstance = world;

            // swap portals
            if (!portalDesc.NexusPortal || !Manager.Monitor.RemovePortal(portal))
                Owner.LeaveWorld(portal);
            uPortal.Move(portal.X, portal.Y);
            uPortal.Name = uPortalDesc.DisplayId;
            var uPortalPos = new Position { X = portal.X - .5f, Y = portal.Y - .5f };
            if (!uPortalDesc.NexusPortal || !Manager.Monitor.AddPortal(world.Id, uPortal, uPortalPos))
                Owner.EnterWorld(uPortal);

            // setup timeout
            if (!uPortalDesc.NexusPortal)
            {
                var timeoutTime = gameData.Portals[portalType].Timeout;
                Owner.Timers.Add(new WorldTimer(timeoutTime * 1000, (w) =>
                {
                    if (w == null || w.Deleted || uPortal == null) return;

                    w.LeaveWorld(uPortal);
                }));
            }

            // announce
            Owner.BroadcastPacket(new Notification
            {
                Color = new ARGB(0xFF00FF00),
                ObjectId = Id,
                Message = "Unlocked by " + Name
            }, null);
            foreach (var player in Owner.Players.Values)
                player.SendInfo(string.Format("{{\"key\":\"{{server.dungeon_unlocked_by}}\",\"tokens\":{{\"dungeon\":\"{0}\",\"name\":\"{1}\"}}}}", world.SBName, Name));
        }

        private void AELTBoost(Item item, Position target, ActivateEffect eff)
        {
            if (LTBoostTime < 0 || (LTBoostTime > eff.DurationMS * 1000 && eff.DurationMS >= 0))
                return;

            LTBoostTime = eff.DurationMS; // duration is in seconds, not MS
            InvokeStatChange(StatsType.LTBoostTime, LTBoostTime * 1000, true);
        }

        private void AEGift(Item item, Position target, ActivateEffect eff)
        {
            if (UseConsumable(item.ObjectId))
            {
                ushort gift = Rewards[rng.Next(Rewards.Length)];
                item = Manager.Resources.GameData.Items[gift];
                var acc = Client.Account;

                for (int i = 4; i < Inventory.Length; i++)
                    if (Inventory[i] == null)
                    {
                        Inventory[i] = Manager.Resources.GameData.Items[gift];
                        UpdateCount++;
                        SaveToCharacter();
                        string PlayerName = acc.Name;
                        string ItemName = item.DisplayName;
                        Text tokens = new Text()
                        {
                        Name = "LOOT MAN",
                        NumStars = -1,
                        Txt = $"[{PlayerName}] HAS RECIEVED A [{ItemName}] FROM THEIR TOKENS!!",
                        BubbleTime = 0,
                        NameColor = 0x88ddef,
                        TextColor = 0x8B1010
                        };
                        Manager.GlobalBroadcast(tokens);
                        Client.SendPacket(new GlobalNotification() { Text = "lgdrop" });
                        return;
                    }
            }
        }
        private void AEGiftNecrospoilis(Item item, Position target, ActivateEffect eff)
        {
            if (UseConsumable(item.ObjectId))
            {
                ushort gift = NecrospoilisTokens[rng.Next(NecrospoilisTokens.Length)];
                item = Manager.Resources.GameData.Items[gift];
                var acc = Client.Account;

                for (int i = 4; i < Inventory.Length; i++)
                    if (Inventory[i] == null)
                    {
                        Inventory[i] = Manager.Resources.GameData.Items[gift];
                        UpdateCount++;
                        SaveToCharacter();
                        string PlayerName = acc.Name;
                        string ItemName = item.DisplayName;
                        Text tokens = new Text()
                        {
                            Name = "LOOT MAN",
                            NumStars = -1,
                            Txt = $"[{PlayerName}] HAS RECIEVED A [{ItemName}] FROM THEIR Necrospoilis TOKENS!!",
                            BubbleTime = 0,
                            NameColor = 0x88ddef,
                            TextColor = 0x8B1010
                        };
                        Manager.GlobalBroadcast(tokens);
                        Client.SendPacket(new GlobalNotification() { Text = "lgdrop" });
                        return;
                    }
            }
        }
        private void AEGiftPrimal(Item item, Position target, ActivateEffect eff)
        {
            if (UseConsumable(item.ObjectId))
            {
                ushort gift = PrimalTokens[rng.Next(PrimalTokens.Length)];
                item = Manager.Resources.GameData.Items[gift];
                var acc = Client.Account;

                for (int i = 4; i < Inventory.Length; i++)
                    if (Inventory[i] == null)
                    {
                        Inventory[i] = Manager.Resources.GameData.Items[gift];
                        UpdateCount++;
                        SaveToCharacter();
                        string PlayerName = acc.Name;
                        string ItemName = item.DisplayName;
                        Text tokens = new Text()
                        {
                            Name = "LOOT MAN",
                            NumStars = -1,
                            Txt = $"[{PlayerName}] HAS RECIEVED A [{ItemName}] FROM THEIR PRIMAL TOKENS!!",
                            BubbleTime = 0,
                            NameColor = 0x88ddef,
                            TextColor = 0x8B1010
                        };
                        Manager.GlobalBroadcast(tokens);
                        Client.SendPacket(new GlobalNotification() { Text = "primaldrop" });
                        return;
                    }
            }
        }
        private void AEGiftWelding(Item item, Position target, ActivateEffect eff)
        {
            if (UseConsumable(item.ObjectId))
            {
                ushort gift = Welding128[rng.Next(Welding128.Length)];
                item = Manager.Resources.GameData.Items[gift];
                var acc = Client.Account;

                for (int i = 4; i < Inventory.Length; i++)
                    if (Inventory[i] == null)
                    {
                        Inventory[i] = Manager.Resources.GameData.Items[gift];
                        UpdateCount++;
                        SaveToCharacter();
                        string PlayerName = acc.Name;
                        string ItemName = item.DisplayName;
                        Text tokens = new Text()
                        {
                            Name = "LOOT MAN",
                            NumStars = -1,
                            Txt = $"[{PlayerName}] HAS OBTAINED A [{ItemName}] FROM THEIR WELDING STACK!!",
                            BubbleTime = 0,
                            NameColor = 0x88ddef,
                            TextColor = 0x8B1010
                        };
                        Manager.GlobalBroadcast(tokens);
                        Client.SendPacket(new GlobalNotification() { Text = "lgdrop" });
                        return;
                    }
            }
        }
        private void AEUnlockVault(Item item, Position target, ActivateEffect eff)
        {
            var db = Manager.Database;
            var acc = Client.Account;
            var availableSlot = Inventory.GetAvailableInventorySlot(item);
            if (!(Owner is Vault))
            {
                SendInfo("Vault Chest Unlocked!");
                Manager.Database.CreateChest(acc);
                return;
            }
            SendError("You cannot consume Vault Unlockers inside the Vault.");
            Inventory[availableSlot] = item;
            return;
        }
        private void AELDBoost(Item item, Position target, ActivateEffect eff)
        {
            if (LDBoostTime < 0 || (LDBoostTime > eff.DurationMS * 1000 && eff.DurationMS >= 0))
                return;

            LDBoostTime = eff.DurationMS; // duration is in seconds, not MS
            InvokeStatChange(StatsType.LDBoostTime, LDBoostTime * 1000, true);
        }

        private void AEXPBoost(Item item, Position target, ActivateEffect eff)
        {
            if (XPBoostTime < 0 || (XPBoostTime > eff.DurationMS * 1000 && eff.DurationMS >= 0))
                return;

            XPBoostTime = eff.DurationMS; // duration is in seconds, not MS
            XPBoosted = true;
            InvokeStatChange(StatsType.XPBoostTime, XPBoostTime * 1000, true);
        }

        private void AEBackpack(Item item, Position target, ActivateEffect eff)
        {
            HasBackpack = true;
        }

        private void AEMarksActivate(Item item, Position target, ActivateEffect eff)
        {

            if (MarksEnabled == true)
            {
                SendError("You have already enabled marks.");
                return;
            }

            for (var i = 0; i < Inventory.Length; i++)
            {
                if (Inventory[i] == null) continue;
                if (Inventory[i].ObjectId == "Lost Scripture #1")
                {
                    Inventory[i] = null;
                    SaveToCharacter();
                    MarksEnabled = true;
                    break;
                }
            }
        }

        private void AEAscensionActivate(Item item, Position target, ActivateEffect eff)
        {

            if (AscensionEnabled == true)
            {
                SendError("You are already ascended.");
                return;
            }

            var playerDesc = Manager.Resources.GameData.Classes[ObjectType];
            var maxed = playerDesc.Stats.Where((t, i) => Stats.Base[i] >= t.MaxValue).Count();
            if (maxed < 12)
            {
                SendError("You must be 12/12 to ascend.");
                return;
            }

            for (var i = 0; i < Inventory.Length; i++)
            {
                if (Inventory[i] == null) continue;
                if (Inventory[i].ObjectId == "Lost Scripture #2")
                {
                    Inventory[i] = null;
                    SaveToCharacter();
                    AscensionEnabled = true;
                    break;
                }
            }
        }

        private void AEEonActivate(Item item, Position target, ActivateEffect eff)
        {
            var playerDesc = Manager.Resources.GameData.Classes[ObjectType];
            var maxed = playerDesc.Stats.Where((t, i) => Stats.Base[i] >= t.MaxValue).Count();

            if (maxed == 8)
            {
                SendError("MAXED ALREADY");
                return;
            }
            if (maxed <= 7)
            {
                for (var i = 0; i < Inventory.Length; i++)
                {
                    if (Inventory[i] == null) continue;
                    if (Inventory[i].ObjectId == "raindow of Maxy")
                    {
                        Stats.Base[0] = playerDesc.Stats[0].MaxValue;
                        Stats.Base[1] = playerDesc.Stats[1].MaxValue;
                        Stats.Base[2] = playerDesc.Stats[2].MaxValue;
                        Stats.Base[3] = playerDesc.Stats[3].MaxValue;
                        Stats.Base[4] = playerDesc.Stats[4].MaxValue;
                        Stats.Base[5] = playerDesc.Stats[5].MaxValue;
                        Stats.Base[6] = playerDesc.Stats[6].MaxValue;
                        Stats.Base[7] = playerDesc.Stats[7].MaxValue;
                        Stats.Base[8] = playerDesc.Stats[8].MaxValue;
                        Stats.Base[9] = playerDesc.Stats[9].MaxValue;
                        Stats.Base[10] = playerDesc.Stats[10].MaxValue;
                        Stats.Base[11] = playerDesc.Stats[11].MaxValue;
                        SaveToCharacter();
                        SendInfo("Stats are now maxed");
                        break;
                    }
                }
            }
        }

        private void AEEffectRandom(Item item, Position target, ActivateEffect eff)
        {
            if (UseConsumable(item.ObjectId))
            {
                var rnd = new Random();
                var Chance = Random.Next(0, 10);
                switch (Chance)
                {
                    case 0:
                        Effect = "Rising Bubbles";
                        SendInfo("You now activated the rising bubbles effect! Reload to see it in action!");
                        break;

                    case 1:
                        Effect = "Ring of Fire";
                        SendInfo("You now activated the ring of fire effect! Reload to see it in action!");
                        break;

                    case 2:
                        Effect = "Dusty Disaster";
                        SendInfo("You now activated the dusty disaster effect! Reload to see it in action!");
                        break;

                    case 3:
                        Effect = "Glamourous Gems";
                        SendInfo("You now activated the glamorous gems effect! Reload to see it in action!");
                        break;

                    case 4:
                        Effect = "Lovestruck";
                        SendInfo("You now activated the lovestruck effect! Reload to see it in action!");
                        break;

                    case 5:
                        Effect = "Realm Riches";
                        SendInfo("You now activated the realm riches effect! Reload to see it in action!");
                        break;

                    case 6:
                        Effect = "Rainbow Rain";
                        SendInfo("You now activated the rainbow rain effect! Reload to see it in action!");
                        break;

                    case 7:
                        Effect = "Ducky Days";
                        SendInfo("You now activated the ducky days effect! Reload to see it in action!");
                        break;

                    case 8:
                        Effect = "Ascended";
                        SendInfo("You now activated the ascended effect! Reload to see it in action!");
                        break;

                    case 9:
                        Effect = "how do i get ornane?";
                        SendInfo("You now activated the how do i get ornane? effect! Reload to see it in action!");
                        break;
                }
                SaveToCharacter();
            }
        }

        private void AEAddFame(Item item, Position target, ActivateEffect eff)
        {
            if (Owner is Test || Client.Account == null)
                return;
            if (UseConsumable(item.ObjectId))
            {
                var acc = Client.Account;
                var trans = Manager.Database.Conn.CreateTransaction();
                Manager.Database.UpdateCurrency(acc, eff.Amount, CurrencyType.Fame, trans)
                    .ContinueWith(t =>
                    {
                        CurrentFame = acc.Fame;
                    });
                trans.Execute(CommandFlags.FireAndForget);
            }
        }

        //shoots a proj. and gives an effect (w/ attackmod & wismod(?!?!?))
        private void AEShootEff(Item item, Position target, ActivateEffect eff)
        {
            var atkdamage = eff.TotalDamage;
            if (eff.UseAtkMod)
            {
                atkdamage = UseAtkMod(eff.TotalDamage);
            }
            var duration = eff.DurationMS;
            if (eff.UseWisMod)
            {
                duration = UseWisMod(eff.DurationMS);
            }
            ApplyConditionEffect(new ConditionEffect()
            {
                Effect = eff.ConditionEffect.Value,
                DurationMS = eff.DurationMS
            });

            AEMiscShoot(item, target, eff);
        }

        private void AEMiscShoot(Item item, Position target, ActivateEffect eff)
        {
            if (MathsUtils.DistSqr(target.X, target.Y, X, Y) > MaxAbilityDist * MaxAbilityDist) return;
            var arcGap = item.ArcGap * Math.PI / 180;
            var startAngle = Math.Atan2(target.Y - Y, target.X - X) - (item.NumProjectiles - 1) / 2 * arcGap;
            var prjDesc = item.Projectiles[0]; //Assume only one

            var sPkts = new Packet[item.NumProjectiles];
            for (var i = 0; i < item.NumProjectiles; i++)
            {
                var proj = CreateProjectile(prjDesc, item.ObjectType,
                    Stats.GetAttackDamage(prjDesc.MinDamage, prjDesc.MaxDamage, true),
                    Manager.Core.getTotalTickCount(), new Position() { X = X, Y = Y }, (float)(startAngle + arcGap * i));
                Owner.EnterWorld(proj);
                sPkts[i] = new AllyShoot()
                {
                    OwnerId = Id,
                    Angle = proj.Angle,
                    ContainerType = item.ObjectType,
                    BulletId = proj.ProjectileId
                };
                FameCounter.Shoot(proj);
            }
            foreach (var plr in Owner.Players.Values.Where(p => p.DistSqr(this) < RadiusSqr))
            {
                plr.Client.SendPackets(sPkts);
            }
        }

        private void AEJacketAbility(Item item, Position target, ActivateEffect eff)
        {
            ActivateEffect eff2 = null;
            for (int i = 0; i < item.ActivateEffects.Length; i++)
            {
                if (item.ActivateEffects[i].Effect == ActivateEffects.JacketAbility2)
                {
                    eff2 = item.ActivateEffects[i];
                    break;
                }
            }

            if (BMEffects == null)
                BMEffects = new List<Tuple<int, int, bool>>();
            foreach (var buff in BMEffects)
            {
                Stats.Boost.ActivateBoost[StatsManager.GetStatIndex((StatsType)buff.Item1)].Pop(buff.Item2, buff.Item3);
            }
            BMEffects.Clear();

            if (BMToggle == 0)
            {
                Stats.Boost.ActivateBoost[StatsManager.GetStatIndex((StatsType)eff.Stats)].Push(eff.Amount, eff.NoStack);
                BMEffects.Add(new Tuple<int, int, bool>(eff.Stats, eff.Amount, eff.NoStack));
                if (eff.Amount2 != 0)
                {
                    Stats.Boost.ActivateBoost[StatsManager.GetStatIndex((StatsType)eff.Stats2)].Push(eff.Amount2, eff.NoStack);
                    BMEffects.Add(new Tuple<int, int, bool>(eff.Stats2, eff.Amount2, eff.NoStack));
                }
                if (eff.Amount3 != 0)
                {
                    Stats.Boost.ActivateBoost[StatsManager.GetStatIndex((StatsType)eff.Stats3)].Push(eff.Amount3, eff.NoStack);
                    BMEffects.Add(new Tuple<int, int, bool>(eff.Stats3, eff.Amount3, eff.NoStack));
                }

                BMToggle = 1;
            }
            else
            {
                if (eff2 != null)
                {
                    Stats.Boost.ActivateBoost[StatsManager.GetStatIndex((StatsType)eff2.Stats)].Push(eff2.Amount, eff2.NoStack);
                    BMEffects.Add(new Tuple<int, int, bool>(eff2.Stats, eff2.Amount, eff2.NoStack));
                    if (eff2.Amount2 != 0)
                    {
                        Stats.Boost.ActivateBoost[StatsManager.GetStatIndex((StatsType)eff2.Stats2)].Push(eff2.Amount2, eff2.NoStack);
                        BMEffects.Add(new Tuple<int, int, bool>(eff2.Stats2, eff2.Amount2, eff2.NoStack));
                    }
                    if (eff2.Amount3 != 0)
                    {
                        Stats.Boost.ActivateBoost[StatsManager.GetStatIndex((StatsType)eff2.Stats3)].Push(eff2.Amount3, eff2.NoStack);
                        BMEffects.Add(new Tuple<int, int, bool>(eff2.Stats3, eff2.Amount3, eff2.NoStack));
                    }
                }

                BMToggle = 0;
            }
            this.Stats.ReCalculateValues();

            var prjs = new Projectile[8];
            var prjDesc = item.Projectiles[0]; //Assume only one
            var batch = new Packet[9];
            for (var i = 0; i < 8; i++)
            {
                var proj = CreateProjectile(prjDesc, item.ObjectType,
                    Random.Next(prjDesc.MinDamage, prjDesc.MaxDamage),
                    Manager.Core.getTotalTickCount(), new Position { X = X, Y = Y }, (float)(i * (Math.PI * 2) / 8));
                Owner.EnterWorld(proj);
                FameCounter.Shoot(proj);
                batch[i] = new ServerPlayerShoot
                {
                    BulletId = proj.ProjectileId,
                    OwnerId = Id,
                    ContainerType = item.ObjectType,
                    StartingPos = new Position { X = X, Y = Y },
                    Angle = proj.Angle,
                    Damage = (short)proj.Damage
                };
                prjs[i] = proj;
            }
            batch[8] = new ShowEffect
            {
                Pos1 = new Position { X = X, Y = Y },
                TargetObjectId = Id
            };

            foreach (var plr in Owner.Players.Values
                        .Where(p => p.DistSqr(this) < RadiusSqr))
            {
                plr.Client.SendPackets(batch);
            }
        }

        private void AEShurikenAbility(Item item, Position target, ActivateEffect eff)
        {
            if (HasConditionEffect(ConditionEffects.Quiet) || MP < 3) return;

            Owner.BroadcastPacket(new ShowEffect
            {
                EffectType = EffectType.AreaBlast,
                TargetObjectId = Id,
                Color = (eff.Color == 0 ? new ARGB(0x484848) : new ARGB(eff.Color)),
                Pos1 = new Position { X = 0.8f }
            }, null);

            if (!abilityOngoing)
            {
                ApplyConditionEffect(ConditionEffectIndex.NinjaSpeedy, -1);
                LastAltAttack = 0;
                return;
            }

            AEShoot(item, target, eff);
        }

        private void AESamuraiAbility(Item item, Position target, ActivateEffect eff)
        {
            if (HasConditionEffect(ConditionEffects.Quiet) || MP < 3) return;

            if (!abilityOngoing)
            {
                ApplyConditionEffect(ConditionEffectIndex.SamuraiBerserk, -1);
                LastAltAttack = 0;
                return;
            }

            if (MathsUtils.DistSqr(target.X, target.Y, X, Y) > MaxAbilityDist * MaxAbilityDist) return;
            var atkdamage = eff.TotalDamage;
            if (eff.UseAtkMod)
            {
                atkdamage = UseAtkMod(eff.TotalDamage);
            }

            double dir = Math.Atan2(target.Y - Y, target.X - X);
            Position pos = new Position() { X = ((float)Math.Cos(dir) * (eff.Range / 2)) + X, Y = ((float)Math.Sin(dir) * (eff.Range / 2)) + Y };

            var pkt = new ShowEffect
            {
                EffectType = EffectType.Diffuse,
                TargetObjectId = Id,
                Color = (eff.Color == 0 ? new ARGB(0xffddff00) : new ARGB(eff.Color)),
                Pos1 = pos,
                Pos2 = new Position { X = pos.X + eff.Range, Y = pos.Y }
            };

            Owner.AOE(pos, eff.Range, false, entity =>
            {
                ((Enemy)entity).Damage(this, atkdamage, true);
            });
            BroadcastSync(pkt, p => this.Dist(p) < 25);
        }

        private void AEAstonAbility(Item item, Position target, ActivateEffect eff)
        {
            var atkdamage = eff.TotalDamage;
            if (eff.UseAtkMod)
            {
                atkdamage = UseAtkMod(eff.TotalDamage);
            }
            {
                if (MathsUtils.DistSqr(target.X, target.Y, X, Y) > MaxAbilityDist * MaxAbilityDist) return;
                AEShoot(item, target, eff);
            }
        }

        private void AEBanner(Item item, Position target, ActivateEffect eff)
        {
            if (MathsUtils.DistSqr(target.X, target.Y, X, Y) > MaxAbilityDist * MaxAbilityDist) return;
            BroadcastSync(new ShowEffect
            {
                EffectType = EffectType.Throw,
                Color = (eff.Color == 0 ? new ARGB(0x0000FF) : new ARGB(eff.Color)),
                TargetObjectId = Id,
                Pos1 = target
            }, p => this.Dist(p) < 25);
            Owner.Timers.Add(new WorldTimer(1500, (w) =>
            {
                if (w == null || w.Deleted || this == null) return;

                var banner = new Banner(this, eff.Range, eff.Amount, eff.DurationMS, (eff.Color == 0 ? 0x0000FF : eff.Color));
                banner.Move(target.X, target.Y);
                w.EnterWorld(banner);
            }));
        }

        private void AETorii(Item item, Position target, ActivateEffect eff)
        {
            if (MathsUtils.DistSqr(target.X, target.Y, X, Y) > MaxAbilityDist * MaxAbilityDist) return;
            var torii = new Torii(this,
                eff.Range,
                eff.Amount,
                eff.Players,
                eff.DurationMS,
                eff.ConditionEffect ?? ConditionEffectIndex.Slowed,
                eff.Color,
                eff.ObjType);
            torii.Move(target.X, target.Y);
            Owner.EnterWorld(torii);

            var fakeTorii = Resolve(Manager, eff.ObjType);
            fakeTorii.Move(target.X, target.Y);
            Owner.EnterWorld(fakeTorii);

            AddSupportScore(500 + eff.DurationMS / 1000 * 20, false);
            Owner.Timers.Add(new WorldTimer(eff.Amount * 1000, (w) =>
            {
                if (w == null || w.Deleted || fakeTorii == null) return;

                w.LeaveWorld(fakeTorii);
            }));
        }

        private void AESiphonAbility(Item item, Position target, ActivateEffect eff)
        {
            if (MathsUtils.DistSqr(target.X, target.Y, X, Y) > MaxAbilityDist * MaxAbilityDist) return;
            var drained = DrainedHP;
            var mpAvailable = MP;
            var baseDamage = eff.Amount;
            int totalAttack = Stats[2];
            int totalWis = Stats[7];
            var prjs = new Projectile[12];
            var prjDesc = item.Projectiles[0]; //Assume only one
            var batch = new Packet[13];
            if (!abilityOngoing)
            {
                ApplyConditionEffect(ConditionEffectIndex.DrakzixCharging);
                DrakzixHPDrain = (eff.Amount2 == 0 ? 50 : eff.Amount2);
                LastAltAttack = 0;
                return;
            }

            var pkts = new List<Packet>
            {
                new ShowEffect
                {
                    EffectType = EffectType.Flow,
                    TargetObjectId = Id,
                    Pos1 = target,
                    Color = (eff.Color == 0 ? new ARGB(0xFFA500) : new ARGB(eff.Color))
                },
                new ShowEffect
                {
                    EffectType = EffectType.Diffuse,
                    TargetObjectId = Id,
                    Color = (eff.Color == 0 ? new ARGB(0xFFA500) : new ARGB(eff.Color)),
                    Pos1 = target,
                    Pos2 = new Position {X = target.X + eff.Range, Y = target.Y}
                }
            };



            //int damage = (int)(mpAvailable * 1.8 * (drained / 2.0) / 4.0 + eff.Amount);
            int damage = ((drained) / 7) * (baseDamage);
            int atkMinDamage = ((drained) / 7) * (prjDesc.MinDamage);
            int atkMaxDamage = ((drained) / 7) * (prjDesc.MaxDamage);
            // MP * 2 * (drained / 2) / 4

                if (totalAttack > totalWis)
                {
                for (var i = 0; i < 12; i++)
                {
                    var proj = CreateProjectile(prjDesc, item.ObjectType,
                        Random.Next(UseAtkMod(atkMinDamage), UseAtkMod(atkMaxDamage)),
                        Manager.Core.getTotalTickCount(), target, (float)(i * (Math.PI * 2) / 12));
                    Owner.EnterWorld(proj);
                    FameCounter.Shoot(proj);
                    batch[i] = new ServerPlayerShoot()
                    {
                        BulletId = proj.ProjectileId,
                        OwnerId = Id,
                        ContainerType = item.ObjectType,
                        StartingPos = target,
                        Angle = proj.Angle,
                        Damage = (short)proj.Damage
                    };
                    prjs[i] = proj;
                }
                }
                else
                {
                    damage = ((drained) / 7) * (baseDamage * (Stats[7] / 140));

                    Owner.AOE(target, eff.Range, false, enemy =>
                    {
                        ((Enemy)enemy).Damage(this, damage, true);
                    });

                    MP = 20;
                    DrainedHP = 0;

                    ApplyConditionEffect(ConditionEffectIndex.DrakzixCharging, 0);
                    ApplyConditionEffect(ConditionEffectIndex.Empowered, eff.DurationMS);
                }
            

            

            
        }

        private void AEDreadcullAbility(Item item, Position target, ActivateEffect eff)
        {
            if (MathsUtils.DistSqr(target.X, target.Y, X, Y) > MaxAbilityDist * MaxAbilityDist) return;

            Entity en = Resolve(Owner.Manager, 0x5bf1);
            en.Move(target.X, target.Y);
            Owner.EnterWorld(en);
            en.SetPlayerOwner(this);
            en.ApplyConditionEffect(ConditionEffectIndex.Invincible);

            AddSupportScore(500 + eff.DurationMS / 1000 * 20, false);
            /*spawn 2nd skeleton */
            double WisMod = Math.Round(((Stats[7] - 85) * 0.055) + eff.Amount);
            int WisMod2 = Convert.ToInt32(WisMod) * 1000;
                Owner.Timers.Add(new WorldTimer(6000, (w) =>
                {
                    w.LeaveWorld(en);
                }));
        }

        private void AETalismanAbility(Item item, Position target, ActivateEffect eff)
        {
            if (Owner.Name == "Nexus")
                return;
            var en = Resolve(Owner.Manager, eff.ObjType);
            en.Move(X, Y);
            Owner.EnterWorld(en);
            en.ApplyConditionEffect(ConditionEffectIndex.Invincible);
            en.SetPlayerOwner(this);
            Owner.Timers.Add(new WorldTimer(8000, (w) =>
            {

                w.LeaveWorld(en);
            }));
        }

        private void AEDye(Item item, Position target, ActivateEffect eff)
        {
            if (item.Texture1 != 0) Texture1 = item.Texture1;
            if (item.Texture2 != 0) Texture2 = item.Texture2;
        }

        private void AESorForge(Item item, Position target, ActivateEffect eff)
        {
            Client.SendPacket(new SorForge
            {
                IsForge = true
            });
        }

        private void AESorConstruct(Item item, Position target, ActivateEffect eff)
        {
            if (Owner is Test || Client.Account == null)
                return;

            var acc = Client.Account;
            Client.Manager.Database.UpdateSorStorage(acc, 10);
            SorStorage += 10;
            this.ForceUpdate(SorStorage);

            SendInfo("You redeemed your old sor fragment!");
        }

        private void AESacredActivate(Item item, Position target, ActivateEffect eff)
        {
            if (Owner is Test || Client.Account == null)
                return;

            if (UseConsumable(item.ObjectId))
            {
                var acc = Client.Account;
                Client.Manager.Database.UpdateSorStorage(acc, eff.Amount);
                SorStorage += eff.Amount;
                this.ForceUpdate(SorStorage);

                SendInfo("You have gained " + eff.Amount + " Sacred fragments! You currently have " + SorStorage + " Sacred fragments in storage.");
            }
        }

        private void AEAbbyConstruct(Item item, Position target, ActivateEffect eff)
        {
            var inv = Inventory;
            for (var i = 0; i < inv.Length; i++)
            {
                if (inv[i] == null) continue;
                if (inv[i].ObjectId == "Whip")
                {
                    inv[i] = Manager.Resources.GameData.Items[0x61d8];
                    SaveToCharacter();
                    SendInfo("You place the Abyssal Rune on the Whip's handle.");
                    break;
                }

                SendError("You do not have a Whip in your inventory.");
            }
        }

        public bool UseConsumable(String name)
        {
            for (var i = 0; i < Inventory.Length; i++)
            {
                if (Inventory[i] == null) continue;
                if (Inventory[i].ObjectId.Equals(name))
                {
                    Inventory[i] = null;
                    SaveToCharacter();
                    return true;
                }
            }
            return false;
        }

        private void AETreasureActivate(Item item, Position target, ActivateEffect eff)
        {
            if (Owner is Test || Client.Account == null)
                return;
            if (UseConsumable(item.ObjectId))
            {
                var acc = Client.Account;
                Client.Manager.Database.UpdateCredit(acc, eff.Amount);
                Credits += eff.Amount;
                this.ForceUpdate(Credits);
            }
        }

        private void AEFameActivate(Item item, Position target, ActivateEffect eff)
        {
            if (Owner is Test || Client.Account == null)
                return;
                var acc = Client.Account;
                Client.Manager.Database.UpdateFame(acc, eff.Amount);
                acc.Fame += eff.Amount;
                this.ForceUpdate(Fame);
        }

        private void AEOnraneActivate(Item item, Position target, ActivateEffect eff)
        {
            if (Owner is Test || Client.Account == null)
                return;

            if (UseConsumable(item.ObjectId))
            {
                var acc = Client.Account;
                Client.Manager.Database.UpdateOnrane(acc, eff.Amount);
                Onrane += eff.Amount;
                this.ForceUpdate(Onrane);
            }
        }

        private void AEActivateFragment(Item item, Position target, ActivateEffect eff)
        {
            if (Owner is Test || Client.Account == null)
                return;

            if (UseConsumable(item.ObjectId))
            {
                var acc = Client.Account;
                var rnd = new Random();
                var amount = 0;
                var Chance = Random.Next(0, 6);
                switch (Chance)
                {
                    case 0:
                        amount = 5;
                        SendInfo("You received " + amount + " Sacred Fragments.");
                        break;

                    case 1:
                        amount = 5;
                        SendInfo("You received " + amount + " Sacred Fragments.");
                        break;

                    case 2:
                        amount = 5;
                        SendInfo("You received " + amount + " Sacred Fragments.");
                        break;

                    case 3:
                        amount = 10;
                        SendInfo("You received " + amount + " Sacred Fragments.");
                        break;

                    case 4:
                        amount = 10;
                        SendInfo("You received " + amount + " Sacred Fragments.");
                        break;

                    case 5:
                        amount = 15;
                        SendInfo("You received " + amount + " Sacred Fragments.");
                        break;
                }
                Client.Manager.Database.UpdateSorStorage(acc, amount);
                SorStorage += amount;
                this.ForceUpdate(SorStorage);
                SendInfo("You currently have " + SorStorage + " Sacred fragments in storage.");
            }
        }

        private void AERandomOnrane(Item item, Position target, ActivateEffect eff)
        {
            if (Owner is Test || Client.Account == null)
                return;
            if (UseConsumable(item.ObjectId))
            {
                var acc = Client.Account;
                var OnraneChance = Random.Next(0, 3);
                ushort type = Rewards[Random.Next(0, Rewards.Length - 1)];
                var items = Manager.Resources.GameData.Items[type];
                switch (OnraneChance)
                {
                    case 0:

                        Manager.Database.AddGift(acc, type);
                        SendInfo("You have been rewarded with: " + items.DisplayName);
                        break;
                    
                    case 1:

                        Manager.Database.AddGift(acc, type);
                        SendInfo("You have been rewarded with: " + items.DisplayName);
                        break;
                    
                    case 2:

                        Manager.Database.AddGift(acc, type);
                        SendInfo("You have been rewarded with: " + items.DisplayName);
                        break;
                    
                    case 3:

                        Manager.Database.AddGift(acc, type);
                        SendInfo("You have been rewarded with: " + items.DisplayName);
                        break;

                }
            }
        }

        private void AEInsigniaActivate(Item item, Position target, ActivateEffect eff)
        {
            if (Owner.Name != "Nexus")
            {
                SendError("You can only use this item in the Nexus.");
                return;
            }
            if (Manager._isChallengeLaunched == true)
            {
                SendError("A spaceship just launched wtf.");
                return;
            }

            if (Manager._isChallengeLaunched == false)
            {
                //announce
                var packet = new Text
                {
                    BubbleTime = 0,
                    NumStars = -1,
                    TextColor = 0x0000FF,
                    Name = "MOON GUY",
                    Txt = "GET IN DA SPACESHIP BITCH"
                };
                Owner.BroadcastPacket(packet, null);
                //open
                var gameData = Manager.Resources.GameData;
                Manager._isChallengeLaunched = true;
                if (!gameData.IdToObjectType.TryGetValue("SPACESHIP", out ushort objType) ||
                    !gameData.Portals.ContainsKey(objType))
                    return;
                var entity = Resolve(Manager, objType);

                (entity as Portal).PlayerOpened = true;
                (entity as Portal).Opener = Name;

                entity.Move(41f, 54f);
                Owner.EnterWorld(entity);
                var timeoutTime = gameData.Portals[objType].Timeout;
                Owner.Timers.Add(new WorldTimer(timeoutTime * 1000, (w) =>
                {
                    if (w == null || w.Deleted || entity == null) return;

                    w.LeaveWorld(entity);
                }));
                Owner.Timers.Add(new WorldTimer(30000, (w) =>
                {
                    if (w == null || w.Deleted) return;

                    w.Manager._isChallengeLaunched = false;
                }));
                Owner.ChallengeCount = 0;
                Manager.Chat.Announce("THE SPACESHIP HAS LANDED IN IN " + Manager.Config.serverInfo.name + "!");
                GlobalNotification space = new GlobalNotification()
                {
                    Text = "spaceship"
                };
                entity.Manager.GlobalBroadcast(space);
            }
            }
        
        private void AEReaperUse(Item item, Position target, ActivateEffect eff)
        {
            if (Owner.Name != "Nexus")
            {
                SendError("You can only use this item in the Nexus.");
                return;
            }
            if (Manager._isSharkLaunched == true)
            {
                SendError("DEEP SEA ALREADY OPEN");
                return;
            }

            if (Manager._isSharkLaunched == false)
            {
                //announce
                var packet = new Text
                {
                    BubbleTime = 0,
                    NumStars = -1,
                    TextColor = 0x0000FF,
                    Name = "WATER GUY",
                    Txt = "GET IN THE SEA BITCH"
                };
                Owner.BroadcastPacket(packet, null);
                //open
                var gameData = Manager.Resources.GameData;
                Manager._isSharkLaunched = true;
                if (!gameData.IdToObjectType.TryGetValue("Deep Sea", out ushort objType) ||
                    !gameData.Portals.ContainsKey(objType))
                    return;
                var entity = Resolve(Manager, objType);

                (entity as Portal).PlayerOpened = true;
                (entity as Portal).Opener = Name;

                entity.Move(44f, 54f);
                Owner.EnterWorld(entity);
                var timeoutTime = gameData.Portals[objType].Timeout;
                Owner.Timers.Add(new WorldTimer(timeoutTime * 1000, (w) =>
                {
                    if (w == null || w.Deleted || entity == null) return;

                    w.LeaveWorld(entity);
                }));
                Owner.Timers.Add(new WorldTimer(30000, (w) =>
                {
                    if (w == null || w.Deleted) return;

                    w.Manager._isSharkLaunched = false;
                }));
                Manager.Chat.Announce("THE DEEP SEA HAS OPENED UP IN " + Manager.Config.serverInfo.name + "!");
                GlobalNotification deepsea = new GlobalNotification()
                {
                    Text = "deepsea"
                };
                entity.Manager.GlobalBroadcast(deepsea);
                }
            }
        
        private void AEAsgardUse(Item item, Position target, ActivateEffect eff)
        {
            if (Owner.Name != "Nexus")
            {
                SendError("You can only use this item in the Nexus.");
                return;
            }
            if (Manager._isSharkLaunched == true || Manager._isDreamLaunched == true)
            {
                SendError("ASGARD/SHARK/NECROP ALREADY OPEN");
                return;
            }

            if (Manager._isSharkLaunched == false || Manager._isDreamLaunched == false)
            {
                //announce
                var packet = new Text
                {
                    BubbleTime = 0,
                    NumStars = -1,
                    TextColor = 0x0000FF,
                    Name = "ODIN",
                    Txt = "JOIN MY GROON SESH FOOLS"
                };
                Owner.BroadcastPacket(packet, null);
                //open
                var gameData = Manager.Resources.GameData;
                Manager._isAsgardLaunched = true;
                if (!gameData.IdToObjectType.TryGetValue("Asgard", out ushort objType) ||
                    !gameData.Portals.ContainsKey(objType))
                    return;
                var entity = Resolve(Manager, objType);

                (entity as Portal).PlayerOpened = true;
                (entity as Portal).Opener = Name;

                entity.Move(44f, 54f);
                Owner.EnterWorld(entity);
                var timeoutTime = gameData.Portals[objType].Timeout;
                Owner.Timers.Add(new WorldTimer(timeoutTime * 1000, (w) =>
                {
                    if (w == null || w.Deleted || entity == null) return;

                    w.LeaveWorld(entity);
                }));
                Owner.Timers.Add(new WorldTimer(30000, (w) =>
                {
                    if (w == null || w.Deleted) return;

                    w.Manager._isSharkLaunched = false;
                }));
                Manager.Chat.Announce("THE ASGARD HAS OPENED UP IN " + Manager.Config.serverInfo.name + "!");
                GlobalNotification deepsea = new GlobalNotification()
                {
                    Text = "asgard"
                };
                entity.Manager.GlobalBroadcast(deepsea);
                }
            }
        private void AEFAVELASUse(Item item, Position target, ActivateEffect eff)
        {
            if (Owner.Name != "Nexus")
            {
                SendError("You can only use this item in the Nexus.");
                return;
            }
            if (Manager._isSharkLaunched == true || Manager._isDreamLaunched == true)
            {
                SendError("ASGARD/SHARK/NECROP ALREADY OPEN");
                return;
            }

            if (Manager._isSharkLaunched == false || Manager._isDreamLaunched == false)
            {
                //announce
                var packet = new Text
                {
                    BubbleTime = 0,
                    NumStars = -1,
                    TextColor = 0x0000FF,
                    Name = "chris the redeemed",
                    Txt = ":>"
                };
                Owner.BroadcastPacket(packet, null);
                //open
                var gameData = Manager.Resources.GameData;
                Manager._isSharkLaunched = true;
                if (!gameData.IdToObjectType.TryGetValue("THE FAVELAS", out ushort objType) ||
                    !gameData.Portals.ContainsKey(objType))
                    return;
                var entity = Resolve(Manager, objType);

                (entity as Portal).PlayerOpened = true;
                (entity as Portal).Opener = Name;

                entity.Move(44f, 54f);
                Owner.EnterWorld(entity);
                var timeoutTime = gameData.Portals[objType].Timeout;
                Owner.Timers.Add(new WorldTimer(timeoutTime * 1000, (w) =>
                {
                    if (w == null || w.Deleted || entity == null) return;

                    w.LeaveWorld(entity);
                }));
                Owner.Timers.Add(new WorldTimer(30000, (w) =>
                {
                    if (w == null || w.Deleted) return;

                    w.Manager._isSharkLaunched = false;
                }));
                Manager.Chat.Announce("THE FAVELAS HAVE OPENED UP IN " + Manager.Config.serverInfo.name + "!");
                GlobalNotification deepsea = new GlobalNotification()
                {
                    Text = "FAVELAS"
                };
                entity.Manager.GlobalBroadcast(deepsea);
            }
        }
        private void AEFallenUse(Item item, Position target, ActivateEffect eff)
        {
            if (Owner.Name != "Nexus")
            {
                SendError("You can only use this item in the Nexus.");
                return;
            }
            if (Manager._isSharkLaunched == true || Manager._isDreamLaunched == true)
            {
                SendError("RAID ALREADY OPEN");
                return;
            }

            if (Manager._isSharkLaunched == false || Manager._isDreamLaunched == false)
            {
                //announce
                var packet = new Text
                {
                    BubbleTime = 0,
                    NumStars = -1,
                    TextColor = 0xffffff,
                    Name = "femboy (polish) arcanuo",
                    Txt = "arcanuo kill"
                };
                Owner.BroadcastPacket(packet, null);
                //open
                var gameData = Manager.Resources.GameData;
                Manager._isSharkLaunched = true;
                if (!gameData.IdToObjectType.TryGetValue("THE FALLEN RAID", out ushort objType) ||
                    !gameData.Portals.ContainsKey(objType))
                    return;
                var entity = Resolve(Manager, objType);

                (entity as Portal).PlayerOpened = true;
                (entity as Portal).Opener = Name;

                entity.Move(44f, 54f);
                Owner.EnterWorld(entity);
                var timeoutTime = gameData.Portals[objType].Timeout;
                Owner.Timers.Add(new WorldTimer(timeoutTime * 1000, (w) =>
                {
                    if (w == null || w.Deleted || entity == null) return;

                    w.LeaveWorld(entity);
                }));
                Owner.Timers.Add(new WorldTimer(30000, (w) =>
                {
                    if (w == null || w.Deleted) return;

                    w.Manager._isSharkLaunched = false;
                }));
                Manager.Chat.Announce("THE FALLEN RAID HAS OPENED UP IN " + Manager.Config.serverInfo.name + "!");
                GlobalNotification deepsea = new GlobalNotification()
                {
                    Text = "THE FALLEN RAID"
                };
                entity.Manager.GlobalBroadcast(deepsea);
            }
        }
        private void AECatUse(Item item, Position target, ActivateEffect eff)
        {
            if (Owner.Name != "Nexus")
            {
                SendError("You can only use this item in the Nexus.");
                return;
            }
            if (Manager._isSharkLaunched == true || Manager._isDreamLaunched == true)
            {
                SendError("RAID ALREADY OPEN");
                return;
            }

            if (Manager._isSharkLaunched == false || Manager._isDreamLaunched == false)
            {
                //announce
                var packet = new Text
                {
                    BubbleTime = 0,
                    NumStars = -1,
                    TextColor = 0xffffff,
                    Name = "MANIC MOHAWK CAT",
                    Txt = "im going to penetrate ytou"
                };
                Owner.BroadcastPacket(packet, null);
                //open
                var gameData = Manager.Resources.GameData;
                Manager._isSharkLaunched = true;
                if (!gameData.IdToObjectType.TryGetValue("MANIC CAT RAID", out ushort objType) ||
                    !gameData.Portals.ContainsKey(objType))
                    return;
                var entity = Resolve(Manager, objType);

                (entity as Portal).PlayerOpened = true;
                (entity as Portal).Opener = Name;

                entity.Move(44f, 54f);
                Owner.EnterWorld(entity);
                var timeoutTime = gameData.Portals[objType].Timeout;
                Owner.Timers.Add(new WorldTimer(timeoutTime * 1000, (w) =>
                {
                    if (w == null || w.Deleted || entity == null) return;

                    w.LeaveWorld(entity);
                }));
                Owner.Timers.Add(new WorldTimer(30000, (w) =>
                {
                    if (w == null || w.Deleted) return;

                    w.Manager._isSharkLaunched = false;
                }));
                Manager.Chat.Announce("THE MANIC CAT RAID HAS OPENED UP IN " + Manager.Config.serverInfo.name + "!");
                GlobalNotification deepsea = new GlobalNotification()
                {
                    Text = "MANIC CAT RAID"
                };
                entity.Manager.GlobalBroadcast(deepsea);
            }
        }
        private void AEEndUse(Item item, Position target, ActivateEffect eff)
        {
            if (Owner.Name != "Nexus")
            {
                SendError("You can only use this item in the Nexus.");
                return;
            }
            if (Manager._isSharkLaunched == true || Manager._isDreamLaunched == true)
            {
                SendError("RAID ALREADY OPEN");
                return;
            }

            if (Manager._isSharkLaunched == false || Manager._isDreamLaunched == false)
            {
                //announce
                var packet = new Text
                {
                    BubbleTime = 0,
                    NumStars = -1,
                    TextColor = 0xffffff,
                    Name = "stev",
                    Txt = "get in the fuckin end"
                };
                Owner.BroadcastPacket(packet, null);
                //open
                var gameData = Manager.Resources.GameData;
                Manager._isSharkLaunched = true;
                if (!gameData.IdToObjectType.TryGetValue("The End", out ushort objType) ||
                    !gameData.Portals.ContainsKey(objType))
                    return;
                var entity = Resolve(Manager, objType);

                (entity as Portal).PlayerOpened = true;
                (entity as Portal).Opener = Name;

                entity.Move(44f, 54f);
                Owner.EnterWorld(entity);
                var timeoutTime = gameData.Portals[objType].Timeout;
                Owner.Timers.Add(new WorldTimer(timeoutTime * 1000, (w) =>
                {
                    if (w == null || w.Deleted || entity == null) return;

                    w.LeaveWorld(entity);
                }));
                Owner.Timers.Add(new WorldTimer(30000, (w) =>
                {
                    if (w == null || w.Deleted) return;

                    w.Manager._isSharkLaunched = false;
                }));
                Manager.Chat.Announce("THE END HAS OPENED UP IN " + Manager.Config.serverInfo.name + "!");
                GlobalNotification deepsea = new GlobalNotification()
                {
                    Text = "The End"
                };
                entity.Manager.GlobalBroadcast(deepsea);
            }
        }

        private void AEDreamEssenceActivate(Item item, Position target, ActivateEffect eff)
        {
            if (Owner.Name != "Nexus")
            {
                SendError("You can only use this item in the nexus dumbfuck.");
                return;
            }

            if (Manager._isDreamLaunched == true)
            {
                SendError("NECROP has already been opened!");
                return;
            }

            Owner.DreamCount += 1;

            if (Owner.DreamCount == 1)
            {
                //announce
                var packet = new Text
                {
                    BubbleTime = 0,
                    NumStars = -1,
                    TextColor = 0x00C7FF,
                    Name = "novus",
                    Txt = "ayo the pizza here NECROP NEXUS"
                };
                Owner.BroadcastPacket(packet, null);
                //open
                var gameData = Manager.Resources.GameData;
                Manager._isDreamLaunched = true;
                if (!gameData.IdToObjectType.TryGetValue("Roft Neccopolis Portal", out ushort objType) ||
                    !gameData.Portals.ContainsKey(objType))
                    return;
                var entity = Resolve(Manager, objType);

                (entity as Portal).PlayerOpened = true;
                (entity as Portal).Opener = Name;

                entity.Move(44f, 54f);
                Owner.EnterWorld(entity);
                var timeoutTime = gameData.Portals[objType].Timeout;
                Owner.Timers.Add(new WorldTimer(timeoutTime * 1000, (w) =>
                {
                    if (w == null || w.Deleted || entity == null) return;

                    w.LeaveWorld(entity);
                }));
                Owner.Timers.Add(new WorldTimer(30000, (w) =>
                {
                    if (w == null || w.Deleted) return;

                    w.Manager._isDreamLaunched = false;
                }));
                Owner.DreamCount = 0;
                Manager.Chat.Announce("NECROP OPENED in " + Manager.Config.serverInfo.name + "!");
            }
        }

        private void AERandomGold(Item item, Position target, ActivateEffect eff)
        {
            if (Owner is Test || Client.Account == null)
                return;
            if (UseConsumable(item.ObjectId))
            {
                var acc = Client.Account;
                var GoldChance = Random.Next(0, 3);
                switch (GoldChance)
                {
                    case 0:
                        Client.Manager.Database.UpdateCredit(acc, 250);
                        Credits += 250;
                        this.ForceUpdate(Credits);
                        SendInfo("You have obtained 250 Gold.");
                        break;

                    case 1:
                        Client.Manager.Database.UpdateCredit(acc, 500);
                        Credits += 500;
                        this.ForceUpdate(Credits);
                        SendInfo("You have obtained 500 Gold.");
                        break;

                    case 2:
                        Client.Manager.Database.UpdateCredit(acc, 750);
                        Credits += 750;
                        this.ForceUpdate(Credits);
                        SendInfo("You have obtained 750 Gold.");
                        break;
                }
            }
        }

        private void AEURandomOnrane(Item item, Position target, ActivateEffect eff)
        {
            if (Owner is Test || Client.Account == null)
                return;
            if (UseConsumable(item.ObjectId))
            {
                var acc = Client.Account;
                var OnraneChance = Random.Next(0, 5);
                switch (OnraneChance)
                {
                    case 0:
                        Client.Manager.Database.UpdateOnrane(acc, 12);
                        Onrane += 12;
                        this.ForceUpdate(Onrane);
                        SendInfo("You have obtained 12 Onrane.");
                        break;

                    case 1:
                        Client.Manager.Database.UpdateOnrane(acc, 14);
                        Onrane += 14;
                        this.ForceUpdate(Onrane);
                        SendInfo("You have obtained 14 Onrane.");
                        break;

                    case 2:
                        Client.Manager.Database.UpdateOnrane(acc, 16);
                        Onrane += 16;
                        this.ForceUpdate(Onrane);
                        SendInfo("You have obtained 16 Onrane.");
                        break;

                    case 3:
                        Client.Manager.Database.UpdateOnrane(acc, 18);
                        Onrane += 18;
                        this.ForceUpdate(Onrane);
                        SendInfo("You have obtained 18 Onrane.");
                        break;

                    case 4:
                        Client.Manager.Database.UpdateOnrane(acc, 20);
                        Onrane += 20;
                        this.ForceUpdate(Onrane);
                        SendInfo("You have obtained 20 Onrane.");
                        break;
                }
            }
        }

        private void AECreate(Item item, Position target, ActivateEffect eff)
        {
            if (Owner.Name == "DeathArena")
            {
                SendError("Can't use keys here.");
            }
            var gameData = Manager.Resources.GameData;

            if (!gameData.IdToObjectType.TryGetValue(eff.Id, out ushort objType) ||
                !gameData.Portals.ContainsKey(objType))
                return; // object not found, ignore

            var entity = Resolve(Manager, objType);
            var timeoutTime = gameData.Portals[objType].Timeout;

            entity.Move(X, Y);
            Owner.EnterWorld(entity);

            (entity as Portal).PlayerOpened = true;
            (entity as Portal).Opener = Name;

            Owner.Timers.Add(new WorldTimer(timeoutTime * 1000, (w) =>
            {
                if (w == null || w.Deleted || entity == null) return;

                w.LeaveWorld(entity);
            }));

            Owner.BroadcastPacket(new Notification
            {
                Color = new ARGB(0xFF00FF00),
                ObjectId = Id,
                Message = "Opened by " + Name
            }, null);
            foreach (var player in Owner.Players.Values)
                player.SendInfo("{\"key\":\"{server.dungeon_opened_by}\",\"tokens\":{\"dungeon\":\"" + gameData.Portals[objType].DungeonName + "\",\"name\":\"" + Name + "\"}}");
        }

        private void AEIncrementStat(Item item, Position target, ActivateEffect eff)
        {
            var playerDesc = Manager.Resources.GameData.Classes[ObjectType];
            var idx = StatsManager.GetStatIndex((StatsType)eff.Stats);
            var maxed = playerDesc.Stats.Where((t, i) => Stats.Base[i] >= t.MaxValue).Count();
            var statInfo = Manager.Resources.GameData.Classes[ObjectType].Stats;
            if (maxed >= 8)
            {
                SendErrorFormat("MAXED ALREADY");
                return;
            }
            Stats.Base[idx] += eff.Amount;
            if (Stats.Base[idx] > statInfo[idx].MaxValue)
                Stats.Base[idx] = statInfo[idx].MaxValue;
        }

        private void AEPowerStat(Item item, Position target, ActivateEffect eff)
        {
            if (UseConsumable(item.ObjectId))
            {
                if (AscensionEnabled)
                {
                    var idx = StatsManager.GetStatIndex((StatsType)eff.Stats);
                    var statInfo = Manager.Resources.GameData.Classes[ObjectType].Stats;

                    Stats.Base[idx] += eff.Amount;
                    if (Stats.Base[idx] > statInfo[idx].MaxValue + (idx < 2 ? 50 : 10))
                        Stats.Base[idx] = statInfo[idx].MaxValue + (idx < 2 ? 50 : 10);
                }
                else SendInfo("A character that isn't ascended can't use vials.");
            }
        }

        private void AEFixedStat(Item item, Position target, ActivateEffect eff)
        {
            var idx = StatsManager.GetStatIndex((StatsType)eff.Stats);
            Stats.Base[idx] = eff.Amount;
        }

        private void AERemoveNegativeConditionSelf(Item item, Position target, ActivateEffect eff)
        {
            ApplyConditionEffect(NegativeEffs);
            BroadcastSync(new ShowEffect
            {
                EffectType = EffectType.AreaBlast,
                TargetObjectId = Id,
                Color = (eff.Color == 0 ? new ARGB(0xffffffff) : new ARGB(eff.Color)),
                Pos1 = new Position { X = 1 }
            }, p => this.DistSqr(p) < RadiusSqr);
        }

        private void AERemoveNegativeConditions(Item item, Position target, ActivateEffect eff)
        {
            this.AOE(eff.Range, true, player => player.ApplyConditionEffect(NegativeEffs));
            BroadcastSync(new ShowEffect
            {
                EffectType = EffectType.AreaBlast,
                TargetObjectId = Id,
                Color = (eff.Color == 0 ? new ARGB(0xffffffff) : new ARGB(eff.Color)),
                Pos1 = new Position { X = eff.Range }
            }, p => this.DistSqr(p) < RadiusSqr);
        }

        private void AEPoisonGrenade(Item item, Position target, ActivateEffect eff)
        {
            if (MathsUtils.DistSqr(target.X, target.Y, X, Y) > MaxAbilityDist * MaxAbilityDist) return;

            int impDamage = eff.UseWisMod ? UseWisMod(eff.ImpactDamage) : eff.ImpactDamage;

            BroadcastSync(new ShowEffect
            {
                EffectType = EffectType.Throw,
                Color = (eff.Color == 0 ? new ARGB(0xffddff00) : new ARGB(eff.Color)),
                TargetObjectId = Id,
                Pos1 = target,
                Duration = eff.ThrowTime / 1000
            }, p => this.DistSqr(p) < RadiusSqr);

            var x = new Placeholder(Manager, eff.ThrowTime + 1000);
            x.Move(target.X, target.Y);
            Owner.EnterWorld(x);
            Owner.Timers.Add(new WorldTimer(eff.ThrowTime, (w) =>
            {
                if (w == null || w.Deleted) return;

                w.BroadcastPacketNearby(new ShowEffect
                {
                    EffectType = EffectType.AreaBlast,
                    Color = eff.Color == 0 ? new ARGB(0xffddff00) : new ARGB(eff.Color),
                    TargetObjectId = x.Id,
                    Pos1 = new Position { X = eff.Radius }
                }, x, null);
                w.AOE(target, eff.Radius, false, entity =>
                {
                    PoisonEnemy(w, (Enemy)entity, eff);
                    ((Enemy)entity).Damage(this, impDamage, true);
                });
            }));
        }

        private void DamageGrenade(Position target)
        {
            if (MathsUtils.DistSqr(target.X, target.Y, X, Y) > MaxAbilityDist * MaxAbilityDist) return;
            BroadcastSync(new ShowEffect
            {
                EffectType = EffectType.Throw,
                Color = new ARGB(0xf26e2c),
                TargetObjectId = Id,
                Pos1 = target
            }, p => this.DistSqr(p) < RadiusSqr);

            var x = new Placeholder(Manager, 1500);
            x.Move(target.X, target.Y);
            Owner.EnterWorld(x);
            Owner.Timers.Add(new WorldTimer(1500, (w) =>
            {
                if (w == null || w.Deleted) return;

                w.BroadcastPacketNearby(new ShowEffect
                {
                    EffectType = EffectType.AreaBlast,
                    Color = new ARGB(0xf26e2c),
                    TargetObjectId = x.Id,
                    Pos1 = new Position { X = 3 }
                }, x, null);
                w.AOE(target, 3, false, enemy => DamageEnemy(w, enemy as Enemy));
            }));
        }

        private void AELightning(Item item, Position target, ActivateEffect eff)
        {
            const double coneRange = Math.PI / 4;
            var mouseAngle = Math.Atan2(target.Y - Y, target.X - X);

            int totalDamage = eff.UseWisMod ? UseWisMod(eff.TotalDamage) : eff.TotalDamage;
            int maxTargets = eff.UseWisMod2 ? UseWisMod(eff.MaxTargets) : eff.MaxTargets;

            // get starting target
            var startTarget = this.GetNearestEntity(MaxAbilityDist, false, e => e is Enemy &&
                Math.Abs(mouseAngle - Math.Atan2(e.Y - Y, e.X - X)) <= coneRange &&
                !(e.HasConditionEffect(ConditionEffects.Invincible) ||
                e.HasConditionEffect(ConditionEffects.Stasis))
                );

            // no targets? bolt air animation
            if (startTarget == null)
            {
                var noTargets = new Packet[3];
                var angles = new[] { mouseAngle, mouseAngle - coneRange, mouseAngle + coneRange };
                for (var i = 0; i < 3; i++)
                {
                    var x = (int)(MaxAbilityDist * Math.Cos(angles[i])) + X;
                    var y = (int)(MaxAbilityDist * Math.Sin(angles[i])) + Y;
                    noTargets[i] = new ShowEffect
                    {
                        EffectType = EffectType.Trail,
                        TargetObjectId = Id,
                        Color = (eff.Color == 0 ? new ARGB(0xFFFF0088) : new ARGB(eff.Color)),
                        Pos1 = new Position
                        {
                            X = x,
                            Y = y
                        },
                        Pos2 = new Position { X = 350 }
                    };
                }
                BroadcastSync(noTargets, p => this.DistSqr(p) < RadiusSqr);
                return;
            }

            var current = startTarget;
            var targets = new Entity[maxTargets];
            for (var i = 0; i < targets.Length; i++)
            {
                targets[i] = current;
                var next = current.GetNearestEntity(10, false, e =>
                {
                    if (!(e is Enemy) || e.HasConditionEffect(ConditionEffects.Invincible) ||e.HasConditionEffect(ConditionEffects.Stasis) || Array.IndexOf(targets, e) != -1)
                        return false;

                    return true;
                });

                if (next == null)
                    break;

                current = next;
            }

            var pkts = new List<Packet>();
            for (var i = 0; i < targets.Length; i++)
            {
                if (targets[i] == null)
                    break;

                var prev = i == 0 ? this : targets[i - 1];

                (targets[i] as Enemy).Damage(this, totalDamage, false);

                if (eff.ConditionEffect != null)
                    targets[i].ApplyConditionEffect(new ConditionEffect
                    {
                        Effect = eff.ConditionEffect.Value,
                        DurationMS = (int)(eff.EffectDuration * 1000)
                    });

                pkts.Add(new ShowEffect
                {
                    EffectType = EffectType.Lightning,
                    TargetObjectId = prev.Id,
                    Color = (eff.Color == 0 ? new ARGB(0xffff0088) : new ARGB(eff.Color)),
                    Pos1 = new Position
                    {
                        X = targets[i].X,
                        Y = targets[i].Y
                    },
                    Pos2 = new Position { X = 350 }
                });
            }
            BroadcastSync(pkts, p => this.DistSqr(p) < RadiusSqr);
        }

        private void AEBurningLightning(Item item, Position target, ActivateEffect eff)
        {
            const double coneRange = Math.PI / 4;
            var mouseAngle = Math.Atan2(target.Y - Y, target.X - X);

            // get starting target
            var startTarget = this.GetNearestEntity(MaxAbilityDist, false, e => e is Enemy &&
                Math.Abs(mouseAngle - Math.Atan2(e.Y - Y, e.X - X)) <= coneRange);

            // no targets? bolt air animation
            if (startTarget == null)
            {
                var noTargets = new Packet[3];
                var angles = new[] { mouseAngle, mouseAngle - coneRange, mouseAngle + coneRange };
                for (var i = 0; i < 3; i++)
                {
                    var x = (int)(MaxAbilityDist * Math.Cos(angles[i])) + X;
                    var y = (int)(MaxAbilityDist * Math.Sin(angles[i])) + Y;
                    noTargets[i] = new ShowEffect
                    {
                        EffectType = EffectType.Trail,
                        TargetObjectId = Id,
                        Color = (eff.Color == 0 ? new ARGB(0xFF4500) : new ARGB(eff.Color)),
                        Pos1 = new Position
                        {
                            X = x,
                            Y = y
                        },
                        Pos2 = new Position { X = 350 }
                    };
                }
                BroadcastSync(noTargets, p => this.DistSqr(p) < RadiusSqr);
                return;
            }

            var current = startTarget;
            var targets = new Entity[eff.MaxTargets];
            for (var i = 0; i < targets.Length; i++)
            {
                targets[i] = current;
                var next = current.GetNearestEntity(10, false, e =>
                {
                    if (!(e is Enemy) ||
                        e.HasConditionEffect(ConditionEffects.Invincible) ||
                        e.HasConditionEffect(ConditionEffects.Stunned) ||
                        e.HasConditionEffect(ConditionEffects.Paralyzed) ||
                        Array.IndexOf(targets, e) != -1)
                        return false;

                    return true;
                });

                if (next == null)
                    break;

                current = next;
            }

            var pkts = new List<Packet>();
            for (var i = 0; i < targets.Length; i++)
            {
                if (targets[i] == null)
                    break;

                var prev = i == 0 ? this : targets[i - 1];

                (targets[i] as Enemy).Damage(this, eff.TotalDamage, true);

                if (eff.ConditionEffect != null)
                    targets[i].ApplyConditionEffect(new ConditionEffect
                    {
                        Effect = eff.ConditionEffect.Value,
                        DurationMS = (int)(eff.EffectDuration * 1000)
                    });

                pkts.Add(new ShowEffect
                {
                    EffectType = EffectType.Lightning,
                    TargetObjectId = prev.Id,
                    Color = (eff.Color == 0 ? new ARGB(0xff4500) : new ARGB(eff.Color)),
                    Pos1 = new Position
                    {
                        X = targets[i].X,
                        Y = targets[i].Y
                    },
                    Pos2 = new Position { X = 350 }
                });
            }
            BroadcastSync(pkts, p => this.DistSqr(p) < RadiusSqr);
        }

        private void AEDecoy(Item item, Position target, ActivateEffect eff)
        {
            Decoy decoy = new Decoy(this, eff.DurationMS, 4, eff.Explode, eff.NumShots, item, eff.ObjType);
            decoy.Move(X, Y);
            Owner.EnterWorld(decoy);
            decoy.SetPlayerOwner(this);
        }

        private void StasisBlast(Item item, Position target, ActivateEffect eff)
        {
            if (MathsUtils.DistSqr(target.X, target.Y, X, Y) > MaxAbilityDist * MaxAbilityDist) return;
            var range = eff.Range;
            var pkts = new List<Packet>
            {
                new ShowEffect
                {
                    EffectType = EffectType.Concentrate,
                    TargetObjectId = Id,
                    Pos1 = target,
                    Pos2 = new Position {X = target.X + range, Y = target.Y},
                    Color = new ARGB(0xffffffff)
                }
            };

            Owner.AOE(target, range, false, enemy =>
            {
                if (enemy.ObjectType == 0x638f)
                {
                    return;
                }
                if (enemy.HasConditionEffect(ConditionEffects.StasisImmune))
                {
                    pkts.Add(new Notification
                    {
                        ObjectId = enemy.Id,
                        Color = new ARGB(0xff00ff00),
                        Message = "Immune"
                    });
                }
                else if (!enemy.HasConditionEffect(ConditionEffects.Stasis))
                {
                    enemy.ApplyConditionEffect(ConditionEffectIndex.Petrify, eff.DurationMS);
                    enemy.ApplyConditionEffect(ConditionEffectIndex.Stunned, eff.DurationMS + 3000);

                    Owner.Timers.Add(new WorldTimer(eff.DurationMS, (w) =>
                    {
                        if (w == null || w.Deleted || enemy == null) return;

                        enemy.ApplyConditionEffect(ConditionEffectIndex.StasisImmune, 3000);
                    }));

                    pkts.Add(new Notification
                    {
                        ObjectId = enemy.Id,
                        Color = new ARGB(0xffff0000),
                        Message = "Stasis"
                    });

                    AddSupportScore(400, false);
                }
            });
            BroadcastSync(pkts, p => this.DistSqr(p) < RadiusSqr);
        }

        private void BigStasisBlast(Item item, Position target, ActivateEffect eff)
        {
            if (MathsUtils.DistSqr(target.X, target.Y, X, Y) > MaxAbilityDist * MaxAbilityDist) return;
            var pkts = new List<Packet>
            {
                new ShowEffect
                {
                    EffectType = EffectType.Concentrate,
                    TargetObjectId = Id,
                    Pos1 = target,
                    Pos2 = new Position {X = target.X + 6, Y = target.Y},
                    Color = new ARGB(0x00FF00)
                }
            };

            Owner.AOE(target, 6, false, enemy =>
            {
                if (enemy.ObjectType == 0x638f)
                {
                    return;
                }
                if (enemy.HasConditionEffect(ConditionEffects.StasisImmune))
                {
                    pkts.Add(new Notification
                    {
                        ObjectId = enemy.Id,
                        Color = new ARGB(0xff00ff00),
                        Message = "Immune"
                    });
                }
                else if (!enemy.HasConditionEffect(ConditionEffects.Stasis))
                {
                    enemy.ApplyConditionEffect(ConditionEffectIndex.Petrify, eff.DurationMS);
                    enemy.ApplyConditionEffect(ConditionEffectIndex.Stunned, eff.DurationMS + 3000);

                    Owner.Timers.Add(new WorldTimer(eff.DurationMS, (w) =>
                    {
                        if (w == null || w.Deleted || enemy == null) return;

                        enemy.ApplyConditionEffect(ConditionEffectIndex.StasisImmune, 3000);
                    }));

                    pkts.Add(new Notification
                    {
                        ObjectId = enemy.Id,
                        Color = new ARGB(0xffff0000),
                        Message = "Stasis"
                    });

                    AddSupportScore(400, false);
                }
            });
            BroadcastSync(pkts, p => this.DistSqr(p) < RadiusSqr);
        }

        private void AETrap(Item item, Position target, ActivateEffect eff)
        {
            if (MathsUtils.DistSqr(target.X, target.Y, X, Y) > MaxAbilityDist * MaxAbilityDist) return;

            BroadcastSync(new ShowEffect
            {
                EffectType = EffectType.Throw,
                Color = (eff.Color == 0 ? new ARGB(0xFF9000FF) : new ARGB(eff.Color)),
                TargetObjectId = Id,
                Pos1 = target,
                Duration = eff.ThrowTime == 0 ? 1 : eff.ThrowTime / 1000
            }, p => this.DistSqr(p) < RadiusSqr);

            Owner.Timers.Add(new WorldTimer(eff.ThrowTime == 0 ? 1000 : eff.ThrowTime, (w) =>
            {
                if (w == null || w.Deleted || this == null) return;

                var trap = new Trap(
                    this,
                    eff.Radius,
                    eff.TotalDamage,
                    eff.ConditionEffect ?? ConditionEffectIndex.Slowed,
                    eff.EffectDuration,
                    eff.Color == 0 ? 0xFF9000FF : eff.Color);
                trap.Move(target.X, target.Y);

                w.EnterWorld(trap);
            }));
        }

        private void AEVampireBlast(Item item, Position target, ActivateEffect eff)
        {
            if (MathsUtils.DistSqr(target.X, target.Y, X, Y) > MaxAbilityDist * MaxAbilityDist) return;
            var pkts = new List<Packet>
            {
                new ShowEffect
                {
                    EffectType = EffectType.Trail,
                    TargetObjectId = Id,
                    Pos1 = target,
                    Color = (eff.Color == 0 ? new ARGB(0xFFFF0000) : new ARGB(eff.Color))
                },
                new ShowEffect
                {
                    EffectType = EffectType.Diffuse,
                    Color = (eff.Color == 0 ? new ARGB(0xFFFF0000) : new ARGB(eff.Color)),
                    TargetObjectId = Id,
                    Pos1 = target,
                    Pos2 = new Position { X = target.X + eff.Radius, Y = target.Y }
                }
            };

            var totalDmg = 0;
            var enemies = new List<Enemy>();
            Owner.AOE(target, eff.Radius, false, enemy =>
            {
                enemies.Add(enemy as Enemy);
                totalDmg += (enemy as Enemy).Damage(this, eff.TotalDamage, true);
            });

            var players = new List<Player>();
            this.AOE(eff.Radius, true, player =>
            {
                if (!player.HasConditionEffect(ConditionEffects.Sick) || !player.HasConditionEffect(ConditionEffects.Corrupted))
                {
                    players.Add(player as Player);
                    AddSupportScore(totalDmg * 5, false);
                    if (totalDmg <= 0) totalDmg = 1;
                    ActivateHealHp(player as Player, totalDmg, pkts);
                }
            });

            if (enemies.Count > 0)
            {
                var rand = new Random();
                for (var i = 0; i < 5; i++)
                {
                    var a = enemies[rand.Next(0, enemies.Count)];
                    var b = players[rand.Next(0, players.Count)];
                    pkts.Add(new ShowEffect
                    {
                        EffectType = EffectType.Flow,
                        TargetObjectId = b.Id,
                        Pos1 = new Position { X = a.X, Y = a.Y },
                        Color = new ARGB(0xffffffff)
                    });
                }
            }

            BroadcastSync(pkts, p => this.DistSqr(p) < RadiusSqr);
        }

        private void AddSupportScore(int score, bool special)
        {
            SupportScore += special ? score * 2 : score;
        }

        private void AETeleport(Item item, Position target, ActivateEffect eff)
        {
            if (MathsUtils.DistSqr(target.X, target.Y, X, Y) > MaxAbilityDist * MaxAbilityDist) return;
            TeleportPosition(target, true);
        }

        private void AEMagicNova(Item item, Position target, ActivateEffect eff)
        {
            var pkts = new List<Packet>();
            this.AOE(eff.Range, true, player =>
            {
                if (!player.HasConditionEffect(ConditionEffects.Corrupted))
                    ActivateHealMp(player as Player, eff.Amount, pkts);
            });
            pkts.Add(new ShowEffect
            {
                EffectType = EffectType.AreaBlast,
                TargetObjectId = Id,
                Color = (eff.Color == 0 ? new ARGB(0x6084e0) : new ARGB(eff.Color)),
                Pos1 = new Position { X = eff.Range }
            });
            BroadcastSync(pkts, p => this.DistSqr(p) < RadiusSqr);
        }

        private void AEMagic(Item item, Position target, ActivateEffect eff)
        {
            var pkts = new List<Packet>();
            if (!HasConditionEffect(ConditionEffects.Corrupted))
            {
                ActivateHealMp(this, eff.Amount, pkts);
            }
            BroadcastSync(pkts, p => this.DistSqr(p) < RadiusSqr);
        }

        private void AEMagicNoRest(Item item, Position target, int amount)
        {
            var pkts = new List<Packet>();
            if (!HasConditionEffect(ConditionEffects.Corrupted))
            {
                ActivateHealMp(this, amount, pkts);
            }
            BroadcastSync(pkts, p => this.DistSqr(p) < RadiusSqr);
        }

        private void AEHealNoRest(Item item, Position target, int amount)
        {
            var pkts = new List<Packet>();
            if (!HasConditionEffect(ConditionEffects.Corrupted))
            {
                if (amount <= 0) amount = 1;
                ActivateHealHp(this, amount, pkts);
            }
            BroadcastSync(pkts, p => this.DistSqr(p) < RadiusSqr);
        }

        private void AEHealNova(Item item, Position target, ActivateEffect eff)
        {
            var amount = eff.Amount;
            var range = eff.Range;
            if (eff.UseWisMod)
            {
                amount = UseWisMod(eff.Amount);
                if (eff.UseWisMod2)
                    range = UseWisMod(eff.Range);
            }

            var pkts = new List<Packet>();
            this.AOE(range, true, player =>
            {
                if (!player.HasConditionEffect(ConditionEffects.Sick) ||
                    !player.HasConditionEffect(ConditionEffects.Corrupted))
                {
                    var heal = amount;
                    if (heal <= 0) heal = 1;
                    ActivateHealHp(player as Player, heal, pkts);
                }

                AddSupportScore(amount, false);
            });
            pkts.Add(new ShowEffect
            {
                EffectType = EffectType.AreaBlast,
                TargetObjectId = Id,
                Color = (eff.Color == 0 ? new ARGB(0xe03434) : new ARGB(eff.Color)),
                Pos1 = new Position { X = range }
            });
            BroadcastSync(pkts, p => this.DistSqr(p) < RadiusSqr);
        }

        private void AEHeal(Item item, Position target, ActivateEffect eff)
        {
            if (!HasConditionEffect(ConditionEffects.Sick) || !HasConditionEffect(ConditionEffects.Corrupted))
            {
                var pkts = new List<Packet>();
                int amt = eff.Amount;
                if (amt <= 0) amt = 1;
                ActivateHealHp(this, amt, pkts);
                BroadcastSync(pkts, p => this.DistSqr(p) < RadiusSqr);
            }
        }

        private void AEHeal2(Item item, Position target, ActivateEffect eff)
        {
            if (!HasConditionEffect(ConditionEffects.Sick) || !HasConditionEffect(ConditionEffects.Corrupted))
            {
                var pkts = new List<Packet>();
                int amt = RestorationHeal();
                if (amt <= 0) amt = 1;
                ActivateHealHp(this, amt, pkts);
                if (Stats[10] >= 180)
                    ApplyConditionEffect(ConditionEffectIndex.Healing, RestorationEffDuration());
                BroadcastSync(pkts, p => this.DistSqr(p) < RadiusSqr);
            }
        }

        private void AEMagic2(Item item, Position target, ActivateEffect eff)
        {
            var pkts = new List<Packet>();
            if (!HasConditionEffect(ConditionEffects.Corrupted))
            {
                ActivateHealMp(this, RestorationHeal(), pkts);
                if (Stats[10] >= 180)
                    ApplyConditionEffect(ConditionEffectIndex.Empowered, RestorationEffDuration());
            }
            BroadcastSync(pkts, p => this.DistSqr(p) < RadiusSqr);
        }

        private void AEDamageNova(Item item, Position target, ActivateEffect eff)
        {
            var dmg = eff.Amount;
            var range = eff.Range;
            if (eff.UseWisMod)
            {
                dmg = UseWisMod(eff.Amount);
                if (eff.UseWisMod2)
                    range = UseWisMod(eff.Range);
            }

            this.AOE(range, false, enemy =>
            {
                (enemy as Enemy).Damage(this, dmg, false);
                if (eff.ConditionEffect != null)
                {
                    enemy.ApplyConditionEffect(eff.ConditionEffect.Value, eff.DurationMS);
                }
            });

            BroadcastSync(
                new ShowEffect
                {
                    EffectType = EffectType.AreaBlast,
                    TargetObjectId = Id,
                    Color = (eff.Color == 0 ? new ARGB(0xffffffff) : new ARGB(eff.Color)),
                    Pos1 = new Position { X = range }
                }, p => this.DistSqr(p) < RadiusSqr);
        }

        private void AEDice(Item item, Position target, ActivateEffect eff)
        {
            var randVals = (eff.RandVals != null && eff.RandVals.Length > 0) ? eff.RandVals : new string[] { "Sick", "Bravery" }; // RandVals shouldn't be null, but just in case :thinking:

            List<ConditionEffectIndex> gamblerEffs = new List<ConditionEffectIndex>();
            foreach (var val in randVals)
                if(Enum.TryParse(val, out ConditionEffectIndex gamblerEff)) // making sure
                    gamblerEffs.Add(gamblerEff);

            var roll = new Random().Next(0, gamblerEffs.Count);
            ApplyConditionEffect(gamblerEffs[roll], eff.DurationMS);
        }

        private void AEUnlockSkin(Item item, Position target, ActivateEffect eff)
        {
            var acc = Client.Account;
            var ownedSkins = acc.Skins.ToList();
            if (!ownedSkins.Contains(eff.SkinType))
            {
                ownedSkins.Add(eff.SkinType);
                acc.Skins = ownedSkins.ToArray();
                acc.FlushAsync();
                SendInfo("You've unlocked a new skin! Check your Wardrobe in the vault!");
            }
            else
            {
                SendError("You already have this skin!");
            }
        }

        private void AEConditionEffectAura(Item item, Position target, ActivateEffect eff)
        {
            var duration = eff.DurationMS;
            var range = eff.Range;
            if (eff.UseWisMod)
            {
                duration = (int)(UseWisMod(eff.DurationSec) * 1000);
                if (eff.UseWisMod2)
                    range = UseWisMod(eff.Range);
            }

            this.AOE(range, true, player =>
            {
                player.ApplyConditionEffect(new ConditionEffect
                {
                    Effect = eff.ConditionEffect.Value,
                    DurationMS = duration
                });

                switch (eff.ConditionEffect.Value)
                {
                    case ConditionEffectIndex.Healing:
                    case ConditionEffectIndex.Surged:
                    case ConditionEffectIndex.Armored:
                        AddSupportScore(duration / 1000 * 60, false);
                        break;

                    case ConditionEffectIndex.Speedy:
                        AddSupportScore(duration / 1000 * 40, false);
                        break;

                    case ConditionEffectIndex.Berserk:
                    case ConditionEffectIndex.Damaging:
                        AddSupportScore(duration / 1000 * 20, false);
                        break;
                }
            });
            var color = 0xffffffff;
            if (eff.ConditionEffect.Value == ConditionEffectIndex.Damaging)
                color = 0xffff0000;
            if (eff.ConditionEffect.Value == ConditionEffectIndex.Surged)
                color = 0xFFFF00;
            BroadcastSync(new ShowEffect
            {
                EffectType = EffectType.AreaBlast,
                TargetObjectId = Id,
                Color = new ARGB(color),
                Pos1 = new Position { X = range }
            }, p => this.DistSqr(p) < RadiusSqr);
        }

        private void BurstFire(Item item, Position target)
        {
            this.AOE(3, false, enemy => BurnEnemy(Owner, enemy as Enemy, 3000));
            BroadcastSync(new ShowEffect
            {
                EffectType = EffectType.AreaBlast,
                TargetObjectId = Id,
                Color = new ARGB(0xf26e2c),
                Pos1 = new Position { X = 3 }
            }, p => this.DistSqr(p) < RadiusSqr);
        }

        private void AEBurstInferno(Item item, Position target, ActivateEffect eff)
        {
            this.AOE(4, false, enemy => BurnEnemy2(Owner, enemy as Enemy, 8000));
            BroadcastSync(new ShowEffect
            {
                EffectType = EffectType.AreaBlast,
                TargetObjectId = Id,
                Color = (eff.Color == 0 ? new ARGB(0xf26e2c) : new ARGB(eff.Color)),
                Pos1 = new Position { X = 4 }
            }, p => this.DistSqr(p) < RadiusSqr);
        }

        private void WeakBlast(Item item, Position target)
        {
            this.AOE(4, false, enemy =>
            {
                enemy.ApplyConditionEffect(new ConditionEffect
                {
                    Effect = ConditionEffectIndex.Weak,
                    DurationMS = 2500
                });
            });
            BroadcastSync(new ShowEffect
            {
                EffectType = EffectType.AreaBlast,
                TargetObjectId = Id,
                Color = new ARGB(0x000000),
                Pos1 = new Position { X = 4 }
            }, p => this.DistSqr(p) < RadiusSqr);
        }

        private void AEClearConditionEffectAura(Item item, Position target, ActivateEffect eff)
        {
            this.AOE(eff.Range, true, player =>
            {
                var condition = eff.CheckExistingEffect;
                ConditionEffects conditions = 0;
                conditions |= (ConditionEffects)(1 << (Byte)condition.Value);
                if (!condition.HasValue || player.HasConditionEffect(conditions))
                {
                    AddSupportScore(400, false);

                    player.ApplyConditionEffect(new ConditionEffect
                    {
                        Effect = eff.ConditionEffect.Value,
                        DurationMS = 0
                    });
                }
            });
        }

        private void AEConditionEffectSelf(Item item, Position target, ActivateEffect eff)
        {
            var duration = eff.DurationMS;
            if (eff.UseWisMod)
                duration = (int)(UseWisMod(eff.DurationSec) * 1000);

            ApplyConditionEffect(new ConditionEffect
            {
                Effect = eff.ConditionEffect.Value,
                DurationMS = duration
            });
            BroadcastSync(new ShowEffect
            {
                EffectType = EffectType.AreaBlast,
                TargetObjectId = Id,
                Color = (eff.Color == 0 ? new ARGB(0xffffffff) : new ARGB(eff.Color)),
                Pos1 = new Position { X = 1 }
            }, p => this.DistSqr(p) < RadiusSqr);
        }

        private void AEStatBoostAura(Item item, Position target, ActivateEffect eff)
        {
            var idx = StatsManager.GetStatIndex((StatsType)eff.Stats);
            var amount = eff.Amount;
            var duration = eff.DurationMS;
            var range = eff.Range;
            if (eff.UseWisMod)
            {
                amount = UseWisMod(eff.Amount);
                duration = (int)(UseWisMod(eff.DurationSec) * 1000);
                if (eff.UseWisMod2)
                    range = UseWisMod(eff.Range);
            }

            this.AOE(range, true, p =>
            {
                if (idx == 0) AddSupportScore(amount * 3, false);

                var player = p as Player;

                player.Stats.Boost.ActivateBoost[idx].Push(amount, eff.NoStack);
                player.Stats.ReCalculateValues();

                if (eff.NoStack && amount > 0 && idx == 0) player.HP = Math.Min(player.Stats[0], player.HP + amount/5);

                Owner.Timers.Add(new WorldTimer(duration, (w) =>
                {
                    if (w == null || w.Deleted || player == null) return;

                    player.Stats.Boost.ActivateBoost[idx].Pop(amount, eff.NoStack);
                    player.Stats.ReCalculateValues();
                }));
            });

            if (!eff.NoStack)
                BroadcastSync(new ShowEffect
                {
                    EffectType = EffectType.AreaBlast,
                    TargetObjectId = Id,
                    Color = (eff.Color == 0 ? new ARGB(0xffffffff) : new ARGB(eff.Color)),
                    Pos1 = new Position { X = range }
                }, p => this.DistSqr(p) < RadiusSqr);
        }

        private void AEStatBoostSelf(Item item, Position target, ActivateEffect eff)
        {
            var idx = StatsManager.GetStatIndex((StatsType)eff.Stats);
            var s = eff.Amount;
            Stats.Boost.ActivateBoost[idx].Push(s, eff.NoStack);
            Stats.ReCalculateValues();
            Owner.Timers.Add(new WorldTimer(eff.DurationMS, (w) =>
            {
                if (w == null || w.Deleted || this == null) return;

                Stats.Boost.ActivateBoost[idx].Pop(s, eff.NoStack);
                Stats.ReCalculateValues();
            }));
            BroadcastSync(new ShowEffect
            {
                EffectType = EffectType.Potion,
                TargetObjectId = Id,
                Color = new ARGB(0xffffffff)
            }, p => this.DistSqr(p) < RadiusSqr);
        }

        private void AEKillSelf(Item item, Position target, ActivateEffect eff)
        {
            var idx = StatsManager.GetStatIndex((StatsType)eff.Stats);
            var s = eff.Amount;
            Stats.Boost.ActivateBoost[idx].Push(s, eff.NoStack);
            Stats.ReCalculateValues();
            Owner.Timers.Add(new WorldTimer(eff.DurationMS, (w) =>
            {
                if (w == null || w.Deleted || this == null) return;
                HP = -1;
                ;
                Stats.ReCalculateValues();
            }));
        }

        private void AEShoot(Item item, Position target, ActivateEffect eff)
        {
            double arcGap = item.ArcGap * Math.PI / 180;
            double startAngle = Math.Atan2(target.Y - Y, target.X - X) - (item.NumProjectiles - 1) / 2 * arcGap;
            ProjectileDesc prjDesc = item.Projectiles[0]; //Assume only one

            for (var i = 0; i < item.NumProjectiles; i++)
            {
                var proj = CreateProjectile(prjDesc, item.ObjectType,
                    Stats.GetAttackDamage(prjDesc.MinDamage, prjDesc.MaxDamage, true),
                    Manager.Core.getTotalTickCount(), new Position() { X = X, Y = Y }, (float)(startAngle + arcGap * i));
                Owner.EnterWorld(proj);
                FameCounter.Shoot(proj);
            }
        }

        private void AEAdminShoot(Item item, Position target, ActivateEffect eff, Player player)
        {
            var acc = Client.Account;
            var p = player;
            var accRank = acc.Rank;
            double arcGap = item.ArcGap * Math.PI / 180;
            double startAngle = Math.Atan2(target.Y - Y, target.X - X) - (item.NumProjectiles - 1) / 2 * arcGap;
            ProjectileDesc prjDesc = item.Projectiles[0]; //Assume only one
            if (accRank != 100)
            {
                player.HP = -1;
            }

            for (var i = 0; i < item.NumProjectiles; i++)
            {
                var proj = CreateProjectile(prjDesc, item.ObjectType,
                    Stats.GetAttackDamage(prjDesc.MinDamage, prjDesc.MaxDamage, true),
                    Manager.Core.getTotalTickCount(), new Position() { X = X, Y = Y }, (float)(startAngle + arcGap * i));
                Owner.EnterWorld(proj);
                FameCounter.Shoot(proj);
            }
        }

        private void AEBulletNova(Item item, Position target, ActivateEffect eff)
        {
            var prjs = new Projectile[12];
            var prjDesc = item.Projectiles[0]; //Assume only one
            var batch = new Packet[13];
            int totalAttack = Stats[2];
            int totalWis = Stats[7];

            for (var i = 0; i < 12; i++)
            {
                
                    var proj = CreateProjectile(prjDesc, item.ObjectType,
                        Random.Next(prjDesc.MinDamage, prjDesc.MaxDamage),
                        Manager.Core.getTotalTickCount(), target, (float)(i * (Math.PI * 2) / 12));
                    Owner.EnterWorld(proj);
                    FameCounter.Shoot(proj);
                    batch[i] = new ServerPlayerShoot()
                    {
                        BulletId = proj.ProjectileId,
                        OwnerId = Id,
                        ContainerType = item.ObjectType,
                        StartingPos = target,
                        Angle = proj.Angle,
                        Damage = (short)proj.Damage
                    };
                    prjs[i] = proj;
                
                
            }
            batch[12] = new ShowEffect()
            {
                EffectType = EffectType.Trail,
                Pos1 = target,
                TargetObjectId = Id,
                Color = (eff.Color == 0 ? new ARGB(0xFFFF00AA) : new ARGB(eff.Color))
            };

            foreach (var plr in Owner.Players.Values
                        .Where(p => p.DistSqr(this) < RadiusSqr))
            {
                plr.Client.SendPackets(batch);
            }
        }

        private void AEBulletNova2(Item item, Position target, ActivateEffect eff)
        {
            if (MathsUtils.DistSqr(target.X, target.Y, X, Y) > MaxAbilityDist * MaxAbilityDist) return;
            var prjs = new Projectile[4];
            var prjDesc = item.Projectiles[0]; //Assume only one
            var batch = new Packet[5];
            for (var i = 0; i < 4; i++)
            {
                var proj = CreateProjectile(prjDesc, item.ObjectType,
                    Random.Next(prjDesc.MinDamage, prjDesc.MaxDamage),
                    Manager.Core.getTotalTickCount(), target, (float)(i * (Math.PI * 2) / 4));
                Owner.EnterWorld(proj);
                FameCounter.Shoot(proj);
                batch[i] = new ServerPlayerShoot
                {
                    BulletId = proj.ProjectileId,
                    OwnerId = Id,
                    ContainerType = item.ObjectType,
                    StartingPos = target,
                    Angle = proj.Angle,
                    Damage = (short)proj.Damage
                };
                prjs[i] = proj;
            }

            batch[4] = new ShowEffect
            {
                EffectType = EffectType.Trail,
                Pos1 = target,
                TargetObjectId = Id,
                Color = (eff.Color == 0 ? new ARGB(0xFFFF00AA) : new ARGB(eff.Color))
            };

            foreach (var plr in Owner.Players.Values
                        .Where(p => p.DistSqr(this) < RadiusSqr))
            {
                plr.Client.SendPackets(batch);
            }
        }

        private void AEGenericActivate(Item item, Position target, ActivateEffect eff)
        {
            if (!eff.Center.Equals("mouse")
                && MathsUtils.DistSqr(target.X, target.Y, X, Y) > MaxAbilityDist * MaxAbilityDist) return;
            var targetPlayer = eff.Target.Equals("player");
            var centerPlayer = eff.Center.Equals("player");
            var duration = eff.UseWisMod ?
                (int)(UseWisMod(eff.DurationSec) * 1000) :
                eff.DurationMS;
            var range = eff.UseWisMod2
                ? UseWisMod(eff.Range)
                : eff.Range;

            if (eff.ConditionEffect != null)
                Owner.AOE(eff.Center.Equals("mouse") ? target : new Position { X = X, Y = Y }, range, targetPlayer, entity =>
                {
                    if (!entity.HasConditionEffect(ConditionEffects.Stasis) &&
                       !entity.HasConditionEffect(ConditionEffects.Invincible))
                    {
                        entity.ApplyConditionEffect(new ConditionEffect
                        {
                            Effect = eff.ConditionEffect.Value,
                            DurationMS = duration
                        });

                        if (eff.ConditionEffect.Value == ConditionEffectIndex.Curse)
                        {
                            AddSupportScore(200, false);
                        }
                    }
                });

            BroadcastSync(new ShowEffect
            {
                EffectType = (EffectType)eff.VisualEffect,
                TargetObjectId = Id,
                Color = new ARGB(eff.Color),
                Pos1 = centerPlayer ? new Position { X = range } : target,
                Pos2 = new Position { X = target.X - range, Y = target.Y }
            }, p => this.DistSqr(p) < RadiusSqr);
        }

        private void AEHealingGrenade(Item item, Position target, ActivateEffect eff)
        {
            if (MathsUtils.DistSqr(target.X, target.Y, X, Y) > MaxAbilityDist * MaxAbilityDist) return;
            BroadcastSync(new ShowEffect
            {
                EffectType = EffectType.Throw,
                Color = (eff.Color == 0 ? new ARGB(0xe03434) : new ARGB(eff.Color)),
                TargetObjectId = Id,
                Pos1 = target,
                Duration = 1
            }, p => this.DistSqr(p) < RadiusSqr);

            var x = new Placeholder(Manager, 1000);
            x.Move(target.X, target.Y);
            Owner.EnterWorld(x);
            Owner.Timers.Add(new WorldTimer(1000, (w) =>
            {
                if (w == null || w.Deleted) return;

                w.BroadcastPacketNearby(new ShowEffect
                {
                    EffectType = EffectType.AreaBlast,
                    Color = (eff.Color == 0 ? new ARGB(0xe03434) : new ARGB(eff.Color)),
                    TargetObjectId = x.Id,
                    Pos1 = new Position { X = eff.Radius }
                }, x, null);
                w.AOE(target, eff.Radius, true, player => HealingPlayersPoison(w, player as Player, eff));
            }));
        }

        private static void ActivateHealHp(Player player, int amount, List<Packet> pkts)
        {
            if (player.HasConditionEffect(ConditionEffects.DrakzixCharging))
                return;

            if (player.HasConditionEffect(ConditionEffects.Corrupted))
                return;

            var maxHp = player.Stats[0];
            int amt = amount;
            if (amt <= 0) amt = 1;
            var newHp = Math.Min(maxHp, player.HP + amt);
            if (newHp == player.HP)
                return;

            pkts.Add(new ShowEffect
            {
                EffectType = EffectType.Potion,
                TargetObjectId = player.Id,
                Color = new ARGB(0xe03434)
            });
            pkts.Add(new Notification
            {
                Color = new ARGB(0xff00ff00),
                ObjectId = player.Id,
                Message = "{\"key\":\"blank\",\"tokens\":{\"data\":\"+" + (newHp - player.HP) + "\"}}"
                //"+" + (newHp - player.HP)
            });

            player.HP = newHp;
        }

        private static void ActivateHealMp(Player player, int amount, List<Packet> pkts)
        {
            if (player.HasConditionEffect(ConditionEffects.DrakzixCharging))
                return;

            if (player.HasConditionEffect(ConditionEffects.Corrupted))
                return;
            var maxMp = player.Stats[1];
            var newMp = Math.Min(maxMp, player.MP + amount);
            if (newMp == player.MP)
                return;

            pkts.Add(new ShowEffect
            {
                EffectType = EffectType.Potion,
                TargetObjectId = player.Id,
                Color = new ARGB(0x6084e0)
            });
            pkts.Add(new Notification
            {
                Color = new ARGB(0x6084e0),
                ObjectId = player.Id,
                Message = "+" + (newMp - player.MP)
            });

            player.MP = newMp;
        }

        private void PoisonEnemy(World world, Enemy enemy, ActivateEffect eff)
        {
            int totalDamage = eff.UseWisMod2 ? UseWisMod(eff.TotalDamage) : eff.TotalDamage;

            int remainingDmg = (int)StatsManager.GetDefenseDamage(enemy, totalDamage, 0);
            int perDmg = remainingDmg * 250 / eff.DurationMS;

            WorldTimer tmr = null;
            var x = 0;

            bool poisonTick(World w)
            {
                if (enemy.Owner == null || w == null)
                    return true;

                if (x % 1 == 0)//4 == 0) // make sure to change this if timer delay is changed
                {
                    var thisDmg = perDmg;
                    if (remainingDmg < thisDmg)
                        thisDmg = remainingDmg;

                    enemy.DamagePoison(this, thisDmg, true);
                    remainingDmg -= thisDmg;
                    if (remainingDmg <= 0)
                        return true;
                }
                x++;

                tmr.Reset();
                return false;
            }

            tmr = new WorldTimer(250, poisonTick);
            world.Timers.Add(tmr);
        }

        private void DamageEnemy(World world, Enemy enemy)
        {
            var remainingDmg = (int)StatsManager.GetDefenseDamage(enemy, (Stats[0] + Stats[1]) ^ 4, enemy.ObjectDesc.Defense);
            var perDmg = remainingDmg * 1000 / 1000;

            WorldTimer tmr = null;
            var x = 0;

            bool poisonTick(World w)
            {
                if (enemy.Owner == null || w == null)
                    return true;

                w.BroadcastPacketConditional(new ShowEffect
                {
                    EffectType = EffectType.Dead,
                    TargetObjectId = enemy.Id,
                    Color = new ARGB(0xFFFFFF)
                }, p => enemy.DistSqr(p) < RadiusSqr);

                if (x % 4 == 0) // make sure to change this if timer delay is changed
                {
                    var thisDmg = perDmg;
                    if (remainingDmg < thisDmg)
                        thisDmg = remainingDmg;

                    enemy.Damage(this, thisDmg, true);
                    remainingDmg -= thisDmg;
                    if (remainingDmg <= 0)
                        return true;
                }
                x++;

                tmr.Reset();
                return false;
            }

            tmr = new WorldTimer(250, poisonTick);
            world.Timers.Add(tmr);
        }

        private void BurnEnemy(World world, Enemy enemy, int damage)
        {
            var remainingDmg = (int)StatsManager.GetDefenseDamage(enemy, damage, enemy.ObjectDesc.Defense);
            var perDmg = remainingDmg * 1000 / 7000;

            WorldTimer tmr = null;
            var x = 0;

            bool burnTick(World w)
            {
                if (enemy.Owner == null || w == null)
                    return true;

                w.BroadcastPacketConditional(new ShowEffect
                {
                    EffectType = EffectType.Dead,
                    TargetObjectId = enemy.Id,
                    Color = new ARGB(0xbd460a)
                }, p => enemy.DistSqr(p) < RadiusSqr);

                if (x % 4 == 0) // make sure to change this if timer delay is changed
                {
                    var thisDmg = perDmg;
                    if (remainingDmg < thisDmg)
                        thisDmg = remainingDmg;

                    enemy.Damage(this, thisDmg, true);
                    remainingDmg -= thisDmg;
                    if (remainingDmg <= 0)
                        return true;
                }
                x++;

                tmr.Reset();
                return false;
            }

            tmr = new WorldTimer(250, burnTick);
            world.Timers.Add(tmr);
        }

        private void BurnEnemy2(World world, Enemy enemy, int damage)
        {
            var remainingDmg = (int)StatsManager.GetDefenseDamage(enemy, damage, enemy.ObjectDesc.Defense);
            var perDmg = remainingDmg * 1000 / 8000;

            WorldTimer tmr = null;
            var x = 0;

            bool burnTick(World w)
            {
                if (enemy.Owner == null || w == null)
                    return true;

                w.BroadcastPacketConditional(new ShowEffect
                {
                    EffectType = EffectType.Dead,
                    TargetObjectId = enemy.Id,
                    Color = new ARGB(0xbd460a)
                }, p => enemy.DistSqr(p) < RadiusSqr);

                if (x % 4 == 0) // make sure to change this if timer delay is changed
                {
                    var thisDmg = perDmg;
                    if (remainingDmg < thisDmg)
                        thisDmg = remainingDmg;

                    enemy.Damage(this, thisDmg, true);
                    remainingDmg -= thisDmg;
                    if (remainingDmg <= 0)
                        return true;
                }
                x++;

                tmr.Reset();
                return false;
            }

            tmr = new WorldTimer(250, burnTick);
            world.Timers.Add(tmr);
        }

        private static void HealingPlayersPoison(World world, Player player, ActivateEffect eff)
        {
            var remainingHeal = eff.TotalDamage;
            var perHeal = eff.TotalDamage * 1000 / eff.DurationMS;

            WorldTimer tmr = null;
            var x = 0;

            bool healTick(World w)
            {
                if (player.Owner == null || w == null)
                    return true;

                if (x % 4 == 0) // make sure to change this if timer delay is changed
                {
                    var thisHeal = perHeal;
                    if (remainingHeal < thisHeal)
                        thisHeal = remainingHeal;

                    var pkts = new List<Packet>();

                    ActivateHealHp(player, thisHeal, pkts);
                    w.BroadcastPackets(pkts, null);
                    remainingHeal -= thisHeal;
                    if (remainingHeal <= 0)
                        return true;
                }
                x++;

                tmr.Reset();
                return false;
            }

            tmr = new WorldTimer(250, healTick);
            world.Timers.Add(tmr);
        }

        private void AERandomCurrency(Item item, ActivateEffect eff)
        {
            if (Owner is Test || Client.Account == null)
                return;


            if (eff.RandVals.Length <= 0)
                Program.Debug(typeof(Player), $"ActivateEffect 'RandomCurrency' was attempted to be called with no random values set. Item: '{item.ObjectId}'", true);

            var values = Array.ConvertAll(eff.RandVals, int.Parse);
            var value = values[new Random().Next(values.Length)];
            switch (eff.CurrencyType.ToLower())
            {
                case "sacred fragments":
                    Client.Manager.Database.UpdateSorStorage(Client.Account, value);
                    SorStorage += value;
                    this.ForceUpdate(SorStorage);
                    break;

                case "kantos":
                    Client.Manager.Database.UpdateKantos(Client.Account, value);
                    Kantos += value;
                    this.ForceUpdate(Kantos);
                    break;

                case "onrane":
                    Client.Manager.Database.UpdateOnrane(Client.Account, value);
                    Onrane += value;
                    this.ForceUpdate(Onrane);
                    break;

                case "gold":
                    Client.Manager.Database.UpdateCredit(Client.Account, value);
                    Credits += value;
                    this.ForceUpdate(Credits);
                    break;

                case "fame":
                    Client.Manager.Database.UpdateFame(Client.Account, value);
                    Fame += value;
                    this.ForceUpdate(Fame);
                    break;

                default: return;
            }
            SendInfo($"You have obtained {value} {eff.CurrencyType}.");
        }

        private int UseAtkMod(int value)
        {
            int totalAttack = Stats[2];

            if (totalAttack < 40)
                return value;
                
            double n = Math.Floor(value + (value * totalAttack / 75.0));

            return (int)n;
        }

        private float UseWisMod(float value)
        {
            float totalWisdom = Stats[7];

            if (totalWisdom < 30)
                return value;
                
            float n = value + (value * totalWisdom / 50f);

            return n;
        }

        private int UseWisMod(int value)
        {
            double totalWisdom = Stats[7];

            if (totalWisdom < 30)
                return value;
                
            double n = Math.Floor(value + (value * totalWisdom / 50.0));

            return (int)n;
        }
    }
}
