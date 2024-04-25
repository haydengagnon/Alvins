using common.resources;
using wServer.logic.behaviors;
using wServer.logic.loot;
using wServer.logic.transitions;
using wServer.realm.cores;

namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ RotfAsgard = () => Behav()

            .Init("ASG Heimdallr",
                new State(
                    new ScaleDEF(0.2),
                    new ScaleHP(1),

                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                    new State("Waiting Player",

                        new PlayerWithinTransition(15, "Start", false)
                    ),

                    new State("Start",
                        new Taunt("you not be here ..Banana."),
                        new Flash(0xFF0000, 1, 3),

                        new TimedTransition(3000, "Start Shooting")
                    ),

                    new State("Start Shooting",
                        new RemoveConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Shoot(20, 8, fixedAngle: 0, projectileIndex: 2, coolDownOffset: 0, coolDown: 100,
                            rotateAngle: 11),

                        new HpLessTransition(0.75, "Second Phase")
                    ),

                    new State("Second Phase",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable, false, 3000),

                        new Taunt(
                            "bisexual BLAST."),

                        new TimedTransition(3000, "Second Phase Start")
                    ),

                    new State("Second Phase Start",
                        new Wander(0.5),
                        new Shoot(10, 8, projectileIndex: 3, fixedAngle: 48, coolDownOffset: 400, coolDown: 1000),
                        new Shoot(10, 8, projectileIndex: 2, fixedAngle: 72, coolDownOffset: 600, coolDown: 1000),
                        new Shoot(10, 8, projectileIndex: 1, fixedAngle: 96, coolDownOffset: 800, coolDown: 1000),
                        new Shoot(20, 6, shootAngle: 15, projectileIndex: 0, coolDown: 600, coolDownOffset: 100),

                        new HpLessTransition(0.50, "Third Phase")
                    ),

                    new State("Third Phase",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable, false, 3000),
                        new Flash(0xFF0000, 1, 3),
                        new Taunt("Your Attacts not hurt me!"),

                        new TimedTransition(3000, "Third Phase Start")
                    ),

                    new State("Third Phase Start",
                        new Shoot(20, 7, fixedAngle: 0, projectileIndex: 2, coolDownOffset: 0, coolDown: 2000,
                            rotateAngle: 5),

                        new HpLessTransition(0.25, "Rage")
                    ),

                    new State("Rage",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable, false, 1400),
                        new Flash(0xFF0000, 1, 3),
                        new Taunt("myst what the fuck."),

                        new TimedTransition(3000, "Rage Start")
                    ),

                    new State("Rage Start",
                        new Wander(0.5),
                        new Shoot(20, 26, fixedAngle: 22.5, projectileIndex: 3, coolDown: 2000)
                    )
                ),
                new Threshold(0.001,
                    new TierLoot(12, ItemType.Weapon, 0.07),
                    new TierLoot(13, ItemType.Armor, 0.07),
                    new TierLoot(6, ItemType.Ring, 0.07),
                    new TierLoot(6, ItemType.Ability, 0.07),
                    new ItemLoot("Potion of Attack", 1),
                    new ItemLoot("Potion of Defense", 1),
                    //new ItemLoot("Music of the Gods", 1 / 225f),
                    new ItemLoot("Gjajjobingo", 1 / 225f),

                    new ItemLoot("load of piss Sword", 1 / 225f)
                )
            )
            .Init("ASG Minion1",
                new State(
                    new Wander(0.5),
                    new Shoot(15, 1, coolDownOffset: 0, coolDown: 1000)
                ),
                new Threshold(0.001,

                    new ItemLoot("Golden Asgardian Helmet", 0.001)
                )
            )
            .Init("ASG Minion2",
                new State(
                    new Wander(0.5),
                    new Shoot(15, 1, coolDownOffset: 500, coolDown: 1000),
                    new Shoot(15, 1, coolDownOffset: 500, coolDown: 1000, projectileIndex: 1)
                ),
                new Threshold(0.001,

                    new ItemLoot("Platinum Asgardian Helmet", 0.001)
                )
            )
            .Init("ASG Minion3",
                new State(
                    new Wander(0.5),
                    new Shoot(15, 1, coolDownOffset: 500, coolDown: 2000),
                    new Shoot(15, 1, coolDownOffset: 500, coolDown: 2000, projectileIndex: 1),
                    new Shoot(15, 1, coolDownOffset: 500, coolDown: 2000, projectileIndex: 2)
                ),
                new Threshold(0.001,

                    new ItemLoot("Obsidian Asgardian Helmet", 0.001)
                )
            )


            .Init("ASG Odin",
                new State(
                    new ScaleDEF(0.2),
                    new ScaleHP(1),

                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                    new State("Waiting Player",

                        new PlayerWithinTransition(15, "Start", false)
                    ),

                    new State("Start",
                        new Taunt("interupt Groon sesh... prepare to die!"),
                        new Flash(0xFF0000, 1, 3),

                        new TimedTransition(3000, "Start Shooting")
                    ),

                    new State("Start Shooting",
                        new RemoveConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Follow(0.3, 20, 0),
                        new Shoot(20, 2, shootAngle: 30, projectileIndex: 0, coolDownOffset: 0, coolDown: 800),
                        new Shoot(20, 2, shootAngle: 80, projectileIndex: 0, coolDownOffset: 0, coolDown: 800),
                        new Shoot(20, 2, shootAngle: -30, projectileIndex: 0, coolDownOffset: 0, coolDown: 800),
                        new Shoot(20, 2, shootAngle: -80, projectileIndex: 0, coolDownOffset: 0, coolDown: 800),
                        new Shoot(20, 6, 15, 1, coolDown: 1200, coolDownOffset: 500),

                        new TimedTransition(5000, "Second Phase")
                    ),

                    new State("Second Phase",
                        new ReturnToSpawn(0.5),
                        new Shoot(20, 2, shootAngle: 30, projectileIndex: 0, coolDownOffset: 0, coolDown: 800),
                        new Shoot(20, 2, shootAngle: 80, projectileIndex: 0, coolDownOffset: 0, coolDown: 800),
                        new Shoot(20, 2, shootAngle: -30, projectileIndex: 0, coolDownOffset: 0, coolDown: 800),
                        new Shoot(20, 2, shootAngle: -80, projectileIndex: 0, coolDownOffset: 0, coolDown: 800),
                        new Shoot(20, 6, 15, 1, coolDown: 1400, coolDownOffset: 500),
                        new Taunt("Defeat the mortals."),

                        new TimedTransition(2000, "Second Phase Start")
                    ),

                    new State("Second Phase Start",
                        new Taunt("you will be GOONED!"),
                        new Follow(0.3, 20, 0),
                        new Shoot(20, 2, shootAngle: 30, projectileIndex: 0, coolDownOffset: 0, coolDown: 800),
                        new Shoot(20, 2, shootAngle: 80, projectileIndex: 0, coolDownOffset: 0, coolDown: 800),
                        new Shoot(20, 2, shootAngle: -30, projectileIndex: 0, coolDownOffset: 0, coolDown: 800),
                        new Shoot(20, 2, shootAngle: -80, projectileIndex: 0, coolDownOffset: 0, coolDown: 800),
                        new Shoot(20, 6, 15, 1, coolDown: 2000, coolDownOffset: 500),

                        new TimedTransition(5000, "Third Phase")
                    ),

                    new State("Third Phase",
                        new Flash(0xFF0000, 1, 3),
                        new Shoot(20, 2, shootAngle: 30, projectileIndex: 0, coolDownOffset: 0, coolDown: 800),
                        new Shoot(20, 2, shootAngle: 80, projectileIndex: 0, coolDownOffset: 0, coolDown: 800),
                        new Shoot(20, 2, shootAngle: -30, projectileIndex: 0, coolDownOffset: 0, coolDown: 800),
                        new Shoot(20, 2, shootAngle: -80, projectileIndex: 0, coolDownOffset: 0, coolDown: 800),
                        new Shoot(20, 6, 15, 1, coolDown: 1400, coolDownOffset: 500),
                        new ReturnToSpawn(0.5),
                        new Taunt("grgohe12!"),

                        new TimedTransition(2000, "Third Phase Start")
                    ),

                    new State("Third Phase Start",
                        new Follow(0.3, 20, 0),
                        new Shoot(20, 2, shootAngle: 30, projectileIndex: 0, coolDownOffset: 0, coolDown: 800),
                        new Shoot(20, 2, shootAngle: 80, projectileIndex: 0, coolDownOffset: 0, coolDown: 800),
                        new Shoot(20, 2, shootAngle: -30, projectileIndex: 0, coolDownOffset: 0, coolDown: 800),
                        new Shoot(20, 2, shootAngle: -80, projectileIndex: 0, coolDownOffset: 0, coolDown: 800),
                        new Shoot(20, 6, 15, 1, coolDown: 1400, coolDownOffset: 500),
                        new Reproduce("ASG Minion1", 20, 5, coolDown: 2000),
                        new Reproduce("ASG Minion2", 20, 5, coolDown: 2000),

                        new TimedTransition(5000, "Fourth Phase")
                    ),

                    new State("Fourth Phase",
                        new Flash(0xFF0000, 1, 3),
                        new Shoot(20, 2, shootAngle: 30, projectileIndex: 0, coolDownOffset: 0, coolDown: 600),
                        new Shoot(20, 2, shootAngle: 80, projectileIndex: 0, coolDownOffset: 0, coolDown: 600),
                        new Shoot(20, 2, shootAngle: -30, projectileIndex: 0, coolDownOffset: 0, coolDown: 800),
                        new Shoot(20, 2, shootAngle: -80, projectileIndex: 0, coolDownOffset: 0, coolDown: 800),
                        new Shoot(20, 6, 15, 1, coolDown: 1400, coolDownOffset: 500),
                        new Reproduce("ASG Minion1", 20, 5, coolDown: 2000),
                        new Reproduce("ASG Minion2", 20, 5, coolDown: 2000),
                        new ReturnToSpawn(0.5),
                        new Taunt("fuck i sent my password"),

                        new TimedTransition(2000, "Fourth Phase Start")
                    ),

                    new State("Fourth Phase Start",
                        new Follow(0.3, 20, 0),
                        new Shoot(20, 2, shootAngle: 30, projectileIndex: 0, coolDownOffset: 0, coolDown: 600),
                        new Shoot(20, 2, shootAngle: 80, projectileIndex: 0, coolDownOffset: 0, coolDown: 600),
                        new Shoot(20, 2, shootAngle: -30, projectileIndex: 0, coolDownOffset: 0, coolDown: 800),
                        new Shoot(20, 2, shootAngle: -80, projectileIndex: 0, coolDownOffset: 0, coolDown: 800),
                        new Shoot(20, 12, 15, 1, coolDown: 1400, coolDownOffset: 500),
                        new Reproduce("ASG Minion1", 20, 5, coolDown: 2000),
                        new Reproduce("ASG Minion2", 20, 5, coolDown: 2000),
                        new TimedTransition(5000, "Third Phase Start"),

                        new HpLessTransition(0.5, "Fifth Phase")
                    ),

                    new State("Fifth Phase",
                        new Flash(0xFF0000, 1, 3),
                        new Shoot(20, 3, shootAngle: 30, projectileIndex: 0, coolDownOffset: 0, coolDown: 600),
                        new Shoot(20, 3, shootAngle: 80, projectileIndex: 0, coolDownOffset: 0, coolDown: 800),
                        new Shoot(20, 3, shootAngle: -30, projectileIndex: 0, coolDownOffset: 0, coolDown: 600),
                        new Shoot(20, 3, shootAngle: -80, projectileIndex: 0, coolDownOffset: 0, coolDown: 800),
                        new Shoot(20, 11, 15, 1, coolDown: 1200, coolDownOffset: 500),
                        new Reproduce("ASG Minion1", 20, 5, coolDown: 2000),
                        new Reproduce("ASG Minion2", 20, 5, coolDown: 2000),
                        new ReturnToSpawn(1),
                        new Taunt("imreally feeling it"),

                        new TimedTransition(2000, "Fifth Phase Start")
                    ),

                    new State("Fifth Phase Start",
                        new Shoot(20, 4, shootAngle: 30, projectileIndex: 0, coolDownOffset: 0, coolDown: 200),
                        new Shoot(20, 4, shootAngle: 80, projectileIndex: 0, coolDownOffset: 0, coolDown: 800),
                        new Shoot(20, 4, shootAngle: -30, projectileIndex: 0, coolDownOffset: 0, coolDown: 1200),
                        new Shoot(20, 4, shootAngle: -80, projectileIndex: 0, coolDownOffset: 0, coolDown: 1200),
                        new Shoot(20, 7, 45, fixedAngle: 0, projectileIndex: 2, coolDown: 150, rotateAngle: 10),
                        new Reproduce("ASG Minion1", 20, 5, coolDown: 2000),
                        new Reproduce("ASG Minion2", 20, 5, coolDown: 2000),

                        new TimedTransition(7500, "Third Phase Start")
                    )
                ),

                new Threshold(0.001,
                    new TierLoot(12, ItemType.Weapon, 0.07),
                    new TierLoot(13, ItemType.Armor, 0.07),
                    new TierLoot(6, ItemType.Ring, 0.07),
                    new TierLoot(6, ItemType.Ability, 0.07),
                    new ItemLoot("Potion of Attack", 1),
                    new ItemLoot("Potion of Dexterity", 1),
                    new ItemLoot("Potion of Life", 1),
                    new ItemLoot("Potion of Mana", 1),
                    new ItemLoot("GCENSORED", 1 / 225f),
                    new ItemLoot("11am Groon sesh Grarbs", 1 / 225f),

                    new ItemLoot("odin's pet cats", 1 / 225f)
                )
            )
            .Init("ASG Thor",
                new State(
                    new ScaleDEF(0.2),
                    new ScaleHP(1),

                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                    new State("Waiting Player",

                        new PlayerWithinTransition(15, "Start", false)
                    ),

                    new State("Start",
                        new Taunt("Hammer."),
                        new Flash(0xFF0000, 1, 3),

                        new TimedTransition(3000, "Start Shooting")
                    ),

                    new State("Start Shooting",
                        new Taunt("Hammer."),
                        new RemoveConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Wander(0.5),
                        new Shoot(20, 12, 30, fixedAngle: 0, projectileIndex: 3, coolDownOffset: 0, coolDown: 600),
                        new Shoot(20, 8, 20, projectileIndex: 1, coolDownOffset: 0, coolDown: 1000),
                        new Shoot(20, 3, 15, projectileIndex: 2, coolDownOffset: 0, coolDown: 1000),

                        new TimedTransition(10000, "Second Phase")
                    ),

                    new State("Second Phase",
                        new ReturnToSpawn(0.5),
                        new Taunt("Hammer."),

                        new TimedTransition(1500, "Second Phase Start")
                    ),

                    new State("Second Phase Start",
                        new Shoot(20, 7, fixedAngle: 0, projectileIndex: 3, coolDownOffset: 0, coolDown: 100,
                            rotateAngle: 5),
                        new Shoot(20, 6, shootAngle: 15, projectileIndex: 4, coolDown: 600, coolDownOffset: 100),

                        new TimedTransition(10000, "Third Phase")
                    ),

                    new State("Third Phase",
                        new Flash(0xFF0000, 1, 3),
                        new ReturnToSpawn(0.5),
                        new Taunt("Hammer."),

                        new TimedTransition(1500, "Third Phase Start")
                    ),

                    new State("Third Phase Start",
                        new Follow(0.5, 20),
                        new Shoot(20, 12, 30, fixedAngle: 0, projectileIndex: 3, coolDownOffset: 0, coolDown: 600),
                        new Shoot(20, 8, 20, projectileIndex: 1, coolDownOffset: 0, coolDown: 1000),
                        new Shoot(20, 3, 15, projectileIndex: 2, coolDownOffset: 0, coolDown: 1000),

                        new TimedTransition(10000, "Second Phase")
                    )
                ),

                new Threshold(0.001,
                    new TierLoot(12, ItemType.Weapon, 0.07),
                    new TierLoot(13, ItemType.Armor, 0.07),
                    new TierLoot(6, ItemType.Ring, 0.07),
                    new TierLoot(6, ItemType.Ability, 0.07),
                    new ItemLoot("Potion of Wisdom", 1),
                    new ItemLoot("Potion of Vitality", 1),
                    new ItemLoot("Potion of Life", 1),
                    new ItemLoot("Potion of Mana", 1),
                    //new ItemLoot("hammer shirts", 1 / 225f),
                    new ItemLoot("my old hammer", 1 / 225f),
                    new ItemLoot("Thor's Breasts", 1 / 225f),

                    new ItemLoot("harrmert of hammers of Thor", 1 / 225f)
                )
            )
            .Init("ASG Loki",
                new State(
                    new ScaleDEF(0.2),
                    new ScaleHP(1),

                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                    new State("Waiting Player",

                        new PlayerWithinTransition(15, "Start", true)
                    ),

                    new State("Start",
                        new Taunt("Hey It's Me! Loki! I heard you're strong."),
                        new Flash(0xFF0000, 1, 3),
                        new Follow(1, 20, 0),

                        new TimedTransition(3000, "Start Shooting")
                    ),

                    new State("Start Shooting",
                        new RemoveConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Follow(1, 20, 0),
                        new Shoot(20, 8, 20, projectileIndex: 0, coolDownOffset: 0, coolDown: 600),

                        new TimedTransition(5000, "Second Phase")
                    ),

                    new State("Second Phase",
                        new Follow(1, 20, 0),
                        new Taunt("Hey It's Me! Loki! I heard you're strong."),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable, false, 1500),
                        new Flash(0xFF0000, 1, 1),

                        new TimedTransition(1500, "Third Phase")
                    ),

                    new State("Third Phase",
                        new Taunt("For my next trick, I will make myself Goku"),
                        new Follow(1, 20, 0),
                        new Shoot(20, 8, 15, projectileIndex: 1, coolDownOffset: 0, coolDown: 200),
                        new Follow(1, 20, 0),

                        new TimedTransition(5000, "Third Phase Start")
                    ),

                    new State("Third Phase Start",
                        new Follow(1, 20, 0),
                        new Taunt("Hey It's Me! Loki! I heard you're strong."),
                        new Shoot(20, 8, 15, projectileIndex: 1, coolDownOffset: 0, coolDown: 600),
                        new Shoot(20, 6, 35, projectileIndex: 2, coolDownOffset: 1000, coolDown: 800),

                        new TimedTransition(7500, "Second Phase")
                    )
                ),

                new Threshold(0.001,
                    new TierLoot(12, ItemType.Weapon, 0.07),
                    new TierLoot(13, ItemType.Armor, 0.07),
                    new TierLoot(6, ItemType.Ring, 0.07),
                    new TierLoot(6, ItemType.Ability, 0.07),
                    new ItemLoot("Potion of Speed", 1),
                    new ItemLoot("Potion of Life", 1),
                    new ItemLoot("Potion of Mana", 1),
                    new ItemLoot("boomerangot2193", 1 / 225f),
                    new ItemLoot("wtf is a raiment", 1 / 225f),
                    new ItemLoot("M Y P R I S M I N B I O", 0.003),

                    new ItemLoot("Loki's Horns", 1 / 225f)
                )
            )
            .Init("ASG Hela",
                new State(
                    new ScaleDEF(0.2),
                    new ScaleHP(1),
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                    new State("Waiting Player",

                        new PlayerWithinTransition(15, "Start", true)
                    ),

                    new State("Start",
                        new Taunt("You have come a long way, just to die!"),
                        new Follow(0.8, 20, 0),

                        new TimedTransition(5000, "Start Shooting")
                    ),

                    new State("Start Shooting",
                        new RemoveConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Follow(0.8, 20, 0),
                        new Shoot(20, 4, 20, projectileIndex: 0, coolDownOffset: 0, coolDown: 600),
                        new Shoot(20, 8, 45, projectileIndex: 1, coolDownOffset: 0, coolDown: 400),

                        new TimedTransition(5000, "Second Phase")
                    ),

                    new State("Second Phase",
                        new Taunt("You start to annoy me! I will now strike harder!"),
                        new Follow(0.8, 20, 0),

                        new TimedTransition(1000, "Third Phase")
                    ),

                    new State("Third Phase",
                        new Follow(0.8, 20, 0),
                        new Shoot(20, 8, 10, projectileIndex: 0, coolDownOffset: 0, coolDown: 200),
                        new Shoot(20, 8, 45, projectileIndex: 1, coolDownOffset: 0, coolDown: 600),

                        new TimedTransition(5000, "Fourth Phase")
                    ),

                    new State("Fourth Phase",
                        new Taunt("Deal with the vermins."),
                        new Reproduce("ASG Minion3", 10, 10, 1000),
                        new Follow(0.8, 20, 0),
                        new Shoot(20, 8, 10, projectileIndex: 0, coolDownOffset: 0, coolDown: 600),
                        new Shoot(20, 6, 35, projectileIndex: 1, coolDownOffset: 0, coolDown: 200),

                        new TimedTransition(5000, "Fifth Phase")
                    ),

                    new State("Fifth Phase",
                        new Taunt("Powered by thousands of souls, instant death."),
                        new Reproduce("ASG Minion3", 10, 10, 1000),
                        new ReturnToSpawn(1),
                        new Shoot(20, 8, 45, projectileIndex: 1, fixedAngle: 0, coolDown: 200, rotateAngle: 10),
                        new Shoot(20, 8, 45, projectileIndex: 2, fixedAngle: 0, coolDown: 200, rotateAngle: 10),
                        new Shoot(20, 8, 45, projectileIndex: 3, fixedAngle: 0, coolDown: 200, rotateAngle: 10),

                        new TimedTransition(2500, "Sixth Phase")
                    ),

                    new State("Sixth Phase",
                        new ReturnToSpawn(1),
                        new Shoot(20, 8, 45, projectileIndex: 3, fixedAngle: 0, coolDown: 300, rotateAngle: 20),
                        new Shoot(20, 6, 25, projectileIndex: 2, coolDownOffset: 0, coolDown: 600),
                        new Shoot(20, 8, 15, projectileIndex: 1, coolDownOffset: 0, coolDown: 800),

                        new TimedTransition(10000, "Start Shooting")
                    )
                ),

                new Threshold(0.001,
                    new TierLoot(12, ItemType.Weapon, 0.07),
                    new TierLoot(13, ItemType.Armor, 0.07),
                    new TierLoot(6, ItemType.Ring, 0.07),
                    new TierLoot(6, ItemType.Ability, 0.07),
                    new ItemLoot("Potion of Wisdom", 1),
                    new ItemLoot("Potion of Vitality", 1),
                    new ItemLoot("Potion of Life", 1),
                    new ItemLoot("Potion of Mana", 1),
                    new ItemLoot("Potion of Attack", 1),
                    new ItemLoot("Potion of Dexterity", 1),
                    new ItemLoot("Potion of Speed", 1),
                    new ItemLoot("Potion of Defense", 1),
                    new ItemLoot("Demonic Power of my OC Hela", 1 / 325f),
                    new ItemLoot("gamer hcair", 1 / 225f),
                    new ItemLoot("hela gamer girl ajr", 1 / 225f),

                    new ItemLoot("Lost Scripture #2", 1 / 75f)
                )
            );

    }






}
