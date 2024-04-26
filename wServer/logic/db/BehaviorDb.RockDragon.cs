using common.resources;
using wServer.logic.behaviors;
using wServer.logic.loot;
using wServer.logic.transitions;

namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ RockDragon = () => Behav()
            .Init("Devourer of Gods",
                new State(
                    new DropPortalOnDeath("SPACE3 portal", 1),
                    new State("default",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new PlayerWithinTransition(15, "spawnbody")
                        ),
                    new State("spawnbody",
                        new ChangeSize(60, 120),
                        new SetAltTexture(0),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Spawn("Body Segment A", 1, 1, coolDown: 99999),
                        new Spawn("Body Segment B", 1, 1, coolDown: 99999),
                        new Spawn("Body Segment C", 1, 1, coolDown: 99999),
                        new Spawn("Body Segment D", 1, 1, coolDown: 99999),
                        new Spawn("Body Segment E", 1, 1, coolDown: 99999),
                        new Spawn("Body Segment F", 1, 1, coolDown: 99999),
                        new Spawn("Body Segment G", 1, 1, coolDown: 99999),
                        new Spawn("Body Segment H", 1, 1, coolDown: 99999),
                        new Spawn("Body Segment I", 1, 1, coolDown: 99999),
                        new Spawn("Body Segment Tail", 1, 1, coolDown: 99999),
                        new TimedTransition(400, "weirdmovement")
                        ),
                    new State("weirdmovement",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Sequence(
                            new Timed(5000,
                            new Prioritize(
                                new Swirl(2.9, 8, targeted: false),
                                new Wander(1.2)
                                    )
                                ),
                            new Timed(2500,
                                new Prioritize(
                                    new Follow(2.9, 99, 0, coolDown: 8000)
                                    )
                                )
                            ),
                        new Shoot(5, count: 10, shootAngle: 10, projectileIndex: 1, coolDown: 100),
                        new EntitiesNotExistsTransition(9999, "vulnerable", "Body Segment A", "Body Segment B", "Body Segment C", "Body Segment D", "Body Segment E", "Body Segment F", "Body Segment G", "Body Segment H", "Body Segment I")
                        ),
                    new State("vulnerable",
                        new ChangeSize(5, 165),
                        new RemoveEntity(9999, "Body Segment Tail"),
                        new Sequence(
                            new Timed(1250,
                                new ReturnToSpawn(0.95)
                                ),
                            new Timed(1700,
                                new BackAndForth(1.2, 5)
                                ),
                            new Timed(2000,
                                new Prioritize(
                                    new Follow(1.1, 11, 1),
                                    new StayCloseToSpawn(0.95, 8)
                                    )
                                )
                            ),
                        new Shoot(8, count: 10, projectileIndex: 3, coolDown: 400),
                        new Shoot(10, 10, projectileIndex: 2, coolDown: 400)
                        )
                    ),
                new Threshold(0.00001,
                new ItemLoot("Legendsary Token 2", 1 / 1f),
                new ItemLoot("Legendary sparlking shiald", 1 / 250f),
                new ItemLoot("cosmos is gay", 1 / 250f),
                new ItemLoot("worm staff", 1 / 75f),
                new ItemLoot("Vexed", 1 / 75f),
                new ItemLoot("devourer of arrows", 1 / 75f),
                new ItemLoot("gem helm", 1 / 75f),
                new ItemLoot("trans pride!!!!!!!!!!", 1 / 75f),
                new ItemLoot("terrarian hide", 1 / 75f),
                new ItemLoot("5000 Fame", 1 / 1f),
                new ItemLoot("vacuum PURPLE", 1 / 75f),
                new ItemLoot("calamity gem", 1 / 75f),
                //new ItemLoot("T3 UNLOCKER", 1 / 200f),
                new ItemLoot("Greater Potion of Life", 1),
                new ItemLoot("Greater Potion of Mana", 1),
                new ItemLoot("Greater Potion of Vitality", 1),
                new ItemLoot("Greater Potion of Dexterity", 1),
                new ItemLoot("Greater Potion of Speed", 1),
                new ItemLoot("Greater Potion of Attack", 1),
                new ItemLoot("Greater Potion of Defense", 1),
                new ItemLoot("Greater Potion of Wisdom", 1)
                )
            )
        .Init("space2ground",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                new State("1",
                    new GroundTransform("devourer", 100, persist: true),
                    new TimedTransition(900, "2")
                    ),
                new State("2",
                    new GroundTransform("devourertwo", 100, persist: true),
                    new TimedTransition(900, "3")
                    ),
                new State("3",
                    new GroundTransform("devourerthree", 100, persist: true),
                    new TimedTransition(900, "4")
                    ),
                new State("4",
                    new GroundTransform("devourertwo", 100, persist: true),
                    new TimedTransition(900, "1")
                    )
                )
            )
                .Init("Body Segment Bomb",
                    new State(
                    new ScaleHP(0.05),
                    new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                      new State("BoutToBlow",
                      new TimedTransition(1000, "bom")
                        ),
                    new State("bom",
                       new Shoot(99, shootAngle: 60, count: 6, projectileIndex: 0),
                       new Suicide()
                    )))
       .Init("Body Segment A",
             new State(
                 new ScaleHP(0.05),
                new Shoot(99, 1, null, 0, coolDown: 600),
                new TransformOnDeath("Body Segment Bomb", 1, 1, 1),
                new State("go",
                   new Protect(3, "Devourer of Gods", protectionRange: 2)
                    )
                 )
              )
         .Init("Body Segment B",
             new State(
                 new ScaleHP(0.05),
            new Shoot(99, 1, null, 0, coolDown: 700),
              new TransformOnDeath("Body Segment Bomb", 1, 1, 1),
               new State("go",
                   new Protect(3, "Body Segment A", protectionRange: 2),
                   new EntitiesNotExistsTransition(9999, "2plan", "Body Segment A")
                        ),
               new State("2plan",
                   new Protect(3, "Devourer of Gods", protectionRange: 2)
                        )
                    )
                )
         .Init("Body Segment C",
             new State(
                 new ScaleHP(0.05),
              new Shoot(99, 1, null, 0, coolDown: 800),
              new TransformOnDeath("Body Segment Bomb", 1, 1, 1),
               new State("go",
                   new Protect(3, "Body Segment B", protectionRange: 2),
                   new EntitiesNotExistsTransition(9999, "2plan", "Body Segment B")
                        ),
               new State("2plan",
                   new Protect(3, "Devourer of Gods", protectionRange: 2)
                        )
                     )
                )
         .Init("Body Segment D",
             new State(
                 new ScaleHP(0.05),
            new Shoot(99, 1, null, 0, coolDown: 900),
              new TransformOnDeath("Body Segment Bomb", 1, 1, 1),
               new State("go",
                   new Protect(3, "Body Segment C", protectionRange: 2),
                   new EntitiesNotExistsTransition(9999, "2plan", "Body Segment C")
                        ),
               new State("2plan",
                   new Protect(3, "Devourer of Gods", protectionRange: 2)
                        )
                 )
            )
         .Init("Body Segment E",
             new State(
                 new ScaleHP(0.05),
            new Shoot(99, 1, null, 0, coolDown: 1000),
              new TransformOnDeath("Body Segment Bomb", 1, 1, 1),
               new State("go",
                   new Protect(3, "Body Segment D", protectionRange: 2),
                   new EntitiesNotExistsTransition(9999, "2plan", "Body Segment D")
                        ),
               new State("2plan",
                   new Protect(3, "Devourer of Gods", protectionRange: 2)
                        )
                 )
            )
         .Init("Body Segment F",
             new State(
                 new ScaleHP(0.05),
            new Shoot(99, 1, null, 0, coolDown: 1100),
              new TransformOnDeath("Body Segment Bomb", 1, 1, 1),
               new State("go",
                   new Protect(3, "Body Segment E", protectionRange: 2),
                   new EntitiesNotExistsTransition(9999, "2plan", "Body Segment E")
                        ),
               new State("2plan",
                   new Protect(3, "Devourer of Gods", protectionRange: 2)
                        )
                 )
            )
         .Init("Body Segment G",
             new State(
                 new ScaleHP(0.05),
            new Shoot(99, 1, null, 0, coolDown: 1200),
              new TransformOnDeath("Body Segment Bomb", 1, 1, 1),
               new State("go",
                   new Protect(3, "Body Segment E", protectionRange: 2),
                   new EntitiesNotExistsTransition(9999, "2plan", "Body Segment F")
                        ),
               new State("2plan",
                   new Protect(3, "Devourer of Gods", protectionRange: 2)
                        )
                 )
            )
         .Init("Body Segment H",
             new State(
                 new ScaleHP(0.05),
            new Shoot(99, 1, null, 0, coolDown: 1300),
              new TransformOnDeath("Body Segment Bomb", 1, 1, 1),
               new State("go",
                   new Protect(3, "Body Segment G", protectionRange: 2),
                   new EntitiesNotExistsTransition(9999, "2plan", "Body Segment G")
                        ),
               new State("2plan",
                   new Protect(3, "Devourer of Gods", protectionRange: 2)
                        )
                 )
            )
         .Init("Body Segment I",
             new State(
                 new ScaleHP(0.05),
            new Shoot(99, 1, null, 0, coolDown: 1400),
              new TransformOnDeath("Body Segment Bomb", 1, 1, 1),
               new State("go",
                   new Protect(3, "Body Segment H", protectionRange: 2),
                   new EntitiesNotExistsTransition(9999, "2plan", "Body Segment H")
                        ),
               new State("2plan",
                   new Protect(3, "Devourer of Gods", protectionRange: 2)
                        )
                 )
            )
        .Init("Body Segment Tail",
             new State(
                 new ScaleHP(0.05),
            new Shoot(99, 1, null, 0, coolDown: 1500),
              new ConditionalEffect(ConditionEffectIndex.Invulnerable),
               new State("go",
                   new Protect(3, "Body Segment I", protectionRange: 2),
                   new EntitiesNotExistsTransition(9999, "2plan", "Body Segment I")
                        ),
               new State("2plan",
                   new Protect(3, "Devourer of Gods", protectionRange: 2)
                        )
                 )
            )
           .Init("Rock Dragon Bat",
                    new State(
                      new State("BoutToBlow",
                      new Prioritize(
                        new Follow(0.5, 8, 1),
                        new Wander(0.2)
                        ),
                      new Shoot(7, count: 3, shootAngle: 8, projectileIndex: 3, coolDown: 1300),
                      new HpLessTransition(0.11, "bom"),
                      new TimedTransition(5500, "bom")
                        ),
                    new State("bom",
                       new Shoot(8.4, count: 7, projectileIndex: 2),
                       new Suicide()
                    )))
        ;
    }
}