using common.resources;
using wServer.logic.behaviors;
using wServer.logic.loot;
using wServer.logic.transitions;
//asdfasdfasdf
namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ BloodMage = () => Behav()
#region zaragon
            .Init("Blood Boss Anchor",
                new State(
                    new ConditionalEffect(ConditionEffectIndex.Invincible)
                    )
                )

            .Init("Stone of Blood 1",
                new State(
                    new Shoot(10, count: 4, projectileIndex: 0, coolDown: 400)
                    )
                )

            .Init("Zaragon, the Blood Mage",
                new State(
                    new ScaleHP(0.5),
                    new Orbit(0.3, 3, target: "Blood Boss Anchor"),
                    new HpLessTransition(0.18, "Dead1"),
                    new State("swag",
                        new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                        new PlayerWithinTransition(8, "Waiting")
                        ),
                    new State("Waiting",
                        new Flash(0x00FF00, 1, 2),
                        new Taunt("No one but me shall seal your fate, {PLAYER}."),
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new TimedTransition(8000, "Sentry1")
                        ),
                    new State(
                        new Shoot(8.4, count: 8, projectileIndex: 3, coolDown: 400),
                        new State("Sentry1",
                            new Flash(0x00FFFF, 1, 2),
                            new ConditionalEffect(ConditionEffectIndex.StunImmune),
                            new RemoveEntity(99, "Stone of Blood 1"),
                            new Shoot(8.4, count: 6, shootAngle: 10, projectileIndex: 0, coolDown: 400),
                            new Shoot(8.4, count: 3, shootAngle: 10, projectileIndex: 0, predictive: 2, coolDown: 400, coolDownOffset: 3000),
                            new Shoot(8.4, count: 1, projectileIndex: 1, coolDown: 2000),
                            new TimedTransition(8000, "Sentry2")
                            ),
                        new State("Sentry2",
                            new TossObject("Stone of Blood 1", range: 8, coolDown: 2000),
                            new Shoot(8.4, count: 10, projectileIndex: 1, coolDown: 3000),
                            new Shoot(8.4, count: 10, shootAngle: 30, projectileIndex: 0, predictive: 1, coolDown: 1000, coolDownOffset: 2000),
                            new TimedTransition(8000, "Sentry3")
                            ),
                        new State("Sentry3",
                            new Flash(0x00FFFF, 1, 2),
                            new ConditionalEffect(ConditionEffectIndex.StunImmune),
                            new Shoot(8.4, count: 18, projectileIndex: 0, coolDown: 3000),
                            new Shoot(8.4, count: 5, shootAngle: 5, projectileIndex: 2, coolDown: 400),
                            new TimedTransition(8000, "Sentry4")
                            ),
                        new State("Sentry4",
                            new ConditionalEffect(ConditionEffectIndex.Armored),
                            new Grenade(3, 180, range: 7),
                            new Shoot(8.4, count: 12, projectileIndex: 2, coolDown: 2000),
                            new Shoot(8.4, count: 3, shootAngle: 16, projectileIndex: 2, coolDown: 400),
                            new TimedTransition(8000, "Sentry5")
                            ),
                        new State("Sentry5",
                            new Flash(0x00FFFF, 1, 2),
                            new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                            new Shoot(8.4, count: 10, projectileIndex: 0, coolDown: 2000),
                            new Shoot(8.4, count: 1, projectileIndex: 1, coolDown: 400),
                            new TimedTransition(5000, "spiral")
                            ),
                        new State("spiral",
                            new Flash(0x00FFFF, 1, 2),
                            new ConditionalEffect(ConditionEffectIndex.Armored),
                            new ConditionalEffect(ConditionEffectIndex.StunImmune),
                            new Shoot(8.4, count: 2, shootAngle: 60, projectileIndex: 1, coolDown: 600),
                            new Sequence(
                                new SpiralShoot(15, 4, 6, projectileIndex: 4, range: 20, coolDown: 300),
                                new SpiralShoot(-15, 4, 6, fixedAngle: 45, projectileIndex: 4, range: 20, coolDown: 300)
                                ),
                            new TimedTransition(7000, "Sentry1")
                            )
                        ),
                    new State("Dead1",
                        new RemoveEntity(99, "Blood Boss Anchor"),
                        new Taunt("Finally, rest."),
                        new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                        new Flash(0x0000FF, 0.2, 3),
                        new TimedTransition(3250, "dead")
                        ),
                    new State("dead",
                        new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                        new DropPortalOnDeath("The Catacombs Portal"),
                        new Suicide()
                        )
                    ),
                new MostDamagers(3,
                    LootTemplates.Sor3Perc()
                    ),
                new Threshold(0.01,
                    new ItemLoot("Sacred Essence", 0.0005),
                    new ItemLoot("Legendary Essence", 0.0005),
                    new ItemLoot("Potion of Vitality", 0.20),
                    new ItemLoot("Bloodbath Dagger", 0.02),
                    new ItemLoot("God Blood Robes", 0.01),
                    new ItemLoot("Banner of the Blood Mountains", 0.02),
                    new ItemLoot("Titan Blood Armor", 0.02),
                    new TierLoot(8, ItemType.Weapon, 0.3),
                    new TierLoot(9, ItemType.Weapon, 0.275),
                    new TierLoot(10, ItemType.Weapon, 0.225),
                    new TierLoot(11, ItemType.Weapon, 0.08),
                    new TierLoot(8, ItemType.Armor, 0.2),
                    new TierLoot(9, ItemType.Armor, 0.175),
                    new TierLoot(10, ItemType.Armor, 0.15),
                    new TierLoot(11, ItemType.Armor, 0.1),
                    new TierLoot(12, ItemType.Armor, 0.05),
                    new TierLoot(4, ItemType.Ability, 0.15),
                    new TierLoot(5, ItemType.Ability, 0.1),
                    new TierLoot(5, ItemType.Ring, 0.05)
                    )
                );
#endregion zaragon
    }
}