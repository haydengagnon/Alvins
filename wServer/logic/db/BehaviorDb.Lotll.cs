using common.resources;
using StackExchange.Redis;
using wServer.logic.behaviors;
using wServer.logic.loot;
using wServer.logic.transitions;

namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ Lotll = () => Behav()
        .Init("Lord of the Lost Lands",
                new State(
                    new ScaleHP(1),
                    new ConditionalEffect(ConditionEffectIndex.Armored, true),
                    new DropPortalOnDeath("Lost Halls Portal", 0.33, timeout: 60),
                    new HpLessTransition(0.15, "IMDONELIKESOOOODONE!"),
                    new State("timetogeticey",
                        new PlayerWithinTransition(8, "announce")
                        ),
                    new State("announce",
                        new Announce("A Lord of the Lost Lands has been discovered in the Realm!"),
                        new TimedTransition(1000, "startupandfireup")
                        ),
                    new State("startupandfireup",
                        new SetAltTexture(0),
                        new Wander(0.3),
                        new Shoot(10, count: 7, shootAngle: 7, coolDownOffset: 1100, angleOffset: 270, coolDown: 2250),
                        new Shoot(10, count: 7, shootAngle: 7, coolDownOffset: 1100, angleOffset: 90, coolDown: 2250),
                        new Shoot(10, count: 7, shootAngle: 7, coolDown: 2250),
                        new Shoot(10, count: 7, shootAngle: 7, angleOffset: 180, coolDown: 2250),
                        new TimedTransition(10000, "GatherUp")
                        ),
                    new State("GatherUp",
                        new SetAltTexture(3),
                        new Taunt("GATHERING POWER!"),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Shoot(8.4, count: 6, shootAngle: 60, projectileIndex: 1, coolDown: 4550),
                        new Shoot(8.4, count: 6, shootAngle: 60, predictive: 2, projectileIndex: 1, coolDown: 2700),
                        new TimedTransition(5750, "protect")
                        ),
                    new State("protect",
                        //Minions spawn
                        new ConditionalEffect(ConditionEffectIndex.StunImmune),
                        new TossObject("Guardian of the Lost Lands", 5, 0, coolDown: 9999999),
                        new TossObject("Guardian of the Lost Lands", 5, 90, coolDown: 9999999),
                        new TossObject("Guardian of the Lost Lands", 5, 180, coolDown: 9999999),
                        new TossObject("Guardian of the Lost Lands", 5, 270, coolDown: 9999999),
                        new TimedTransition(1000, "crystals")
                        ),
                    new State("crystals",
                        new SetAltTexture(1),
                        new ConditionalEffect(ConditionEffectIndex.StunImmune),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new TossObject("Protection Crystal", 4, 0, coolDown: 9999999),
                        new TossObject("Protection Crystal", 4, 45, coolDown: 9999999),
                        new TossObject("Protection Crystal", 4, 90, coolDown: 9999999),
                        new TossObject("Protection Crystal", 4, 135, coolDown: 9999999),
                        new TossObject("Protection Crystal", 4, 180, coolDown: 9999999),
                        new TossObject("Protection Crystal", 4, 225, coolDown: 9999999),
                        new TossObject("Protection Crystal", 4, 270, coolDown: 9999999),
                        new TossObject("Protection Crystal", 4, 315, coolDown: 9999999),
                        new TimedTransition(2100, "checkforcrystals")
                        ),
                    new State("checkforcrystals",
                        new SetAltTexture(1),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new EntitiesNotExistsTransition(9999, "startupandfireup", "Protection Crystal")
                        ),
                    new State("IMDONELIKESOOOODONE!",
                        new Taunt("NOOOOOOOOOOOOOOO!"),
                        new SetAltTexture(3),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                        new Flash(0xFF0000, 0.2, 3),
                        new TimedTransition(5250, "dead")
                        ),
                    new State("dead",
                        new Shoot(8.4, count: 6, shootAngle: 60, projectileIndex: 1),
                        new Suicide()
                        )
                    ),
                new MostDamagers(3,
                    LootTemplates.StatPots()
                ),
                new MostDamagers(3,
                    LootTemplates.Sor3Perc()
                    ),
                new Threshold(0.01,
                    new ItemLoot("Sacred Essence", 0.0005),
                    new ItemLoot("Legendary Essence", 0.0005),
                    new ItemLoot("Shield of Ogmur", 0.02),
                    new TierLoot(11, ItemType.Weapon, 0.15),
                    new TierLoot(11, ItemType.Armor, 0.15),
                    new TierLoot(12, ItemType.Armor, 0.05),
                    new TierLoot(12, ItemType.Weapon, 0.05),
                    new ItemLoot("Potion of Life", 0.2),
                    new ItemLoot("Potion of Mana", 0.2)
                )
            )
            .Init("Protection Crystal",
                new State(
                    new Prioritize(
                        new Orbit(0.3, 4, 10, "Lord of the Lost Lands")
                        ),
                    new Shoot(8, count: 4, shootAngle: 7, coolDown: 500)
                    )
            )
            .Init("Guardian of the Lost Lands",
                new State(
                    new State("Full",
                        new Spawn("Knight of the Lost Lands", 2, 1, coolDown: 4000),
                        new Prioritize(
                            new Follow(0.6, 20, 6),
                            new Wander(0.2)
                            ),
                        new Shoot(10, count: 8, fixedAngle: 360 / 8, coolDown: 3000, projectileIndex: 1),
                        new Shoot(10, count: 5, shootAngle: 10, coolDown: 1500),
                        new HpLessTransition(0.25, "Low")
                        ),
                    new State("Low",
                        new Prioritize(
                            new StayBack(0.6, 5),
                            new Wander(0.2)
                            ),
                        new Shoot(10, count: 8, fixedAngle: 360 / 8, coolDown: 3000, projectileIndex: 1),
                        new Shoot(10, count: 5, shootAngle: 10, coolDown: 1500)
                        )
                    ),
                new ItemLoot("Health Potion", 0.1),
                new ItemLoot("Magic Potion", 0.1)
            )
            .Init("Knight of the Lost Lands",
                new State(
                    new Prioritize(
                        new Follow(1, 20, 4),
                        new StayBack(0.5, 2),
                        new Wander(0.3)
                        ),
                    new Shoot(13, 1, coolDown: 700)
                    ),
                new ItemLoot("Health Potion", 0.1),
                new ItemLoot("Magic Potion", 0.1)
            )
            ;
    }
}