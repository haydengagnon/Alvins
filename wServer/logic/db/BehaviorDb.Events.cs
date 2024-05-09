#region

using common.resources;
using System.Xml.Linq;
using wServer.logic.behaviors;
using wServer.logic.loot;
using wServer.logic.transitions;
using wServer.networking.packets.outgoing;
using wServer.realm.commands;
using wServer.realm.entities;
using wServer.realm.worlds.logic;

#endregion

namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ Events = () => Behav()

        #region stuff
        #region Skull Shrine

            .Init("Skull Shrine",
            new State(
                    new ScaleHP(0.5),
                    new Shoot(25, 9, 10, predictive: 1),
                    new Spawn("Red Flaming Skull", 8, coolDown: 5000),
                    new Spawn("Blue Flaming Skull", 10, coolDown: 1000),
                    new Reproduce("Red Flaming Skull", 10, 8, coolDown: 5000),
                    new Reproduce("Blue Flaming Skull", 10, 10, coolDown: 1000),
                new State("basic",
                    new HpLessTransition(0.4, "immune")
                   ),
                new State("immune",
                    new Flash(0xFF0000, 1, 2),
                    new TimedTransition(3000, "immune2")
                   ),
                new State("immune2",
                    new Flash(0x0000FF, 1, 2),
                    new ConditionalEffect(ConditionEffectIndex.StunImmune)
                   )
                ),
                new MostDamagers(3,
                        LootTemplates.Sor1Perc()
                    ),
              new MostDamagers(3,
                     LootTemplates.StatPots()
                     ),
                new Threshold(0.0001,
                    new TierLoot(10, ItemType.Weapon, 0.2),
                    new TierLoot(11, ItemType.Weapon, 0.1),
                    new TierLoot(12, ItemType.Armor, 0.05),
                    new TierLoot(12, ItemType.Weapon, 0.05),
                    new ItemLoot("Greater Potion of Life", 0.2),
                    new ItemLoot("Greater Potion of Mana", 0.2),
                    new TierLoot(10, ItemType.Armor, 0.2),
                    new TierLoot(11, ItemType.Armor, 0.1),
                    new ItemLoot("Flaming Boomerang", 0.02),
                    new ItemLoot("Sacred Essence", 0.0005),
                    new ItemLoot("Legendary Essence", 0.0005),
                    new ItemLoot("Dagger of Brimstone", 0.02),
                    new ItemLoot("Orb of Conflict", 0.01)
                    )
                )
        #endregion Skull Shrine
        #region 
        .Init("DEEZ NUTS GUY",
            new State(
                new ScaleHP(0.05),
                new State("WAIT",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new PlayerWithinTransition(8, "2w"),
                    new Wander(0.5)
                   ),
                new State("2w",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new TimedTransition(3000, "2")
                    ),
                new State("2",
                    new Flash(0x0000FF, 1, 2),
                    new Taunt("DEEZ NUTS!!"),
                    new Shoot(99, 10, 16, predictive: 0.1, coolDown: 3500),
                    new Wander(0.5),
                    new HpLessTransition(0.5, "3")
                   ),
                new State("3",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new Taunt("GOTTEM"),
                    new SoundPlay(0),
                    new TimedTransition(2000, "dead")
                    ),
                new State("dead",
                    new Suicide()
                   )
                ),
              new MostDamagers(1,
                     LootTemplates.StatPots()
                     ),
                new Threshold(0.0001,
                    new ItemLoot("Spaceship Token", 1 / 1000f),
                    new ItemLoot("raindow of Maxy", 1 / 25f),
                    new ItemLoot("Potion of Vitality", 0.5),
                    new ItemLoot("beach Key", 1 / 15f),

                    new ItemLoot("Potion of Dexterity", 0.5),
                    new ItemLoot("Potion of Speed", 0.5),
                    new ItemLoot("Potion of Attack", 0.5),
                    new ItemLoot("Potion of Defense", 0.5),
                    new ItemLoot("Potion of Wisdom", 0.5),
                    new ItemLoot("DEEZ NUTS PHONE ROUND", 1 / 50f),
                    new ItemLoot("eggoblade", 1 / 50f),
                    new ItemLoot("Orb of Conflict", 1 / 10000f),
                    new ItemLoot("Dreadcull T1 Upgrade", 1 / 300f),
                    new ItemLoot("HEAD noob", 1 / 50f),
                    new ItemLoot("NUT SWORD", 1 / 50f)
                    )
                )
        .Init("Puggsy Monkey",
            new State(
                new ScaleHP(0.05),
                new State("1",
                    new Wander(0.1),
                    new Shoot(15, 3, 15, 0, coolDown: 2500, coolDownOffset: 2500),
                    new Shoot(15, 3, 15, 0, coolDown: 2500, coolDownOffset: 3000),
                    new Shoot(15, 3, 15, 0, coolDown: 2500, coolDownOffset: 3700)
                    )
                ),
            new Threshold(0.0001,
                new ItemLoot("Quiver of Thunder", 1 / 10f),
                new ItemLoot("Legendsary Token 4", 0.01)
            )
        )
        .Init("Banan PHON",
            new State(
                new ScaleHP(0.05),
                new StayCloseToSpawn(2, 8),
                new State("WAIT",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new PlayerWithinTransition(8, "2w"),
                    new Wander(0.5)
                   ),
                new State("2w",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new TimedTransition(3000, "2")
                    ),
                new State("2",
                    new Taunt("ring ring ring ! BAANA PHONE"),
                    new Shoot(99, 10, 16, predictive: 0.1, coolDown: 2500),
                    new Wander(0.5),
                    new HpLessTransition(0.4, "dead")
                   ),
                new State("3",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new Taunt("GOTTEM"),
                    new TimedTransition(2000, "dead")
                    ),
                new State("dead",
                    new Suicide()
                   )
                ),
              new MostDamagers(1,
                     LootTemplates.StatPots()
                     ),
                new Threshold(0.0001,
                    new ItemLoot("raindow of Maxy", 1 / 25f),
                    new ItemLoot("Potion of Vitality", 0.5),
                    new ItemLoot("Potion of Dexterity", 0.5),
                    new ItemLoot("Potion of Speed", 0.5),
                    new ItemLoot("Potion of Attack", 0.5),
                    new ItemLoot("Potion of Defense", 0.5),
                    new ItemLoot("Potion of Wisdom", 0.5),
                    new ItemLoot("banana armor", 1 / 75f),
                    new ItemLoot("beach Key", 1 / 15f),

                    new ItemLoot("BANANA BOW", 1 / 50f),
                    new ItemLoot("Spaceship Token", 1 / 1000f),
                    new ItemLoot("Spiritclaw", 1 / 1000f),
                    new ItemLoot("Bonnea daggers", 1 / 150f),
                    new ItemLoot("Dreadcull T1 Upgrade", 1 / 300f),
                    new ItemLoot("Hlemet of the PHONE", 1/ 50f)
                    )
                )
        .Init("Deez Nuts Spawner",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new State("1",
                        new Spawn("DEEZ NUTS GUY", 1, 1, 1000000),
                        new TimedTransition(1000, "2")
                        ),
                    new State("2",
                        new EntityNotExistsTransition("DEEZ NUTS GUY", 999, "3")
                        ),
                    new State("3",
                        new TimedTransition(6000, "1")
                        )
                    )
                )
        .Init("Banana Spawner",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new State("1",
                        new Spawn("Banan PHON", 1, 1, 1000000),
                        new TimedTransition(1000, "2")
                        ),
                    new State("2",
                        new EntityNotExistsTransition("Banan PHON", 999, "3")
                        ),
                    new State("3",
                        new TimedTransition(6000, "1")
                        )
                    )
                )
            .Init("Red Flaming Skull",
                new State(
                    new Prioritize(
                        new Wander(.6),
                        new Follow(.6, 20, 3)
                        ),
                    new Shoot(15, 2, 5, 0, predictive: 1, coolDown: 750)
                    )
            )
            .Init("Blue Flaming Skull",
                new State(
                    new Prioritize(
                        new Orbit(1, 20, target: "Skull Shrine", radiusVariance: 0.5),
                        new Wander(.6)
                        ),
                    new Shoot(15, 2, 5, 0, predictive: 1, coolDown: 750)
                    )
                )

            .Init("Gru",
                new State(
                    new State("1",
                        new TimedTransition(30000, "2")
                        ),
                    new State("2",
                        new Taunt("Level your character to 20 with /l20 !"),
                        new TimedTransition(1000, "1")
                        )
                    )
                )
            .Init("Spaceman",
                    new State(
                        new State("1",
                            new Taunt("blast off in 35 SECONDS."),
                            new TimedTransition(15000, "1a")
                            ),
                        new State("1a",
                            new Taunt("blast off in 20 SECONDS."),
                            new TimedTransition(15000, "1b")
                            ),
                        new State("1b",
                            new Taunt("blast off in 5 SECONDS."),
                            new TimedTransition(5000, "2")
                            ),
                        new State("2",
                            new Taunt("BLAST OFF!")
                            )
                        )
                    )
            .Init("MOLEY",
                new State(
                    new State("wait",
                        new SetAltTexture(1),
                        new PlayerTextTransition("1", "moley")
                        ),
                        new State("1",
                            new SetAltTexture(0),
                            new Taunt("YOU"),
                            new SoundPlay(0),
                            new TimedTransition(600, "1a")
                            ),
                        new State("1a",
                            new Taunt("ARE"),
                            new TimedTransition(600, "1b")
                            ),
                        new State("1b",
                            new Taunt("SO"),
                            new TimedTransition(600, "2")
                            ),
                        new State("2",
                            new Taunt("YUMMY YUMMY YUMMY YUMMY FAT FAT FAT"),
                            new TimedTransition(7000, "3")
                            ),
                        new State("3",
                            new Suicide()
                            )
                        )
                    )
            .Init("trollface",
                new State(
                    new State("wait",
                        new PlayerWithinTransition(99, "3")
                        ),
                        new State("1",
                            new SetAltTexture(0),
                            new Taunt("YOU"),
                            new SoundPlay(0),
                            new TimedTransition(600, "1a")
                            ),
                        new State("1a",
                            new Taunt("ARE"),
                            new TimedTransition(600, "1b")
                            ),
                        new State("1b",
                            new Taunt("SO"),
                            new TimedTransition(600, "2")
                            ),
                        new State("2",
                            new SoundPlay(0),
                            new TimedTransition(2000, "3")
                            ),
                        new State("3",
                            new Suicide()
                            )
                        )
                    )
            .Init("nomoresleep",
                new State(
                    new State("wait",
                        new PlayerWithinTransition(99, "2")
                        ),
                        new State("1",
                            new SetAltTexture(0),
                            new Taunt("YOU"),
                            new SoundPlay(0),
                            new TimedTransition(600, "1a")
                            ),
                        new State("1a",
                            new Taunt("ARE"),
                            new TimedTransition(600, "1b")
                            ),
                        new State("1b",
                            new Taunt("SO"),
                            new TimedTransition(600, "2")
                            ),
                        new State("2",
                            new SoundPlay(0),
                            new TimedTransition(2000, "3")
                            ),
                        new State("3",
                            new Suicide()
                            )
                        )
                    )
                .Init("SANS",
                    new State(

                        new ScaleHP(0.05),
                        new State("1",
                            new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                            new PlayerWithinTransition(10, "1a")
                            ),
                        new State("1a",
                            new Taunt("WELCOME TO MY BLACK LOL"),
                            new SoundPlay(0),
                            new TimedTransition(1500, "1b")
                            ),
                        new State("1b",
                            new Taunt("WANT 2 DIE? OK"),
                            new TimedTransition(1500, "2")
                            ),
                        new State("2",
                            new RemoveConditionalEffect(ConditionEffectIndex.Invincible),
                            new Wander(0.3),
                            new Shoot(99, 10, 36, 0, coolDown: 1000),
                            new Shoot(99, 3, 5, 0, coolDown: 200),
                            new HpLessTransition(0.6, "3")
                            ),
                        new State("3",
                            new Wander(0.3),
                            new Taunt("SPIKER BONES."),
                            new Shoot(99, 10, 36, 0, coolDown: 1000),
                            new Shoot(99, 10, 30, 0, coolDown: 400),
                            new HpLessTransition(0.3, "4")
                            ),
                        new State("4",
                            new ReturnToSpawn(1),
                            new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                            new Taunt("STOP KIULLING PLEASE"),
                            new SpiralShoot(20, 360, 18, 20, 3, coolDown: 1000),
                            new TimedTransition(2000, "dead1")
                            ),
                        new State("dead1",
                            new Taunt("I H ATE U"),
                            new Flash(0xFF0000, 1, 2),
                            new TimedTransition(1000, "dead2")
                            ),
                        new State("dead2",
                            new Suicide()
                            )
                        ),
                new Threshold(0.0001,
                    new ItemLoot("raindow of Maxy", 1 / 25f),
                    new ItemLoot("Spaceship Token", 1 / 1000f),
                    new ItemLoot("sons sparlking", 1 / 150f),
                    new ItemLoot("skull of sans", 1 / 50f),
                    new ItemLoot("Last Corridor Key", 1 / 15f),
                    new ItemLoot("spamton skull", 1 / 10000f),
                    new ItemLoot("Defense Eon", 1 / 1000f),
                    new ItemLoot("Dreadcull T2 Upgrade", 1 / 300f),
                    new ItemLoot("bones son", 1 / 50f),
                    new ItemLoot("sanas rink", 1 / 50f)
                    )
                )
            .Init("Ian",
                    new State(

                        new ScaleHP(0.2),
                        new DropPortalOnDeath("SPACE 2", YAdjustment: -3),
                        new State("1",
                            new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                            new PlayerWithinTransition(10, "1a")
                            ),
                        new State("1a",
                            new Taunt("hey how u can breath in space"),
                            new SoundPlay(0),
                            new TimedTransition(3000, "1b")
                            ),
                        new State("1b",
                            new Taunt("breathing  NONE"),
                            new TimedTransition(3000, "2")
                            ),
                        new State("2",
                            new RemoveConditionalEffect(ConditionEffectIndex.Invincible),
                            new Shoot(99, 10, 36, 0, coolDown: 1000),
                            new Shoot(99, 4, 50, 0, coolDown: 200),
                            new HpLessTransition(0.7, "3")
                            ),
                        new State("3",
                            new Wander(0.3),
                            new Taunt("GET IH MAH BELLAY"),
                            new Shoot(99, 10, 36, 1, coolDown: 3000),
                            new HpLessTransition(0.4, "4")
                            ),
                        new State("4",
                            new ReturnToSpawn(1),
                            new Taunt("lol sucks to be U"),
                            new SpiralShoot(20, 360, 18, 20, 3, coolDown: 200),
                            new HpLessTransition(0.1, "dead1")
                            ),
                        new State("dead1",
                            new ConditionalEffect(ConditionEffectIndex.Invincible),
                            new Taunt("ALVIN CUM"),
                            new Flash(0xFF0000, 1, 2),
                            new TimedTransition(2000, "dead2")
                            ),
                        new State("dead2",
                            new Suicide()
                            )
                        ),
                new Threshold(0.0001,
                    new ItemLoot("ian gloggle", 1 / 50f),
                    new ItemLoot("vacuum Brown", 1 / 50f),
                    new ItemLoot("chipmunk torture device", 1 / 100f),
                    new ItemLoot("Spaceship Token", 1 / 1000f),
                    //new ItemLoot("T2 UNLOCKER", 1),
                    new ItemLoot("Greater Potion of Life", 1),
                    new ItemLoot("Greater Potion of Mana", 1),
                    new ItemLoot("Greater Potion of Vitality", 1),
                    new ItemLoot("Greater Potion of Dexterity", 1),
                    new ItemLoot("Greater Potion of Speed", 1),
                    new ItemLoot("Dreadcull T2 Upgrade", 1 / 300f),
                    new ItemLoot("Greater Potion of Attack", 1),
                    new ItemLoot("Greater Potion of Defense", 1),
                    new ItemLoot("Greater Potion of Wisdom", 1)
                    )
                )
            .Init("golden freddy",
                    new State(

                        new ScaleHP(0.05),
                        new State("1",
                            new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                            new PlayerWithinTransition(10, "1a")
                            ),
                        new State("1a",
                            new Taunt("..."),
                            new SoundPlay(0),
                            new TimedTransition(3000, "1b")
                            ),
                        new State("1b",
                            new TimedTransition(3000, "2")
                            ),
                        new State("2",
                            new RemoveConditionalEffect(ConditionEffectIndex.Invincible),
                            new Shoot(99, 10, 36, 0, coolDown: 1200),
                            new Shoot(99, 5, 30, 0, coolDown: 700),
                            new HpLessTransition(0.7, "3")
                            ),
                        new State("3",
                            new Wander(0.3),
                            new SpiralShoot(20, 360, 18, 20, 2, coolDown: 2000),
                            new HpLessTransition(0.4, "4")
                            ),
                        new State("4",
                            new ReturnToSpawn(1),
                            new Shoot(99, 10, 36, 1, coolDown: 600),
                            new HpLessTransition(0.2, "dead2")
                            ),
                        new State("dead1",
                            new ConditionalEffect(ConditionEffectIndex.Invincible),
                            new Flash(0xFF0000, 1, 2),
                            new TimedTransition(2000, "dead2")
                            ),
                        new State("dead2",
                            new Suicide()
                            )
                        ),
                new Threshold(0.01,
                    new ItemLoot("raindow of Maxy", 1 / 25f),
                    new ItemLoot("Potion of Life", 1),
                    new ItemLoot("Potion of Mana", 1),
                    new ItemLoot("Potion of Vitality", 1),
                    new ItemLoot("Potion of Dexterity", 1),
                    new ItemLoot("Fnaf pizza key", 1 / 15f),

                    new ItemLoot("Potion of Speed", 1),
                    new ItemLoot("Potion of Attack", 1),
                    new ItemLoot("Potion of Defense", 1),
                    new ItemLoot("Potion of Wisdom", 1),
                    new ItemLoot("fnaf PIZZA FULL", 1 / 50f),
                    new ItemLoot("fazbear bow", 1 / 50f),
                    new ItemLoot("fazbear hat", 1 / 50f),
                    new ItemLoot("wand of freddo", 1 / 100f),
                    new ItemLoot("Dreadcull T2 Upgrade", 1 / 300f),
                    new ItemLoot("pizzer slice", 1 / 150f),
                    new ItemLoot("Spaceship Token", 1 / 1000f)
                    )
                )
        .Init("chinese egghead",
            new State(


                new ScaleHP(0.05),
                new State("WAIT",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new PlayerWithinTransition(8, "2w"),
                    new Wander(0.5)
                   ),
                new State("2w",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new TimedTransition(3000, "2")
                    ),
                new State("2",
                    new Taunt("xue hua piao piao"),
                    new Shoot(99, 10, 16, predictive: 0.1, coolDown: 2500),
                    new Wander(0.5),
                    new HpLessTransition(0.4, "3")
                   ),
                new State("3",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new Taunt("bei feng xiao xiao"),
                    new TimedTransition(2000, "dead")
                    ),
                new State("dead",
                    new Suicide()
                   )
                ),
              new MostDamagers(1,
                     LootTemplates.StatPots()
                     ),
                new Threshold(0.0001,
                    new ItemLoot("raindow of Maxy", 1 / 25f),
                    new TierLoot(12, ItemType.Weapon, 0.25),
                    new TierLoot(12, ItemType.Armor, 0.25),
                    new ItemLoot("Potion of Vitality", 0.5),
                    new ItemLoot("Potion of Dexterity", 0.5),
                    new ItemLoot("Potion of Speed", 0.5),
                    new ItemLoot("Potion of Attack", 0.5),
                    new ItemLoot("Potion of Defense", 0.5),
                    new ItemLoot("Lair of Alvin key", 1 / 15f),

                    new ItemLoot("Potion of Wisdom", 0.5),
                    new TierLoot(11, ItemType.Weapon, 0.25),
                    new TierLoot(11, ItemType.Armor, 0.25),
                    new TierLoot(6, ItemType.Ring, 0.25),
                    new TierLoot(5, ItemType.Ring, 0.25),
                    new ItemLoot("teleporter yellow", 1 / 50f),
                    new ItemLoot("choina stave", 1 / 150f),
                    new ItemLoot("Dreadcull T1 Upgrade", 1 / 300f),
                    new ItemLoot("Spaceship Token", 1 / 1000f),
                    new ItemLoot("xue hua tome tome", 1 / 150f),
                    new ItemLoot("egg trap", 1 / 50f)
                    )
                )
        .Init("talking toilet",
                    new State(

                        new ScaleHP(0.05),
                        new State("1",
                            new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                            new PlayerWithinTransition(10, "1a")
                            ),
                        new State("1a",
                            new Taunt("mah mah mah mah mah"),
                            new TimedTransition(3000, "1b")
                            ),
                        new State("1b",
                            new TimedTransition(1000, "2")
                            ),
                        new State("2",
                            new RemoveConditionalEffect(ConditionEffectIndex.Invincible),
                            new Shoot(99, 10, 12, 0, coolDown: 1200),
                            new HpLessTransition(0.5, "dead2")
                            ),
                        new State("3",
                            new Wander(0.1),
                            new SpiralShoot(20, 360, 18, 20, 2, coolDown: 1000),
                            new HpLessTransition(0.4, "4")
                            ),
                        new State("4",
                            new ReturnToSpawn(1),
                            new Shoot(99, 5, 6, 1, coolDown: 1500),
                            new HpLessTransition(0.2, "dead2")
                            ),
                        new State("dead1",
                            new ConditionalEffect(ConditionEffectIndex.Invincible),
                            new Flash(0xFF0000, 1, 2),
                            new TimedTransition(2000, "dead2")
                            ),
                        new State("dead2",
                            new Suicide()
                            )
                        ),
                new Threshold(0.01,
                    new ItemLoot("raindow of Maxy", 1 / 25f),
                    new TierLoot(12, ItemType.Weapon, 0.25),
                    new TierLoot(12, ItemType.Armor, 0.25),
                    new ItemLoot("Potion of Vitality", 0.5),
                    new ItemLoot("Potion of Dexterity", 0.5),
                    new ItemLoot("Potion of Speed", 0.5),
                    new ItemLoot("Potion of Attack", 0.5),
                    new ItemLoot("Potion of Defense", 0.5),
                    new ItemLoot("Lair of Alvin key", 1 / 15f),

                    new ItemLoot("Potion of Wisdom", 0.5),
                    new TierLoot(11, ItemType.Weapon, 0.25),
                    new TierLoot(11, ItemType.Armor, 0.25),
                    new TierLoot(6, ItemType.Ring, 0.25),
                    new TierLoot(5, ItemType.Ring, 0.25),
                    new ItemLoot("poop robe", 1 / 50f),
                    new ItemLoot("plunger wand", 1 / 50f),
                    new ItemLoot("seal covered in SHIT", 1 / 50f),
                    new ItemLoot("Dreadcull T1 Upgrade", 1 / 300f),
                    new ItemLoot("box ring scary anger", 1 / 150f),
                    new ItemLoot("the ROLLER", 1 / 150f),
                    new ItemLoot("Spaceship Token", 1 / 1000f)
                    )
                )
        .Init("Sans Spawner",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new State("1",
                        new Spawn("SANS", 1, 1, 1000000),
                        new TimedTransition(1000, "2")
                        ),
                    new State("2",
                        new EntityNotExistsTransition("SANS", 999, "3")
                        ),
                    new State("3",
                        new TimedTransition(7000, "1")
                        )
                    )
                )
        .Init("Alvin Logo",
            new State(
            new ConditionalEffect(ConditionEffectIndex.Invincible),
                new State("1",
                    new SetAltTexture(0, 1, 1000, true)
                    ),
                new State("2",
                    new EntityNotExistsTransition("SANS", 999, "3")
                    ),
                new State("3",
                    new TimedTransition(8000, "1")
                    )
                    )
                )
        .Init("Freddy Spawner",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new State("1",
                        new Spawn("golden freddy", 1, 1, 1000000),
                        new TimedTransition(1000, "2")
                        ),
                    new State("2",
                        new EntityNotExistsTransition("golden freddy", 999, "3")
                        ),
                    new State("3",
                        new TimedTransition(8000, "1")
                        )
                    )
                )
        .Init("Toilet Spawner",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new State("1",
                        new Spawn("talking toilet", 1, 1, 1000000),
                        new TimedTransition(1000, "2")
                        ),
                    new State("2",
                        new EntityNotExistsTransition("talking toilet", 999, "3")
                        ),
                    new State("3",
                        new TimedTransition(8000, "1")
                        )
                    )
                )
        .Init("Chinese Spawner",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new State("1",
                        new Spawn("chinese egghead", 1, 1, 1000000),
                        new TimedTransition(1000, "2")
                        ),
                    new State("2",
                        new EntityNotExistsTransition("chinese egghead", 999, "3")
                        ),
                    new State("3",
                        new TimedTransition(8000, "1")
                        )
                    )
                )
        .Init("Shoop Spawner",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new State("1",
                        new Spawn("shoop da woop", 1, 1, 1000000),
                        new TimedTransition(1000, "2")
                        ),
                    new State("2",
                        new EntityNotExistsTransition("shoop da woop", 999, "3")
                        ),
                    new State("3",
                        new TimedTransition(8000, "1")
                        )
                    )
                )
        .Init("Triangle Spawner",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new State("1",
                        new Spawn("dancing triangle", 1, 1, 1000000),
                        new TimedTransition(1000, "2")
                        ),
                    new State("2",
                        new EntityNotExistsTransition("dancing triangle", 999, "3")
                        ),
                    new State("3",
                        new TimedTransition(8000, "1")
                        )
                    )
                )
        .Init("Puggsy Spawner",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new State("1",
                        new Spawn("puggsy", 1, 1, 1000000),
                        new TimedTransition(1000, "2")
                        ),
                    new State("2",
                        new EntityNotExistsTransition("puggsy", 999, "3")
                        ),
                    new State("3",
                        new TimedTransition(8000, "1")
                        )
                    )
                )
        .Init("GUMBALL Spawner",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new State("1",
                        new Spawn("GUMBALL", 1, 1, 1000000),
                        new TimedTransition(1000, "2")
                        ),
                    new State("2",
                        new EntityNotExistsTransition("GUMBALL", 999, "3")
                        ),
                    new State("3",
                        new TimedTransition(8000, "1")
                        )
                    )
                )
        .Init("Sunbaby Spawner",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new State("1",
                        new Spawn("sun baby", 1, 1, 1000000),
                        new TimedTransition(1000, "2")
                        ),
                    new State("2",
                        new EntityNotExistsTransition("sun baby", 999, "3")
                        ),
                    new State("3",
                        new TimedTransition(8000, "1")
                        )
                    )
                )
        .Init("Blaze Spawner",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new State("1",
                        new Spawn("blaze", 1, 1, 1000000),
                        new TimedTransition(1000, "2")
                        ),
                    new State("2",
                        new EntityNotExistsTransition("blaze", 999, "3")
                        ),
                    new State("3",
                        new TimedTransition(8000, "1")
                        )
                    )
                )
        .Init("Wither Spawner",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new State("1",
                        new Spawn("Wither Skeleton", 1, 1, 1000000),
                        new TimedTransition(1000, "2")
                        ),
                    new State("2",
                        new EntityNotExistsTransition("Wither Skeleton", 999, "3")
                        ),
                    new State("3",
                        new TimedTransition(5000, "1")
                        )
                    )
                )
        .Init("Mindy Spawner",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new State("1",
                        new Spawn("Mindy", 1, 1, 1000000),
                        new TimedTransition(1000, "2")
                        ),
                    new State("2",
                        new EntityNotExistsTransition("Mindy", 999, "3")
                        ),
                    new State("3",
                        new TimedTransition(8000, "1")
                        )
                    )
                )
        .Init("Vera Spawner",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new State("1",
                        new Spawn("Vera", 1, 1, 1000000),
                        new TimedTransition(1000, "2")
                        ),
                    new State("2",
                        new EntityNotExistsTransition("Vera", 999, "3")
                        ),
                    new State("3",
                        new TimedTransition(8000, "1")
                        )
                    )
                )
        .Init("Buddy Spawner",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new State("1",
                        new Spawn("Interactive Buddy", 1, 1, 1000000),
                        new TimedTransition(1000, "2")
                        ),
                    new State("2",
                        new EntityNotExistsTransition("Interactive Buddy", 999, "3")
                        ),
                    new State("3",
                        new TimedTransition(10000, "1")
                        )
                    )
                )
        .Init("Tong Spawner",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new State("1",
                        new Spawn("Tong Shau Ping", 1, 1, 1000000),
                        new TimedTransition(1000, "2")
                        ),
                    new State("2",
                        new EntityNotExistsTransition("Tong Shau Ping", 999, "3")
                        ),
                    new State("3",
                        new TimedTransition(8000, "1")
                        )
                    )
                )
        .Init("Nadine Spawner",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new State("1",
                        new Spawn("Nadine", 1, 1, 1000000),
                        new TimedTransition(1000, "2")
                        ),
                    new State("2",
                        new EntityNotExistsTransition("Nadine", 999, "3")
                        ),
                    new State("3",
                        new TimedTransition(8000, "1")
                        )
                    )
                )
        .Init("darkreaper",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                new State("1",
                    new ConditionEffectRegion(ConditionEffectIndex.Darkness, 999)
                    )
                )
            )
        .Init("Godes Spawner",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new State("1",
                        new Spawn("godes", 1, 1, 1000000),
                        new TimedTransition(1000, "2")
                        ),
                    new State("2",
                        new EntityNotExistsTransition("godes", 999, "3")
                        ),
                    new State("3",
                        new TimedTransition(8000, "1")
                        )
                    )
                )
        .Init("Ghast Spawner",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new State("1",
                        new Spawn("ghast", 1, 1, 1000000),
                        new TimedTransition(1000, "2")
                        ),
                    new State("2",
                        new EntityNotExistsTransition("ghast", 999, "3")
                        ),
                    new State("3",
                        new TimedTransition(8000, "1")
                        )
                    )
                )
        .Init("Magma Spawner",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new State("1",
                        new Spawn("magma Cube", 1, 1, 1000000),
                        new TimedTransition(1000, "2")
                        ),
                    new State("2",
                        new EntityNotExistsTransition("magma Cube", 999, "3")
                        ),
                    new State("3",
                        new TimedTransition(8000, "1")
                        )
                    )
                )
        .Init("blaze",
            new State(
                new StayCloseToSpawn(2, 8),
                new State("1",
                    new Wander(0.4),
                    new SoundPlay(0),
                    new Spawn("blazefiremin", 1, 0.1, 10),
                    new TimedTransition(3000, "2"),
                    new HpLessTransition(0.3, "3")
                    ),
                new State("2",
                    new RemoveEntity(99, "blazefiremin"),
                    new Shoot(99, 1, null, 0, coolDown: 380),
                    new TimedTransition(1000, "1"),
                    new HpLessTransition(0.3, "3")
                    ),
                new State("3",
                    new Shoot(99, 3, 3, 0, coolDown: 500)
                        )
                    ),
                new Threshold(0.0001,
                    new ItemLoot("netherite hlomet", 1 / 150f),
                    new ItemLoot("blaze rod", 1 / 50f),
                    new ItemLoot("blaze key", 1 / 15f),
                    new ItemLoot("blaze powder", 1 / 100f),
                    new ItemLoot("blaze powered armors", 1 / 50f),
                    new ItemLoot("Nether key", 1 / 15f),
                    new ItemLoot("BURN BABY BURN", 1 / 50f),
                    new ItemLoot("Dreadcull T5 Upgrade", 1 / 300f),
                    new ItemLoot("Deep Sea Token", 1 / 750f)

                )
            )
        .Init("magma Cube",
            new State(

                new StayCloseToSpawn(2, 8),
                new State("1",
                    new Wander(0.4),
                    new Shoot(99, 20, 20, 0, coolDown: 400),
                    new HpLessTransition(0.3, "3")
                    ),
                new State("3",
                    new ReturnToSpawn(1),
                    new Flash(0xFF0000, 0.2, 20),
                    new SpiralShoot(8, 360, 6, 60, coolDown: 100),
                    new Shoot(99, 10, 15, 0, coolDown: 10000)
                        )
                    ),
                new Threshold(0.0001,
                    new ItemLoot("slimy brim brim", 1 / 150f),
                    new ItemLoot("lava waders grey", 1 / 50f),
                    new ItemLoot("Nether key", 1 / 15f),
                    new ItemLoot("Dreadcull T5 Upgrade", 1 / 300f),
                    new ItemLoot("netherfire orb", 1 / 150f),
                    new ItemLoot("creamy armor rack", 1 / 50f)
                )
            )
        .Init("ghast",
            new State(

                new State("1",
                    new Wander(0.8),
                    new Shoot(99, 12, 15, 0, coolDown: 1500),
                    new TimedTransition(3000, "2"),
                    new HpLessTransition(0.3, "3")
                    ),
                new State("2",
                    new ReturnToSpawn(1),
                    new SoundPlay(0),
                    new Shoot(99, 10, 36, 1, coolDown: 1200),
                    new TimedTransition(1500, "2a"),
                    new HpLessTransition(0.3, "3")
                    ),
                new State("2a",
                    new Shoot(99, 10, 36, 1, 45, coolDown: 1200),
                    new TimedTransition(1500, "1"),
                    new HpLessTransition(0.3, "3")
                    ),
                new State("3",
                    new Flash(0xFF0000, 1, 3),
                    new Shoot(99, 10, 36, 0, coolDown: 2200),
                    new Shoot(99, 5, 5, 0, coolDown: 700)
                        )
                    ),
                new Threshold(0.0001,
                    new ItemLoot("Ghast Tear", 1 / 150f),
                    new ItemLoot("ghats brain", 1 / 50f),
                    new ItemLoot("Nether key", 1 / 15f),

                    new ItemLoot("Dreadcull T5 Upgrade", 1 / 300f),
                    new ItemLoot("fiery staff", 1 / 50f),
                    new ItemLoot("netheriter blod", 1 / 50f)
                )
            )
        .Init("Mindy",
            new State(

                new ScaleHP(0.05),
                new State("1",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new TimedTransition(3000, "2")
                    ),
                new State("2",
                    new Orbit(3, 7, 99, "Mindy Spawner", 2, 5),
                    new Shoot(99, 3, 120, 1, coolDown: 1000),
                    new Shoot(99, 3, 120, 2, coolDown: 1000),
                    new Shoot(99, 3, 120, 3, coolDown: 1000),
                    new HpLessTransition(0.5, "3")
                    ),
                new State("3",
                    new ReturnToSpawn(1),
                    new Flash(0xFF0000, 1, 3),
                    new Shoot(99, 10, 36, 0, rotateAngle: 10, coolDown: 300),
                    new Shoot(99, 3, 120, 1, coolDown: 1200)
                        )
                    ),
                new Threshold(0.0001,
                    new ItemLoot("CARAMELLA MIC", 1 / 150f),
                    new ItemLoot("MISSA INTE CHANSEN", 1 / 50f),
                    new ItemLoot("NU ÄR VI HÄR MED", 1 / 50f),
                    new ItemLoot("Caramelldansen Key", 1 / 15f),
                    new ItemLoot("eye of gardens", 1 / 50f),
                    new ItemLoot("Dreadcull T6 Upgrade", 1 / 300f),
                    new ItemLoot("Spaceship Token", 1 / 275f),
                    new ItemLoot("Deep Sea Token", 1 / 500f)
                )
            )
        .Init("Vera",
            new State(

                new ScaleHP(0.05),
                new StayCloseToSpawn(5, 8),
                new State("1",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new TimedTransition(3000, "2")
                    ),
                new State("2",
                    new Wander(1.5),
                    new Orbit(2, 4, 99, "Vera Spawner", 1.5, 5),
                    new Shoot(99, 4, 4, 1, coolDown: 1000),
                    new Shoot(99, 6, 60, 1, coolDown: 800),
                    new HpLessTransition(0.5, "3")
                    ),
                new State("3",
                    new Wander(2),
                    new Flash(0xFF0000, 1, 3),
                    new Shoot(99, 4, 4, 1, coolDown: 1000),
                    new Shoot(99, 6, 60, 0, rotateAngle: 10, coolDown: 600)
                        )
                    ),
                new Threshold(0.0001,
                    new ItemLoot("GAMER BELT", 1 / 150f),
                    new ItemLoot("CARAMEL EXPERIENCE", 1 / 50f),
                    new ItemLoot("bikini trap", 1 / 50f),
                    new ItemLoot("Caramelldansen Key", 1 / 15f),

                    new ItemLoot("Dreadcull T6 Upgrade", 1 / 100f),
                    new ItemLoot("BLOWN THE FUCK OUT", 1 / 50f),
                    new ItemLoot("Spaceship Token", 1 / 275f),
                    new ItemLoot("Deep Sea Token", 1 / 500f)
                )
            )
        .Init("Reaper Shark",
            new State(
                new DropPortalOnDeath("The Abyss", 0.25f),
                new ScaleHP(1.5),
                new State("1",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new TimedTransition(2000, "2")
                    ),
                new State("2",
                    new Follow(1, 99, 0, 100000, 100),
                    new Shoot(99, 6, 60, 0, coolDown: 3000),
                    new Shoot(99, 5, 18, 1, coolDown: 2000),
                    new HpLessTransition(0.5, "3a"),
                    new TimedTransition(5000, "3")
                    ),
                new State("3",
                    new Follow(1.2, 99, 0, 100000, 100),
                    new Shoot(99, 7, 20, 2, coolDown: 3000),
                    new Shoot(99, 5, 40, 2, fixedAngle: 180, coolDown: 2000, coolDownOffset: 300),
                    new HpLessTransition(0.5, "3a"),
                    new TimedTransition(3000, "2")
                        ),
                new State("3a",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new ReturnToSpawn(1),
                    new TimedTransition(3000, "4")
                        ),
                new State("4",
                    new Flash(0x0000FF, 1, 3),
                    new Shoot(99, 4, 90, 2, coolDown: 2200),
                    new Shoot(99, 6, 60, 0, coolDown: 1100),
                    new Orbit(1.2, 5, 99, "darkreaper"),
                    new Wander(0.5),
                    new Shoot(99, 5, 20, 1, coolDown: 600),
                    new HpLessTransition(0.2, "5")
                    ),
                new State("5",
                    new Follow(1.2, 99, 0, 100000, 100),
                    new Shoot(99, 4, 15, 2, coolDown: 1400),
                    new Shoot(99, 5, 20, 2, fixedAngle: 180, coolDown: 1500, coolDownOffset: 300),
                    new Shoot(99, 1, null, 0, coolDown: 200)
                        )
                    ),
                new Threshold(0.0001,
                    new ItemLoot("50k Fame", 1 / 15f),
                    new ItemLoot("5000 Fame", 1 / 5f),
                    new ItemLoot("Curved Blade", 1 / 50f),
                    new ItemLoot("Terminus", 1 / 100f),
                    new ItemLoot("Dreadcull T6 Upgrade", 1 / 300f),
                    new ItemLoot("NLE Choppa", 1 / 50f),
                    new ItemLoot("Vooom bambambam BOOSH", 1 / 50f),
                    new ItemLoot("Box of reaper teeth", 1 / 50f),
                    new ItemLoot("speedy wet-pack", 1 / 150f)
                )
            )
        .Init("Nadine",
            new State(

                new ScaleHP(0.05),
                new StayCloseToSpawn(5, 8),
                new State("1",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new TimedTransition(3000, "2")
                    ),
                new State("2",
                    new Wander(1.5),
                    new Orbit(1.5, 2, 99, "Nadine Spawner", 2, 3),
                    new Shoot(99, 4, 4, 0, coolDown: 1000),
                    new Shoot(99, 6, 60, 1, coolDown: 1600),
                    new HpLessTransition(0.5, "3")
                    ),
                new State("3",
                    new Wander(2),
                    new Flash(0xFF0000, 1, 3),
                    new Shoot(99, 4, 4, 1, coolDown: 1000),
                    new Shoot(99, 6, 60, 0, rotateAngle: 10, coolDown: 900)
                        )
                    ),
                new Threshold(0.0001,
                    new ItemLoot("KLAPPA ERA HÄNDER", 1 / 150f),
                    new ItemLoot("LYSSNA OCH LÄR", 1 / 50f),
                    new ItemLoot("jag skar mig i fingret", 1 / 50f),
                    new ItemLoot("dancer prancer", 1 / 50f),
                    new ItemLoot("Caramelldansen Key", 1 / 15f),
                    new ItemLoot("ASGARD Token", 1 / 175f),
                    new ItemLoot("FAVELA Token", 1 / 250f),
                    new ItemLoot("Deep Sea Token", 1 / 300f),
                    new ItemLoot("A Fallen Light (Token)", 1 / 500f),
                    new ItemLoot("Spaceship Token", 1 / 275f),
                    new ItemLoot("Dreadcull T6 Upgrade", 1 / 300f),
                    new ItemLoot("GÖR SOM VI GÖR", 1 / 50f),
                    new ItemLoot("Deep Sea Token", 1 / 500f)
                )
            )
        .Init("Wither Skeleton",
            new State(


                new StayCloseToSpawn(1, 10),
                new State("1",
                    new Follow(1.2, 10, 0, coolDown: 100),
                    new Shoot(99, 12, 10, 0, coolDown: 2000),
                    new HpLessTransition(0.3, "3")
                    ),
                new State("3",
                    new Flash(0xFF0000, 1, 3),
                    new Shoot(99, 10, 36, 0, coolDown: 1200),
                    new Shoot(99, 5, 5, 0, coolDown: 1000)
                        )
                    ),
                new Threshold(0.0001,
                    new ItemLoot("coal amror", 1 / 150f),
                    new ItemLoot("Dreadcull T5 Upgrade", 1 / 300f),
                    new ItemLoot("ashes to ashes", 1 / 50f),
                    new ItemLoot("Spaceship Token", 1 / 275f),
                    new ItemLoot("Wither Catalyst", 1 / 50000f),
                    new ItemLoot("wither skeleton skull", 1 / 50f)

                )
            )
        .Init("Interactive Buddy",
            new State(

                new StayCloseToSpawn(3, 15),
                new ScaleHP(0.05),
                new State("0",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new TimedTransition(3000, "1")
                    ),
                new State("1",
                    new Flash(0xFF0000, 1, 3),
                    new Shoot(99, 10, 36, 0, coolDown: 1200),
                    new Shoot(99, 5, 5, 0, coolDown: 100)
                        )
                    ),
                new Threshold(0.00001,
                    new ItemLoot("50k Fame", 1 / 100f),
                    new ItemLoot("5000 Fame", 1 / 20f),
                    new ItemLoot("Deep Space Key", 1 / 15f),
                    new ItemLoot("viva la vida", 1 / 50f),
                    new ItemLoot("midnight", 1 / 50f),
                    new ItemLoot("interactivity", 1 / 50f),
                    new ItemLoot("Dreadcull T6 Upgrade", 1 / 300f),
                    new ItemLoot("interactive dagger", 1 / 50f),
                    new ItemLoot("Deep Sea Token", 1 / 500f),
                    new ItemLoot("Spaceship Token", 1 / 275f),
                    new ItemLoot("Peace of Mind", 1 / 150f)
                )
            )
        .Init("blazefiremin",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                new ScaleHP(0.4),
                new State("1",
                    new Orbit(1.2, 0.5, 10, "blaze")
                    )
                )
            )
        .Init("finnish Spawner",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new State("1",
                        new Spawn("finnish warrior", 1, 1, 1000000),
                        new TimedTransition(1000, "2")
                        ),
                    new State("2",
                        new EntityNotExistsTransition("finnish warrior", 999, "3")
                        ),
                    new State("3",
                        new TimedTransition(7000, "1")
                        )
                    )
                )
        .Init("Yakup Spawner",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new State("1",
                        new Spawn("yakupozi", 1, 1, 1000000),
                        new TimedTransition(1000, "2")
                        ),
                    new State("2",
                        new EntityNotExistsTransition("yakupozi", 999, "3")
                        ),
                    new State("3",
                        new TimedTransition(8000, "1")
                        )
                    )
                )
        .Init("Soulja Spawner",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new State("1",
                        new Spawn("soulja boi", 1, 1, 1000000),
                        new TimedTransition(1000, "2")
                        ),
                    new State("2",
                        new EntityNotExistsTransition("soulja boi", 999, "3")
                        ),
                    new State("3",
                        new TimedTransition(8000, "1")
                        )
                    )
                )
        .Init("Billy Spawner",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new State("1",
                        new Spawn("Billy Herrington", 1, 1, 1000000),
                        new TimedTransition(1000, "2")
                        ),
                    new State("2",
                        new EntityNotExistsTransition("Billy Herrington", 999, "3")
                        ),
                    new State("3",
                        new TimedTransition(8000, "1")
                        )
                    )
                )
        .Init("Baldi Spawner",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new State("1",
                        new Spawn("baldi", 1, 1, 1000000),
                        new TimedTransition(1000, "2")
                        ),
                    new State("2",
                        new EntityNotExistsTransition("baldi", 999, "3")
                        ),
                    new State("3",
                        new TimedTransition(8000, "1")
                        )
                    )
                )
        .Init("Pickle Spawner",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new State("1",
                        new Spawn("Pickle Rick", 1, 1, 1000000),
                        new TimedTransition(1000, "2")
                        ),
                    new State("2",
                        new EntityNotExistsTransition("Pickle Rick", 999, "3")
                        ),
                    new State("3",
                        new TimedTransition(8000, "1")
                        )
                    )
                )
        .Init("Herobrine Spawner",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new State("1",
                        new Spawn("Herobrine", 1, 1, 1000000),
                        new TimedTransition(1000, "2")
                        ),
                    new State("2",
                        new EntityNotExistsTransition("Herobrine", 999, "3")
                        ),
                    new State("3",
                        new TimedTransition(8000, "1")
                        )
                    )
                )
        .Init("Chef Spawner",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new State("1",
                        new Spawn("LH Marble Colossus", 1, 1, 1000000),
                        new TimedTransition(1000, "2")
                        ),
                    new State("2",
                        new EntityNotExistsTransition("LH Marble Colossus", 999, "3")
                        ),
                    new State("3",
                        new TimedTransition(8000, "1")
                        )
                    )
                )
        .Init("Orange Spawner",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new State("1",
                        new Spawn("Annoying Orange", 1, 1, 1000000),
                        new TimedTransition(1000, "2")
                        ),
                    new State("2",
                        new EntitiesNotExistsTransition(999, "3", "Annoying Orange", "Pear", "Apple", "Marshmallow")
                        ),
                    new State("3",
                        new TimedTransition(8000, "1")
                        )
                    )
                )
        .Init("Pear Spawner",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new State("1",
                        new Spawn("Pear", 1, 1, 1000000),
                        new TimedTransition(1000, "2")
                        ),
                    new State("2",
                        new EntitiesNotExistsTransition(999, "3", "Annoying Orange", "Pear", "Apple", "Marshmallow")
                        ),
                    new State("3",
                        new TimedTransition(8000, "1")
                        )
                    )
                )
        .Init("Apple Spawner",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new State("1",
                        new Spawn("Apple", 1, 1, 1000000),
                        new TimedTransition(1000, "2")
                        ),
                    new State("2",
                        new EntitiesNotExistsTransition(999, "3", "Annoying Orange", "Pear", "Apple", "Marshmallow")
                        ),
                    new State("3",
                        new TimedTransition(8000, "1")
                        )
                    )
                )
        .Init("Marshmallow Spawner",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new State("1",
                        new Spawn("Marshmallow", 1, 1, 1000000),
                        new TimedTransition(1000, "2")
                        ),
                    new State("2",
                        new EntitiesNotExistsTransition(999, "3", "Annoying Orange", "Pear", "Apple", "Marshmallow")
                        ),
                    new State("3",
                        new TimedTransition(8000, "1")
                        )
                    )
                )
                .Init("Annoying Orange",
                    new State(
                        new ScaleHP(0.05),
                        new State("1",
                            new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                            new PlayerWithinTransition(10, "1a")
                            ),
                        new State("1a",
                            new Taunt("GET OUT MY KITCHEN HAHHAAHHAH"),
                            new SoundPlay(0),
                            new TimedTransition(3000, "2")
                            ),
                        new State("2",
                            new RemoveConditionalEffect(ConditionEffectIndex.Invincible),
                            new Wander(0.3),
                            new Shoot(99, 10, 36, 0, coolDown: 1000),
                            new Shoot(99, 5, 15, 0, coolDown: 1000),
                            new HpLessTransition(0.6, "3")
                            ),
                        new State("3",
                            new Wander(0.3),
                            new Taunt("KNIFE!!"),
                            new SoundPlay(1),
                            new Shoot(99, 3, 5, 1, coolDown: 1500),
                            new HpLessTransition(0.3, "dead1")
                            ),
                        new State("dead1",
                            new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                            new Taunt("PEAR AVENGE ME!!!!!!!!!!!!!!!!S"),
                            new Flash(0x00FF00, 1, 2),
                            new TimedTransition(2000, "dead2")
                            ),
                        new State("dead2",
                            new Suicide()
                            )
                        ),
                new Threshold(0.0001,
                    new ItemLoot("raindow of Maxy", 1 / 25f),
                    new ItemLoot("ORNAGE ESENZE", 1 / 150f),
                    new ItemLoot("Potion of Life", 0.5),
                    new ItemLoot("Potion of Mana", 0.5),
                    new ItemLoot("Potion of Vitality", 1),
                    new ItemLoot("Potion of Dexterity", 1),
                    new ItemLoot("Potion of Speed", 1),
                    new ItemLoot("Dreadcull T2 Upgrade", 1 / 300f),
                    new ItemLoot("Potion of Attack", 1),
                    new ItemLoot("Potion of Defense", 1),
                    new ItemLoot("Potion of Wisdom", 1),
                    new ItemLoot("scepter zest orange", 1 / 50f),
                    new ItemLoot("Kitchen key", 1 / 15f),
                    new ItemLoot("Spaceship Token", 1 / 275f),
                    new ItemLoot("evil shiald", 1 / 50f),
                    new ItemLoot("knife", 1 / 50f)
                    )
                )
            .Init("Tong Shau Ping",
                    new State(

                        new StayCloseToSpawn(5, 6),
                        new ScaleHP(0.05),
                        new State("1",
                            new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                            new TimedTransition(1000, "2")
                            ),
                        new State("2",
                            new RemoveConditionalEffect(ConditionEffectIndex.Invincible),
                            new Wander(1.5),
                            new Shoot(99, 10, 36, 0, coolDown: 6000),
                            new Shoot(99, 6, 16, 0, coolDown: 600),
                            new HpLessTransition(0.15, "3")
                            ),
                        new State("3",
                            new ConditionalEffect(ConditionEffectIndex.Armored, true),
                            new RemoveConditionalEffect(ConditionEffectIndex.Invincible),
                            new Wander(4),
                            new Shoot(99, 10, 36, 0, coolDown: 200)
                            )
                        ),
                new Threshold(0.0001,
                    new ItemLoot("50k Fame", 1 / 100f),
                    new ItemLoot("5000 Fame", 1 / 20f),
                    new ItemLoot("fuckin ugly reds", 1 / 50f),
                    new ItemLoot("balo seal", 1 / 50f),
                    new ItemLoot("Hong Kong Key", 1 / 15f),
                    new ItemLoot("Dreadcull T7 Upgrade", 1 / 300f),
                    new ItemLoot("kong hong 97", 1 / 50f),
                    new ItemLoot("she Hong my Kong", 1 / 50f),
                    new ItemLoot("Deep Sea Token", 1 / 500f),
                    new ItemLoot("Spaceship Token", 1 / 275f),
                    new ItemLoot("alvin realms is gonna disappear", 1 / 200f)
                    )
                )
            .Init("Pear",
                    new State(

                        new ScaleHP(0.05),
                        new State("1a",
                            new Taunt("hello i am pear"),
                            new TimedTransition(3000, "2")
                            ),
                        new State("2",
                            new RemoveConditionalEffect(ConditionEffectIndex.Invincible),
                            new Wander(0.3),
                            new Shoot(99, 10, 36, 0, rotateAngle: 6, coolDown: 300),
                            new HpLessTransition(0.6, "3")
                            ),
                        new State("3",
                            new Wander(0.3),
                            new Taunt("lol"),
                            new Shoot(99, 9, 4, 1, coolDown: 1500),
                            new HpLessTransition(0.3, "dead1")
                            ),
                        new State("dead1",
                            new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                            new Taunt("hey aple yo hi"),
                            new Flash(0x00FF00, 1, 2),
                            new TimedTransition(2000, "dead2")
                            ),
                        new State("dead2",
                            new Suicide()
                            )
                        ),
                new Threshold(0.0001,
                    new ItemLoot("raindow of Maxy", 1 / 25f),
                    new ItemLoot("pear ward", 1 / 150f),
                    new ItemLoot("Potion of Life", 0.5),
                    new ItemLoot("Potion of Mana", 0.5),
                    new ItemLoot("Potion of Vitality", 1),
                    new ItemLoot("Potion of Dexterity", 1),
                    new ItemLoot("Potion of Speed", 1),
                    new ItemLoot("Potion of Attack", 1),
                    new ItemLoot("Dreadcull T2 Upgrade", 1 / 300f),
                    new ItemLoot("Potion of Defense", 1),
                    new ItemLoot("Potion of Wisdom", 1),
                    new ItemLoot("pear purples i on lols", 1 / 50f),
                    new ItemLoot("pear head UT", 1 / 50f),
                    new ItemLoot("Spaceship Token", 1 / 275f),
                    new ItemLoot("sorry FOR party rockin", 1 / 50f)
                    )
                )
            .Init("Apple",
                    new State(

                        new ScaleHP(0.05),
                        new State("1a",
                            new Taunt("HEY ORANGE STFU"),
                            new TimedTransition(3000, "2")
                            ),
                        new State("2",
                            new RemoveConditionalEffect(ConditionEffectIndex.Invincible),
                            new Wander(0.3),
                            new Shoot(99, 10, 36, 0, rotateAngle: 6, coolDown: 1000),
                            new HpLessTransition(0.6, "3")
                            ),
                        new State("3",
                            new Wander(0.3),
                            new Taunt("KNIFE KNIFE KNIFE KNIFE KNIFE KNIFE KNIFE KNIFE"),
                            new Shoot(99, 6, 175, 1, rotateAngle: 5, coolDown: 200),
                            new HpLessTransition(0.3, "dead1")
                            ),
                        new State("dead1",
                            new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                            new Taunt("MALLOWS RANG WHERE I ON"),
                            new Flash(0x00FF00, 1, 2),
                            new TimedTransition(2000, "dead2")
                            ),
                        new State("dead2",
                            new Suicide()
                            )
                        ),
                new Threshold(0.0001,
                    new ItemLoot("raindow of Maxy", 1 / 25f),
                    new ItemLoot("apple slice", 1 / 200f),
                    new ItemLoot("Potion of Life", 0.5),
                    new ItemLoot("Potion of Mana", 0.5),
                    new ItemLoot("Potion of Vitality", 1),
                    new ItemLoot("Potion of Dexterity", 1),
                    new ItemLoot("Potion of Speed", 1),
                    new ItemLoot("Potion of Attack", 1),
                    new ItemLoot("Potion of Defense", 1),
                    new ItemLoot("Potion of Wisdom", 1),
                    new ItemLoot("Dreadcull T2 Upgrade", 1 / 300f),
                    new ItemLoot("caramelized bruh nugget", 1 / 50f),
                    new ItemLoot("sweet candy drop", 1 / 50f),
                    new ItemLoot("Spaceship Token", 1 / 275f),
                    new ItemLoot("candee sword", 1 / 200f)
                    )
                )
            .Init("Marshmallow",
                    new State(

                        new ScaleHP(0.05),
                        new TimedTransition(3000, "1a"),
                        new State("1a",
                            new RemoveConditionalEffect(ConditionEffectIndex.Invincible),
                            new Taunt("RING RING BANANA PRONH"),
                            new Shoot(99, 3, 36, 0, rotateAngle: 6, coolDown: 200),
                            new TimedTransition(3000, "dead1")
                            ),
                        new State("dead1",
                            new Taunt("GG go agane i am chest loot N O DONT KILL"),
                            new Flash(0x00FF00, 1, 2),
                            new HpLessTransition(0.4, "dead2")
                            ),
                        new State("dead2",
                            new Suicide()
                            )
                        ),
                new Threshold(0.0001,
                    new ItemLoot("raindow of Maxy", 1 / 25f),
                    new ItemLoot("Spaceship Token", 1 / 1000f),
                    new ItemLoot("Potion of Life", 0.5),
                    new ItemLoot("Potion of Mana", 0.5),
                    new ItemLoot("Potion of Vitality", 1),
                    new ItemLoot("Potion of Dexterity", 1),
                    new ItemLoot("Potion of Speed", 1),
                    new ItemLoot("Dreadcull T2 Upgrade", 1 / 300f),
                    new ItemLoot("Potion of Attack", 1),
                    new ItemLoot("Potion of Defense", 1),
                    new ItemLoot("Potion of Wisdom", 1),
                    new ItemLoot("mallow ring", 1 / 50f),
                    new ItemLoot("sticky seal", 1 / 150f),
                    new ItemLoot("Spaceship Token", 1 / 275f),
                    new ItemLoot("charred Quiver", 1 / 200f)
                    )
                )
            .Init("Herobrine",
                new State(

                    new ScaleHP(0.05),
                    new DropPortalOnDeath("The Nether", 0.2, null),
                    new State("1",
                        new PlayerWithinTransition(15, "1a"),
                        new ConditionalEffect(ConditionEffectIndex.Invincible)
                        ),
                    new State("1a",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new Taunt("WELCOME TO MY MINE"),
                        new TimedTransition(3000, "2")
                        ),
                    new State("2",
                        new RemoveConditionalEffect(ConditionEffectIndex.Invincible),
                        new Wander(0.8),
                        new SpiralShoot(20, 360, 8, 45, 0, 0, coolDown: 800),
                        new HpLessTransition(0.6, "3")
                        ),
                    new State("3",
                        new Wander(0.3),
                        new Taunt("RAAAAH AAH MYAARJIE4IR J34IU4"),
                        new SpiralShoot(20, 360, 10, 36, 0, 0, coolDown: 1100),
                        new Shoot(99, 3, 5, 1, coolDown: 400),
                        new HpLessTransition(0.3, "dead1")
                        ),
                    new State("dead1",
                        new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                        new Taunt("ok"),
                        new Flash(0x00FF00, 1, 2),
                        new TimedTransition(2000, "dead2")
                        ),
                    new State("dead2",
                        new Suicide()
                        )
                    ),
            new Threshold(0.0001,
                new ItemLoot("diamond chestplate", 1 / 50f),
                new ItemLoot("mc shield irons", 1 / 50f),
                new ItemLoot("Legendsary Token 1", 1 / 2f),
                new ItemLoot("steve head STOLEN", 1 / 50f),
                new ItemLoot("Alvin Craft Key", 1 / 15f),

                new ItemLoot("DiAMOND SWORD", 1 / 150f),
                new ItemLoot("Diamond Pickaxe", 1 / 50f),
                new ItemLoot("Spaceship Token", 1 / 500f),
                new ItemLoot("Dreadcull T3 Upgrade", 1 / 300f)
                    )
                )
            .Init("Pickle Rick",
                new State(

                    new ScaleHP(0.4),
                    new State("1",
                        new PlayerWithinTransition(15, "1a"),
                        new ConditionalEffect(ConditionEffectIndex.Invincible)
                        ),
                    new State("1a",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new Taunt("IM PICKLE RICKE"),
                        new TimedTransition(3000, "2")
                        ),
                    new State("2",
                        new RemoveConditionalEffect(ConditionEffectIndex.Invincible),
                        new Follow(0.4, 10, 0),
                        new Shoot(99, 7, 20, 0, coolDown: 1500),
                        new HpLessTransition(0.7, "3")
                        ),
                    new State("3",
                        new ReturnToSpawn(1),
                        new Taunt("WHERES MY SZECHUAN SAUCE  !!!!!!!!!!!!!"),
                        new SpiralShoot(20, 360, 6, 60, 0, 0, coolDown: 1000),
                        new Shoot(99, 3, 5, 0, coolDown: 4000),
                        new HpLessTransition(0.3, "dead1")
                        ),
                    new State("dead1",
                        new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                        new Taunt("reREEEEEEEEEEEEEEEEEEE"),
                        new Flash(0x0000FF, 1, 2),
                        new TimedTransition(2000, "dead2")
                        ),
                    new State("dead2",
                        new Suicide()
                        )
                    ),
            new Threshold(0.0001,
                new ItemLoot("raindow of Maxy", 1 / 25f),
                new ItemLoot("pickol suit", 1 / 150f),
                new ItemLoot("pickle helm", 1 / 50f),
                new ItemLoot("pickel wond", 1 / 50f),
                new ItemLoot("r9ick ring", 1 / 50f),
                new ItemLoot("Spaceship Token", 1 / 1000f),
                new ItemLoot("Potion of Life", 0.5),
                new ItemLoot("Pickle Tunnel key", 1 / 15f),

                new ItemLoot("Potion of Mana", 0.5),
                new ItemLoot("Potion of Vitality", 1),
                new ItemLoot("Potion of Dexterity", 1),
                new ItemLoot("Dreadcull T2 Upgrade", 1 / 300f),
                new ItemLoot("Potion of Speed", 1),
                new ItemLoot("Potion of Attack", 1),
                new ItemLoot("Potion of Defense", 1),
                new ItemLoot("Potion of Wisdom", 1)
                    )
                )
        .Init("godes",
                new State(

                    new ScaleHP(0.05),
                    new State("1",
                        new PlayerWithinTransition(15, "1a"),
                        new ConditionalEffect(ConditionEffectIndex.Invincible)
                        ),
                    new State("1a",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new Taunt("GODES ULTANDS"),
                        new TimedTransition(2000, "2")
                        ),
                    new State("2",
                        new Wander(0.3),
                        new RemoveConditionalEffect(ConditionEffectIndex.Invincible),
                        new Shoot(99, 9, 20, 0, coolDown: 3000),
                        new HpLessTransition(0.7, "3")
                        ),
                    new State("3",
                        new SpiralShoot(6, 360, 6, 60, 0, 0, coolDown: 900),
                        new HpLessTransition(0.3, "dead1")
                        ),
                    new State("dead1",
                        new Taunt("andlands papot eats"),
                        new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                        new Flash(0x00FF00, 1, 2),
                        new TimedTransition(2000, "dead2")
                        ),
                    new State("dead2",
                        new Suicide()
                        )
                    ),
            new Threshold(0.0001,
                new ItemLoot("raindow of Maxy", 1 / 25f),
                new ItemLoot("Sword of locked Gold Test", 1 / 50f),
                new ItemLoot("godes JORTS", 1 / 50f),
                new ItemLoot("Shields Of Pinks Master Samnents", 1 / 50f),
                new ItemLoot("BOOK OF TURKEY", 1 / 50f),
                new ItemLoot("Spaceship Token", 1 / 1000f),
                new ItemLoot("Potion of Life", 0.5),
                new ItemLoot("Potion of Mana", 0.5),
                new ItemLoot("Dreadcull T2 Upgrade", 1 / 300f),
                new ItemLoot("Potion of Vitality", 1),
                new ItemLoot("Potion of Dexterity", 1),
                new ItemLoot("Potion of Speed", 1),
                new ItemLoot("Potion of Attack", 1),
                new ItemLoot("Potion of Defense", 1),
                new ItemLoot("Potion of Wisdom", 1)
                    )
                )
        .Init("baldi",
                new State(
                    new DropPortalOnDeath("Pizza Parlor", 0.2, null),
                    new ScaleHP(0.05),
                    new State("1",
                        new PlayerWithinTransition(15, "1a"),
                        new ConditionalEffect(ConditionEffectIndex.Invincible)
                        ),
                    new State("1a",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new Taunt("..."),
                        new TimedTransition(3000, "2")
                        ),
                    new State("2",
                        new SoundPlay(0),
                        new RemoveConditionalEffect(ConditionEffectIndex.Invincible),
                        new Wander(0.5),
                        new Follow(0.8, 99, 0),
                        new Shoot(99, 5, 5, 1, coolDown: 800),
                        new Shoot(99, 8, 45, 0, coolDown: 1000),
                        new HpLessTransition(0.6, "2w")
                        ),
                    new State("2w",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new Taunt("...."),
                        new TimedTransition(3500, "3")
                        ),
                    new State("3",
                        new Wander(0.5),
                        new Follow(1.75, 99, 0),
                        new Flash(0xFF0000, 1, 2),
                        new Taunt("you'rE DOING FANTASITIC"),
                        new Shoot(99, 20, 18, 2, coolDown: 100),
                        new HpLessTransition(0.2, "dead2")
                        ),
                    new State("dead2",
                        new Suicide()
                        )
                    ),
            new Threshold(0.0001,
                new ItemLoot("baldi HEAD why dead", 1 / 200f),
                new ItemLoot("pencil staff", 1 / 50f),
                new ItemLoot("baldi Key", 1 / 15f),

                new ItemLoot("PENCIL CUTTER", 1 / 50f),
                new ItemLoot("SWEDEN GANG", 1 / 50f),
                new ItemLoot("Legendsary Token 1", 1 / 5f),
                new ItemLoot("NOTEBOOK BALDI", 1 / 50f),
                new ItemLoot("Spaceship Token", 1 / 500f),
                new ItemLoot("Dreadcull T3 Upgrade", 1 / 300f)
                    )
                )
            .Init("Billy Herrington",
                new State(

                    new ScaleHP(0.05),
                    new State("1",
                        new PlayerWithinTransition(15, "1a"),
                        new ConditionalEffect(ConditionEffectIndex.Invincible)
                        ),
                    new State("1a",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new Taunt("ASS WE CAN"),
                        new TimedTransition(3000, "2")
                        ),
                    new State("2",
                        new RemoveConditionalEffect(ConditionEffectIndex.Invincible),
                        new Wander(0.4),
                        new SpiralShoot(6, 360, 6, 60, 0, 0, coolDown: 200),
                        new Shoot(99, 8, 20, 0, coolDown: 1500),
                        new HpLessTransition(0.7, "3")
                        ),
                    new State("3",
                        new ReturnToSpawn(1),
                        new Wander(0.4),
                        new Taunt("GET YOUR ASS BACK HERE"),
                        new Shoot(99, 8, 20, 0, coolDown: 300),
                        new Shoot(99, 3, 5, 0, coolDown: 1000),
                        new HpLessTransition(0.2, "dead1")
                        ),
                    new State("dead1",
                        new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                        new Taunt("im taking that ass.. u want me to take it too dont u"),
                        new Flash(0x0000FF, 1, 2),
                        new TimedTransition(2000, "dead2")
                        ),
                    new State("dead2",
                        new Suicide()
                        )
                    ),
            new Threshold(0.0001,
                new ItemLoot("ANIKI PRISM", 1 / 200f),
                new ItemLoot("Legendsary Token 1", 1 / 5f),
                new ItemLoot("ass dagger", 1 / 50f),
                new ItemLoot("Sex DUNGEON Key", 1 / 15f),

                new ItemLoot("robes of cock", 1 / 50f),
                new ItemLoot("SLAVE BOW", 1 / 50f),
                new ItemLoot("Spaceship Token", 1 / 500f),
                new ItemLoot("Dreadcull T3 Upgrade", 1 / 300f)
                    )
                )
            .Init("soulja boi",
                new State(

                    new ScaleHP(0.05),
                    new State("1",
                        new PlayerWithinTransition(15, "1a"),
                        new ConditionalEffect(ConditionEffectIndex.Invincible)
                        ),
                    new State("1a",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new Taunt("i love rs gold man"),
                        new SoundPlay(0),
                        new TimedTransition(3000, "2")
                        ),
                    new State("2",
                        new RemoveConditionalEffect(ConditionEffectIndex.Invincible),
                        new Wander(0.4),
                        new Shoot(99, 8, 15, 0, coolDown: 1000),
                        new Shoot(99, 8, 45, 0, coolDown: 500),
                        new HpLessTransition(0.7, "3")
                        ),
                    new State("3",
                        new ReturnToSpawn(1),
                        new Wander(0.4),
                        new SpiralShoot(30, 360, 6, 177, 1, coolDown: 800),
                        new Shoot(99, 12, 16, 0, coolDown: 500),
                        new HpLessTransition(0.2, "dead1")
                        ),
                    new State("dead1",
                        new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                        new Taunt("CRANK DAT"),
                        new Flash(0x0000FF, 1, 2),
                        new TimedTransition(2000, "dead2")
                        ),
                    new State("dead2",
                        new Suicide()
                        )
                    ),
            new Threshold(0.0001,
                new ItemLoot("Legendsary Token 1", 1 / 5f),
                new ItemLoot("Orendands Ring", 1 / 150f),
                new ItemLoot("Cash Robe", 1 / 150f),
                new ItemLoot("cash trap", 1 / 50f),
                new ItemLoot("Soulja in Egypt key", 1 / 15f),

                new ItemLoot("money hide", 1 / 50f),
                new ItemLoot("dollar STAR", 1 / 50f),
                new ItemLoot("Spaceship Token", 1 / 500f),
                new ItemLoot("Dreadcull T3 Upgrade", 1 / 300f)
                    )
                )
        .Init("finnish warrior",
                new State(

                    new ScaleHP(0.05),
                    new State("1",
                        new PlayerWithinTransition(15, "1a"),
                        new ConditionalEffect(ConditionEffectIndex.Invincible)
                        ),
                    new State("1a",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new Taunt("rakettien liukumäki"),
                        new TimedTransition(2000, "2")
                        ),
                    new State("2",
                        new RemoveConditionalEffect(ConditionEffectIndex.Invincible),
                        new Wander(1),
                        new Shoot(99, 36, 10, 1, coolDown: 2500),
                        new Shoot(99, 8, 45, 0, coolDown: 5000),
                        new HpLessTransition(0.4, "3")
                        ),
                    new State("3",
                        new Follow(0.3, 30, 0, coolDown: 10),
                        new Shoot(99, 8, 45, 0, coolDown: 5000),
                        new Shoot(99, 18, 10, 1, coolDown: 800, rotateAngle: 15, defaultAngle: 0),
                        new HpLessTransition(0.2, "dead1")
                        ),
                    new State("dead1",
                        new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                        new Taunt("vihaan ruotsia"),
                        new Flash(0xFFA500, 0.2, 10),
                        new TimedTransition(1000, "dead2")
                        ),
                    new State("dead2",
                        new Suicide()
                        )
                    ),
            new Threshold(0.0001,
                new ItemLoot("DANGER PASKA", 1 / 150f),
                new ItemLoot("hide armor", 1 / 100f),
                new ItemLoot("Legendsary Token 1", 1 / 5f),
                new ItemLoot("finsword", 1 / 50f),
                new ItemLoot("FINLAND key", 1 / 15f),

                new ItemLoot("suomi perkele", 1 / 50f),
                new ItemLoot("FINLAND SCEPTER", 1 / 50f),
                new ItemLoot("Dreadcull T4 Upgrade", 1 / 300f),
                new ItemLoot("FINLAND PRIDE", 1 / 10000f),
                new ItemLoot("Spaceship Token", 1 / 500f)
                    )
                )
        .Init("puggsy",
                new State(
                    new DropPortalOnDeath("Me'Cosi's Island", 0.2, null),
                    new StayCloseToSpawn(4, 11),
                    new ScaleHP(0.05),
                    new State("1",
                        new PlayerWithinTransition(15, "1a"),
                        new ConditionalEffect(ConditionEffectIndex.Invincible)
                        ),
                    new State("1a",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new Flash(0xFFFFFF, 0.5, 6),
                        new TimedTransition(3000, "2")
                        ),
                    new State("2",
                        new RemoveConditionalEffect(ConditionEffectIndex.Invincible),
                        new Buzz(0.5, 3.1),
                        new Shoot(99, 5, 25, 1, coolDown: 1100),
                        new SpiralShoot(5, 360, 8, 45, 0, 0, coolDown: 200),
                        new HpLessTransition(0.4, "3")
                        ),
                    new State("3",
                        new Wander(0.5),
                        new Shoot(99, 32, 11.25, 0, coolDown: 2200),
                        new Shoot(99, 32, 5, 0, coolDown: 999999),
                        new Shoot(99, 8, 45, 1, coolDown: 200, rotateAngle: 5),
                        new HpLessTransition(0.1, "dead1")
                        ),
                    new State("dead1",
                        new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                        new Taunt("super swag"),
                        new Flash(0xFFA500, 0.2, 10),
                        new TimedTransition(2000, "dead2")
                        ),
                    new State("dead2",
                        new Suicide()
                        )
                    ),
            new Threshold(0.0001,
                new ItemLoot("Invulnerability Shield", 1 / 500f),
                new ItemLoot("tomatoes", 1 / 15f),
                new ItemLoot("tomatoes", 1 / 25f),
                new ItemLoot("tomatoes", 1 / 55f),
                new ItemLoot("tomatoes", 1 / 65f),
                new ItemLoot("tomatoes", 1 / 35f),
                new ItemLoot("tomatoes", 1 / 25f),
                new ItemLoot("tomatoes", 1 / 55f),
                new ItemLoot("tomatoes", 1 / 65f),
                new ItemLoot("tomatoes", 1 / 25f),
                new ItemLoot("tomatoes", 1 / 25f),
                new ItemLoot("tomatoes", 1 / 55f),
                new ItemLoot("tomatoes", 1 / 65f),
                new ItemLoot("tomatoes", 1 / 45f),
                new ItemLoot("tomatoes", 1 / 25f),
                new ItemLoot("tomatoes", 1 / 55f),
                new ItemLoot("tomatoes", 1 / 65f),
                new ItemLoot("tomatoes", 1 / 25f),
                new ItemLoot("tomatoes", 1 / 25f),
                new ItemLoot("tomatoes", 1 / 55f),
                new ItemLoot("tomatoes", 1 / 65f),
                new ItemLoot("tomatoes", 1 / 35f),
                new ItemLoot("tomatoes", 1 / 25f),
                new ItemLoot("tomatoes", 1 / 55f),
                new ItemLoot("tomatoes", 1 / 65f),
                new ItemLoot("Dreadcull T5 Upgrade", 1 / 300f),
                new ItemLoot("tomatoes", 1 / 75f),
                new ItemLoot("Spaceship Token", 1 / 500f),
                new ItemLoot("throwing quiver", 1 / 50f),
                new ItemLoot("poggy gun", 1 / 50f)
                    )
                )
        .Init("GUMBALL",
                new State(

                    new StayCloseToSpawn(4, 12),
                    new ScaleHP(0.5),
                    new State("1",
                        new PlayerWithinTransition(15, "1a"),
                        new ConditionalEffect(ConditionEffectIndex.Invincible)
                        ),
                    new State("1a",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new Flash(0x00FFFF, 0.5, 6),
                        new TimedTransition(3000, "2")
                        ),
                    new State("2",
                        new RemoveConditionalEffect(ConditionEffectIndex.Invincible),
                        new Wander(1),
                        new Shoot(99, 5, 6, 1, coolDown: 1700),
                        new SpiralShoot(15, 360, 10, 18, 0, 0, coolDown: 1200),
                        new HpLessTransition(0.4, "3")
                        ),
                    new State("3",
                        new Wander(0.5),
                        new SpiralShoot(15, 360, 10, 18, 0, 0, coolDown: 1500),
                        new Shoot(99, 5, 10, 1, coolDown: 1100, rotateAngle: 5),
                        new HpLessTransition(0.1, "dead1")
                        ),
                    new State("dead1",
                        new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                        new Taunt("Black Lives Matter"),
                        new Flash(0xFFFF00, 0.2, 10),
                        new TimedTransition(2000, "dead2")
                        ),
                    new State("dead2",
                        new Suicide()
                        )
                    ),
            new Threshold(0.0001,
                new ItemLoot("50k Fame", 1 / 100f),
                new ItemLoot("gumy staff", 1 / 50f),
                //new ItemLoot("forbidden key", 1 / 500f),
                new ItemLoot("yetimi gay rat", 1 / 50f),
                new ItemLoot("Gumball Land Key", 1 / 15f),
                new ItemLoot("mouldy YUM YUM", 1 / 50f),
                new ItemLoot("Dreadcull T8 Upgrade", 1 / 300f),
                new ItemLoot("sealed candy skull", 1 / 200f),
                new ItemLoot("Deep Sea Token", 1 / 500f)
                    )
                )
        .Init("sun baby",
                new State(

                    new StayCloseToSpawn(4, 11),
                    new ScaleHP(0.05),
                    new State("1",
                        new PlayerWithinTransition(15, "1a"),
                        new ConditionalEffect(ConditionEffectIndex.Invincible)
                        ),
                    new State("1a",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new Flash(0xFFFFFF, 0.5, 6),
                        new TimedTransition(3000, "2")
                        ),
                    new State("2",
                        new Taunt("subscribe to twitch.tv/dogfarm123 with your twitch primes"),
                        new RemoveConditionalEffect(ConditionEffectIndex.Invincible),
                        new Buzz(0.5, 3.1, coolDown: 10),
                        new Shoot(99, 5, 25, 1, coolDown: 3200),
                        new SpiralShoot(5, 360, 10, 36, 0, 0, coolDown: 2400),
                        new TimedTransition(3000, "2a"),
                        new HpLessTransition(0.4, "3")
                        ),
                    new State("2a",
                        new RemoveConditionalEffect(ConditionEffectIndex.Invincible),
                        new Buzz(0.5, 3.1, coolDown: 10),
                        new Shoot(99, 5, 25, 1, coolDown: 3200),
                        new SpiralShoot(-5, 360, 10, 36, 0, 0, coolDown: 1200),
                        new TimedTransition(3000, "2a"),
                        new HpLessTransition(0.4, "3")
                        ),
                    new State("3",
                        new Wander(0.5),
                        new Shoot(99, 10, 36, 0, coolDown: 4200),
                        new Shoot(99, 8, 45, 1, coolDown: 1600, rotateAngle: 5),
                        new HpLessTransition(0.1, "dead1")
                        ),
                    new State("dead1",
                        new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                        new Taunt("WAAAAAAAAAAA"),
                        new Flash(0xFFA500, 0.2, 10),
                        new TimedTransition(2000, "dead2")
                        ),
                    new State("dead2",
                        new Suicide()
                        )
                    ),
            new Threshold(0.0001,
                new ItemLoot("sun bottle", 1 / 150f),
                new ItemLoot("sunblade", 1 / 50f),
                new ItemLoot("Sun Field key", 1 / 15f),
                new ItemLoot("Dreadcull T5 Upgrade", 1 / 300f),
                new ItemLoot("sunbab2", 1 / 50f),
                new ItemLoot("sun book", 1 / 50f),
                new ItemLoot("sunburnt robe", 1 / 50f)


                    )
                )
            .Init("shoop da woop",
                new State(

                    new ScaleHP(0.05),
                    new State("1",
                        new PlayerWithinTransition(15, "1a"),
                        new ConditionalEffect(ConditionEffectIndex.Invincible)
                        ),
                    new State("1a",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new Taunt("IMA FIRIN MY LAZAH"),
                        new SoundPlay(0),
                        new TimedTransition(3000, "2")
                        ),
                    new State("2",
                        new RemoveConditionalEffect(ConditionEffectIndex.Invincible),
                        new Wander(0.2),
                        new Shoot(radius: 99, count: 10, projectileIndex: 0, fixedAngle: 36, coolDown: 40),
                        new TimedTransition(2000, "2w"),
                        new HpLessTransition(0.5, "3")
                        ),
                    new State("2w",
                        new Shoot(radius: 99, count: 10, projectileIndex: 0, fixedAngle: 36, angleOffset: 15, coolDown: 40),
                        new TimedTransition(2000, "2"),
                        new HpLessTransition(0.5, "3")
                        ),
                    new State("3",
                        new ReturnToSpawn(1),
                        new Wander(0.4),
                        new SpiralShoot(180, 360, 12, 16, 1, coolDown: 2000),
                        new Shoot(99, 8, 20, 0, coolDown: 3000),
                        new HpLessTransition(0.2, "dead1")
                        ),
                    new State("dead1",
                        new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                        new Taunt("BRAH"),
                        new Flash(0x0000FF, 1, 2),
                        new TimedTransition(2000, "dead2")
                        ),
                    new State("dead2",
                        new Suicide()
                        )
                    ),
            new Threshold(0.0001,
                new ItemLoot("Legendsary Token 1", 1 / 5f),
                new ItemLoot("LAZER GUN REAL", 1 / 100f),
                new ItemLoot("LAZER ESENZE", 1 / 50f),
                new ItemLoot("rainbow road key", 1 / 15f),

                new ItemLoot("rainbow SEAL", 1 / 50f),
                new ItemLoot("LAZAH", 1 / 50f),
                new ItemLoot("Dreadcull T4 Upgrade", 1 / 300f)
                    )
                )
        .Init("dancing triangle",
                new State(

                    new ScaleHP(0.05),
                    new State("1",
                        new PlayerWithinTransition(15, "1a"),
                        new ConditionalEffect(ConditionEffectIndex.Invincible)
                        ),
                    new State("1a",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new Flash(0xFF0000, 1, 3),
                        new TimedTransition(3000, "2")
                        ),
                    new State("2",
                        new RemoveConditionalEffect(ConditionEffectIndex.Invincible),
                        new Shoot(radius: 99, count: 6, projectileIndex: 0, fixedAngle: 60, rotateAngle: 3, coolDown: 150),
                        new HpLessTransition(0.7, "3")
                        ),
                    new State("3",
                        new Wander(0.5),
                        new Shoot(99, 8, 20, 0, coolDown: 2000),
                        new HpLessTransition(0.2, "dead1")
                        ),
                    new State("dead1",
                        new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                        new Taunt("YAJAJAJAAAJA"),
                        new Flash(0x0000FF, 1, 2),
                        new TimedTransition(2000, "dead2")
                        ),
                    new State("dead2",
                        new Suicide()
                        )
                    ),
            new Threshold(0.0001,
                new ItemLoot("Legendsary Token 1", 1 / 5f),
                new ItemLoot("triangle helmet", 1 / 150f),
                new ItemLoot("for real this time", 1 / 50f),
                new ItemLoot("for real this time", 1 / 50f),
                new ItemLoot("for real this time", 1 / 50f),
                new ItemLoot("for real this time", 1 / 50f),
                new ItemLoot("for real this time", 1 / 50f),
                new ItemLoot("for real this time", 1 / 50f),
                new ItemLoot("for real this time", 1 / 50f),
                new ItemLoot("for real this time", 1 / 50f),
                new ItemLoot("for real this time", 1 / 50f),
                new ItemLoot("shape shoes", 1 / 50f),
                new ItemLoot("yellows sword", 1 / 50f),
                new ItemLoot("Dreadcull T4 Upgrade", 1 / 300f)
                    )
                )
        .Init("yakupozi",
                new State(

                    new ScaleHP(0.3),
                    new State("1",
                        new PlayerWithinTransition(15, "1a"),
                        new ConditionalEffect(ConditionEffectIndex.Invincible)
                        ),
                    new State("1a",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new Flash(0x0000FF, 0.5, 6),
                        new TimedTransition(2000, "2")
                        ),
                    new State("2",
                        new SoundPlay(0),
                        new Taunt("WAAAAAAAAAAAA"),
                        new RemoveConditionalEffect(ConditionEffectIndex.Invincible),
                        new SpiralShoot(2, 180, 6, null, 0, 60, coolDown: 200),
                        new Shoot(99, 3, 3, 0, coolDown: 1500),
                        new HpLessTransition(0.7, "3")
                        ),
                    new State("3",
                        new Wander(0.5),
                        new Shoot(99, 8, 20, 0, coolDown: 500),
                        new HpLessTransition(0.2, "dead1")
                        ),
                    new State("dead1",
                        new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                        new SoundPlay(0),
                        new Taunt("WAAAAAAAAAAAA"),
                        new Flash(0x0000FF, 1, 2),
                        new TimedTransition(2000, "dead2")
                        ),
                    new State("dead2",
                        new Suicide()
                        )
                    ),
            new Threshold(0.0001,
                //new ItemLoot("revives free", 1 / 1000f),
                new ItemLoot("Potion of Life", 0.5),
                new ItemLoot("Potion of Mana", 0.5),
                new ItemLoot("Potion of Vitality", 1),
                new ItemLoot("Dreadcull T4 Upgrade", 1 / 300f),
                new ItemLoot("Potion of Dexterity", 1),
                new ItemLoot("Potion of Speed", 1),
                new ItemLoot("Potion of Attack", 1),
                new ItemLoot("Potion of Defense", 1),
                new ItemLoot("Potion of Wisdom", 1)
                    )
                )
        .Init("Me'Cosi",
            new State(

                new ScaleHP(0.5),
                new State("1",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new PlayerWithinTransition(5, "2", true)
                    ),
                new State("2",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new Taunt(". . ."),
                    new Flash(0xFF0000, 2, 2),
                    new ChangeMusic("thecoveboss"),
                    new TimedTransition(3500, "3")
                    ),
                new State("3",
                    new StayCloseToSpawn(0.5, 5),
                    new Wander(0.1),
                    new SpiralShoot(10, 20, 10, 45, 0, coolDown: 333),
                    new Shoot(99, 4, 11, 1, coolDown: 4000),
                    new HpLessTransition(0.75, "4")
                    ),
                new State("4",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new ReturnToSpawn(2, 99),
                    new Taunt("!"),
                    new TossObject("Mini Mushroom", range: 2, angle: 270, coolDown: 99999),
                    new TossObject("Mini Mushroom", range: 2, angle: 0, coolDown: 99999),
                    new TossObject("Mini Mushroom", range: 2, angle: 90, coolDown: 99999),
                    new TossObject("Mini Mushroom", range: 2, angle: 180, coolDown: 99999),
                    new TimedTransition(3000, "5")
                    ),
                new State("5",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new Shoot(99, 20, 18, 2, coolDown: 15000),
                    new Taunt("roft neccopolis came out :("),
                    new EntityNotExistsTransition("Mini Mushroom", 99, "6")
                    ),
                new State("6",
                    new Taunt(":mushroom:"),
                    new Reproduce("Mini Mushroom", densityRadius: 24, densityMax: 3, coolDown: 2000),
                    new Shoot(99, 4, 90, 3, 90, coolDown: 200),
                    new Shoot(99, 4, 41, 4, coolDown: 1800),
                    new HpLessTransition(0.25, "7")
                    ),
                new State("7",
                    new Taunt("!!!"),
                    new Flash(0xFF0000, 3, 6),
                    new Follow(1, 10, 3),
                    new Shoot(99, 6, 24, 5, coolDown: 300),
                    new Shoot(99, 10, 36, 4, coolDown: 300, coolDownOffset: 350)
                    )
                ),
            new Threshold(0.0001,
                new ItemLoot("Deep Sea Token", 1 / 300f),
                new ItemLoot("Legendsary Token 32", 1 / 3f),
                new ItemLoot("Legendsary Token 64", 1 / 25f),
                new ItemLoot("Dreadcull T8 Upgrade", 1 / 200f),
                new ItemLoot("50k Fame", 1 / 25f),
                new ItemLoot("mushroom helm", 1 / 50f),
                new ItemLoot("yes yes yes yes", 1 / 50f),
                new ItemLoot("mushroom brown", 1 / 50f),
                new ItemLoot("shroom shield", 1 / 150f)
                )
            )
    .Init("Mini Mushroom",
            new State(

                new ScaleHP(1),
                new State("1",
                    new Follow(1.5, 99, 0),
                    new Wander(0.25),
                    new Shoot(10, count: 3, shootAngle: 37, projectileIndex: 0, fixedAngle: 0, coolDown: 700, coolDownOffset: 0),
                    new Shoot(10, count: 3, shootAngle: 37, projectileIndex: 0, fixedAngle: 180, coolDown: 700, coolDownOffset: 0),
                    new Shoot(10, count: 3, shootAngle: 37, projectileIndex: 0, fixedAngle: 90, coolDown: 700, coolDownOffset: 800),
                    new Shoot(10, count: 3, shootAngle: 37, projectileIndex: 0, fixedAngle: 270, coolDown: 700, coolDownOffset: 800)
                    )
                ),
            new Threshold(0.0001,
                new ItemLoot("shroom shield", 1 / 1000000f)
                )
            )
        .Init("Cosi Rage",
            new State(
                new State("waiting",
                    new ConditionalEffect(ConditionEffectIndex.Invincible, true)
                    ),
                new State("2",
                    new ChangeGroundOnDeath(new[] { "SB Tile4" }, new[] { "SB Tile4Boss" }, 200),
                    new Suicide()
                    )
                )
            )

        .Init("octahedron LEGS Spawner",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new State("1",
                        new Spawn("octahedron LEGS", 1, 1, 1000000),
                        new TimedTransition(1000, "2")
                        ),
                    new State("2",
                        new EntityNotExistsTransition("octahedron LEGS", 999, "3")
                        ),
                    new State("3",
                        new TimedTransition(10000, "1")
                        )
                    )
                )
        .Init("octahedron LEGS",
            new State(

                new ScaleHP(0.05),
                new State("1",
                    new Follow(1.1, 99, 0),
                    new Wander(0.25),
                    new Taunt("GUH"),
                    new Shoot(99, count: 2, shootAngle: 7, projectileIndex: 1, fixedAngle: 0, coolDown: 300, coolDownOffset: 0),
                    new Shoot(99, count: 2, shootAngle: 15, projectileIndex: 1, fixedAngle: 180, coolDown: 300, coolDownOffset: 0),
                    new Shoot(99, count: 3, shootAngle: 45, projectileIndex: 1, fixedAngle: 90, coolDown: 300, coolDownOffset: 800),
                    new Shoot(99, count: 5, shootAngle: 120, projectileIndex: 1, fixedAngle: 270, coolDown: 300, coolDownOffset: 800),
                    new TimedTransition(10000, "2")
                    ),
                new State("2",
                    new Wander(0.25),
                    new Taunt("i cannot breathe bro i a octahedron"),
                    new Shoot(99, count: 3, shootAngle: 37, projectileIndex: 0, rotateAngle: 30, fixedAngle: 0, coolDown: 300, coolDownOffset: 0),
                    new Shoot(99, count: 3, shootAngle: 37, projectileIndex: 0, rotateAngle: 30, fixedAngle: 180, coolDown: 300, coolDownOffset: 0),

                    new TimedTransition(10000, "3")
                    ),
                new State("3",
                    new Wander(0.25),
                    new Taunt("oh yeah you got an ass on you alright spread your ass open dude you could do the rumpshake huh the thugshaker gimme the thugshaker dude shake your ass take your hands off it and shake that shit put your shit up i know u can shake it shake it! yeah thats some thug ass right there oh yeah work look good bro yeah yeah nice huh alright that'll work for him put that condom on you ready to shit on that shit?  man letrs just get this over with alright lets get this over witg"),
                    new Shoot(99, count: 3, shootAngle: 37, projectileIndex: 2, rotateAngle: 15, fixedAngle: 90, coolDown: 400, coolDownOffset: 0),
                    new Shoot(99, count: 3, shootAngle: 37, projectileIndex: 2, rotateAngle: 15, fixedAngle: 180, coolDown: 400, coolDownOffset: 0),
                    new Shoot(99, count: 3, shootAngle: 37, projectileIndex: 2, rotateAngle: 15, fixedAngle: 90, coolDown: 400, coolDownOffset: 0),
                    new Shoot(99, count: 3, shootAngle: 37, projectileIndex: 2, rotateAngle: 15, fixedAngle: 270, coolDown: 400, coolDownOffset: 0),
                    new TimedTransition(10000, "1")
                    )
                ),
            new Threshold(0.0001,
                new ItemLoot("Spaceship Token", 1 / 500f),
                new ItemLoot("dagger hedron", 1 / 50f),
                new ItemLoot("pink pickle thung", 1 / 50f),
                new ItemLoot("octahedron LAIR Key", 1 / 15f),
                new ItemLoot("Dreadcull T6 Upgrade", 1 / 300f),
                new ItemLoot("what the fuck", 1 / 50f),
                new ItemLoot("octahedron", 1 / 200f),
                new ItemLoot("LEGS", 1 / 200f)
                )
            )
        .Init("SKELETON Spawner",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new State("1",
                        new Spawn("necrop SKELETON", 1, 1, 1000000),
                        new TimedTransition(500, "2")
                        ),
                    new State("2",
                        new EntityNotExistsTransition("necrop SKELETON", 999, "3")
                        ),
                    new State("3",
                        new TimedTransition(2000, "1")
                        )
                    )
                )
        .Init("necrop SKELETON",
            new State(
                new State("1",
                    new Follow(1.5, 99, 0),
                    new Wander(0.25),
                    new Shoot(10, count: 3, shootAngle: 10, projectileIndex: 0, coolDown: 1500, coolDownOffset: 0)
                    )
                )
            )
        .Init("necrop NORGHUS",
                new State(
                    new ScaleDEF(0.2),
                    new ScaleHP(1),
                    new State("attack 1",
                        new HpLessTransition(.8, "shadowmans"),
                        new State("spirals 1",
                            new Taunt(0.7, "BURN!!", "HAHAHAHAHAHA!", "LEAVE THIS PLACE!"),
                            new SetAltTexture(0),
                            new Sequence(
                                new SpiralShoot(20, 4, 3, 10, fixedAngle: 160, range: 20, coolDown: 200),
                                new Simultaneous(
                                    new SpiralShoot(10, 4, 3, 10, fixedAngle: 100, range: 20, coolDown: 200),
                                    new SpiralShoot(10, 4, 3, 10, fixedAngle: 220, range: 20, coolDown: 200),
                                    new SpiralShoot(10, 4, 3, 10, fixedAngle: 340, range: 20, coolDown: 200)
                                    ),
                                new SpiralShoot(9, 11, 6, 60, 1, fixedAngle: 0, range: 20, coolDown: 200),
                                new SpiralShoot(-9, 6, 6, 60, 1, fixedAngle: 27, range: 20, coolDown: 200),
                                new SpiralShoot(9, 7, 6, 60, fixedAngle: 45, range: 20, coolDown: 200),
                                new SpiralShoot(-9, 11, 6, 60, fixedAngle: 99, range: 20, coolDown: 200),
                                new SpiralShoot(9, 8, 6, 60, 1, fixedAngle: 9, range: 20, coolDown: 200),
                                new TransitionOnTick("idle 1")
                                )
                            ),
                        new State("idle 1",
                            new Taunt(0.5, "Foolish whelps... leave me be..."),
                            new ConditionalEffect(ConditionEffectIndex.Armored),
                            new SetAltTexture(0),
                            new Flash(0xfFF0000, 0.5, 7),
                            new TimedTransition(4000, "spirals 1")
                            )
                        ),
                    new State("shadowmans",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new State("make some shadowmans",
                            new Taunt("BE CONSUMED BY SHADOW!"),
                            new TossObject("shtrs shadowmans", 5, 0, coolDown: 9999999, coolDownOffset: 1900, count: 8, angleOffset: 45),
                            new TimedTransition(4000, "shadowmans wait")
                            ),
                        new State("shadowmans wait",
                            new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                            new EntityNotExistsTransition("shtrs shadowmans", 100, "attack 2")
                            )
                        ),
                    new State("attack 2",
                        new HpLessTransition(.6, "eyes"),
                        new State("spirals 2",
                            new Taunt(0.7, "BURN!!", "HAHAHAHAHAHA!", "LEAVE THIS PLACE!"),
                            new SetAltTexture(0),
                            new Sequence(
                                new SpiralShoot(20, 4, 3, 10, fixedAngle: 160, range: 20, coolDown: 200),
                                new Simultaneous(
                                    new SpiralShoot(10, 4, 3, 10, fixedAngle: 100, range: 20, coolDown: 200),
                                    new SpiralShoot(10, 4, 3, 10, fixedAngle: 220, range: 20, coolDown: 200),
                                    new SpiralShoot(10, 4, 3, 10, fixedAngle: 340, range: 20, coolDown: 200)
                                    ),
                                new SpiralShoot(9, 11, 6, 60, 1, fixedAngle: 0, range: 20, coolDown: 200),
                                new SpiralShoot(-9, 6, 6, 60, 1, fixedAngle: 27, range: 20, coolDown: 200),
                                new SpiralShoot(9, 7, 6, 60, fixedAngle: 45, range: 20, coolDown: 200),
                                new SpiralShoot(-9, 11, 6, 60, fixedAngle: 99, range: 20, coolDown: 200),
                                new SpiralShoot(9, 8, 6, 60, 1, fixedAngle: 9, range: 20, coolDown: 200),
                                new TransitionOnTick("idle 2")
                                )
                            ),
                        new State("idle 2",
                            new Taunt(0.5, "Foolish whelps... leave me be..."),
                            new ConditionalEffect(ConditionEffectIndex.Armored),
                            new SetAltTexture(0),
                            new Flash(0xfFF0000, 0.5, 7),
                            new TimedTransition(4000, "spirals 2")
                            )
                        ),
                    new State("eyes",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Taunt("EYE see you!"),
                        new EntityNotExistsTransition("shtrs eyeswarmer", 10, "attack 3")
                        ),
                    new State("attack 3",
                        new HpLessTransition(.4, "blobombs"),
                        new Taunt("DO NOT TEST MY PATIENCE!"),
                        new State("spirals 3",
                            new Taunt(0.7, "BURN!!", "HAHAHAHAHAHA!", "LEAVE THIS PLACE!"),
                            new SetAltTexture(0),
                            new Sequence(
                                new SpiralShoot(20, 4, 3, 10, fixedAngle: 160, range: 20, coolDown: 200),
                                new Simultaneous(
                                    new SpiralShoot(10, 4, 3, 10, fixedAngle: 100, range: 20, coolDown: 200),
                                    new SpiralShoot(10, 4, 3, 10, fixedAngle: 220, range: 20, coolDown: 200),
                                    new SpiralShoot(10, 4, 3, 10, fixedAngle: 340, range: 20, coolDown: 200)
                                    ),
                                new SpiralShoot(9, 11, 6, 60, 1, fixedAngle: 0, range: 20, coolDown: 200),
                                new SpiralShoot(-9, 6, 6, 60, 1, fixedAngle: 27, range: 20, coolDown: 200),
                                new SpiralShoot(9, 7, 6, 60, fixedAngle: 45, range: 20, coolDown: 200),
                                new SpiralShoot(-9, 11, 6, 60, fixedAngle: 99, range: 20, coolDown: 200),
                                new SpiralShoot(9, 8, 6, 60, 1, fixedAngle: 9, range: 20, coolDown: 200),
                                new TransitionOnTick("idle 3")
                                )
                            ),
                        new State("idle 3",
                            new Taunt(0.5, "Foolish whelps... leave me be..."),
                            new ConditionalEffect(ConditionEffectIndex.Armored),
                            new SetAltTexture(0),
                            new Flash(0xfFF0000, 0.5, 7),
                            new TimedTransition(4000, "spirals 3")
                            )
                        ),
                    new State("blobombs",
                        new State(
                            new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                            new Taunt("YOU SHALL BE FOOD FOR THE ETHER. BLOBS, ATTACK!"),
                            new TimedTransition(12000, "attack 4")
                            ),
                    new State("attack 4",
                        new HpLessTransition(.2, "pillars"),
                        new Taunt("I WILL NOT FALL!"),
                        new State("spirals 4",
                            new Taunt(0.7, "BURN!!", "HAHAHAHAHAHA!", "LEAVE THIS PLACE!"),
                            new SetAltTexture(0),
                            new Sequence(
                                new SpiralShoot(20, 4, 3, 10, fixedAngle: 160, range: 20, coolDown: 200),
                                new Simultaneous(
                                    new SpiralShoot(10, 4, 3, 10, fixedAngle: 100, range: 20, coolDown: 200),
                                    new SpiralShoot(10, 4, 3, 10, fixedAngle: 220, range: 20, coolDown: 200),
                                    new SpiralShoot(10, 4, 3, 10, fixedAngle: 340, range: 20, coolDown: 200)
                                    ),
                                new SpiralShoot(9, 11, 6, 60, 1, fixedAngle: 0, range: 20, coolDown: 200),
                                new SpiralShoot(-9, 6, 6, 60, 1, fixedAngle: 27, range: 20, coolDown: 200),
                                new SpiralShoot(9, 7, 6, 60, fixedAngle: 45, range: 20, coolDown: 200),
                                new SpiralShoot(-9, 11, 6, 60, fixedAngle: 99, range: 20, coolDown: 200),
                                new SpiralShoot(9, 8, 6, 60, 1, fixedAngle: 9, range: 20, coolDown: 200),
                                new TransitionOnTick("idle 4")
                                )
                            ),
                        new State("idle 4",
                            new Taunt(0.5, "Foolish whelps... leave me be..."),
                            new ConditionalEffect(ConditionEffectIndex.Armored),
                            new SetAltTexture(0),
                            new Flash(0xfFF0000, 0.5, 7),
                            new TimedTransition(4000, "spirals 4")
                            )
                        ),
                    new State("pillars",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Taunt("ACTIVATING PILLAR DEFENSES."),
                        new Order(30, "shtrs Pillar 1", "PROTECT"),
                        new Order(30, "shtrs Pillar 2", "PROTECT"),
                        new Order(30, "shtrs Pillar 3", "PROTECT"),
                        new Order(30, "shtrs Pillar 4", "PROTECT"),
                        new EntitiesNotExistsTransition(30, "attack 5", "shtrs Pillar 1", "shtrs Pillar 2", "shtrs Pillar 3", "shtrs Pillar 4")
                        ),
                    new State("attack 5",
                        new HpLessTransition(.05, "die"),
                        new Taunt("DESTROYING ME WILL ONLY BRING YOU CLOSER TO DEATH!"),
                        new State("spirals 5",
                            new Taunt(0.7, "BURN!!", "LEAVE THIS PLACE!"),
                            new SetAltTexture(0),
                            new Sequence(
                                new SpiralShoot(20, 4, 3, 10, fixedAngle: 160, range: 20, coolDown: 200),
                                new Simultaneous(
                                    new SpiralShoot(10, 4, 3, 10, fixedAngle: 100, range: 20, coolDown: 200),
                                    new SpiralShoot(10, 4, 3, 10, fixedAngle: 220, range: 20, coolDown: 200),
                                    new SpiralShoot(10, 4, 3, 10, fixedAngle: 340, range: 20, coolDown: 200)
                                    ),
                                new SpiralShoot(9, 11, 6, 60, 1, fixedAngle: 0, range: 20, coolDown: 200),
                                new SpiralShoot(-9, 6, 6, 60, 1, fixedAngle: 27, range: 20, coolDown: 200),
                                new SpiralShoot(9, 7, 6, 60, fixedAngle: 45, range: 20, coolDown: 200),
                                new SpiralShoot(-9, 11, 6, 60, fixedAngle: 99, range: 20, coolDown: 200),
                                new SpiralShoot(9, 8, 6, 60, 1, fixedAngle: 9, range: 20, coolDown: 200),
                                new TransitionOnTick("idle 5")
                                )
                            ),
                        new State("idle 5",
                            new Taunt(0.5, "Foolish whelps... leave me be..."),
                            new ConditionalEffect(ConditionEffectIndex.Armored),
                            new SetAltTexture(0),
                            new Flash(0xfFF0000, 0.5, 10),
                            new TimedTransition(4000, "spirals 5")
                            )
                        ),
                    new State("die",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new State("die die",
                            new Taunt("YOU KNOW NOT WHAT YOU HAVE DONE!"),
                            new TimedTransition(4000, "suicide")
                            ),
                        new State("suicide",
                            new Suicide()
                         )
                      )
                   )
                ),
                 new Threshold(0.01,
                    //new ItemLoot("Potion of Life", 0.2),
                    //new ItemLoot("Potion of Mana", 0.2),
                    new ItemLoot("godes primal ultimate RING", 1 / 600f),
                    new ItemLoot("THE STORM", 1 / 600f),
                    new ItemLoot("tome of Femoid Attention", 1 / 600f),
                    new ItemLoot("primal NORGHUS Upgrade", 1 / 2500f),
                    new ItemLoot("50k Fame", 1 / 150f),
                    new ItemLoot("Necrospoilis Token 1", 1 / 30f),
                    new ItemLoot("Necrospoilis Token 2", 1 / 150f),
                    new ItemLoot("AR quiver", 1 / 600f),
                    new ItemLoot("Dreadcull T9 Upgrade", 1 / 1000f),
                    new ItemLoot("fleshy brown", 1 / 600f)
            ))

            .Init("necrop LYCAON",
            new State(
                new ScaleDEF(0.2),
                new ScaleHP(1),
                new State("1",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new TimedTransition(2000, "3")
                    ),
                new State("3",
                    new Follow(3, 99, 0, 100000, 100),
                    new Shoot(99, 13, 15, 2, coolDown: 600),
                    new Shoot(99, 13, 15, 2, fixedAngle: 180, coolDown: 600, coolDownOffset: 300),
                    new Shoot(99, 1, null, 0, coolDown: 50)
                        )),
                 new Threshold(0.01,
                    //new ItemLoot("Potion of Life", 0.2),
                    //new ItemLoot("Potion of Mana", 0.2),
                    new ItemLoot("50k Fame", 1 / 150f),
                    new ItemLoot("kitty cat UwU :3", 1 / 600f),
                    new ItemLoot("smile heart cute :)", 1 / 600f),
                    new ItemLoot("Necrospoilis Token 1", 1 / 50f),
                    new ItemLoot("Necrospoilis Token 2", 1 / 150f),
                    new ItemLoot("Dreadcull T9 Upgrade", 1 / 1000f),
                    new ItemLoot("primal LYCAON Upgrade", 1 / 2500f),
                    new ItemLoot("fursuit owo", 1 / 600f),
                    new ItemLoot("rotmg predator Necklace", 1 / 600f)
                )
            )

            .Init("necrop NOVUS",
            new State(
                new ScaleDEF(0.2),
                new ScaleHP(1),
                new State("1",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new TimedTransition(2000, "2")
                    ),
                new State("2",
                    new Shoot(99, count: 1, shootAngle: 1, projectileIndex: 0, rotateAngle: 15, fixedAngle: 0, coolDown: 100, coolDownOffset: 0),
                    new Shoot(99, count: 1, shootAngle: 1, projectileIndex: 1, rotateAngle: 15, fixedAngle: 15, coolDown: 100, coolDownOffset: 0),
                    new Shoot(99, count: 1, shootAngle: 1, projectileIndex: 2, rotateAngle: -30, fixedAngle: 0, coolDown: 100, coolDownOffset: 0)
                        )),
                 new Threshold(0.01,
                    //new ItemLoot("Potion of Life", 0.2),
                    //new ItemLoot("Potion of Mana", 0.2),
                    new ItemLoot("apex Legends", 1 / 600f),
                    new ItemLoot("Dreadcull T9 Upgrade", 1 / 1000f),
                    //new ItemLoot("Shadow warp", 1 / 40000f),
                    new ItemLoot("50k Fame", 1 / 150f),
                    new ItemLoot("Necrospoilis Token 1", 1 / 30f),
                    new ItemLoot("Necrospoilis Token 2", 1 / 150f),
                    new ItemLoot("primal NOVUS Upgrade", 1 / 2500f),
                    new ItemLoot("extreme prejuice of ssceptr?", 1 / 600f),
                    new ItemLoot("polish skull", 1 / 600f)
                )
            )

            .Init("necrop ELITHOR",
            new State(
                new ScaleDEF(0.2),
                new ScaleHP(1),
                new State("1",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new TimedTransition(2000, "2")
                    ),
                new State("2",
                    new Shoot(99, count: 12, shootAngle: 30, projectileIndex: 0, rotateAngle: 15, fixedAngle: 0, coolDown: 100, coolDownOffset: 0),
                    new Shoot(99, count: 12, shootAngle: 30, projectileIndex: 1, rotateAngle: 15, fixedAngle: 15, coolDown: 100, coolDownOffset: 0),
                    new Shoot(99, count: 12, shootAngle: 30, projectileIndex: 2, rotateAngle: -30, fixedAngle: 0, coolDown: 100, coolDownOffset: 0)
                        )),
                 new Threshold(0.01,
                    //new ItemLoot("Potion of Life", 0.2),
                    //new ItemLoot("Potion of Mana", 0.2),
                    new ItemLoot("fleshy ten don", 1 / 300f),
                    new ItemLoot("welding", 1 / 700f),
                    new ItemLoot("Dreadcull T9 Upgrade", 1 / 1000f),
                    new ItemLoot("50k Fame", 1 / 150f),
                    new ItemLoot("Necrospoilis Token 1", 1 / 30f),
                    new ItemLoot("Necrospoilis Token 2", 1 / 150f),
                    new ItemLoot("primal ELITHOR Upgrade", 1 / 2500f),
                    new ItemLoot("destiny modenator", 1 / 600f),
                    new ItemLoot("Twilight Grommit", 1 / 600f),
                    new ItemLoot("eligor goul goglge", 1 / 600f)
                )
            )

            .Init("necrop tHUSULA",
            new State(
                new ScaleDEF(0.2),
                new ScaleHP(1),
                new State("1",
                    new Follow(1.5, 99, 0),
                    new Wander(0.25),
                    new Shoot(10, count: 4, shootAngle: 180, projectileIndex: 0, coolDown: 100, coolDownOffset: 0),
                    new Shoot(10, count: 2, shootAngle: 45, projectileIndex: 0, coolDown: 100, coolDownOffset: 0)
                    )
                ),
             new Threshold(0.01,
                    //new ItemLoot("Potion of Life", 0.2),
                    //new ItemLoot("Potion of Mana", 0.2),
                    new ItemLoot("50k Fame", 1 / 150f),
                    new ItemLoot("Necrospoilis Token 1", 1 / 30f),
                    new ItemLoot("Necrospoilis Token 2", 1 / 150f),
                    new ItemLoot("Dreadcull T9 Upgrade", 1 / 1000f),
                    new ItemLoot("CUCKSON SLASHER (welsh)", 1 / 600f),
                    new ItemLoot("primal THUSULA Upgrade", 1 / 2500f),
                    new ItemLoot("seal balls nuts", 1 / 600f),
                    new ItemLoot("zolmex server breastplate", 1 / 600f)
            ))

            .Init("necrop BEL",

            new State(
                new ScaleDEF(0.2),
                new ScaleHP(1),
                new State("1",
                    new Follow(0.7, 99, 0),
                    new Wander(0.25),
                    new Shoot(10, count: 4, shootAngle: 15, projectileIndex: 0, coolDown: 400, coolDownOffset: 0)
                    )
                ),
             new Threshold(0.01,
                    //new ItemLoot("Potion of Life", 0.2),
                    new ItemLoot("50k Fame", 1 / 150f),
                    new ItemLoot("Dreadcull T9 Upgrade", 1 / 1000f),
                    //new ItemLoot("Potion of Mana", 0.2),
                    new ItemLoot("Necrospoilis Token 1", 1 / 30f),
                    new ItemLoot("Necrospoilis Token 2", 1 / 150f),
                    new ItemLoot("[ROTF] Mad loot montage", 1 / 600f),
                    new ItemLoot("primal BEL Upgrade", 1 / 2500f),
                    new ItemLoot("cucky helmet", 1 / 600f),
                    new ItemLoot("boobie plate", 1 / 600f)
                )
            )

            .Init("necrop DIAGON",
            new State(
                new ScaleDEF(0.2),
                new ScaleHP(1),
                new State("1",
                    new Wander(0.25),
                    new Shoot(99, count: 12, shootAngle: 30, projectileIndex: 0, rotateAngle: 15, fixedAngle: 0, coolDown: 1100, coolDownOffset: 0),
                    new Shoot(10, count: 2, shootAngle: 15, projectileIndex: 0, coolDown: 1000, coolDownOffset: 0)
                    )
                ),
             new Threshold(0.01,
                    //new ItemLoot("Potion of Life", 0.2),
                    //new ItemLoot("Potion of Mana", 0.2),
                    new ItemLoot("50k Fame", 1 / 150f),
                    new ItemLoot("Dreadcull T9 Upgrade", 1 / 1000f),
                    new ItemLoot("Wand of the Cock Balls", 1 / 600f),
                    new ItemLoot("STARFORCE Key", 1 / 15f),
                    new ItemLoot("Necrospoilis Token 1", 1 / 30f),
                    new ItemLoot("Necrospoilis Token 2", 1 / 150f),
                    new ItemLoot("primal DIAGON Upgrade", 1 / 2500f),
                    new ItemLoot("Robe of the Deez Nuts", 1 / 600f),
                    new ItemLoot("no no no no", 1 / 600f)
                )
            )

            .Init("Zucky Zucc",
            new State(
                new ScaleHP(0.1),

                new State("0",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new TimedTransition(3000, "1")
                    ),
                new State("1",
                    new Wander(1),
                    new Shoot(99, count: 6, shootAngle: 60, projectileIndex: 1, rotateAngle: 15, fixedAngle: 0, coolDown: 1100, coolDownOffset: 0),
                    new Shoot(10, count: 2, shootAngle: 15, projectileIndex: 0, coolDown: 500, coolDownOffset: 0)
                    )
                ),
            new Threshold(0.0001,
                    new ItemLoot("50k Fame", 1 / 50f),
                    //new ItemLoot("stupid asshole this is same rarity as handcannon you fucking suck", 1 / 15000f),
                    //new ItemLoot("Dreadcull T8 Upgrade", 1 / 300f),
                    new ItemLoot("primal craft but Common", 1 / 150f),
                    new ItemLoot("Roft Neccopolis Key", 1 / 100f),
                    new ItemLoot("STARFORCE Key", 1 / 15f),
                    //new ItemLoot("Spirit of the Heart", 1 / 1000f),
                    new ItemLoot("HANDCANNON", 1 / 30000f),
                    new ItemLoot("green bean lean machine", 1 / 100f)
                )
            )

            .Init("ARCANUICA image slopy",
            new State(
                new ScaleHP(0.1),

                new State("0",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new TimedTransition(3000, "1")
                    ),
                new State("1",
                    new Follow(1.5, 99, 0),
                    new Wander(0.25),
                    new Shoot(99, count: 6, shootAngle: 10, projectileIndex: 0, coolDown: 100, coolDownOffset: 0)
                    )
                ),
            new Threshold(10,
                new ItemLoot("HANDCANNON", 1 / 30000f)
                ),
            new Threshold(0.0001,
                    new ItemLoot("50k Fame", 1 / 50f),
                    //new ItemLoot("stupid asshole this is same rarity as handcannon you fucking suck", 1 / 15000f),
                    //new ItemLoot("Dreadcull T8 Upgrade", 1 / 300f),
                    //new ItemLoot("Spirit of the Heart", 1 / 1000f),
                    new ItemLoot("primal craft but Common", 1 / 150f),
                    new ItemLoot("STARFORCE Key", 1 / 15f),

                    new ItemLoot("Roft Neccopolis Key", 1 / 100f),
                    new ItemLoot("no mana?", 1 / 100f)
                )
            )

            .Init("EL BANDITONIO rodriguez",
            new State(
                new ScaleHP(0.1),

                new State("0",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new TimedTransition(3000, "1")
                    ),
                new State("1",
                    new Wander(0.25),
                    new Shoot(99, count: 6, shootAngle: 60, projectileIndex: 1, rotateAngle: 15, fixedAngle: 0, coolDown: 100, coolDownOffset: 0),
                    new Shoot(99, count: 4, shootAngle: 15, projectileIndex: 0, coolDown: 400, coolDownOffset: 0)
                    )
                ),
                        new Threshold(10,
                new ItemLoot("HANDCANNON", 1 / 25000f)
                ),
            new Threshold(0.0001,
                    new ItemLoot("50k Fame", 1 / 50f),
                    new ItemLoot("Roft Neccopolis Key", 1 / 100f),
                    new ItemLoot("STARFORCE Key", 1 / 15f),

                    new ItemLoot("primal craft but Common", 1 / 150f)
                //new ItemLoot("Dreadcull T8 Upgrade", 1 / 300f),
                //new ItemLoot("Spirit of the Heart", 1 / 1000f),
                )
            )
        .Init("gemgemgem gem gem",
            new State(
                new ScaleHP(0.1),

                new State("0",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new TimedTransition(3000, "1")
                    ),
                new State("1",
                    new Shoot(99, count: 6, shootAngle: 60, projectileIndex: 1, rotateAngle: 15, fixedAngle: 0, coolDown: 600, coolDownOffset: 0),
                    new Shoot(99, count: 4, shootAngle: 15, projectileIndex: 0, coolDown: 800, coolDownOffset: 0),
                    new Shoot(99, count: 4, shootAngle: 90, projectileIndex: 0, coolDown: 1500, coolDownOffset: 0)
                    )
                ),
            new Threshold(10,
                new ItemLoot("HANDCANNON", 1 / 30000f)
                ),
            new Threshold(0.0001,
                    new ItemLoot("50k Fame", 1 / 50f),
                    //new ItemLoot("Dreadcull T8 Upgrade", 1 / 300f),
                    new ItemLoot("primal craft but Common", 1 / 150f),
                    //new ItemLoot("stupid asshole this is same rarity as handcannon you fucking suck", 1 / 15000f),
                    new ItemLoot("Roft Neccopolis Key", 1 / 100f),
                    //new ItemLoot("Spirit of the Heart", 1 / 1000f),
                    new ItemLoot("STARFORCE Key", 1 / 15f),

                    new ItemLoot("gemgem's gemgem and gem gem's gem", 1 / 100f)
                )
            )
        .Init("rock man i forgot name",
            new State(
                new ScaleHP(0.1),

                new State("0",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new TimedTransition(3000, "1")
                    ),
                new State("1",
                    new Shoot(99, count: 6, shootAngle: 60, projectileIndex: 1, rotateAngle: 15, fixedAngle: 0, coolDown: 1200, coolDownOffset: 0),
                    new Shoot(99, count: 3, shootAngle: 20, projectileIndex: 0, coolDown: 50, coolDownOffset: 0)
                    )
                ),
                        new Threshold(10,
                new ItemLoot("HANDCANNON", 1 / 30000f)
                ),
            new Threshold(0.0001,
                    new ItemLoot("50k Fame", 1 / 50f),
                    new ItemLoot("primal craft but Common", 1 / 150f),
                    //new ItemLoot("stupid asshole this is same rarity as handcannon you fucking suck", 1 / 15000f),
                    new ItemLoot("Roft Neccopolis Key", 1 / 100f),
                    //new ItemLoot("Spirit of the Heart", 1 / 1000f),
                    new ItemLoot("STARFORCE Key", 1 / 15f),

                    //new ItemLoot("Dreadcull T8 Upgrade", 1 / 3000f),
                    new ItemLoot("hornyshot", 1 / 100f)
                )
            )

        .Init("BIG MAN ORTAR",
            new State(
                new ScaleHP(0.1),

                new State("0",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new TimedTransition(3000, "1")
                    ),
                new State("1",
                    new Follow(1, 99, 0),
                    new Wander(0.25),
                     new Shoot(99, count: 6, shootAngle: 600, projectileIndex: 0, coolDown: 1400, coolDownOffset: 0),
                    new Shoot(99, count: 6, shootAngle: 10, projectileIndex: 1, coolDown: 2000, coolDownOffset: 0)
                    )
                ),
                        new Threshold(10,
                new ItemLoot("HANDCANNON", 1 / 30000f)
                ),
            new Threshold(0.0001,
                    new ItemLoot("50k Fame", 1 / 50f),
                    //new ItemLoot("stupid asshole this is same rarity as handcannon you fucking suck", 1 / 15000f),
                    new ItemLoot("Roft Neccopolis Key", 1 / 100f),
                    new ItemLoot("ortars LEAN", 1 / 150f),
                    new ItemLoot("primal craft but Common", 1 / 150f),
                    //new ItemLoot("Spirit of the Heart", 1 / 1000f),
                    new ItemLoot("STARFORCE Key", 1 / 15f),

                    //new ItemLoot("Dreadcull T8 Upgrade", 1 / 300f),
                    new ItemLoot("uohhh ortar Enchantment :sob:", 1 / 100f)

                )
            )

            .Init("crymbal",
            new State(
                new ScaleHP(0.1),
                new State("0",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new TimedTransition(3000, "1")
                    ),
                new State("1",
                    new Shoot(99, count: 4, shootAngle: 90, projectileIndex: 0, coolDown: 500)
                    )
                ),
                        new Threshold(10,
                new ItemLoot("HANDCANNON", 1 / 30000f)
                ),
            new Threshold(0.0001,
                    //new ItemLoot("Dreadcull T8 Upgrade", 1 / 300f),
                    new ItemLoot("50k Fame", 1 / 50f),
                    new ItemLoot("STARFORCE Key", 1 / 15f),

                    new ItemLoot("primal craft but Common", 1 / 150f),
                    //new ItemLoot("Spirit of the Heart", 1 / 1000f),
                    //new ItemLoot("stupid asshole this is same rarity as handcannon you fucking suck", 1 / 15000f),
                    new ItemLoot("Roft Neccopolis Key", 1 / 50f)
                )
            )

            .Init("MOAB",
            new State(

                new State("1",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new TimedTransition(500, "2")
                    ),
                new State("2",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, false, 2000),
                    new Follow(1.5, 99, 0, 100000, 100),
                    new Shoot(99, 1, null, 1, coolDown: 450),
                    new TimedTransition(3500, "3")
                        ),
                new State("3",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, false, 2000),
                    new Wander(1.5),
                    new Shoot(99, 4, 15, 2, coolDown: 600, coolDownOffset: 300),
                    new TimedTransition(3500, "2")
                        )),
            new Threshold(0.0001,
                    new ItemLoot("MONKEY MONEY", 1 / 150f),
                    new ItemLoot("da banana", 1 / 150f),
                    new ItemLoot("moab RPG NUKE", 1 / 150f),
                    new ItemLoot("SNIPER MONKEY HELMET", 1 / 100f),
                    new ItemLoot("ASGARD Token", 1 / 175f),
                    new ItemLoot("FAVELA Token", 1 / 250f),
                    new ItemLoot("Deep Sea Token", 1 / 300f),
                    new ItemLoot("A Fallen Light (Token)", 1 / 500f),
                    new ItemLoot("bloons Key", 1 / 200f),
                    new ItemLoot("ninja monkey RAMPAGE EXTREME", 1 / 300f),
                    new ItemLoot("ENGINEER MONKEY VEST", 1 / 150f)
                )
            )
            .Init("MOAB 2",
            new State(

                new State("1",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new TimedTransition(500, "3")
                    ),
                new State("2",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, false, 2000),
                    new Follow(1.5, 99, 0, 100000, 100),
                    new Shoot(99, 1, null, 1, coolDown: 450),
                    new TimedTransition(3500, "3")
                        ),
                new State("3",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, false, 2000),
                    new Wander(1.5),
                    new Shoot(99, 4, 15, 2, coolDown: 600, coolDownOffset: 300),
                    new TimedTransition(3500, "2")
                        )),
                new Threshold(0.000001,
                    new ItemLoot("moab RPG NUKE", 1 / 300f)
                )
            )

        .Init("DDT",
            new State(

                new State("1",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new TimedTransition(500, "2")
                    ),
                new State("2",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new Follow(3, 99, 0, 100000, 100),
                    new Shoot(99, 10, 32, 0, coolDown: 3500),
                    new PlayerWithinTransition(6, "3")
                        ),
                new State("3",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, false, 1000),
                    new Wander(0.5),
                    new Shoot(99, 12, 30, 1, coolDown: 600, rotateAngle: 4),
                    new TimedTransition(3000, "2")
                        )),
                new Threshold(0.000001,
                    new ItemLoot("MONKEY MONEY", 1 / 150f),
                    new ItemLoot("dartling GUN", 1 / 150f),
                    new ItemLoot("fire BLASTER", 1 / 150f),
                    new ItemLoot("ASGARD Token", 1 / 175f),
                    new ItemLoot("FAVELA Token", 1 / 250f),
                    new ItemLoot("Deep Sea Token", 1 / 300f),
                    new ItemLoot("A Fallen Light (Token)", 1 / 500f),
                    new ItemLoot("bloons Key", 1 / 200f),
                    new ItemLoot("monkey shuriken OG", 1 / 150f),
                    new ItemLoot("ninja monkey RAMPAGE EXTREME", 1 / 300f)

                )
            )

        .Init("Black Bloon",
            new State(
                new State("1",
                    new Taunt("pop"),
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new TimedTransition(2000, "2")
                    ),
                new State("2",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new TossObject("MOAB", 5, 0, 10000000),
                    new TossObject("MOAB", 5, 180, 10000000),

                    new Shoot(99, count: 16, shootAngle: 20, projectileIndex: 0, rotateAngle: 15, fixedAngle: 0, coolDown: 5500, coolDownOffset: 0),
                    new Shoot(99, count: 1, shootAngle: 1, projectileIndex: 1, coolDown: 700, coolDownOffset: 0),
                    new TimedTransition(1500, "3")
                        ),
                new State("3",
                    new Taunt("pop"),
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new TossObject("MOAB 2", 5, 90, 10000000),
                    new TossObject("MOAB 2", 5, 270, 10000000),

                    new Shoot(99, count: 16, shootAngle: 20, projectileIndex: 0, rotateAngle: 15, fixedAngle: 0, coolDown: 5500, coolDownOffset: 0),
                    new Shoot(99, count: 1, shootAngle: 1, projectileIndex: 1, coolDown: 700, coolDownOffset: 0),
                    new TimedTransition(1500, "4")
                        ),
                new State("4",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new EntityNotExistsTransition("MOAB 2", 99, "4a"),
                    new Shoot(99, count: 16, shootAngle: 20, projectileIndex: 0, rotateAngle: 15, fixedAngle: 0, coolDown: 5500, coolDownOffset: 0),
                    new Shoot(99, count: 1, shootAngle: 1, projectileIndex: 1, coolDown: 700, coolDownOffset: 0)
                        ),
                new State("4a",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new EntityNotExistsTransition("MOAB", 99, "5"),
                    new Shoot(99, count: 16, shootAngle: 20, projectileIndex: 0, rotateAngle: 15, fixedAngle: 0, coolDown: 5500, coolDownOffset: 0),
                    new Shoot(99, count: 1, shootAngle: 1, projectileIndex: 1, coolDown: 700, coolDownOffset: 0)
                        ),
                new State("5",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new TossObject("DDT", 5, 90, 10000000),
                    new TossObject("DDT", 5, 270, 10000000),

                    new Shoot(99, count: 16, shootAngle: 20, projectileIndex: 0, rotateAngle: 15, fixedAngle: 0, coolDown: 5500, coolDownOffset: 0),
                    new Shoot(99, count: 1, shootAngle: 1, projectileIndex: 1, coolDown: 700, coolDownOffset: 0),
                    new TimedTransition(1500, "6")
                        ),
                new State("6",
                    new Taunt("pop"),
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new TossObject("DDT", 5, 0, 10000000),
                    new TossObject("DDT", 5, 180, 10000000),

                    new Shoot(99, count: 16, shootAngle: 20, projectileIndex: 0, rotateAngle: 15, fixedAngle: 0, coolDown: 5500, coolDownOffset: 0),
                    new Shoot(99, count: 1, shootAngle: 1, projectileIndex: 1, coolDown: 700, coolDownOffset: 0),
                    new TimedTransition(1500, "7")
                        ),
                new State("7",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new EntityNotExistsTransition("DDT", 99, "8"),
                    new Shoot(99, count: 16, shootAngle: 20, projectileIndex: 0, rotateAngle: 15, fixedAngle: 0, coolDown: 5500, coolDownOffset: 0),
                    new Shoot(99, count: 1, shootAngle: 1, projectileIndex: 1, coolDown: 700, coolDownOffset: 0)
                        ),
                new State("8",
                    new Taunt("pop"),
                    new Shoot(99, count: 16, shootAngle: 20, projectileIndex: 0, rotateAngle: 15, fixedAngle: 0, coolDown: 5500, coolDownOffset: 0),
                    new Shoot(99, count: 1, shootAngle: 1, projectileIndex: 1, coolDown: 700, coolDownOffset: 0)
                        )),
                new Threshold(0.03,
                    new ItemLoot("vortex Key", 1 / 170f),
                    new ItemLoot("lych Key", 1 / 170f),
                    new ItemLoot("bloonarius Key", 1 / 170f),
                    new ItemLoot("50k Fame", 1 / 25f),
                    new ItemLoot("ninja monkey RAMPAGE EXTREME", 1 / 300f),
                    new ItemLoot("bloons Key", 1 / 20f),
                    //new ItemLoot("Implosion", 1 / 35000f),
                    new ItemLoot("MONKEY MONEY", 1 / 150f),
                    new ItemLoot("dartling GUN", 1 / 150f),
                    new ItemLoot("fire BLASTER", 1 / 150f),
                    new ItemLoot("monkey shuriken OG", 1 / 150f),
                    new ItemLoot("ninja monkey RAMPAGE EXTREME", 1 / 300f),

                    new ItemLoot("MONKEY MONEY", 1 / 150f),
                    new ItemLoot("da banana", 1 / 150f),
                    new ItemLoot("SNIPER MONKEY HELMET", 1 / 100f),
                    new ItemLoot("ninja monkey RAMPAGE EXTREME", 1 / 300f),
                    new ItemLoot("ENGINEER MONKEY VEST", 1 / 150f)
                )
            )
        .Init("bloon Spawner",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                new State("0",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new TimedTransition(1000, "1a")
                    ),
                    new State("1a",
                        new Spawn("Black Bloon", 1, 1, 1000000),
                        new TimedTransition(1000, "2a")
                        ),
                    new State("2a",
                        new EntityNotExistsTransition("Black Bloon", 999, "3")
                        ),
                    new State("3",
                        new TimedTransition(8000, "0")
                        )
                    )
                )

        .Init("Vortex Bloons",
            new State(
                new ScaleHP(1),
                new ConditionEffectRegion(ConditionEffectIndex.Nullified, 3, 4),
                new State("1",
                    new Taunt("pop"),
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new TimedTransition(2000, "4")
                    ),
                new State("4",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new Follow(3, 99, 2, 100000, 100),
                    new Shoot(99, count: 4, shootAngle: 20, projectileIndex: 0, coolDown: 2500, coolDownOffset: 0),
                    new Shoot(99, count: 1, shootAngle: 1, projectileIndex: 1, coolDown: 700, coolDownOffset: 0),
                    new TossObject("MOAB", 5, 0, 10000000),
                    new TossObject("MOAB", 5, 180, 10000000),
                    new TimedTransition(6000, "4a")
                        ),
                new State("4a",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new EntityNotExistsTransition("MOAB", 99, "4b"),
                    new Follow(2, 99, 2, 100000, 100),
                    new Shoot(99, count: 4, shootAngle: 20, projectileIndex: 0, coolDown: 2500, coolDownOffset: 0),
                    new Shoot(99, count: 1, shootAngle: 1, projectileIndex: 1, coolDown: 700, coolDownOffset: 0)
                        ),
                new State("4b",
                    new Taunt("pop"),
                    new Wander(2.5),
                    new Shoot(99, count: 4, shootAngle: 20, projectileIndex: 0, coolDown: 2500, coolDownOffset: 0),
                    new Shoot(99, count: 1, shootAngle: 1, projectileIndex: 1, coolDown: 700, coolDownOffset: 0),
                    new HpLessTransition(.5, "5")
                        ),
                new State("5",
                    new Wander(0.5),
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new TossObject("DDT", 5, 90, 10000000),
                    new TossObject("DDT", 5, 270, 10000000),

                    new Shoot(99, count: 4, shootAngle: 20, projectileIndex: 0, coolDown: 2500, coolDownOffset: 0),
                    new Shoot(99, count: 1, shootAngle: 1, projectileIndex: 1, coolDown: 700, coolDownOffset: 0),
                    new TimedTransition(1500, "6")
                        ),
                new State("6",
                    new Wander(1.5),
                    new Taunt("pop"),
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new TossObject("DDT", 5, 0, 10000000),
                    new TossObject("DDT", 5, 180, 10000000),

                    new Shoot(99, count: 4, shootAngle: 20, projectileIndex: 0, coolDown: 2500, coolDownOffset: 0),
                    new Shoot(99, count: 1, shootAngle: 1, projectileIndex: 1, coolDown: 700, coolDownOffset: 0),
                    new TimedTransition(1500, "7")
                        ),
                new State("7",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new Follow(3, 99, 2, 100000, 100),
                    new EntityNotExistsTransition("DDT", 99, "8"),
                    new Shoot(99, count: 16, shootAngle: 20, projectileIndex: 0, rotateAngle: 15, fixedAngle: 0, coolDown: 5500, coolDownOffset: 0),
                    new Shoot(99, count: 1, shootAngle: 1, projectileIndex: 1, coolDown: 700, coolDownOffset: 0)
                        ),
                new State("8",
                    new Taunt("pop"),
                    new Follow(3, 99, 2, 100000, 100),
                    new Shoot(99, count: 4, shootAngle: 20, projectileIndex: 0, coolDown: 2500, coolDownOffset: 0),
                    new Shoot(99, count: 1, shootAngle: 1, projectileIndex: 1, coolDown: 700, coolDownOffset: 0),
                    new TimedTransition(2500, "8a")
                        ),
                new State("8a",
                    new Taunt("pop"),
                    new Wander(2.5),
                    new Shoot(99, count: 4, shootAngle: 20, projectileIndex: 0, coolDown: 2500, coolDownOffset: 0),
                    new Shoot(99, count: 1, shootAngle: 1, projectileIndex: 1, coolDown: 700, coolDownOffset: 0),
                    new TimedTransition(2500, "8")
                        )),
                new Threshold(0.03,
                    new ItemLoot("vortex Key", 1 / 200f),
                    new ItemLoot("Windpiercer", 1 / 700f),
                    new ItemLoot("Dreadcull T9 Upgrade", 1 / 300f),
                    new ItemLoot("Sky Garbs", 1 / 700f),
                    new ItemLoot("Elevation", 1 / 700f)
                )
            )

        .Init("Lych Bloons",
            new State(
              new ScaleHP(1),
                new ConditionEffectRegion(ConditionEffectIndex.Nullified, 3, 4),
                new State("1",
                    new Taunt("pop"),
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new TimedTransition(2000, "4")
                    ),
                new State("4",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new Follow(3, 99, 0, 100000, 100),
                    new Shoot(99, count: 4, shootAngle: 20, projectileIndex: 0, coolDown: 2500, coolDownOffset: 0),
                    new Shoot(99, count: 1, shootAngle: 1, projectileIndex: 1, coolDown: 700, coolDownOffset: 0),
                    new TossObject("DDT", 5, 0, 10000000),
                    new TossObject("DDT", 5, 180, 10000000),
                    new TimedTransition(6000, "4a")
                        ),
                new State("4a",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new EntityNotExistsTransition("DDT", 99, "4b"),
                    new Follow(2, 99, 0, 100000, 100),
                    new Shoot(99, count: 4, shootAngle: 20, projectileIndex: 0, coolDown: 2500, coolDownOffset: 0),
                    new Shoot(99, count: 1, shootAngle: 1, projectileIndex: 1, coolDown: 700, coolDownOffset: 0)
                        ),
                new State("4b",
                    new Taunt("pop"),
                    new Wander(4.5),
                    new Shoot(99, count: 4, shootAngle: 20, projectileIndex: 0, coolDown: 2500, coolDownOffset: 0),
                    new Shoot(99, count: 1, shootAngle: 1, projectileIndex: 1, coolDown: 700, coolDownOffset: 0),
                    new HpLessTransition(.5, "5")
                        ),
                new State("5",
                    new Wander(0.5),
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new TossObject("DDT", 5, 90, 10000000),
                    new TossObject("DDT", 5, 270, 10000000),

                    new Shoot(99, count: 4, shootAngle: 20, projectileIndex: 0, coolDown: 2500, coolDownOffset: 0),
                    new Shoot(99, count: 1, shootAngle: 1, projectileIndex: 1, coolDown: 700, coolDownOffset: 0),
                    new TimedTransition(1500, "6")
                        ),
                new State("6",
                    new Wander(1.5),
                    new Taunt("pop"),
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new TossObject("DDT", 5, 0, 10000000),
                    new TossObject("DDT", 5, 180, 10000000),

                    new Shoot(99, count: 4, shootAngle: 20, projectileIndex: 0, coolDown: 2500, coolDownOffset: 0),
                    new Shoot(99, count: 1, shootAngle: 1, projectileIndex: 1, coolDown: 700, coolDownOffset: 0),
                    new TimedTransition(1500, "7")
                        ),
                new State("7",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new Follow(3, 99, 0, 100000, 100),
                    new EntityNotExistsTransition("DDT", 99, "8"),
                    new Shoot(99, count: 16, shootAngle: 20, projectileIndex: 0, rotateAngle: 15, fixedAngle: 0, coolDown: 5500, coolDownOffset: 0),
                    new Shoot(99, count: 1, shootAngle: 1, projectileIndex: 1, coolDown: 700, coolDownOffset: 0)
                        ),
                new State("8",
                    new Taunt("pop"),
                    new Follow(1.5, 99, 0, 100000, 100),
                    new Shoot(99, count: 4, shootAngle: 20, projectileIndex: 0, coolDown: 2500, coolDownOffset: 0),
                    new Shoot(99, count: 1, shootAngle: 1, projectileIndex: 1, coolDown: 700, coolDownOffset: 0),
                    new TimedTransition(5500, "8a")
                        ),
                new State("8a",
                    new Taunt("pop pop"),
                    new ReproduceChildren(3, 0.5, 3000, "DDT"),
                    new Wander(4.5),
                    new Shoot(99, count: 4, shootAngle: 20, projectileIndex: 0, coolDown: 2500, coolDownOffset: 0),
                    new Shoot(99, count: 1, shootAngle: 1, projectileIndex: 1, coolDown: 700, coolDownOffset: 0),
                    new TimedTransition(6500, "8")
                        )),
                new Threshold(0.03,
                    new ItemLoot("lych Key", 1 / 350f),
                    new ItemLoot("Lychbane", 1 / 350f),
                    new ItemLoot("Dreadcull T9 Upgrade", 1 / 300f),
                    new ItemLoot("Lychplate", 1 / 350f),
                    new ItemLoot("Vision of Lych", 1 / 350f)
                )
            )

        .Init("Bloonarius",
            new State(
             new ScaleHP(1),
                new State("1",
                    new Taunt("pop"),
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new TimedTransition(2000, "4")
                    ),
                new State("4",
                    new MoveTo2(46, 17, speed: 0.5, isMapPosition: true, once: true),
                    new ConditionalEffect(ConditionEffectIndex.Armored),
                    new Shoot(99, count: 4, shootAngle: 20, projectileIndex: 0, coolDown: 2500, coolDownOffset: 0),
                    new Shoot(99, count: 1, shootAngle: 1, projectileIndex: 1, coolDown: 700, coolDownOffset: 0),
                    new TimedTransition(2000, "41")
                        ),
                new State("41",
                    new MoveTo2(46, 17, speed: 0.5, isMapPosition: true, once: true),
                    new TossObject("DDT", 5, 90, 7000),
                    new TossObject("DDT", 5, 270, 7000),
                    new ConditionalEffect(ConditionEffectIndex.Armored),
                    new Shoot(99, count: 4, shootAngle: 20, projectileIndex: 0, coolDown: 2500, coolDownOffset: 0),
                    new Shoot(99, count: 1, shootAngle: 1, projectileIndex: 1, coolDown: 700, coolDownOffset: 0),
                    new TimedTransition(2000, "41"),
                    new EntityExistsTransition("Izual Loot Ctrl", 1, "4a")
                        ),
                new State("4a",
                    new MoveTo2(46, 32, speed: 0.5, isMapPosition: true, once: true),
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new Shoot(99, count: 4, shootAngle: 20, projectileIndex: 0, coolDown: 2500, coolDownOffset: 0),
                    new Shoot(99, count: 1, shootAngle: 1, projectileIndex: 1, coolDown: 700, coolDownOffset: 0),
                    new TimedTransition(1500, "4a1")
                        ),
                new State("4a1",
                    new MoveTo2(46, 32, speed: 0.5, isMapPosition: true, once: true),
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, false, 1000),
                    new Shoot(99, count: 4, shootAngle: 20, projectileIndex: 0, coolDown: 2500, coolDownOffset: 0),
                    new Shoot(99, count: 1, shootAngle: 1, projectileIndex: 1, coolDown: 700, coolDownOffset: 0),
                    new EntityExistsTransition("Izual Loot Ctrl", 1, "4b")
                        ),
                new State("4b",
                    new MoveTo2(18, 32, speed: 0.5, isMapPosition: true, once: true),
                    new ConditionalEffect(ConditionEffectIndex.Armored),
                    new Shoot(99, count: 4, shootAngle: 20, projectileIndex: 0, coolDown: 2500, coolDownOffset: 0),
                    new Shoot(99, count: 1, shootAngle: 1, projectileIndex: 1, coolDown: 700, coolDownOffset: 0),
                    new TimedTransition(1500, "4b1")
                        ),
                new State("4b1",
                    new MoveTo2(18, 32, speed: 0.5, isMapPosition: true, once: true),
                    new TossObject("DDT", 5, 90, 7000),
                    new TossObject("DDT", 5, 270, 7000),
                    new Shoot(99, count: 4, shootAngle: 20, projectileIndex: 0, coolDown: 2500, coolDownOffset: 0),
                    new Shoot(99, count: 1, shootAngle: 1, projectileIndex: 1, coolDown: 700, coolDownOffset: 0),
                    new EntityExistsTransition("Izual Loot Ctrl", 1, "4c")
                        ),
                new State("4c",
                    new MoveTo2(18, 17, speed: 0.5, isMapPosition: true, once: true),
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new Shoot(99, count: 4, shootAngle: 20, projectileIndex: 0, coolDown: 2500, coolDownOffset: 0),
                    new Shoot(99, count: 1, shootAngle: 1, projectileIndex: 1, coolDown: 700, coolDownOffset: 0),
                    new TimedTransition(1500, "4c1")
                        ),
                new State("4c1",
                    new MoveTo2(18, 17, speed: 0.5, isMapPosition: true, once: true),
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, false, 1000),
                    new Shoot(99, count: 4, shootAngle: 20, projectileIndex: 0, coolDown: 2500, coolDownOffset: 0),
                    new Shoot(99, count: 1, shootAngle: 1, projectileIndex: 1, coolDown: 700, coolDownOffset: 0),
                    new EntityExistsTransition("Izual Loot Ctrl", 1, "4")
                        )),
                new Threshold(0.03,
                    new ItemLoot("bloonarius Key", 1 / 200f),
                    new ItemLoot("Druid's Head", 1 / 350f),
                    //new ItemLoot("Overgrowth", 1 / 350f),
                    new ItemLoot("Dreadcull T9 Upgrade", 1 / 300f),
                    new ItemLoot("Regrowth", 1 / 350f)
                )
            )

        .Init("pvzombie Spawner",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new State("1",
                        new Spawn("PVZ ZOMBIE", 1, 1, 1000000),
                        new TimedTransition(1000, "2")
                        ),
                    new State("2",
                        new EntityNotExistsTransition("PVZ ZOMBIE", 999, "3")
                        ),
                    new State("3",
                        new TimedTransition(10000, "1")
                        )
                    )
                )

        .Init("PVZ ZOMBIE",
                new State(

                new StayCloseToSpawn(3, 15),
                new ScaleHP(0.05),
                new State("0",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new TimedTransition(3000, "1")
                    ),
                    new State("1",
                        new Wander(0.4),
                        new Shoot(10, count: 3, shootAngle: 14, projectileIndex: 1, coolDown: 4000),
                        new Shoot(10, count: 7, shootAngle: 16, projectileIndex: 0, coolDown: 4000),
                        new TimedTransition(5250, "2")
                        ),
                     new State("2",
                        new Taunt(1.00, "i need brain"),
                        new Prioritize(
                            new Follow(0.65, 8, 1),
                            new Wander(0.5)
                            ),
                        new ConditionalEffect(ConditionEffectIndex.Armored),
                        new Shoot(10, count: 8, projectileIndex: 0, coolDown: 3000),
                        new Shoot(10, count: 6, projectileIndex: 0, coolDown: 3000, coolDownOffset: 1000),
                        new Shoot(10, count: 4, projectileIndex: 0, coolDown: 3000, coolDownOffset: 1500),
                        new Shoot(10, count: 3, shootAngle: 6, projectileIndex: 1, coolDown: 2000)
                        ))
                    ,
                new Threshold(0.025,
                    new ItemLoot("raindow of Maxy", 1 / 25f),
                    new ItemLoot("zombino hand", 1 / 50f),
                    new ItemLoot("traffic coner", 1 / 150f),
                    new ItemLoot("PVZ ZOMBIE Key", 1 / 15f),

                    new ItemLoot("Dreadcull T2 Upgrade", 1 / 300f),
                    new ItemLoot("zombie drip", 1 / 50f)
                )
            )

        .Init("Solar Flare SEX",
                new State(
                    new ScaleHP(1),
                new State("0",
                    new ScaleDEF(0.2),
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new SetAltTexture(0),
                    new PlayerWithinTransition(2, "1"),
                    new TimedTransition(5000, "2")
                    ),
                    new State("1",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new ConditionEffectRegion(ConditionEffectIndex.Confused, 3, 4),
                        new SetAltTexture(1),
                        new Shoot(25, 36, 10, 1, rotateAngle: 4, coolDown: 250),
                        new NoPlayerWithinTransition(3, "2")
                        ),
                    new State("2",
                        new ConditionalEffect(ConditionEffectIndex.Armored),
                    new SetAltTexture(0),
                    new InvisiToss("PVZ Sunflower", 9, 0, 90000001, coolDownOffset: 0),
                    new InvisiToss("PVZ Sunflower", 9, 90, 90000001, coolDownOffset: 0),
                    new InvisiToss("PVZ Sunflower", 9, 180, 90000001, coolDownOffset: 0),
                    new InvisiToss("PVZ Sunflower", 9, 270, 90000001, coolDownOffset: 0),
                    new Shoot(25, 4, 90, 1, rotateAngle: 4, coolDown: 250),
                    new HpLessTransition(0.8, "31")
                    ),
                    new State("31",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable, false, 4000),
                        new Order(100, "PVZ Sunflower", "3"),
                        new TimedRandomTransition(1000, false, "3", "3a", "3b", "3c")
                        ),
                    new State("3",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable, false, 2000),
                        new Follow(0.6, 100, 1),
                        new SetAltTexture(0),
                        new Shoot(25, 30, 9, 0, rotateAngle: 160, coolDown: 700),
                        new TimedRandomTransition(4000, false, "3a", "3b", "3c")
                        ),
                    new State("3a",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable, false, 2000),
                        new Wander(0.8),
                        new SetAltTexture(0),
                        new Shoot(25, 12, 30, 0, rotateAngle: 4, coolDown: 750),
                        new Shoot(25, 12, 3, 0, coolDown: 3000),
                        new TimedRandomTransition(4000, false, "3", "3b", "3c")
                        ),
                    new State("3b",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable, false, 2000),
                        new SetAltTexture(1),
                        new Shoot(25, 36, 10, 1, rotateAngle: 15, coolDown: 500),
                        new TimedRandomTransition(1000, false, "3a", "3", "3c")
                        ),
                    new State("3c",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable, false, 2000),
                        new Wander(0.8),
                        new SetAltTexture(0),
                        new Shoot(25, 36, 10, 0, coolDown: 1500),
                        new Shoot(25, 6, 40, 1, rotateAngle: 15, coolDown: 1350),
                        new TimedRandomTransition(4000, false, "3a", "3b", "3")
                        )
                     ),
                new Threshold(0.03,
                    new ItemLoot("the Jar", 1 / 1500f),
                    new ItemLoot("vitamin D", 1 / 1200f),
                    new ItemLoot("sunflower GOGGLES", 1 / 1000f),
                    new ItemLoot("Dreadcull T9 Upgrade", 1 / 300f),
                    new ItemLoot("sunflower DRIP", 1 / 1000f),
                    new ItemLoot("drip Skirt epic", 1 / 1000f),
                    new ItemLoot("COB CANNON", 1 / 300f),
                    new ItemLoot("carront MAGIC", 1 / 300f),
                    new ItemLoot("leaf balde pokmeon", 1 / 1000f),

                    new ItemLoot("Kernel of the Gods", 1 / 2000f),
                    new ItemLoot("the NUT", 1 / 550f)
                )
            )

        .Init("PVZ Sunflower",
            new State(

                new ScaleHP(1),
                new State("1",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new Shoot(10, count: 4, shootAngle: 90, projectileIndex: 0, fixedAngle: 0, coolDown: 1500, coolDownOffset: 0),
                    new Shoot(10, count: 4, shootAngle: 90, projectileIndex: 0, fixedAngle: 45, coolDown: 1700, coolDownOffset: 0),
                    new EntityNotExistsTransition("Solar Flare SEX", 999, "2")
                    ),
                new State("2",
                    new Suicide()
                    ),
                new State("3",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new Shoot(10, count: 4, shootAngle: 90, projectileIndex: 1, fixedAngle: 0, coolDown: 300, coolDownOffset: 0),
                    new EntityNotExistsTransition("Solar Flare SEX", 999, "2")
                    )
                )
            )

        .Init("the Travster",
          new State(
              new ScaleHP(0.5),
              new State("Idle",
                  new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                  new PlayerWithinTransition(5, "1")
                  ),
              new State("1",
                  new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                  new Taunt("astroworld was not mid"),
                  new TimedTransition(4000, "2")
                  ),
              new State("2",
                  new Follow(speed: 2.5, range: 10),
                  new Shoot(50, count: 8, shootAngle: 45, projectileIndex: 0, predictive: 0.2, coolDown: 400),
                  new HpLessTransition(0.2, "YoUrEtOoSlOw")
                  ),
              new State("YoUrEtOoSlOw",
                  new Taunt("i hate rs gold man"),
                  new StayBack(speed: 3, distance: 30),
                  new TimedTransition(8000, "rest")
                  ),
              new State("rest",
                  new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                  new Shoot(50, count: 8, shootAngle: 45, projectileIndex: 0, predictive: 0.2, coolDown: 400),
                  new TimedTransition(5000, "YoUrEtOoSlOw")
                  )
              ),
                new MostDamagers(3,
                    LootTemplates.Sor2Perc()
                    ),
                new MostDamagers(5,
                    LootTemplates.StatPots()
                    ),
              new Threshold(0.06,
                  new ItemLoot("Sacred Essence", 0.0005),
                  new ItemLoot("Legendary Essence", 0.0005),
                  new ItemLoot("Greater Potion of Life", 0.5),
                  new ItemLoot("Greater Potion of Mana", 0.5),
                  new ItemLoot("Greater Potion of Vitality", 1),
                  new ItemLoot("Greater Potion of Defense", 1),
                  new ItemLoot("Sanic Helm", 0.002)
              )
            )

                .Init("Evil Orange Spawner",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new State("1",
                        new Spawn("Nightmare Orange", 1, 1, 1000000),
                        new TimedTransition(1000, "2")
                        ),
                    new State("2",
                        new EntitiesNotExistsTransition(999, "3", "Nightmare Orange")
                        ),
                    new State("3",
                        new TimedTransition(8000, "1")
                        )
                    ))

        .Init("Nightmare Orange",
                    new State(

                        new StayCloseToSpawn(5, 6),
                        new ScaleHP(0.15),
                        new State("1",
                            new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                            new TimedTransition(1000, "2")
                            ),
                        new State("2",
                            new RemoveConditionalEffect(ConditionEffectIndex.Invincible),
                            new Wander(0.5),
                            new Shoot(99, 10, 36, 0, coolDown: 2000),
                            new Shoot(99, 6, 16, 1, coolDown: 1400),
                            new HpLessTransition(0.75, "3")
                            ),
                        new State("3",
                            new ConditionalEffect(ConditionEffectIndex.Armored, true),
                            new Wander(1),
                            new Shoot(99, 10, 36, 0, coolDown: 1400),
                            new HpLessTransition(0.35, "4")
                            ),
                        new State("4",
                            new Wander(2),
                            new Shoot(99, 36, 10, 3, coolDown: 1200),
                            new Shoot(99, 10, 36, 1, coolDown: 400),
                            new TimedTransition(6000, "4a")
                            ),
                        new State("4a",
                            new ConditionalEffect(ConditionEffectIndex.Armored, true),
                            new Wander(1),
                            new Shoot(99, 10, 36, 2, coolDown: 1500),
                            new TimedTransition(10500, "4")
                            )
                        ),
                new Threshold(0.0001,
                    //new ItemLoot("nightmare freddy testicle", 1 / 250f),
                    new ItemLoot("Dreadcull T7 Upgrade", 1 / 300f),
                    new ItemLoot("staff Orange Nightmare", 1 / 75f),
                    new ItemLoot("summer realms Katana but a sword", 1 / 150f),
                    new ItemLoot("summer realms Katana but a katana", 1 / 150f),
                    new ItemLoot("Nightmare Kitchen Key", 1 / 15f),
                    //new ItemLoot("vacuum Orange", 1 / 150f),
                    new ItemLoot("evil Helmets Tangerine", 1 / 150f)
                    )
                )

        .Init("PVZ Peashooter",
                new State(
                    new ScaleDEF(0.2),
                    new StayCloseToSpawn(4, 12),
                    new ScaleHP(0.01, true),
                    new State("1",
                        new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                        new PlayerWithinTransition(15, "1a")

                        ),
                    new State("1a",
                        new Wander(0.5),
                        new Shoot(99, 3, 9, 0, coolDown: 1400),
                        new SpiralShoot(-3, 360, 10, 36, 1, 180, coolDown: 1600),
                        new TimedTransition(2000, "2")
                        ),
                    new State("2",
                        new RemoveConditionalEffect(ConditionEffectIndex.Invincible),
                        new Wander(1),
                        new Shoot(99, 4, 8, 0, coolDown: 600),
                        new SpiralShoot(3, 360, 9, 20, 1, 0, coolDown: 1400),
                        new TimedTransition(2000, "3")
                        ),
                    new State("3",
                        new Wander(0.5),
                        new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                        new SpiralShoot(3, 360, 9, 20, 1, 180, coolDown: 1600),
                        new Shoot(99, 4, 8, 0, coolDown: 1200),
                        new TimedTransition(2000, "4")
                        ),
                    new State("4",
                        new Wander(1),
                        new RemoveConditionalEffect(ConditionEffectIndex.Invincible),
                        new SpiralShoot(-3, 360, 9, 20, 1, 180, coolDown: 1400),
                        new Shoot(99, 2, 3, 1, coolDown: 1200),
                        new Shoot(99, 2, 6, 1, coolDown: 1400),
                        new TimedTransition(2000, "1a")
                        )
                    ),
            new Threshold(1,
                new ItemLoot("pea hood", 1 / 1000f),
                new ItemLoot("vitamin COCK", 1 / 1000f),
                new ItemLoot("rose ringAR", 1 / 500f),
                new ItemLoot("forbidden Garment", 1 / 500f),
                new ItemLoot("green Buiver", 1 / 1000f),
                new ItemLoot("PEA V Z Key", 1 / 15f),
                new ItemLoot("ASGARD Token", 1 / 175f),
                new ItemLoot("FAVELA Token", 1 / 250f),
                new ItemLoot("Deep Sea Token", 1 / 300f),
                new ItemLoot("A Fallen Light (Token)", 1 / 500f),
                //   new ItemLoot("Wither Shield", 1 / 40000f),
                new ItemLoot("PVZ PLANT Key", 1 / 125f)
                    )
                )

        #region cancer spawner spam good lord
        .Init("STARFORCE Spawner REAL",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                new State("0",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new TimedRandomTransition(1000, false, "1a", "1b", "1c", "1d", "1e", "1f", "1g", "1h")
                    ),
                    new State("1a",
                        new Spawn("ARCANUICA image slopy", 1, 1, 1000000),
                        new TimedTransition(1000, "2a")
                        ),
                    new State("2a",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("ARCANUICA image slopy", 999, "3")
                        ),
                    new State("1b",
                        new Spawn("EL BANDITONIO rodriguez", 1, 1, 1000000),
                        new TimedTransition(1000, "2b")
                        ),
                    new State("2b",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("EL BANDITONIO rodriguez", 999, "3")
                        ),
                    new State("1c",
                        new Spawn("Zucky Zucc", 1, 1, 1000000),
                        new TimedTransition(1000, "2c")
                        ),
                    new State("2c",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("Zucky Zucc", 999, "3")
                        ),
                    new State("1d",
                        new Spawn("gemgemgem gem gem", 1, 1, 1000000),
                        new TimedTransition(1000, "2d")
                        ),
                    new State("2d",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("gemgemgem gem gem", 999, "3")
                        ),
                    new State("1e",
                        new Spawn("BIG MAN ORTAR", 1, 1, 1000000),
                        new TimedTransition(1000, "2e")
                        ),
                    new State("2e",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("BIG MAN ORTAR", 999, "3")
                        ),
                    new State("1f",
                        new Spawn("BIG MAN ORTAR", 1, 1, 1000000),
                        new TimedTransition(1000, "2f")
                        ),
                    new State("2f",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("BIG MAN ORTAR", 999, "3")
                        ),
                    new State("1g",
                        new Spawn("rock man i forgot name", 1, 1, 1000000),
                        new TimedTransition(1000, "2g")
                        ),
                    new State("2g",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("rock man i forgot name", 999, "3")
                        ),
                    new State("1h",
                        new Spawn("crymbal", 1, 1, 1000000),
                        new TimedTransition(1000, "2h")
                        ),
                    new State("2h",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("crymbal", 999, "3")
                        ),
                    new State("3",
                        new Wander(10f),
                        new StayCloseToSpawn(5f, 10),
                        new TimedTransition(1000, "0")
                    )
                    )

                )
            .Init("STARFORCE Spawner",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                new State("0",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new TimedRandomTransition(1000, false, "1a", "1b", "1c", "1d", "1e", "1f", "1g", "1h", "1i", "1l", "1k", "1l", "1m", "1n")
                    ),
                    new State("1a",
                        new Spawn("ARCANUICA image slopy", 1, 1, 1000000),
                        new TimedTransition(1000, "2a")
                        ),
                    new State("2a",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("ARCANUICA image slopy", 999, "3")
                        ),
                    new State("1b",
                        new Spawn("EL BANDITONIO rodriguez", 1, 1, 1000000),
                        new TimedTransition(1000, "2b")
                        ),
                    new State("2b",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("EL BANDITONIO rodriguez", 999, "3")
                        ),
                    new State("1c",
                        new Spawn("Zucky Zucc", 1, 1, 1000000),
                        new TimedTransition(1000, "2c")
                        ),
                    new State("2c",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("Zucky Zucc", 999, "3")
                        ),
                    new State("1d",
                        new Spawn("gemgemgem gem gem", 1, 1, 1000000),
                        new TimedTransition(1000, "2d")
                        ),
                    new State("2d",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("gemgemgem gem gem", 999, "3")
                        ),
                    new State("1e",
                        new Spawn("BIG MAN ORTAR", 1, 1, 1000000),
                        new TimedTransition(1000, "2e")
                        ),
                    new State("2e",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("BIG MAN ORTAR", 999, "3")
                        ),
                    new State("1f",
                        new Spawn("BIG MAN ORTAR", 1, 1, 1000000),
                        new TimedTransition(1000, "2f")
                        ),
                    new State("2f",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("BIG MAN ORTAR", 999, "3")
                        ),
                    new State("1g",
                        new Spawn("rock man i forgot name", 1, 1, 1000000),
                        new TimedTransition(1000, "2g")
                        ),
                    new State("2g",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("rock man i forgot name", 999, "3")
                        ),
                    new State("1h",
                        new Spawn("crymbal", 1, 1, 1000000),
                        new TimedTransition(1000, "2h")
                        ),
                    new State("2h",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("crymbal", 999, "3")
                        ),
                    new State("1i",
                        new Spawn("PVZ Peashooter", 1, 1, 1000000),
                        new TimedTransition(1000, "2i")
                        ),
                    new State("2i",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("PVZ Peashooter", 999, "3")
                        ),

                    new State("1l",
                        new Spawn("MOAB", 1, 1, 1000000),
                        new TimedTransition(1000, "2l")
                        ),
                    new State("2l",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("MOAB", 999, "3")
                        ),
                    new State("1k",
                        new Spawn("DDT", 1, 1, 1000000),
                        new TimedTransition(1000, "2k")
                        ),
                    new State("2k",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("DDT", 999, "3")
                        ),
                    new State("1l",
                        new Spawn("battleDog", 1, 1, 1000000),
                        new TimedTransition(1000, "2l")
                    ),
                    new State("2l",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("battleDog", 999, "3")
                    ),
                    new State("1m",
                        new Spawn("The Cat", 1, 1, 1000000),
                        new TimedTransition(1000, "2m")
                    ),
                    new State("2m",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("The Cat", 999, "3")
                    ),
                    new State("1n",
                        new Spawn("sunfish jones", 1, 1, 1000000),
                        new TimedTransition(1000, "2n")
                    ),
                    new State("2n",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("sunfish jones", 999, "3")
                    ),
                    new State("3",
                        new Wander(10f),
                        new StayCloseToSpawn(5f, 10),
                        new TimedTransition(1000, "0")
                    )
                    )
                )
        .Init("STARFORCE Spawner2",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new State("0",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new TimedTransition(1000, "1b")
                    ),

                    new State("1b",
                        new Spawn("PVZ Peashooter", 1, 1, 1000000),
                        new TimedTransition(1000, "2b")
                        ),
                    new State("2b",
                        new EntityNotExistsTransition("PVZ Peashooter", 999, "3")
                        ),
                    new State("3",
                        new TimedTransition(8000, "0")
                        )
                    )
                )
        .Init("STARFORCE Spawner3",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                new State("0",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new TimedRandomTransition(1000, false, "1a", "1b", "1c", "1d", "1e", "1f", "1g", "1h", "1i", "1l", "1j", "1k", "1m")
                    ),
                    new State("1a",
                        new Spawn("DEEZ NUTS GUY", 1, 1, 1000000),
                        new TimedTransition(1000, "2a")
                        ),
                    new State("2a",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("DEEZ NUTS GUY", 999, "3")
                        ),
                    new State("1b",
                        new Spawn("Banan PHON", 1, 1, 1000000),
                        new TimedTransition(1000, "2b")
                        ),
                    new State("2b",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("Banan PHON", 999, "3")
                        ),
                    new State("1c",
                        new Spawn("chinese egghead", 1, 1, 1000000),
                        new TimedTransition(1000, "2c")
                        ),
                    new State("2c",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("chinese egghead", 999, "3")
                        ),
                    new State("1d",
                        new Spawn("talking toilet", 1, 1, 1000000),
                        new TimedTransition(1000, "2d")
                        ),
                    new State("2d",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("talking toilet", 999, "3")
                        ),
                    new State("1e",
                        new Spawn("godes", 1, 1, 1000000),
                        new TimedTransition(1000, "2e")
                        ),
                    new State("2e",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("godes", 999, "3")
                        ),
                    new State("1f",
                        new Spawn("SANS", 1, 1, 1000000),
                        new TimedTransition(1000, "2f")
                        ),
                    new State("2f",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("SANS", 999, "3")
                        ),
                    new State("1g",
                        new Spawn("Annoying Orange", 1, 1, 1000000),
                        new TimedTransition(1000, "2g")
                        ),
                    new State("2g",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("Annoying Orange", 999, "3")
                        ),
                    new State("1h",
                        new Spawn("Pear", 1, 1, 1000000),
                        new TimedTransition(1000, "2h")
                        ),
                    new State("2h",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("Pear", 999, "3")
                        ),
                    new State("1i",
                        new Spawn("Apple", 1, 1, 1000000),
                        new TimedTransition(1000, "2i")
                        ),
                    new State("2i",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("Apple", 999, "3")
                        ),
                    new State("1j",
                        new Spawn("Pickle Rick", 1, 1, 1000000),
                        new TimedTransition(1000, "2j")
                        ),
                    new State("2j",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("Pickle Rick", 999, "3")
                        ),
                    new State("1k",
                        new Spawn("Marshmallow", 1, 1, 1000000),
                        new TimedTransition(1000, "2k")
                        ),
                    new State("2k",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("Marshmallow", 999, "3")
                        ),
                    new State("1l",
                        new Spawn("PVZ ZOMBIE", 1, 1, 1000000),
                        new TimedTransition(1000, "2l")
                        ),
                    new State("2l",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("PVZ ZOMBIE", 999, "3")
                        ),
                    new State("1m",
                        new Spawn("golden freddy", 1, 1, 1000000),
                        new TimedTransition(1000, "2m")
                        ),
                    new State("2m",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("golden freddy", 999, "3")
                        ),
                    new State("3",
                        new Wander(12f),
                        new StayCloseToSpawn(5f, 10),
                        new TimedTransition(1000, "0")
                        )
                    )
                )

            .Init("STARFORCE Spawner4",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                new State("0",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new TimedRandomTransition(1000, false, "1b", "1c", "1d", "1e", "1f", "1g", "1h", "1i", "1j", "1k", "1l")
                    ),
                    new State("1b",
                        new Spawn("baldi", 1, 1, 1000000),
                        new TimedTransition(1000, "2b")
                        ),
                    new State("2b",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("baldi", 999, "3")
                        ),
                    new State("1c",
                        new Spawn("Herobrine", 1, 1, 1000000),
                        new TimedTransition(1000, "2c")
                        ),
                    new State("2c",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("Herobrine", 999, "3")
                        ),
                    new State("1d",
                        new Spawn("Billy Herrington", 1, 1, 1000000),
                        new TimedTransition(1000, "2d")
                        ),
                    new State("2d",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("Billy Herrington", 999, "3")
                        ),
                    new State("1e",
                        new Spawn("soulja boi", 1, 1, 1000000),
                        new TimedTransition(1000, "2e")
                        ),
                    new State("2e",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("soulja boi", 999, "3")
                        ),
                    new State("1f",
                        new Spawn("shoop da woop", 1, 1, 1000000),
                        new TimedTransition(1000, "2f")
                        ),
                    new State("2f",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("shoop da woop", 999, "3")
                        ),
                    new State("1g",
                        new Spawn("dancing triangle", 1, 1, 1000000),
                        new TimedTransition(1000, "2g")
                        ),
                    new State("2g",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("dancing triangle", 999, "3")
                        ),
                    new State("1h",
                        new Spawn("finnish warrior", 1, 1, 1000000),
                        new TimedTransition(1000, "2h")
                        ),
                    new State("2h",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("finnish warrior", 999, "3")
                        ),
                    new State("1i",
                        new Spawn("sun baby", 1, 1, 1000000),
                        new TimedTransition(1000, "2i")
                        ),
                    new State("2i",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("sun baby", 999, "3")
                        ),
                    new State("1j",
                        new Spawn("octahedron LEGS", 1, 1, 1000000),
                        new TimedTransition(1000, "2j")
                        ),
                    new State("2j",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("octahedron LEGS", 999, "3")
                        ),
                    new State("1k",
                        new Spawn("puggsy", 1, 1, 1000000),
                        new TimedTransition(1000, "2k")
                        ),
                    new State("2k",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("puggsy", 999, "3")
                        ),
                    new State("1l",
                        new Spawn("hatsune mike", 1, 1, 1000000),
                        new TimedTransition(1000, "2l")
                    ),
                    new State("2l",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("hatsune mike", 999, "3")
                    ),
                    new State("3",
                        new Wander(12f),
                        new StayCloseToSpawn(5f, 10),
                        new TimedTransition(3000, "0")
                        )
                    )
                )//hatsune mike

            .Init("STARFORCE Spawner5",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                new State("0",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new TimedRandomTransition(1000, false, "1a", "1b", "1c", "1d", "1e", "1f", "1g")
                    ),
                    new State("1a",
                        new Spawn("Mindy", 1, 1, 1000000),
                        new TimedTransition(1000, "2a")
                        ),
                    new State("2a",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("Mindy", 999, "3")
                        ),
                    new State("1b",
                        new Spawn("Vera", 1, 1, 1000000),
                        new TimedTransition(1000, "2b")
                        ),
                    new State("2b",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("Vera", 999, "3")
                        ),
                    new State("1c",
                        new Spawn("Nadine", 1, 1, 1000000),
                        new TimedTransition(1000, "2c")
                        ),
                    new State("2c",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("Nadine", 999, "3")
                        ),
                    new State("1d",
                        new Spawn("Interactive Buddy", 1, 1, 1000000),
                        new TimedTransition(1000, "2d")
                        ),
                    new State("2d",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("Interactive Buddy", 999, "3")
                        ),
                    new State("1e",
                        new Spawn("Tong Shau Ping", 1, 1, 1000000),
                        new TimedTransition(1000, "2e")
                        ),
                    new State("2e",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("Tong Shau Ping", 999, "3")
                        ),
                    new State("1f",
                        new Spawn("Nightmare Orange", 1, 1, 1000000),
                        new TimedTransition(1000, "2f")
                        ),
                    new State("2f",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("Nightmare Orange", 999, "3")
                        ),
                    new State("1g",
                        new Spawn("GUMBALL", 1, 1, 1000000),
                        new TimedTransition(1000, "2g")
                        ),
                    new State("2g",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("GUMBALL", 999, "3")
                        ),
                    new State("1h",
                        new Spawn("cakecore", 1, 1, 1000000),
                        new TimedTransition(1000, "2h")
                    ),
                    new State("2h",
                        new EntityNotExistsTransition("cakecore", 999, "3")
                    ),
                    new State("1i",
                        new Spawn("prism of chaos", 1, 1, 1000000),
                        new TimedTransition(1000, "2i")
                    ),
                    new State("2i",
                        new EntityNotExistsTransition("prism of chaos", 999, "3")
                    ),
                    new State("1j",
                        new Spawn("a flare v2", 1, 1, 1000000),
                        new TimedTransition(1000, "2j")
                    ),
                    new State("2j",
                        new EntityNotExistsTransition("a flare v2", 999, "3")
                    ),
                    new State("3",
                        new Wander(10f),
                        new StayCloseToSpawn(5f, 10),
                        new TimedTransition(1000, "0")
                        )
                    )
                )

            .Init("STARFORCE Spawner6",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                new State("0",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new TimedRandomTransition(1000, false, "1a", "1b", "1c", "1d", "1e", "1f", "1g")
                    ),
                    new State("1a",
                        new Spawn("Mindy", 1, 1, 1000000),
                        new TimedTransition(1000, "2a")
                        ),
                    new State("2a",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("Mindy", 999, "3")
                        ),
                    new State("1b",
                        new Spawn("Vera", 1, 1, 1000000),
                        new TimedTransition(1000, "2b")
                        ),
                    new State("2b",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("Vera", 999, "3")
                        ),
                    new State("1c",
                        new Spawn("Nadine", 1, 1, 1000000),
                        new TimedTransition(1000, "2c")
                        ),
                    new State("2c",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("Nadine", 999, "3")
                        ),
                    new State("1d",
                        new Spawn("Interactive Buddy", 1, 1, 1000000),
                        new TimedTransition(1000, "2d")
                        ),
                    new State("2d",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("Interactive Buddy", 999, "3")
                        ),
                    new State("1e",
                        new Spawn("Tong Shau Ping", 1, 1, 1000000),
                        new TimedTransition(1000, "2e")
                        ),
                    new State("2e",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("Tong Shau Ping", 999, "3")
                        ),
                    new State("1f",
                        new Spawn("Nightmare Orange", 1, 1, 1000000),
                        new TimedTransition(1000, "2f")
                        ),
                    new State("2f",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("Nightmare Orange", 999, "3")
                        ),
                    new State("1g",
                        new Spawn("GUMBALL", 1, 1, 1000000),
                        new TimedTransition(1000, "2g")
                        ),
                    new State("2g",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("GUMBALL", 999, "3")
                        ),
                    new State("3",
                        new Wander(10f),
                        new StayCloseToSpawn(5f, 10),
                        new TimedTransition(1000, "0")
                        )
                    )
                )

            .Init("STARFORCE Spawner7",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new State("0",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new TimedTransition(1000, "1b")
                    ),

                    new State("1b",
                        new Spawn("crymbal", 1, 1, 1000000),
                        new TimedTransition(1000, "2b")
                        ),
                    new State("2b",
                        new EntityNotExistsTransition("crymbal", 999, "3")
                        ),
                    new State("3",
                        new TimedTransition(15000, "0")
                        )
                    )
                )
        #endregion cancer spawner spam good lord

        .Init("Mergański, the polak titan",
            new State(
                new ScaleHP(2),
                new ScaleDEF(0.5),
                new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new State("wait",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new PlayerWithinTransition(12, "talk")
                    ),
                    new State("talk",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Taunt("hey guys yetimi did this sprite isnt that cool"),
                        new TimedTransition(4000, "talk2")
                    ),
                     new State("talk2",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Taunt("polak WARCRIME sebafra FEMBOY"),
                        new TimedTransition(4000, "talk3")
                    ),
                    new State("talk3",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Taunt("fight time i will stack shot and kill you"),
                        new TimedTransition(4000, "ohlord")
                        ),
                    new State("ohlord",
                        new Flash(0x000FF, 1, 2),
                        new OrderOnce(99, "FALLEN DOORCLOSE2", "1"),
                        new TimedTransition(1000, "activate")
                        ),
                new State("activate",
                    new Follow(0.2, range: 1),
                    new Shoot(118, coolDown: 600),
                    new Shoot(118, count: 4, shootAngle: 90, rotateAngle: 15, fixedAngle: 0, coolDown: 600),
                    new Shoot(118, 2, shootAngle: 80, coolDown: 600),
                    new Shoot(118, count: 4, shootAngle: 90, rotateAngle: 15, fixedAngle: 0, coolDown: 500),
                    new TossObject("Tod Lava", 3, 35, coolDown: 12000),
                    new TossObject("Tod Lava", 3, 185, coolDown: 12000),
                    new TossObject("Tod Lava", 3, 245, coolDown: 12000),
                    new HpLessTransition(0.7, "kill")
                    ),
                new State("kill",
                    new Taunt("kill"),
                    new Follow(0.2, range: 1),
                    new Charge(2, 99, coolDown: 3000),
                    new Shoot(118, coolDown: 600),
                    new Shoot(118, coolDown: 450),
                    new Shoot(118, count: 4, shootAngle: 90, rotateAngle: 15, fixedAngle: 0, coolDown: 500),
                    new Shoot(118, coolDown: 800),
                    new Shoot(118, coolDown: 550),
                    new Shoot(118, count: 4, shootAngle: 90, rotateAngle: 15, fixedAngle: 0, coolDown: 400),
                    new HpLessTransition(0.4, "kill1.5")
                        ),
                new State("kill1.5",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new ReturnToSpawn(2),
                    new Taunt("hey why hurt lol thats not nice"),
                    new TimedTransition(2000, "kill2")
                        ),
                new State("kill2",
                    new Wander(0.5),
                    new Follow(0.8, 30, 0),
                    new Shoot(118, projectileIndex: 1, count: 4, shootAngle: 90, rotateAngle: 15, fixedAngle: 0, coolDown: 300),
                    new Shoot(118, count: 4, shootAngle: 90, rotateAngle: 15, fixedAngle: 0, coolDown: 500),
                    new Shoot(118, coolDown: 600),
                    new Shoot(118, coolDown: 450),
                    new Shoot(118, coolDown: 800),
                    new Shoot(118, projectileIndex: 1, count: 4, shootAngle: 90, rotateAngle: 15, fixedAngle: 0, coolDown: 400),
                    new HpLessTransition(0.1, "dead")
                        ),
                new State("dead",
                        new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                        new Taunt("i hope you get nothing"),
                        new ReturnToSpawn(2),
                        new Flash(0xFF000, 0.5, 3),
                        new TimedTransition(1500, "dead1")
                        ),
                    new State("dead1",
                        new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                        new Shoot(99, 10, 36, 0, coolDown: 15000),
                        new TimedTransition(100, "dead2")
                        ),
                    new State("dead2",
                        new Suicide()
                        )
                   ),
                new Threshold(0.0001,
                    //new ItemLoot("SPECIAL CRATE!", 1 / 150f),
                    new ItemLoot("toniard of fatly retribution", 1 / 1200f),
                    new ItemLoot("Potion of Speed", 1 / 1f),
                    new ItemLoot("on some real xxgga shit :broken_heart:", 1 / 1200f),
                    new ItemLoot("dingleberry elastic camel copter orbe hides", 1 / 1200f),
                    new ItemLoot("cream filled cookie", 1 / 400f),
                    new ItemLoot("legendary POLAK scepter", 1 / 600f),
                    new ItemLoot("Dreadcull T9 Upgrade", 1 / 4000f)
                    )
                )

        .Init("Aldraginowski, polak Zola",
            new State(
               new State("Start3",
                        new Prioritize(
                             new StayCloseToSpawn(0.5, 3),
                             new Wander(0.15)
                             ),
                        new ConditionalEffect(ConditionEffectIndex.Armored),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable, false, 200),
                        new Shoot(8, count: 1, projectileIndex: 0, fixedAngle: 45, coolDown: 1, coolDownOffset: 2000),
                        new Shoot(8, count: 1, projectileIndex: 0, fixedAngle: 135, coolDown: 1, coolDownOffset: 2000),
                        new Shoot(8, count: 1, projectileIndex: 0, fixedAngle: 225, coolDown: 1, coolDownOffset: 2000),
                        new Shoot(8, count: 1, projectileIndex: 0, fixedAngle: 315, coolDown: 1, coolDownOffset: 2000),
                        new Shoot(8, count: 1, projectileIndex: 0, coolDown: new Cooldown(200, 1000)),
                        new Shoot(8, count: 5, projectileIndex: 0, predictive: 0.5, coolDown: new Cooldown(2000, 2000)),
                        new TimedTransition(600, "Start4")
                        ),
                 new State("Start4",
                        new Prioritize(
                             new StayCloseToSpawn(0.5, 3),
                             new Wander(0.15)
                             ),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable, false, 200),
                        new Shoot(8, count: 1, projectileIndex: 0, fixedAngle: 45, coolDown: 1, coolDownOffset: 2000),
                        new Shoot(8, count: 1, projectileIndex: 0, fixedAngle: 135, coolDown: 1, coolDownOffset: 2000),
                        new Shoot(8, count: 1, projectileIndex: 0, fixedAngle: 225, coolDown: 1, coolDownOffset: 2000),
                        new Shoot(8, count: 1, projectileIndex: 0, fixedAngle: 315, coolDown: 1, coolDownOffset: 2000),
                        new Shoot(8, count: 1, projectileIndex: 0, coolDown: new Cooldown(200, 1000)),
                        new Shoot(8, count: 5, projectileIndex: 0, predictive: 0.5, coolDown: new Cooldown(2000, 2000)),
                        new TimedTransition(600, "Start3")
                        ),
                 new State("kill",
                        new Orbit(.5, 4, target: "FEMBOY (polish) ARCANUO", radiusVariance: 0),
                        new Shoot(8, count: 1, projectileIndex: 0, fixedAngle: 45, coolDown: 1, coolDownOffset: 2000),
                        new Shoot(8, count: 1, projectileIndex: 0, fixedAngle: 135, coolDown: 1, coolDownOffset: 2000),
                        new Shoot(8, count: 1, projectileIndex: 0, fixedAngle: 225, coolDown: 1, coolDownOffset: 2000),
                        new Shoot(8, count: 1, projectileIndex: 0, fixedAngle: 315, coolDown: 1, coolDownOffset: 2000),
                        new Shoot(8, count: 1, projectileIndex: 0, coolDown: new Cooldown(200, 1000)),
                        new Shoot(8, count: 5, projectileIndex: 0, predictive: 0.5, coolDown: new Cooldown(2000, 2000))
                        )
                ),
                new Threshold(0.0001,
                    //new ItemLoot("SPECIAL CRATE!", 1 / 150f),
                    new ItemLoot("I GOT AR LANCE I GOT AR LANCE!!! YESSS", 1 / 3200f),
                    new ItemLoot("Dreadcull T9 Upgrade", 1 / 4000f)
                    )
            )
        .Init("Drannolszkiewicz, The Eternal Gaybeater",
            new State(
                new State("0",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new SetAltTexture(0),
                    new PlayerWithinTransition(2, "2")
                    ),
                    new State("2",

                      new Taunt("fuck you LOL free loot"),
                      new Wander(0.5),
                        new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                        new SpiralShoot(3, 360, 9, 20, 1, 180, coolDown: 1600),
                        new Shoot(99, 4, 8, 0, coolDown: 1200),

                      new HpLessTransition(0.1, "dead1")
                        ),
                    new State("dead1",
                        new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                        new Shoot(99, 10, 36, 0, coolDown: 15000),
                        new TimedTransition(500, "dead2")
                        ),
                    new State("kill",
                        new Orbit(.5, 4, target: "FEMBOY (polish) ARCANUO", radiusVariance: 0),
                        new Shoot(99, 10, 36, 0, coolDown: 1000),
                        new HpLessTransition(0.1, "dead1")
                        ),
                    new State("dead2",
                        new Suicide()
                        )


                ),
                new Threshold(0.0001,
                    //new ItemLoot("SPECIAL CRATE!", 1 / 150f),
                    new ItemLoot("vorve LEG AR", 1 / 3200f),
                    new ItemLoot("Dreadcull T9 Upgrade", 1 / 4000f)
                    )


            )

        .Init("FEMBOY (polish) ARCANUO",
            new State(
                new State("0",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new PlayerWithinTransition(2, "2")
                    ),

                    new State("2",
                    new ScaleHP(1.5),
                    new ScaleDEF(0.5),
                    new Flash(0x000FF, 1, 5),
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new Taunt("arcanuo kill"),
                    new TimedTransition(5000, "3")
                    ),
                    new State("3",
                      new OrderOnce(99, "FALLEN DOORCLOSE1", "1"),
                      new Taunt("arcanuo kill"),
                      new Wander(0.5),
                      new Charge(2, 20, 3000),
                      new Shoot(99, 6, 60, 1, 15, coolDown: 3000),
                      new Shoot(99, 6, 60, 0, 45, 15, coolDown: 1500),
                      new Shoot(99, 4, 15, 2, coolDown: 500),
                      new HpLessTransition(0.7, "4")
                        ),
                    new State("4",

                      new Taunt("arcanuo ANGRY"),
                      new Flash(0xFF000, 1, 5),
                      new Wander(1),
                      new Follow(0.25, 99, 0),
                      new Charge(2, 20, 3000),
                      new Shoot(99, 12, 30, fixedAngle: 0, coolDown: 3000),
                      new Shoot(99, 4, 15, 2, coolDown: 500),
                      new HpLessTransition(0.5, "5")
                        ),
                    new State("5",

                      new Taunt("arcanuo hurt </3"),
                      new Wander(1),
                      new Follow(0.5, 99, 2),
                      new Charge(2, 20, 3000),
                      new Shoot(99, 12, 30, projectileIndex: 1, fixedAngle: 0, coolDown: 3000),
                      new Shoot(99, 6, 60, projectileIndex: 0, fixedAngle: 0, coolDown: 1500),
                      new Shoot(99, 4, 15, 2, coolDown: 500),
                        new HpLessTransition(0.1, "dead")
                        ),
                     new State("dead",
                        new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                        new Taunt("arcanuo dead"),
                        new ReturnToSpawn(2),
                        new Flash(0xFF000, 0.5, 3),
                        new TimedTransition(1500, "dead1")
                        ),
                    new State("dead1",
                        new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                        new Shoot(99, 10, 36, 0, coolDown: 15000),
                        new TimedTransition(500, "dead2")
                        ),
                    new State("dead2",
                        new Spawn("FALLEN TELEPORT", 1, 1, coolDown: 50000),
                        new Suicide()
                        )


                ),
                new Threshold(0.0001,
                    //new ItemLoot("Page of Catatonia", 1 / 700f),
                    new ItemLoot("Short Sword", 1 / 1f),
                    new ItemLoot("arcanuop the arcanuo of valor", 1 / 1500f),
                    new ItemLoot("femboy arcanuo tights", 1 / 600f),
                    new ItemLoot("arcanuo party hat", 1 / 600f),
                    new ItemLoot("arcanuo ANGRY", 1 / 450f),
                    new ItemLoot("Dreadcull T9 Upgrade", 1 / 900f)
                    )
            )

            .Init("Fallen Warrior",
            new State(
                new State("wanbder",
                    new ScaleHP(1),
                    new Wander(0.5),
                      new Charge(2, 20, 3000),
                      new Shoot(99, 1, coolDown: 3000)
                    )
                ),
            new Threshold(0.0001,
                    new ItemLoot("Short Sword", 1 / 1f),
                    new ItemLoot("A Fallen Light (Token)", 1 / 1000f),
                    new ItemLoot("Warrior's HEAD", 1 / 1000f)
                    )
            )
        .Init("FALLEN TELEPORT",
            new State(
                new State("lol",
                    new Taunt("TELEPORT ACTIVATING IN 10 SECONDS"),
                    new TimedTransition(10000, "2")
                    ),
                new State("2",
                    new ReplaceTile("White Floor 3", "CGarden Floor", 1),
                    new TeleportPlayerTo(165, 150, 1)
                    )
                )
            )


        .Init("FALLEN DOORCLOSE1",
            new State(
                new State("0",
                    new ConditionalEffect(ConditionEffectIndex.Invincible)
                    ),
                new State("1",
                    new Taunt("CLOSING IN 5 SECONDS"),
                    new TimedTransition(1000, "2")
                    ),
                new State("2",
                    new Taunt("CLOSING IN 4 SECONDS"),
                    new TimedTransition(1000, "3")
                    ),
                new State("3",
                    new Taunt("CLOSING IN 3 SECONDS"),
                    new TimedTransition(1000, "4")
                    ),
                new State("4",
                    new Taunt("CLOSING IN 2 SECONDS"),
                    new TimedTransition(1000, "5")
                    ),
                new State("5",
                    new Taunt("CLOSING NOW RUN"),
                    new TimedTransition(1000, "6")
                    ),
                new State("6",
                    new ReplaceTile("White Floor 2", "Tod Lava", 1),
                     new Suicide()
                    )


            )
            )
        .Init("FALLEN DOORCLOSE2",
           new State(
                new State("0",
                    new ConditionalEffect(ConditionEffectIndex.Invincible)
                    ),
                new State("1",
                    new Taunt("CLOSING IN 3 SECONDS"),
                    new TimedTransition(3000, "2")
                    ),
                new State("2",
                    new ReplaceTile("White Floor 2", "Tod Lava", 1),
                     new Suicide()
                    )


            )
            )




        #endregion

        #region final Michael 2
        .Init("Michael Spawner",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new State("1",
                        new Spawn("hatsune mike", 1, 1, 1000000),
                        new TimedTransition(1000, "2")
                        ),
                    new State("2",
                        new EntityNotExistsTransition("hatsune mike", 999, "3")
                        ),
                    new State("3",
                        new TimedTransition(6000, "1")
                        )
                    )
                )

        .Init("hatsune mike",
                new State(

                    new ScaleHP(0.05),
                    new State("1",
                        new PlayerWithinTransition(15, "1a"),
                        new ConditionalEffect(ConditionEffectIndex.Invincible)
                        ),
                    new State("1a",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new Taunt("Hello Everyone, I’m Scott, President of Domino’s Pizza! Have you heard of Hatsune Miku? Today I'd like to announce a new collaborative project featuring Hatsune Miku: Domino's App featuring Hatsune Miku! Hatsune Miku exists in a software called Vocaloid. Vocaloid enables you to produce songs. A character named Hatsune Miku sings the songs you create. A great feature is you can create songs as you like. I knew our talented Domino's Pizza crew could work together and create great Vocaloid songs. Bokolo P, Esshi, Chokyoshi, Futitsukeshi, everyone! Amazing Vocaloid songs have been created with the fantastic imagination of the crews from all over Japan! The challenge was successfully carried out; and this new collaborative app was produced. ＤＯＭＩＮＯＳ ＰＩＺＡＡᵃ ᵃ ᵃ Based on Miku's image, the Domino's app changes its appearance a lot of music and illustrations produced by Domino's crew are here. From the menu to the order just like Miku. Once your pizza is delivered, have some fun with Miku! it comes with a social camera function and you can take various poses, pictures of Miku. Very cool! And last, but not least, the live performance! Start the pizza stage live and point the camera towards the pizza box and the pizza box will turn into a live dancing venue. A live performance of Luv4Night produced by Domino's crew! Here we go!"),
                        new TimedTransition(2000, "2")
                        ),
                    new State("2",
                        new RemoveConditionalEffect(ConditionEffectIndex.Invincible),
                        new Wander(1),
                        new Shoot(99, 13, 7, 1, coolDown: 1200),
                        new Shoot(99, 8, 45, 0, coolDown: 3000),
                        new HpLessTransition(0.7, "3")
                        ),
                    new State("3",
                        new Follow(0.3, 30, 0, coolDown: 10),
                        new Shoot(99, 3, 15, 0, coolDown: 1200),
                        new Shoot(99, 7, 6, 1, coolDown: 1600, rotateAngle: 15, defaultAngle: 0),
                        new HpLessTransition(0.2, "dead1")
                        ),
                    new State("dead1",
                        new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                        new Taunt("Spotify Premium $12.99/month"),
                        new Flash(0xFFA500, 0.2, 10),
                        new TimedTransition(1000, "dead2")
                        ),
                    new State("dead2",
                        new Suicide()
                        )
                    ),
            new Threshold(0.0001,
                new ItemLoot("miku Key", 1 / 15f),
                new ItemLoot("microphone left", 1 / 50f),
                new ItemLoot("Legendsary Token 1", 1 / 5f),
                new ItemLoot("hatsune miku", 1 / 50f),
                new ItemLoot("microphone right", 1 / 50f),
                new ItemLoot("Dreadcull T4 Upgrade", 1 / 300f),
                new ItemLoot("6 dolla r :)", 1 / 150f),
                new ItemLoot("Spaceship Token", 1 / 175f)
                    )
                )

        .Init("prism of chaos",
                new State(

                    new ScaleHP(0.05),
                    new State("1",
                        new PlayerWithinTransition(15, "1a"),
                        new ConditionalEffect(ConditionEffectIndex.Invincible)
                        ),
                    new State("1a",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new Taunt("Prism of Chaos is a tier 8 pickaxe that is available in World 2. Prism of Chaos is currently the last pickaxe available in the world, and is also the hardest pickaxe to craft in the game. It has many powerful abilities, each with high luck boosts. Prism of Chaos is infamous for its recipe, containing lots of ores from every layer. "),
                        new TimedTransition(2000, "2")
                        ),
                    new State("2",
                        new RemoveConditionalEffect(ConditionEffectIndex.Invincible),
                        new Wander(1),
                        new Shoot(99, 3, 7, 0, coolDown: 600),
                        new Shoot(99, 8, 45, 1, coolDown: 1800),
                        new TimedTransition(2000, "2a"),
                        new HpLessTransition(0.6, "3")
                        ),
                    new State("2a",
                        new RemoveConditionalEffect(ConditionEffectIndex.Invincible),
                        new Shoot(99, 2, 2, 0, coolDown: 200),
                        new Shoot(99, 8, 45, 1, coolDown: 600),
                        new TimedTransition(2000, "2"),
                        new HpLessTransition(0.6, "3")
                        ),
                    new State("3",
                        new Follow(0.3, 30, 0, coolDown: 10),
                        new Shoot(99, 3, 15, 0, coolDown: 800),
                        new Shoot(99, 18, 20, 1, coolDown: 2100, rotateAngle: 15, defaultAngle: 0),
                        new HpLessTransition(0.2, "dead1")
                        ),
                    new State("dead1",
                        new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                        new Taunt("Prism Burst:spinning prism is conjured, shooting up to 8 orbs out, mining blocks in the orb's path. The orb's color is random and affects its luck and time, scaling inversely based on where it is on a rainbow"),
                        new Flash(0xFFA500, 0.2, 10),
                        new TimedTransition(1000, "dead2")
                        ),
                    new State("dead2",
                        new Suicide()
                        )
                    ),
            new Threshold(0.0001,

                new ItemLoot("Darkstone Pickaxe", 1 / 325f),
                new ItemLoot("Rocc", 1 / 325f),
                new ItemLoot("rexr Key", 1 / 15f),

                new ItemLoot("a flarev2", 1 / 1075f),
                new ItemLoot("prism of prism of chaos", 1 / 175f),
                new ItemLoot("tisk tisk maybe YOURE", 1 / 1075f),
                new ItemLoot("Dreadcull T4 Upgrade", 1 / 300f),
                new ItemLoot("Spaceship Token", 1 / 500f),
                new ItemLoot("nyctophyte", 1 / 50000f)
                    )
                )

        .Init("a flare v2",
                new State(

                    new ScaleHP(0.05),
                    new State("1",
                        new PlayerWithinTransition(15, "1a"),
                        new ConditionalEffect(ConditionEffectIndex.Invincible)
                        ),
                    new State("1a",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new Taunt("a flare v2 is an Unfathomable tier ore created by KlaarV. It has a 1 in 160,000,000 chance of spawning in the Rocc layer in Subworld 1. It currently has no use in the game. "),
                        new TimedTransition(2000, "2")
                        ),
                    new State("2",
                        new RemoveConditionalEffect(ConditionEffectIndex.Invincible),
                        new Wander(1),
                        new Shoot(99, 3, 7, 0, coolDown: 600),
                        new Shoot(99, 8, 45, 1, coolDown: 1800),
                        new TimedTransition(2000, "2a"),
                        new HpLessTransition(0.6, "3")
                        ),
                    new State("2a",
                        new RemoveConditionalEffect(ConditionEffectIndex.Invincible),
                        new Shoot(99, 2, 2, 0, coolDown: 200),
                        new Shoot(99, 8, 45, 1, coolDown: 600),
                        new TimedTransition(2000, "2"),
                        new HpLessTransition(0.6, "3")
                        ),
                    new State("3",
                        new Follow(0.3, 30, 0, coolDown: 10),
                        new Shoot(99, 3, 15, 0, coolDown: 800),
                        new Shoot(99, 18, 20, 1, coolDown: 2100, rotateAngle: 15, defaultAngle: 0),
                        new HpLessTransition(0.2, "dead1")
                        ),
                    new State("dead1",
                        new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                        new Flash(0xFFA500, 0.2, 10),
                        new Taunt("Unfathomable tier ore."),
                        new TimedTransition(1000, "dead2")
                        ),
                    new State("dead2",
                        new Suicide()
                        )
                    ),
            new Threshold(0.0001,

                new ItemLoot("Darkstone Pickaxe", 1 / 325f),
                new ItemLoot("Rocc", 1 / 175f),
                new ItemLoot("rexr Key", 1 / 15f),

                new ItemLoot("a flarev2", 1 / 375f),
                new ItemLoot("prism of prism of chaos", 1 / 1075f),
                new ItemLoot("tisk tisk maybe YOURE", 1 / 1075f),
                new ItemLoot("Dreadcull T4 Upgrade", 1 / 300f),
                new ItemLoot("Spaceship Token", 1 / 500f),
                new ItemLoot("illusory bubblegarments", 1 / 50000f)
                    )
                )

        .Init("cakecore",
                new State(

                    new ScaleHP(0.05),
                    new State("1",
                        new PlayerWithinTransition(15, "1a"),
                        new ConditionalEffect(ConditionEffectIndex.Invincible)
                        ),
                    new State("1a",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new Taunt("hey buddy :) chill the fuck out i saw the word that got moderated you used the fucking r slur?"),
                        new TimedTransition(2000, "2")
                        ),
                    new State("2",
                        new RemoveConditionalEffect(ConditionEffectIndex.Invincible),
                        new Wander(1),
                        new Shoot(99, 3, 7, 0, coolDown: 600),
                        new Shoot(99, 8, 45, 1, coolDown: 1800),
                        new TimedTransition(2000, "2a"),
                        new HpLessTransition(0.6, "3")
                        ),
                    new State("2a",
                        new RemoveConditionalEffect(ConditionEffectIndex.Invincible),
                        new Shoot(99, 2, 2, 0, coolDown: 200),
                        new Shoot(99, 8, 45, 1, coolDown: 600),
                        new TimedTransition(2000, "2"),
                        new HpLessTransition(0.6, "3")
                        ),
                    new State("3",
                        new Follow(0.3, 30, 0, coolDown: 10),
                        new Shoot(99, 3, 15, 0, coolDown: 800),
                        new Shoot(99, 18, 20, 1, coolDown: 2100, rotateAngle: 15, defaultAngle: 0),
                        new HpLessTransition(0.2, "dead1")
                        ),
                    new State("dead1",
                        new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                        new Flash(0xFFA500, 0.2, 10),
                        new Taunt("tisk tisk tisk maybe YOURE the 14 year old"),
                        new TimedTransition(1000, "dead2")
                        ),
                    new State("dead2",
                        new Suicide()
                        )
                    ),
            new Threshold(0.0001,

                new ItemLoot("Darkstone Pickaxe", 1 / 175f),
                new ItemLoot("Rocc", 1 / 1075f),
                new ItemLoot("a flarev2", 1 / 1075f),
                new ItemLoot("rexr Key", 1 / 15f),

                new ItemLoot("prism of prism of chaos", 1 / 1075f),
                new ItemLoot("tisk tisk maybe YOURE", 1 / 325f),
                new ItemLoot("Dreadcull T4 Upgrade", 1 / 300f),
                new ItemLoot("Spaceship Token", 1 / 500f),
                new ItemLoot("dynamo of fate", 1 / 50000f)
                    )
                )

        .Init("Prism Spawner",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new State("1",
                        new Spawn("prism of chaos", 1, 1, 1000000),
                        new TimedTransition(1000, "2")
                        ),
                    new State("2",
                        new EntityNotExistsTransition("prism of chaos", 999, "3")
                        ),
                    new State("3",
                        new TimedTransition(6000, "1")
                        )
                    )
                )
        .Init("FlareV2 Spawner",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new State("1",
                        new Spawn("a flare v2", 1, 1, 1000000),
                        new TimedTransition(1000, "2")
                        ),
                    new State("2",
                        new EntityNotExistsTransition("a flare v2", 999, "3")
                        ),
                    new State("3",
                        new TimedTransition(6000, "1")
                        )
                    )
                )
        .Init("Cakecore Spawner",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new State("1",
                        new Spawn("cakecore", 1, 1, 1000000),
                        new TimedTransition(1000, "2")
                        ),
                    new State("2",
                        new EntityNotExistsTransition("cakecore", 999, "3")
                        ),
                    new State("3",
                        new TimedTransition(6000, "1")
                        )
                    )
                )

        .Init("garlci",
                new State(

                    new ScaleHP(0.05),
                    new State("1",
                        new PlayerWithinTransition(15, "1a"),
                        new ConditionalEffect(ConditionEffectIndex.Invincible)
                        ),
                    new State("1a",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new Taunt("God Will Not Save You Here."),
                        new TimedTransition(400, "2")
                        ),
                    new State("2",
                        new RemoveConditionalEffect(ConditionEffectIndex.Invincible),
                        new Wander(7),
                        new Shoot(99, 3, 7, 2, coolDown: 600),
                        new Shoot(99, 18, 20, 0, coolDown: 400),
                        new TimedTransition(2000, "2a"),
                        new HpLessTransition(0.6, "3")
                        ),
                    new State("2a",
                        new RemoveConditionalEffect(ConditionEffectIndex.Invincible),
                        new Shoot(99, 2, 2, 2, coolDown: 200, predictive: 1),
                        new Shoot(99, 2, 8, 2, coolDown: 200, predictive: 1),
                        new Shoot(99, 18, 20, 0, coolDown: 200),
                        new TimedTransition(2000, "2b"),
                        new HpLessTransition(0.6, "3")
                        ),
                    new State("2b",
                        new Follow(3, 99, 4, predictive: true),
                        new RemoveConditionalEffect(ConditionEffectIndex.Invincible),
                        new Shoot(99, 2, 2, 0, coolDown: 200),
                        new Shoot(99, 8, 45, 1, coolDown: 600),
                        new TimedTransition(2000, "2"),
                        new HpLessTransition(0.6, "3")
                        ),
                    new State("3",
                        new Taunt("You Are Nothing But Dirt To Me."),
                        new Follow(3, 30, 2, coolDown: 400),
                        new Wander(7),
                        new Shoot(99, 3, 15, 0, coolDown: 200),
                        new Shoot(99, 18, 20, 1, coolDown: 600, rotateAngle: 15, defaultAngle: 0),
                        new HpLessTransition(0.2, "dead1")
                        ),
                    new State("dead1",
                        new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                        new Flash(0xFFA500, 0.2, 10),
                        new Taunt("Unfathomable tier ore."),
                        new TimedTransition(1000, "dead2")
                        ),
                    new State("dead2",
                        new Suicide()
                        )
                    ),
            new Threshold(0.0001,

                new ItemLoot("Darkstone Pickaxe", 1 / 325f),
                new ItemLoot("Rocc", 1 / 175f),
                new ItemLoot("rexr Key", 1 / 15f),
                new ItemLoot("a flarev2", 1 / 375f),
                new ItemLoot("prism of prism of chaos", 1 / 1075f),
                new ItemLoot("tisk tisk maybe YOURE", 1 / 1075f),
                new ItemLoot("Dreadcull T4 Upgrade", 1 / 300f),
                new ItemLoot("Spaceship Token", 1 / 500f)
                    )
                )

        .Init("The Cat Spawner",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new State("1",
                        new Spawn("The Cat", 1, 1, 1000000),
                        new TimedTransition(1000, "2")
                        ),
                    new State("2",
                        new EntityNotExistsTransition("The Cat", 999, "3")
                        ),
                    new State("3",
                        new TimedTransition(6000, "1")
                        )
                    )
                )

        .Init("battleDog Spawner",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new State("1",
                        new Spawn("battleDog", 1, 1, 1000000),
                        new TimedTransition(1000, "2")
                        ),
                    new State("2",
                        new EntityNotExistsTransition("battleDog", 999, "3")
                        ),
                    new State("3",
                        new TimedTransition(6000, "1")
                        )
                    )
                )

        .Init("sunfish jones Spawner",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new State("1",
                        new Spawn("sunfish jones", 1, 1, 1000000),
                        new TimedTransition(1000, "2")
                        ),
                    new State("2",
                        new EntityNotExistsTransition("sunfish jones", 999, "3")
                        ),
                    new State("3",
                        new TimedTransition(6000, "1")
                        )
                    )
                )

        .Init("sunfish jones",
            new State(
                new ScaleHP(0.05),
                new State("waiting",
                    new PlayerWithinTransition(12, "begin")
                    ),
                new State("begin",
                    new Shoot(40, 1, projectileIndex: 0, coolDown: 400),
                    new Shoot(40, 4, shootAngle: 30, projectileIndex: 1, coolDown: 800),
                    new Follow(0.6, 10, 2),
                    new TimedTransition(6000, "spray")
                    ),
                new State("spray",
                    new Wander(0.2),
                    new Shoot(40, 6, shootAngle: 10, fixedAngle: 60, coolDown: 1800),
                    new Shoot(40, 6, shootAngle: 10, fixedAngle: 150, coolDown: 2000, coolDownOffset: 400),
                    new Shoot(40, 6, shootAngle: 10, fixedAngle: 240, coolDown: 2000, coolDownOffset: 800),
                    new Shoot(40, 6, shootAngle: 10, fixedAngle: 330, coolDown: 2000, coolDownOffset: 1200),
                    new TimedTransition(6000, "rage")
                    ),
                new State("rage",
                    new Follow(1.3, 10, 1),
                     new Flash(0xfFF0000, flashRepeats: 1, flashPeriod: 2000),
                     new Shoot(40, 2, shootAngle: 30, projectileIndex: 0, coolDown: 200),
                     new Shoot(40, 18, shootAngle: 20, projectileIndex: 2, coolDown: 600, coolDownOffset: 400),
                     new TimedTransition(8000, "begin")
                    )
                    ),
                new Threshold(0.01,
                    new ItemLoot("sniper the GYATT", 1 / 100f),
                    new ItemLoot("battleGato Key", 1 / 15f),
                    new ItemLoot("catpsule armors", 1 / 100f),
                    new ItemLoot("ASGARD Token", 1 / 175f),
                    new ItemLoot("FAVELA Token", 1 / 250f),
                    new ItemLoot("Deep Sea Token", 1 / 300f),
                    new ItemLoot("A Fallen Light (Token)", 1 / 500f),
                    new ItemLoot("Dreamweaver of Darogek", 1 / 100f),
                    new ItemLoot("key cat lol", 1/100f)
                )
            )

        .Init("battleDog",
                new State(
                    new ConditionalEffect(ConditionEffectIndex.StasisImmune),
                    new ScaleHP(1),
                    new State("default",
                        new Wander(0.05),
                        new PlayerWithinTransition(8, "taunt")
                        ),
                    new State(
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new State("taunt",
                        new Taunt("You can't possibly beat me..the almighty BATTLEDOG!"),
                        new TimedTransition(2000, "taunt2")
                        ),
                    new State("taunt2",
                        new Taunt("You don't realize that I am the BATTLE of the DOG!"),
                        new TimedTransition(2000, "Start1")
                        )
                    ),
                    new State("Start1",
                        new Shoot(10, 4, projectileIndex: 1, shootAngle: 8, coolDown: 1000),
                        new Shoot(10, 6, projectileIndex: 0, shootAngle: 14, coolDown: 3000, coolDownOffset: 1000),
                        new Prioritize(
                            new Follow(0.4, acquireRange: 15, range: 8),
                            new Wander(0.6)
                            ),
                        new TimedTransition(9200, "Rush"),
                    new State("Start1a",
                        new Shoot(10, 10, projectileIndex: 0, coolDown: 1000),
                        new TimedTransition(1000, "Start2")
                        ),
                    new State("Start2",
                        new Shoot(10, 10, projectileIndex: 1, coolDown: 1000),
                        new TimedTransition(1000, "Start1a")
                        )
                        ),
                    new State("Rush",
                        new ConditionalEffect(ConditionEffectIndex.Armored),
                        new Taunt(0.50, "kraken tentacle 2 kc"),
                        new Prioritize(
                            new Follow(1, 8, 1),
                            new Wander(1)
                            ),
                        new Shoot(10, 12, projectileIndex: 1, coolDown: 3000),
                        new Shoot(10, 1, projectileIndex: 0, coolDown: 500),
                        new TimedTransition(7400, "Pink")
                        ),
                    new State(
                        new Shoot(10, 8, projectileIndex: 1, coolDown: 2000),
                        new Shoot(10, 4, projectileIndex: 0, coolDown: 4000, coolDownOffset: 2000),
                        new Prioritize(
                            new Follow(0.4, acquireRange: 15, range: 8),
                            new Wander(0.6)
                            ),
                        new TimedTransition(9200, "Start1"),
                    new State("Pink",
                        new SetAltTexture(2),
                        new Shoot(10, 5, projectileIndex: 0, shootAngle: 14, coolDown: 600),
                        new Shoot(10, 3, projectileIndex: 1, shootAngle: 8, predictive: 1, coolDown: 1800, coolDownOffset: 1000),
                        new TimedTransition(5000, "Orange")
                        ),
                    new State("Orange",
                        new SetAltTexture(1),
                        new Shoot(10, 5, projectileIndex: 0, shootAngle: 14, coolDown: 600),
                        new Shoot(10, 3, projectileIndex: 1, shootAngle: 8, predictive: 1, coolDown: 1800, coolDownOffset: 1000),
                        new TimedTransition(5000, "Green")
                        ),
                    new State("Green",
                        new SetAltTexture(3),
                        new Shoot(10, 5, projectileIndex: 0, shootAngle: 14, coolDown: 600),
                        new Shoot(10, 3, projectileIndex: 0, shootAngle: 8, predictive: 1, coolDown: 1800, coolDownOffset: 1000),
                        new TimedTransition(5000, "Pink")
                        )
                        ),
                    new State("dead1",
                        new SetAltTexture(4),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Taunt("dont tell john"),
                        new TimedTransition(4800, "officiallydead")
                        ),
                    new State("officiallydead",
                        new Suicide()

                    )

          ),
          new Threshold(0.0001,

              new ItemLoot("sniper the GYATT", 1 / 100f),
              new ItemLoot("battleGato Key", 1 / 15f),
              new ItemLoot("catpsule armors", 1 / 100f),
              new ItemLoot("God's First Mistake", 1 / 300f),
              new ItemLoot("Spaceship Token", 1 / 175f),
              new ItemLoot("FAVELA Token", 1 / 250f),
              new ItemLoot("Deep Sea Token", 1 / 300f),
              new ItemLoot("A Fallen Light (Token)", 1 / 500f),
              new ItemLoot("ASGARD Token", 1 / 200f),
              new ItemLoot("key cat lol", 1/100f)
          ))

            .Init("The Cat",
                new State(

                    new ScaleHP(0.05),
                    new State("1",
                        new PlayerWithinTransition(15, "1a"),
                        new ConditionalEffect(ConditionEffectIndex.Invincible)
                        ),
                    new State("1a",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new Taunt("chimmy wango"),
                        new TimedTransition(400, "2")
                        ),
                    new State("2",
                        new RemoveConditionalEffect(ConditionEffectIndex.Invincible),
                        new Wander(7),
                        new Shoot(99, 3, 7, 2, coolDown: 1200),
                        new Shoot(99, 18, 20, 0, coolDown: 800),
                        new TimedTransition(2000, "2a"),
                        new HpLessTransition(0.6, "3")
                        ),
                    new State("2a",
                        new RemoveConditionalEffect(ConditionEffectIndex.Invincible),
                        new Shoot(99, 2, 2, 2, coolDown: 600, predictive: 1),
                        new Shoot(99, 18, 20, 0, coolDown: 1000),
                        new TimedTransition(2000, "2b"),
                        new HpLessTransition(0.6, "3")
                        ),
                    new State("2b",
                        new Follow(0.2, 99, 4, predictive: true),
                        new RemoveConditionalEffect(ConditionEffectIndex.Invincible),
                        new Shoot(99, 2, 2, 0, coolDown: 800),
                        new Shoot(99, 8, 45, 1, coolDown: 1400),
                        new TimedTransition(2000, "2"),
                        new HpLessTransition(0.6, "3")
                        ),
                    new State("3",
                        new Taunt("meow :3c"),
                        new Follow(1.5, 30, 2, coolDown: 400),
                        new Wander(7),
                        new Shoot(99, 3, 15, 0, coolDown: 400),
                        new Shoot(99, 18, 20, 1, coolDown: 800, rotateAngle: 15, defaultAngle: 0),
                        new HpLessTransition(0.2, "dead1")
                        ),
                    new State("dead1",
                        new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                        new Flash(0xFFA500, 0.2, 10),
                        new Taunt("we lost fucking korea"),
                        new TimedTransition(1000, "dead2")
                        ),
                    new State("dead2",
                        new Suicide()
                        )
                    ),
            new Threshold(0.0001,

                new ItemLoot("sniper the GYATT", 1 / 100f),
                new ItemLoot("battleGato Key", 1 / 15f),
                new ItemLoot("catpsule armors", 1 / 100f),
                new ItemLoot("Grace of the Colossus", 1 / 300f),
                new ItemLoot("Band of Exoneration", 1 / 150f),
                new ItemLoot("Spaceship Token", 1 / 175f),
                new ItemLoot("FAVELA Token", 1 / 250f),
                new ItemLoot("Deep Sea Token", 1 / 300f),
                new ItemLoot("A Fallen Light (Token)", 1 / 500f),
                new ItemLoot("ASGARD Token", 1 / 200f),
                new ItemLoot("key cat lol", 1/100f)
                    )
                )

                .Init("rs classics man",
                    new State(
                        new ScaleHP(0.05),
                        new State("Idle",
                            new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                            new PlayerWithinTransition(6, "Follow")
                        ),
                        new State("Follow",
                            new Prioritize(
                                new Follow(0.35, 8, 1),
                                new Wander(0.6)
                            ),
                            new Shoot(8, count: 6, projectileIndex: 0, coolDown: 4000),
                            new Shoot(8, count: 8, projectileIndex: 1, coolDown: 4000, coolDownOffset: 1250),
                            new TimedTransition(11000, "Ecto")
                        ),
                        new State("Ecto",
                            new Spawn("Living Ectoplasm", 1, 1, coolDown: 5000),
                            new TimedTransition(2000, "Follow")
                        )
                    ),
                    new Threshold(0.01,
                        new ItemLoot("ASGARD Token", 1 / 175f),
                        new ItemLoot("FAVELA Token", 1 / 250f),
                        new ItemLoot("Deep Sea Token", 1 / 300f),
                        new ItemLoot("A Fallen Light (Token)", 1 / 500f),
                        new ItemLoot("Spaceship Token", 1 / 175f),
                        new ItemLoot("Rose Petal Amulet", 1 / 125f)
                    )
                )

        .Init("pack yak",
                            new State(
                                new ScaleHP(1),
                                new State("Idle",
                                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                                    new PlayerWithinTransition(6, "fight1")
                                ),
                                new State("fight1",
                                    new Prioritize(
                                        new Follow(0.6, 8, 1),
                                        new Wander(1)
                                    ),
                                    new HealSelf(coolDown: 3400),
                                    new Shoot(10, count: 3, shootAngle: 12, projectileIndex: 0, coolDown: 1000),
                                    new Shoot(10, count: 6, shootAngle: 12, projectileIndex: 1, coolDown: 1000, coolDownOffset: 600),
                                    new Shoot(10, count: 7, projectileIndex: 0, coolDown: 1000, coolDownOffset: 1200),
                                    new TimedTransition(6800, "fight2")
                                ),
                                new State("fight2",
                                    new ConditionalEffect(ConditionEffectIndex.Armored),
                                    new Taunt("moooo", "yaak", "waaaa"),
                                    new Prioritize(
                                        new Follow(0.35, 8, 1),
                                        new Wander(1)
                                    ),
                                    new TossObject("The Alive Follower", 7, coolDown: 200),
                                    new TimedTransition(1000, "fight1")
                                )
                            ),

                            new Threshold(0.01,
                                new ItemLoot("ASGARD Token", 1 / 175f),
                                new ItemLoot("FAVELA Token", 1 / 250f),
                                new ItemLoot("Deep Sea Token", 1 / 300f),
                                new ItemLoot("A Fallen Light (Token)", 1 / 500f),
                                new ItemLoot("Spaceship Token", 1 / 175f),
                                new ItemLoot("Rose Petal Amulet", 1 / 125f)
                            )
                        )

                        .Init("durial321",
                            new State(
                                new ScaleHP(1),
                                new State("Idle",
                                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                                    new PlayerWithinTransition(6, "fight1")
                                ),
                                new State("fight1",
                                    new Prioritize(
                                        new Follow(1.3, 10, 1),
                                        new Wander(0.2)
                                    ),
                                    new Shoot(99, count: 8, shootAngle: 4, projectileIndex: 1, coolDown: 400),
                                    new TimedTransition(1500, "fight2")
                                ),
                                new State("fight2",
                                    new Prioritize(
                                        new StayBack(0.7, 6.5),
                                        new Wander(0.5)
                                    ),
                                    new Shoot(99, count: 8, shootAngle: 3, projectileIndex: 0, coolDown: 200, coolDownOffset: 200),
                                    new TimedTransition(3000, "fight1")
                                )
                            ),
                            new Threshold(0.01,
                                new ItemLoot("ASGARD Token", 1 / 175f),
                                new ItemLoot("FAVELA Token", 1 / 250f),
                                new ItemLoot("Deep Sea Token", 1 / 300f),
                                new ItemLoot("A Fallen Light (Token)", 1 / 500f),
                                new ItemLoot("Spaceship Token", 1 / 175f),
                                new ItemLoot("Dark Ice Shard", 1 / 125f),
                                new ItemLoot("Dark Ice Sliver", 1 / 125f)
                            )
                        )

                        .Init("rs classics man Spawner",
                                new State(
                                new ConditionalEffect(ConditionEffectIndex.Invincible),
                                new State("1",
                                new Spawn("rs classics man", 1, 1, 1000000),
                                new TimedTransition(1000, "2")
                                    ),
                                new State("2",
                                new EntityNotExistsTransition("rs classics man", 999, "3")
                                    ),
                                new State("3",
                                new TimedTransition(6000, "1")
                                    )
                                    )
                                    )

                                    .Init("pack yak Spawner",
                                new State(
                                new ConditionalEffect(ConditionEffectIndex.Invincible),
                                new State("1",
                                new Spawn("pack yak", 1, 1, 1000000),
                                new TimedTransition(1000, "2")
                                    ),
                                new State("2",
                                new EntityNotExistsTransition("pack yak", 999, "3")
                                    ),
                                new State("3",
                                new TimedTransition(6000, "1")
                                    )
                                    )
                                    )

                                    .Init("durial321 Spawner",
                                new State(
                                new ConditionalEffect(ConditionEffectIndex.Invincible),
                                new State("1",
                                new Spawn("durial321", 1, 1, 1000000),
                                new TimedTransition(1000, "2")
                                    ),
                                new State("2",
                                new EntityNotExistsTransition("durial321", 999, "3")
                                    ),
                                new State("3",
                                new TimedTransition(6000, "1")
                                    )
                                    )
                                    )
        .Init("SUPREME clamtits",
                            new State(
                                new ScaleHP(1),
                                new State("Idle",
                                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                                    new PlayerWithinTransition(6, "fight1")
                                ),
                                new State("fight1",
                                    new Taunt("im going to sit on you"),
                                    new Follow(0.5, 30, 1),
                                    new Shoot(30, count: 3, shootAngle: 20, projectileIndex: 2, coolDown: 1000),
                                    new Shoot(30, count: 2, shootAngle: 180, projectileIndex: 0, coolDown: 500, rotateAngle: -15),
                                    new TimedTransition(6800, "fight2"),
                                    new HpLessTransition(0.8, "fight2")
                                ),
                                new State("fight2",
                                    new Follow(0.5, 30, 1),
                                    new Shoot(30, count: 3, shootAngle: 20, projectileIndex: 2, coolDown: 1000),
                                    new Shoot(30, count: 2, shootAngle: 180, projectileIndex: 0, coolDown: 500, rotateAngle: -15),
                                    new Spawn("SUPREME clamtits CLONE", 1, 1),
                                    new HpLessTransition(0.6, "fight3")
                                ),
                                new State("fight3",
                                    new ConditionalEffect(ConditionEffectIndex.Armored),
                                    new ReturnToSpawn(3, 50),
                                    new Shoot(30, count: 3, shootAngle: 20, projectileIndex: 1, coolDown: 1000, predictive: 0.5),
                                    new Shoot(30, count: 5, fixedAngle: 0, shootAngle: 72, projectileIndex: 0, coolDown: 250, rotateAngle: -10),
                                    new Order(30, "SUPREME clamtits CLONE", "fight2"),
                                    new HpLessTransition(0.3, "fight4")
                                ),
                                new State("fight4",
                                    new ConditionalEffect(ConditionEffectIndex.Armored),
                                    new Follow(1, 30, 3),
                                    new StayCloseToSpawn(2, 8),
                                    new Shoot(30, count: 5, fixedAngle: 0, shootAngle: 72, projectileIndex: 0, coolDown: 250, rotateAngle: 10),
                                    new Order(30, "SUPREME clamtits CLONE", "fight3"),
                                    new HpLessTransition(0.1, "dead0")
                                    ),
                                new State("dead0",
                                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                                    new Flash(0xFFA500, 0.5, 3),
                                    new Order(30, "SUPREME clamtits CLONE", "dead"),
                                    new Taunt("may i have 3 big macs and 2 large fries"),
                                    new TimedTransition(1500, "dead")
                                    ),
                                new State("dead",
                                    new Shoot(30, 18, 20, 2, coolDown: 99999),
                                    new Suicide()
                                    )
                            ),
                            new Threshold(0.01,
                                new ItemLoot("ASGARD Token", 1 / 175f),
                                new ItemLoot("FAVELA Token", 1 / 250f),
                                new ItemLoot("Deep Sea Token", 1 / 300f),
                                new ItemLoot("A Fallen Light (Token)", 1 / 500f),
                                new ItemLoot("Spaceship Token", 1 / 175f),
                                new ItemLoot("BRIMSTON JEWEL real", 1 / 900f),
                                new ItemLoot("clamtits", 1 / 900f),
                                new ItemLoot("mcdonalds number 1 combo meal", 1 / 350f),
                                new ItemLoot("cosmic blade of the universal cosmic blade of the cosmic of the universe", 1 / 900f),
                                new ItemLoot("LOAD of sea men", 1 / 400f),
                                new ItemLoot("sword of the the of the", 1 / 900f),
                                new ItemLoot("ULTIMATE armors", 1 / 300f),
                                new ItemLoot("Dreadcull T9 Upgrade", 1 / 1200f),
                                new ItemLoot("jsut some dust", 1 / 300f)
                            )
                        )
        .Init("SUPREME clamtits CLONE",
                    new State(
                                new State("fight1",
                                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                                        new Protect(1, "SUPREME clamtits", 30, 1),
                                        new Taunt("FE FI FO FUM"),
                                        new Shoot(20, 2, 180, fixedAngle: 0, projectileIndex: 1, rotateAngle: 30, coolDown: 250)
                                    ),
                                new State("fight2",
                                    new Orbit(2.5, 15, 30, "SUPREME clamtits"),
                                    new Shoot(30, count: 2, shootAngle: 20, projectileIndex: 2, coolDown: 500)
                                ),
                                new State("fight3",
                                    new Follow(1.5, 30, 3),
                                    new Shoot(30, count: 4, shootAngle: 10, projectileIndex: 1, coolDown: 750)
                                ),
                                new State("dead",
                                    new Suicide()
                                    )
                        )
            )
        
        .Init("moon lrod legs",
                            new State(
                                new ScaleHP(0.5),
                                new State("Idle",
                                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                                    new PlayerWithinTransition(6, "fight")
                                ),
                                new State("fight",
                                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                                    new TimedTransition(2000, "fight1")
                                ),
                                new State("fight1",
                                    new Taunt("hey im the moon lord legs how are you today?"),
                                    new Taunt("hey im the moon lord legs how are you today?"),
                                    new Taunt("hey im the moon lord legs how are you today?"),
                                    new Taunt("hey im the moon lord legs how are you today?"),
                                    new Taunt("hey im the moon lord legs how are you today?"),
                                    new Taunt("hey im the moon lord legs how are you today?"),
                                    new Taunt("hey im the moon lord legs how are you today?"),
                                    new Taunt("hey im the moon lord legs how are you today?"),
                                    new Follow(0.5, 30, 1),
                                    new Shoot(30, count: 3, shootAngle: 20, projectileIndex: 1, coolDown: 1500),
                                    new Shoot(30, count: 2, shootAngle: 15, projectileIndex: 0, coolDown: 500),
                                    new HpLessTransition(0.8, "fight2")
                                ),
                                new State("fight2",
                                    new Taunt("hey im the moon lord legs how are you today?"),
                                    new Taunt("hey im the moon lord legs how are you today?"),
                                    new Taunt("hey im the moon lord legs how are you today?"),
                                    new Taunt("hey im the moon lord legs how are you today?"),
                                    new Taunt("hey im the moon lord legs how are you today?"),
                                    new Taunt("hey im the moon lord legs how are you today?"),
                                    new Taunt("hey im the moon lord legs how are you today?"),
                                    new Taunt("hey im the moon lord legs how are you today?"),
                                    new Taunt("hey im the moon lord legs how are you today?"),
                                    new Taunt("hey im the moon lord legs how are you today?"),
                                    new Taunt("hey im the moon lord legs how are you today?"),
                                    new Taunt("hey im the moon lord legs how are you today?"),
                                    new Taunt("hey im the moon lord legs how are you today?"),
                                    new Follow(0.5, 30, 1),
                                    new Shoot(30, count: 3, shootAngle: 10, projectileIndex: 1, coolDown: 1000),
                                    new Shoot(30, count: 2, shootAngle: 15, projectileIndex: 0, coolDown: 500),
                                    new HpLessTransition(0.6, "fight3")
                                ),
                                new State("fight3",
                                    new Taunt("hey im the moon lord legs how are you today?"),
                                    new Taunt("hey im the moon lord legs how are you today?"),
                                    new Taunt("hey im the moon lord legs how are you today?"),
                                    new Taunt("hey im the moon lord legs how are you today?"),
                                    new Taunt("hey im the moon lord legs how are you today?"),
                                    new Taunt("hey im the moon lord legs how are you today?"),
                                    new Taunt("hey im the moon lord legs how are you today?"),
                                    new Taunt("hey im the moon lord legs how are you today?"),
                                    new Taunt("hey im the moon lord legs how are you today?"),
                                    new Taunt("hey im the moon lord legs how are you today?"),
                                    new Taunt("hey im the moon lord legs how are you today?"),
                                    new Taunt("hey im the moon lord legs how are you today?"),
                                    new Taunt("hey im the moon lord legs how are you today?"),
                                    new Taunt("hey im the moon lord legs how are you today?"),
                                    new Taunt("hey im the moon lord legs how are you today?"),
                                    new Taunt("hey im the moon lord legs how are you today?"),
                                    new Follow(0.5, 30, 1),
                                    new Shoot(30, count: 3, shootAngle: 20, projectileIndex: 1, coolDown: 1000),
                                    new Shoot(30, count: 5, shootAngle:10, projectileIndex: 0, coolDown: 250)
                                )
                            ),
                            new Threshold(0.01,
                                new ItemLoot("ASGARD Token", 1 / 375f),
                                new ItemLoot("FAVELA Token", 1 / 350f),
                                new ItemLoot("Deep Sea Token", 1 / 400f),
                                new ItemLoot("A Fallen Light (Token)", 1 / 700f),
                                new ItemLoot("Spaceship Token", 1 / 375f),
                                new ItemLoot("luminite BAR", 1 / 700f),
                                new ItemLoot("luminite", 1 / 200f),
                                new ItemLoot("star wrath", 1 / 900f),
                                new ItemLoot("lunar flar", 1 / 500f),
                                new ItemLoot("s.d.m.g.", 1 / 250f),
                                new ItemLoot("Dreadcull T9 Upgrade", 1 / 1200f)
                            )
                        )
        .Init("enderman",
                            new State(
                                new ScaleHP(0.5),
                                new State("Idle",
                                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                                    new PlayerWithinTransition(6, "fight")
                                ),
                                new State("fight",
                                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                                    new TimedTransition(2000, "fight1")
                                ),
                                new State("fight1",
                                    new Taunt("ender man lol"),
                                    new Follow(0.5, 30, 1),
                                    new Shoot(30, count: 3, shootAngle: 20, projectileIndex: 1, coolDown: 1500),
                                    new Shoot(30, count: 6, shootAngle: 60, projectileIndex: 0, coolDown: 2000),
                                    new HpLessTransition(0.8, "fight2")
                                ),
                                new State("fight2",
                                    new Taunt("ender man lol"),
                                    new Follow(1, 30, 1),
                                    new Shoot(30, count: 3, shootAngle: 10, projectileIndex: 1, coolDown: 1500),
                                    new Shoot(30, count: 12, shootAngle: 30, projectileIndex: 0, coolDown: 2000),
                                    new HpLessTransition(0.6, "fight3")
                                ),
                                new State("fight3",
                                    new Taunt("ender man lol"),
                                    new Follow(2, 30, 1),
                                    new Shoot(30, count: 3, shootAngle: 20, projectileIndex: 1, coolDown: 1500),
                                    new Shoot(30, count: 18, shootAngle: 20, projectileIndex: 0, coolDown: 2000)
                                )
                            ),
                            new Threshold(0.01,
                                new ItemLoot("ASGARD Token", 1 / 375f),
                                new ItemLoot("FAVELA Token", 1 / 350f),
                                new ItemLoot("Deep Sea Token", 1 / 400f),
                                new ItemLoot("A Fallen Light (Token)", 1 / 700f),
                                new ItemLoot("Spaceship Token", 1 / 375f),
                                new ItemLoot("ender pearl", 1 / 200f),
                                new ItemLoot("enderman head", 1 / 400f),
                                new ItemLoot("Dreadcull T9 Upgrade", 1 / 1200f)
                            )
                        )
        .Init("MANIC MOHAWK CAT",
                            new State(
                                new ScaleHP(0.5),
                                new DropPortalOnDeath("MANIC ERASER PORTAL", 1),
                                new State("Idle",
                                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                                    new PlayerWithinTransition(10, "fight")
                                ),
                                new State("fight",
                                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                                    new TimedTransition(3000, "fight1")
                                ),
                                new State("fight1",
                                    new Taunt("MANIC MOHAWK CAT"),
                                    new Wander(1),
                                    new Shoot(30, count: 3, shootAngle: 20, projectileIndex: 1, coolDown: 1500),
                                    new Shoot(30, count: 6, shootAngle: 60, projectileIndex: 0, coolDown: 2000),
                                    new HpLessTransition(0.8, "fight2")
                                ),
                                new State("fight2",
                                    new Wander(1),
                                    new Shoot(30, count: 3, shootAngle: 10, projectileIndex: 1, coolDown: 1500),
                                    new Shoot(30, count: 12, shootAngle: 30, projectileIndex: 0, coolDown: 2000),
                                    new HpLessTransition(0.6, "fight3")
                                ),
                                new State("fight3",
                                    new Taunt("MANIC MOHAWK CAT"),
                                    new Follow(1, 30, 1),
                                    new Shoot(30, count: 3, shootAngle: 20, projectileIndex: 1, coolDown: 1500),
                                    new Shoot(30, count: 18, shootAngle: 20, projectileIndex: 0, coolDown: 2000)
                                )
                            ),
                            new Threshold(0.01,
                                new ItemLoot("pink catseye", 1 / 10f),
                                new ItemLoot("bronze catseye", 1 / 100f),
                                new ItemLoot("SILVER catseye", 1 / 500f),
                                new ItemLoot("GOLD catseye", 1 / 1500f),
                                new ItemLoot("RAINBOW catseye", 1 / 3000f),
                                new ItemLoot("CHAOS catseye", 1 / 15000f),
                                new ItemLoot("a fuckin brick LOL", 1 / 20000f),
                                new ItemLoot("mohawk SKULL", 1 / 1800f),
                                new ItemLoot("Dreadcull T9 Upgrade", 1 / 1200f)
                            )
                        )
        .Init("MANIC ERASER",
                            new State(
                                new ScaleHP(0.5),
                                new DropPortalOnDeath("MANIC AXE CAT PORTAL", 1),
                                new State("Idle",
                                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                                    new PlayerWithinTransition(10, "fight")
                                ),
                                new State("fight",
                                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                                    new TimedTransition(3000, "fight1")
                                ),
                                new State("fight1",
                                    new Taunt("MANIC MOHAWK CAT"),
                                    new Wander(1),
                                    new Shoot(30, count: 3, shootAngle: 20, projectileIndex: 1, coolDown: 1500),
                                    new Shoot(30, count: 6, shootAngle: 60, projectileIndex: 0, coolDown: 2000),
                                    new HpLessTransition(0.8, "fight2")
                                ),
                                new State("fight2",
                                    new Wander(1),
                                    new Shoot(30, count: 3, shootAngle: 10, projectileIndex: 1, coolDown: 1500),
                                    new Shoot(30, count: 12, shootAngle: 30, projectileIndex: 0, coolDown: 2000),
                                    new HpLessTransition(0.6, "fight3")
                                ),
                                new State("fight3",
                                    new Taunt("MANIC MOHAWK CAT"),
                                    new Follow(1, 30, 1),
                                    new Shoot(30, count: 3, shootAngle: 20, projectileIndex: 1, coolDown: 1500),
                                    new Shoot(30, count: 18, shootAngle: 20, projectileIndex: 0, coolDown: 2000)
                                )
                            ),
                            new Threshold(0.01,
                                new ItemLoot("pink catseye", 1 / 10f),
                                new ItemLoot("bronze catseye", 1 / 100f),
                                new ItemLoot("SILVER catseye", 1 / 500f),
                                new ItemLoot("GOLD catseye", 1 / 1500f),
                                new ItemLoot("RAINBOW catseye", 1 / 3000f),
                                new ItemLoot("CHAOS catseye", 1 / 15000f),
                                new ItemLoot("a fuckin brick LOL", 1 / 20000f),
                                new ItemLoot("INFInity eraser", 1 / 1800f),
                                new ItemLoot("Dreadcull T9 Upgrade", 1 / 1200f)
                            )
                        )
        .Init("MANIC AXE CAT",
                            new State(
                                new ScaleHP(0.5),
                                new DropPortalOnDeath("MANIC MACHO LEGS PORTAL", 1),
                                new State("Idle",
                                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                                    new PlayerWithinTransition(10, "fight")
                                ),
                                new State("fight",
                                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                                    new TimedTransition(3000, "fight1")
                                ),
                                new State("fight1",
                                    new Taunt("MANIC MOHAWK CAT"),
                                    new Wander(1),
                                    new Shoot(30, count: 3, shootAngle: 20, projectileIndex: 1, coolDown: 1500),
                                    new Shoot(30, count: 6, shootAngle: 60, projectileIndex: 0, coolDown: 2000),
                                    new HpLessTransition(0.8, "fight2")
                                ),
                                new State("fight2",
                                    new Wander(1),
                                    new Shoot(30, count: 3, shootAngle: 10, projectileIndex: 1, coolDown: 1500),
                                    new Shoot(30, count: 12, shootAngle: 30, projectileIndex: 0, coolDown: 2000),
                                    new HpLessTransition(0.6, "fight3")
                                ),
                                new State("fight3",
                                    new Taunt("MANIC MOHAWK CAT"),
                                    new Follow(1, 30, 1),
                                    new Shoot(30, count: 3, shootAngle: 20, projectileIndex: 1, coolDown: 1500),
                                    new Shoot(30, count: 18, shootAngle: 20, projectileIndex: 0, coolDown: 2000)
                                )
                            ),
                            new Threshold(0.01,
                                new ItemLoot("pink catseye", 1 / 10f),
                                new ItemLoot("bronze catseye", 1 / 100f),
                                new ItemLoot("SILVER catseye", 1 / 500f),
                                new ItemLoot("GOLD catseye", 1 / 1500f),
                                new ItemLoot("RAINBOW catseye", 1 / 3000f),
                                new ItemLoot("CHAOS catseye", 1 / 15000f),
                                new ItemLoot("a fuckin brick LOL", 1 / 20000f),
                                new ItemLoot("battle axe katana lol", 1 / 1800f),
                                new ItemLoot("Dreadcull T9 Upgrade", 1 / 1200f)
                            )
                        )
        .Init("MANIC MACHO LEGS",
                            new State(
                                new ScaleHP(0.5),
                                new DropPortalOnDeath("MANIC LION CAT PORTAL", 1),
                                new State("Idle",
                                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                                    new PlayerWithinTransition(10, "fight")
                                ),
                                new State("fight",
                                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                                    new TimedTransition(3000, "fight1")
                                ),
                                new State("fight1",
                                    new Taunt("MANIC MOHAWK CAT"),
                                    new Wander(1),
                                    new Shoot(30, count: 3, shootAngle: 20, projectileIndex: 1, coolDown: 1500),
                                    new Shoot(30, count: 6, shootAngle: 60, projectileIndex: 0, coolDown: 2000),
                                    new HpLessTransition(0.8, "fight2")
                                ),
                                new State("fight2",
                                    new Wander(1),
                                    new Shoot(30, count: 3, shootAngle: 10, projectileIndex: 1, coolDown: 1500),
                                    new Shoot(30, count: 12, shootAngle: 30, projectileIndex: 0, coolDown: 2000),
                                    new HpLessTransition(0.6, "fight3")
                                ),
                                new State("fight3",
                                    new Taunt("MANIC MOHAWK CAT"),
                                    new Follow(1, 30, 1),
                                    new Shoot(30, count: 3, shootAngle: 20, projectileIndex: 1, coolDown: 1500),
                                    new Shoot(30, count: 18, shootAngle: 20, projectileIndex: 0, coolDown: 2000)
                                )
                            ),
                            new Threshold(0.01,
                                new ItemLoot("pink catseye", 1 / 10f),
                                new ItemLoot("bronze catseye", 1 / 100f),
                                new ItemLoot("SILVER catseye", 1 / 500f),
                                new ItemLoot("GOLD catseye", 1 / 1500f),
                                new ItemLoot("RAINBOW catseye", 1 / 3000f),
                                new ItemLoot("CHAOS catseye", 1 / 15000f),
                                new ItemLoot("a fuckin brick LOL", 1 / 20000f),
                                new ItemLoot("stronk legs", 1 / 1800f),
                                new ItemLoot("Dreadcull T9 Upgrade", 1 / 1200f)
                            )
                        )
        .Init("MANIC LION CAT",
                            new State(
                                new ScaleHP(0.5),
                                new DropPortalOnDeath("MANIC FLYING CAT PORTAL", 1),
                                new State("Idle",
                                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                                    new PlayerWithinTransition(10, "fight")
                                ),
                                new State("fight",
                                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                                    new TimedTransition(3000, "fight1")
                                ),
                                new State("fight1",
                                    new Taunt("MANIC MOHAWK CAT"),
                                    new Wander(1),
                                    new Shoot(30, count: 3, shootAngle: 20, projectileIndex: 1, coolDown: 1500),
                                    new Shoot(30, count: 6, shootAngle: 60, projectileIndex: 0, coolDown: 2000),
                                    new HpLessTransition(0.8, "fight2")
                                ),
                                new State("fight2",
                                    new Wander(1),
                                    new Shoot(30, count: 3, shootAngle: 10, projectileIndex: 1, coolDown: 1500),
                                    new Shoot(30, count: 12, shootAngle: 30, projectileIndex: 0, coolDown: 2000),
                                    new HpLessTransition(0.6, "fight3")
                                ),
                                new State("fight3",
                                    new Taunt("MANIC MOHAWK CAT"),
                                    new Follow(1, 30, 1),
                                    new Shoot(30, count: 3, shootAngle: 20, projectileIndex: 1, coolDown: 1500),
                                    new Shoot(30, count: 18, shootAngle: 20, projectileIndex: 0, coolDown: 2000)
                                )
                            ),
                            new Threshold(0.01,
                                new ItemLoot("pink catseye", 1 / 10f),
                                new ItemLoot("bronze catseye", 1 / 100f),
                                new ItemLoot("SILVER catseye", 1 / 500f),
                                new ItemLoot("GOLD catseye", 1 / 1500f),
                                new ItemLoot("RAINBOW catseye", 1 / 3000f),
                                new ItemLoot("CHAOS catseye", 1 / 15000f),
                                new ItemLoot("a fuckin brick LOL", 1 / 20000f),
                                new ItemLoot("lions mane", 1 / 1800f),
                                new ItemLoot("Dreadcull T9 Upgrade", 1 / 1200f)
                            )
                        )
        .Init("MANIC FLYING CAT",
                            new State(
                                new ScaleHP(0.5),
                                new DropPortalOnDeath("MANIC ISLAND CAT PORTAL", 1),
                                new State("Idle",
                                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                                    new PlayerWithinTransition(10, "fight")
                                ),
                                new State("fight",
                                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                                    new TimedTransition(3000, "fight1")
                                ),
                                new State("fight1",
                                    new Taunt("MANIC MOHAWK CAT"),
                                    new Wander(1),
                                    new Shoot(30, count: 3, shootAngle: 20, projectileIndex: 1, coolDown: 1500),
                                    new Shoot(30, count: 6, shootAngle: 60, projectileIndex: 0, coolDown: 2000),
                                    new HpLessTransition(0.8, "fight2")
                                ),
                                new State("fight2",
                                    new Wander(1),
                                    new Shoot(30, count: 3, shootAngle: 10, projectileIndex: 1, coolDown: 1500),
                                    new Shoot(30, count: 12, shootAngle: 30, projectileIndex: 0, coolDown: 2000),
                                    new HpLessTransition(0.6, "fight3")
                                ),
                                new State("fight3",
                                    new Taunt("MANIC MOHAWK CAT"),
                                    new Follow(1, 30, 1),
                                    new Shoot(30, count: 3, shootAngle: 20, projectileIndex: 1, coolDown: 1500),
                                    new Shoot(30, count: 18, shootAngle: 20, projectileIndex: 0, coolDown: 2000)
                                )
                            ),
                            new Threshold(0.01,
                                new ItemLoot("pink catseye", 1 / 10f),
                                new ItemLoot("bronze catseye", 1 / 100f),
                                new ItemLoot("SILVER catseye", 1 / 500f),
                                new ItemLoot("GOLD catseye", 1 / 1500f),
                                new ItemLoot("RAINBOW catseye", 1 / 3000f),
                                new ItemLoot("CHAOS catseye", 1 / 15000f),
                                new ItemLoot("a fuckin brick LOL", 1 / 20000f),
                                new ItemLoot("laser pew pew", 1 / 1800f),
                                new ItemLoot("Dreadcull T9 Upgrade", 1 / 1200f)
                            )
                        )
        .Init("MANIC ISLAND CAT",
                            new State(
                                new ScaleHP(0.5),
                                new DropPortalOnDeath("MANIC KING DRAGON PORTAL", 1),
                                new State("Idle",
                                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                                    new PlayerWithinTransition(10, "fight")
                                ),
                                new State("fight",
                                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                                    new TimedTransition(3000, "fight1")
                                ),
                                new State("fight1",
                                    new Taunt("MANIC MOHAWK CAT"),
                                    new Wander(1),
                                    new Shoot(30, count: 3, shootAngle: 20, projectileIndex: 1, coolDown: 1500),
                                    new Shoot(30, count: 6, shootAngle: 60, projectileIndex: 0, coolDown: 2000),
                                    new HpLessTransition(0.8, "fight2")
                                ),
                                new State("fight2",
                                    new Wander(1),
                                    new Shoot(30, count: 3, shootAngle: 10, projectileIndex: 1, coolDown: 1500),
                                    new Shoot(30, count: 12, shootAngle: 30, projectileIndex: 0, coolDown: 2000),
                                    new HpLessTransition(0.6, "fight3")
                                ),
                                new State("fight3",
                                    new Taunt("MANIC MOHAWK CAT"),
                                    new Follow(1, 30, 1),
                                    new Shoot(30, count: 3, shootAngle: 20, projectileIndex: 1, coolDown: 1500),
                                    new Shoot(30, count: 18, shootAngle: 20, projectileIndex: 0, coolDown: 2000)
                                )
                            ),
                            new Threshold(0.01,
                                new ItemLoot("pink catseye", 1 / 10f),
                                new ItemLoot("bronze catseye", 1 / 100f),
                                new ItemLoot("SILVER catseye", 1 / 500f),
                                new ItemLoot("GOLD catseye", 1 / 1500f),
                                new ItemLoot("RAINBOW catseye", 1 / 3000f),
                                new ItemLoot("CHAOS catseye", 1 / 15000f),
                                new ItemLoot("a fuckin brick LOL", 1 / 20000f),
                                new ItemLoot("tropical potion", 1 / 1800f),
                                new ItemLoot("Dreadcull T9 Upgrade", 1 / 1200f)
                            )
                        )
        .Init("MANIC KING DRAGON",
                            new State(
                                new ScaleHP(0.5),
                                new DropPortalOnDeath("MANIC JAMEIRA CAT PORTAL", 1),
                                new State("Idle",
                                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                                    new PlayerWithinTransition(10, "fight")
                                ),
                                new State("fight",
                                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                                    new TimedTransition(3000, "fight1")
                                ),
                                new State("fight1",
                                    new Taunt("MANIC MOHAWK CAT"),
                                    new Wander(1),
                                    new Shoot(30, count: 3, shootAngle: 20, projectileIndex: 1, coolDown: 1500),
                                    new Shoot(30, count: 6, shootAngle: 60, projectileIndex: 0, coolDown: 2000),
                                    new HpLessTransition(0.8, "fight2")
                                ),
                                new State("fight2",
                                    new Wander(1),
                                    new Shoot(30, count: 3, shootAngle: 10, projectileIndex: 1, coolDown: 1500),
                                    new Shoot(30, count: 12, shootAngle: 30, projectileIndex: 0, coolDown: 2000),
                                    new HpLessTransition(0.6, "fight3")
                                ),
                                new State("fight3",
                                    new Taunt("MANIC MOHAWK CAT"),
                                    new Follow(1, 30, 1),
                                    new Shoot(30, count: 3, shootAngle: 20, projectileIndex: 1, coolDown: 1500),
                                    new Shoot(30, count: 18, shootAngle: 20, projectileIndex: 0, coolDown: 2000)
                                )
                            ),
                            new Threshold(0.01,
                                new ItemLoot("pink catseye", 1 / 10f),
                                new ItemLoot("bronze catseye", 1 / 100f),
                                new ItemLoot("SILVER catseye", 1 / 500f),
                                new ItemLoot("GOLD catseye", 1 / 1500f),
                                new ItemLoot("RAINBOW catseye", 1 / 3000f),
                                new ItemLoot("CHAOS catseye", 1 / 15000f),
                                new ItemLoot("a fuckin brick LOL", 1 / 20000f),
                                new ItemLoot("dragon flame staff primal", 1 / 1800f),
                                new ItemLoot("Dreadcull T9 Upgrade", 1 / 1200f)
                            )
                        )
        .Init("MANIC JAMEIRA CAT",
                            new State(
                                new ScaleHP(0.5),
                                new State("Idle",
                                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                                    new PlayerWithinTransition(10, "fight")
                                ),
                                new State("fight",
                                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                                    new TimedTransition(3000, "fight1")
                                ),
                                new State("fight1",
                                    new Taunt("MANIC MOHAWK CAT"),
                                    new Wander(1),
                                    new Shoot(30, count: 3, shootAngle: 20, projectileIndex: 1, coolDown: 1500),
                                    new Shoot(30, count: 6, shootAngle: 60, projectileIndex: 0, coolDown: 2000),
                                    new HpLessTransition(0.8, "fight2")
                                ),
                                new State("fight2",
                                    new Wander(1),
                                    new Shoot(30, count: 3, shootAngle: 10, projectileIndex: 1, coolDown: 1500),
                                    new Shoot(30, count: 12, shootAngle: 30, projectileIndex: 0, coolDown: 2000),
                                    new HpLessTransition(0.6, "fight3")
                                ),
                                new State("fight3",
                                    new Taunt("MANIC MOHAWK CAT"),
                                    new Follow(1, 30, 1),
                                    new Shoot(30, count: 3, shootAngle: 20, projectileIndex: 1, coolDown: 1500),
                                    new Shoot(30, count: 18, shootAngle: 20, projectileIndex: 0, coolDown: 2000)
                                )
                            ),
                            new Threshold(0.01,
                                new ItemLoot("pink catseye", 1 / 10f),
                                new ItemLoot("bronze catseye", 1 / 100f),
                                new ItemLoot("SILVER catseye", 1 / 500f),
                                new ItemLoot("GOLD catseye", 1 / 1500f),
                                new ItemLoot("RAINBOW catseye", 1 / 3000f),
                                new ItemLoot("CHAOS catseye", 1 / 15000f),
                                new ItemLoot("a fuckin brick LOL", 1 / 20000f),
                                new ItemLoot("love mommmy shirt", 1 / 1800f),
                                new ItemLoot("Dreadcull T9 Upgrade", 1 / 1200f)
                            )
                        )
        .Init("i need more boolets",
                            new State(
                                new ScaleHP(0.5),
                                new State("Idle",
                                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                                    new PlayerWithinTransition(6, "fight")
                                ),
                                new State("fight",
                                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                                    new TimedTransition(2000, "fight1")
                                ),
                                new State("fight1",
                                    new Taunt("i need more boolets"),
                                    new Taunt("i need more boolets"),
                                    new Taunt("i need more boolets"),
                                    new Taunt("i need more boolets"),
                                    new SoundPlay(0),
                                    new Charge(2, 8, 2000),
                                    new Wander(0.5),
                                    new Shoot(30, count: 3, shootAngle: 20, projectileIndex: 1, coolDown: 2000),
                                    new Shoot(30, count: 12, shootAngle: 30, projectileIndex: 0, coolDown: 1000),
                                    new HpLessTransition(0.5, "fight2")
                                ),
                                new State("fight2",
                                    new Taunt("i need more boolets"),
                                    new Taunt("i need more boolets"),
                                    new Taunt("i need more boolets"),
                                    new Taunt("i need more boolets"),
                                    new SoundPlay(0),
                                    new Follow(1, 50, 3),
                                    new Wander(0.5),
                                    new Shoot(30, count: 3, shootAngle: 20, projectileIndex: 1, coolDown: 1000),
                                    new Shoot(30, count: 4, shootAngle: 10, projectileIndex: 0, coolDown: 1250),
                                    new HpLessTransition(0.1, "fight3")
                                ),
                                new State("fight3",
                                    new Taunt("i need more boolets"),
                                    new Taunt("i need more boolets"),
                                    new Taunt("i need more boolets"),
                                    new SoundPlay(0),
                                    new Follow(1, 50, 3),
                                    new Charge(3, 8, 2000),
                                    new Wander(0.5),
                                    new Shoot(30, count: 8, shootAngle: 45, projectileIndex: 0, coolDown: 1500, predictive: 0.5),
                                    new Shoot(30, count: 10, fixedAngle: 0, shootAngle: 36, projectileIndex: 1, coolDown: 1250)
                                    )
                            ),
                            new Threshold(0.01,
                                new ItemLoot("ASGARD Token", 1 / 375f),
                                new ItemLoot("FAVELA Token", 1 / 350f),
                                new ItemLoot("Deep Sea Token", 1 / 400f),
                                new ItemLoot("A Fallen Light (Token)", 1 / 700f),
                                new ItemLoot("Spaceship Token", 1 / 375f),
                                new ItemLoot("katana or something", 1 / 175f),
                                new ItemLoot("i need more boolets!", 1 / 250f),
                                new ItemLoot("some boolets", 1 / 250f),
                                new ItemLoot("boolet", 1 / 200f),
                                new ItemLoot("Dreadcull T9 Upgrade", 1 / 1200f)
                            )
                        )
        .Init("purple minion real",
                            new State(
                                new ScaleHP(0.5),
                                new DropPortalOnDeath("LH Void Portal", 1 / 10f),
                                new State("Idle",
                                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                                    new PlayerWithinTransition(6, "fight")
                                ),
                                new State("fight",
                                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                                    new TimedTransition(2000, "fight1")
                                ),
                                new State("fight1",
                                    new Wander(0.5),
                                    new Shoot(30, count: 4, shootAngle: 10, projectileIndex: 1, coolDown: 2000),
                                    new Shoot(30, count: 12, shootAngle: 30, projectileIndex: 0, coolDown: 2000),
                                    new HpLessTransition(0.5, "fight2")
                                ),
                                new State("fight2",
                                    new Follow(1, 50, 3),
                                    new Wander(0.5),
                                    new Shoot(30, count: 2, shootAngle: 10, projectileIndex: 1, coolDown: 1000),
                                    new Shoot(30, count: 12, shootAngle: 30, projectileIndex: 0, coolDown: 2000),
                                    new HpLessTransition(0.1, "fight3")
                                ),
                                new State("fight3",
                                    new Charge(3, 6, 2500),
                                    new Wander(0.5),
                                    new Shoot(30, count: 3, shootAngle: 20, projectileIndex: 1, coolDown: 500),
                                    new Shoot(30, count: 12, shootAngle: 30, projectileIndex: 0, coolDown: 2000)
                                    )
                            ),
                            new Threshold(0.01,
                                new ItemLoot("ASGARD Token", 1 / 375f),
                                new ItemLoot("FAVELA Token", 1 / 350f),
                                new ItemLoot("Deep Sea Token", 1 / 400f),
                                new ItemLoot("A Fallen Light (Token)", 1 / 700f),
                                new ItemLoot("Spaceship Token", 1 / 375f),
                                new ItemLoot("the void i think but its a key", 1 / 150f),
                                new ItemLoot("purple minion HEAD", 1 / 250f),
                                new ItemLoot("nsfw purple minion nipples", 1 / 700f),
                                new ItemLoot("Dreadcull T9 Upgrade", 1 / 1000f)
                            )
                        )
        .Init("drake real",
                            new State(
                                new ScaleHP(0.5),
                                new State("Idle",
                                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                                    new PlayerWithinTransition(6, "fight")
                                ),
                                new State("fight",
                                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                                    new TimedTransition(2000, "fight1")
                                ),
                                new State("fight1",
                                    new Taunt("anita max wynn"),
                                    new SoundPlay(0),
                                    new Wander(0.5),
                                    new Shoot(30, count: 3, shootAngle: 20, projectileIndex: 1, coolDown: 2000),
                                    new Shoot(30, count: 2, shootAngle: 10, projectileIndex: 0, coolDown: 1000),
                                    new HpLessTransition(0.5, "fight2")
                                ),
                                new State("fight2",
                                    new Taunt("anita max wynn"),
                                    new SoundPlay(0),
                                    new Follow(1, 50, 3),
                                    new Wander(0.5),
                                    new Shoot(30, count: 3, shootAngle: 20, projectileIndex: 1, coolDown: 1000),
                                    new Shoot(30, count: 2, shootAngle: 180, projectileIndex: 0, coolDown: 500),
                                    new HpLessTransition(0.1, "fight3")
                                ),
                                new State("fight3",
                                    new Taunt("anita max wynn"),
                                    new SoundPlay(0),
                                    new Follow(1, 50, 3),
                                    new Wander(0.5),
                                    new Shoot(30, count: 2, shootAngle: 20, projectileIndex: 0, coolDown: 1000, predictive: 0.5),
                                    new Shoot(30, count: 5, fixedAngle: 0, shootAngle: 72, projectileIndex: 1, coolDown: 1000)
                                    )
                            ),
                            new Threshold(0.01,
                                new ItemLoot("ASGARD Token", 1 / 375f),
                                new ItemLoot("FAVELA Token", 1 / 350f),
                                new ItemLoot("Deep Sea Token", 1 / 400f),
                                new ItemLoot("A Fallen Light (Token)", 1 / 700f),
                                new ItemLoot("Spaceship Token", 1 / 375f),
                                new ItemLoot("drake mic", 1 / 175f),
                                new ItemLoot("drake t shit", 1 / 250f),
                                new ItemLoot("anita max wynn", 1 / 1000f),
                                new ItemLoot("Dreadcull T9 Upgrade", 1 / 1200f)
                            )
                        )
        #endregion final Michael 2
        .Init("skbidi Spawner",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invincible),
                new State("0",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new TimedRandomTransition(1000, false, "1a", "1b", "1c", "1d", "1e", "1f", "1g", "1h", "1i", "1l"/*, "1k", "1l", "1m", "1n"*/)
                    ),
                    new State("1a",
                        new Spawn("moon lrod legs", 1, 1, 1000000),
                        new TimedTransition(1000, "2a")
                        ),
                    new State("2a",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("moon lrod legs", 999, "3")
                        ),
                    new State("1b",
                        new Spawn("moon lrod legs", 1, 1, 1000000),
                        new TimedTransition(1000, "2b")
                        ),
                    new State("2b",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("moon lrod legs", 999, "3")
                        ),
                    new State("1c",
                        new Spawn("drake real", 1, 1, 1000000),
                        new TimedTransition(1000, "2c")
                        ),
                    new State("2c",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("drake real", 999, "3")
                        ),
                    new State("1d",
                        new Spawn("drake real", 1, 1, 1000000),
                        new TimedTransition(1000, "2d")
                        ),
                    new State("2d",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("drake real", 999, "3")
                        ),
                    new State("1e",
                        new Spawn("i need more boolets", 1, 1, 1000000),
                        new TimedTransition(1000, "2e")
                        ),
                    new State("2e",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("i need more boolets", 999, "3")
                        ),
                    new State("1f",
                        new Spawn("i need more boolets", 1, 1, 1000000),
                        new TimedTransition(1000, "2f")
                        ),
                    new State("2f",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("i need more boolets", 999, "3")
                        ),
                    new State("1g",
                        new Spawn("purple minion real", 1, 1, 1000000),
                        new TimedTransition(1000, "2g")
                        ),
                    new State("2g",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("purple minion real", 999, "3")
                        ),
                    new State("1h",
                        new Spawn("purple minion real", 1, 1, 1000000),
                        new TimedTransition(1000, "2h")
                        ),
                    new State("2h",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("purple minion real", 999, "3")
                        ),
                    new State("1i",
                        new Spawn("enderman", 1, 1, 1000000),
                        new TimedTransition(1000, "2i")
                        ),
                    new State("2i",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("enderman", 999, "3")
                        ),

                    new State("1l",
                        new Spawn("enderman", 1, 1, 1000000),
                        new TimedTransition(1000, "2l")
                        ),
                    new State("2l",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("enderman", 999, "3")
                        ),
                    /*
                    new State("1k",
                        new Spawn("DDT", 1, 1, 1000000),
                        new TimedTransition(1000, "2k")
                        ),
                    new State("2k",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("DDT", 999, "3")
                        ),
                    new State("1l",
                        new Spawn("battleDog", 1, 1, 1000000),
                        new TimedTransition(1000, "2l")
                    ),
                    new State("2l",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("battleDog", 999, "3")
                    ),
                    new State("1m",
                        new Spawn("The Cat", 1, 1, 1000000),
                        new TimedTransition(1000, "2m")
                    ),
                    new State("2m",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("The Cat", 999, "3")
                    ),
                    new State("1n",
                        new Spawn("sunfish jones", 1, 1, 1000000),
                        new TimedTransition(1000, "2n")
                    ),
                    new State("2n",
                        new Wander(3),
                        new StayCloseToSpawn(0.5, 21),
                        new EntityNotExistsTransition("sunfish jones", 999, "3")
                    ),
                    */
                    new State("3",
                        new Wander(10f),
                        new StayCloseToSpawn(5f, 10),
                        new TimedTransition(1000, "0")
                    )
                    )
                )
        .Init("ender dragoon",
                new State(
                    new ScaleHP(0.5),
                    new ScaleDEF(.1),
                    new State("default",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new PlayerWithinTransition(10, "start")
                        ),
                    new State("start",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new Flash(0xff0000, 1, 3),
                        new TimedTransition(3000, "weirdmovement")
                        ),
                    new State("weirdmovement",
                        new Taunt("KILL."),
                        new Follow(1, 70, 3),
                        new Shoot(70, 1, 15, 0, coolDown: 25),
                        new Shoot(70, 6, 60, 1, 0, coolDown: 1000),
                        new TimedTransition(2000, "weirdmovement1"),
                        new HpLessTransition(0.8, "1")
                        ),
                    new State("weirdmovement1",
                        new Follow(1, 70, 3),
                        new Shoot(70, 6, 60, 1, 0, coolDown: 1000),
                        new TimedTransition(2000, "weirdmovement"),
                        new HpLessTransition(0.8, "1")
                        ),
                    new State("1",
                        new Follow(3, 70, 5),
                        new StayCloseToSpawn(2, 20),
                        new TossObject("dragoon EGG", 30, coolDown: 5000),
                        new Shoot(70, 4, 10, 2, coolDown: 500),
                        new Shoot(70, 6, 60, 3, 0, coolDown: 2000),
                        new HpLessTransition(0.6, "2")
                        ),
                    new State("2",
                        new Charge(3, 15, 2500),
                        new Wander(.25),
                        new Shoot(70, 3, 10, 2, coolDown: 1500),
                        new Shoot(70, 4, 15, 3, coolDown: 2000),
                        new HpLessTransition(0.4, "3")
                        ),
                    new State("3",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new Taunt("scary dragopn noises"),
                        new Flash(0xff0000, 1, 3),
                        new HealSelf(100, 1000000),
                        new ReturnToSpawn(1.5, 100),
                        new TimedTransition(3000, "3.25")
                        ),
                    new State("3.25",
                        new ConditionalEffect(ConditionEffectIndex.Armored),
                        new Wander(.25),
                        new Shoot(50, 1, fixedAngle: 180, coolDown: 25),
                        new Shoot(50, 1, fixedAngle: 0, coolDown: 25),
                        new Shoot(50, 3, 90, 3, coolDown: 500, predictive: 1),
                        new HpLessTransition(0.2, "4"),
                        new TimedTransition(1000, "3.5")
                        ),
                    new State("3.5",
                        new ConditionalEffect(ConditionEffectIndex.Armored),
                        new Wander(.25),
                        new Shoot(50, 1, fixedAngle: 240, coolDown: 25),
                        new Shoot(50, 1, fixedAngle: 60, coolDown: 25),
                        new Shoot(50, 3, 90, 3, coolDown: 500, predictive: 1),
                        new HpLessTransition(0.2, "4"),
                        new TimedTransition(1000, "3.75")
                        ),
                    new State("3.75",
                        new ConditionalEffect(ConditionEffectIndex.Armored),
                        new Wander(.25),
                        new Shoot(50, 1, fixedAngle: 300, coolDown: 25),
                        new Shoot(50, 1, fixedAngle: 120, coolDown: 25),
                        new Shoot(50, 3, 90, 3, coolDown: 500, predictive: 1),
                        new HpLessTransition(0.2, "4"),
                        new TimedTransition(1000, "3.8")
                        ),
                    new State("3.8",
                        new ConditionalEffect(ConditionEffectIndex.Armored),
                        new Wander(.25),
                        new Shoot(50, 1, fixedAngle: 0, coolDown: 25),
                        new Shoot(50, 1, fixedAngle: 180, coolDown: 25),
                        new Shoot(50, 3, 90, 3, coolDown: 500, predictive: 1),
                        new HpLessTransition(0.2, "4"),
                        new TimedTransition(1000, "3.9")
                        ),
                    new State("3.9",
                        new ConditionalEffect(ConditionEffectIndex.Armored),
                        new Wander(.25),
                        new Shoot(50, 1, fixedAngle: 60, coolDown: 25),
                        new Shoot(50, 1, fixedAngle: 240, coolDown: 25),
                        new Shoot(50, 3, 90, 3, coolDown: 500, predictive: 1),
                        new HpLessTransition(0.2, "4"),
                        new TimedTransition(1000, "3.95")
                        ),
                    new State("3.95",
                        new ConditionalEffect(ConditionEffectIndex.Armored),
                        new Wander(.25),
                        new Shoot(50, 1, fixedAngle: 120, coolDown: 25),
                        new Shoot(50, 1, fixedAngle: 300, coolDown: 25),
                        new Shoot(50, 3, 90, 3, coolDown: 500, predictive: 1),
                        new HpLessTransition(0.2, "4"),
                        new TimedTransition(1000, "3.25")
                        ),
                    new State("4",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new ReturnToSpawn(1.5, 100),
                        new HealSelf(300, 1000000),
                        new Flash(0xff0000, .5, 6),
                        new TimedTransition(3000, "5")
                        ),
                    new State("5",
                        new Follow(2.5, 50, 3),
                        new Shoot(50, 6, 60, 3, coolDown: 1500),
                        new TossObject("dragoon EGG", 30, coolDown: 4000),
                        new Shoot(50, 4, 10, 1, coolDown: 500)
                        )
                    ),
                new Threshold(0.1,
                new ItemLoot("dragon splooge", 1 / 1200f),
                new ItemLoot("Fire Sword", 1),
                new ItemLoot("the dragon egg", 1 / 3000f),
                new ItemLoot("endstones", 1 / 300f),
                new ItemLoot("Dragon Head", 1 / 1200f),
                new ItemLoot("Dragon Heart Pendant", 1 / 1500f),
                new ItemLoot("Dragon's Eye", 1 / 1200f),
                new ItemLoot("Dragonscale Plate", 1 / 1200f),
                new ItemLoot("Elytra", 1 / 1200f),
                new ItemLoot("Dreadcull T9 Upgrade", 1 / 1200f)
                )
            )
        .Init("dragoon EGG",
             new State(
                 new ScaleHP(0.5),
                new State("go",
                   new Flash(0xff0000, 1, 3),
                   new TimedTransition(3000, "blow")
                    ),
                new State("blow",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new Shoot(50, 36, 10, coolDown: 999999),
                    new Suicide()
                    )
                 )
              )
        .Init("Eidolon Wyrm",
                new State(
                    new ScaleHP(1),
                    new ScaleDEF(.1),
                    new State("default",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new PlayerWithinTransition(25, "spawnbody")
                        ),
                    new State("spawnbody",
                        new ChangeSize(60, 120),
                        new Swirl(1, 15, targeted: false),
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new Spawn("Eidolon Segment A", 1, 1, coolDown: 99999),
                        new Spawn("Eidolon Segment B", 1, 1, coolDown: 99999),
                        new Spawn("Eidolon Segment C", 1, 1, coolDown: 99999),
                        new Spawn("Eidolon Segment D", 1, 1, coolDown: 99999),
                        new Spawn("Eidolon Segment E", 1, 1, coolDown: 99999),
                        new Spawn("Eidolon Segment F", 1, 1, coolDown: 99999),
                        new Spawn("Eidolon Segment G", 1, 1, coolDown: 99999),
                        new Spawn("Eidolon Segment H", 1, 1, coolDown: 99999),
                        new Spawn("Eidolon Segment I", 1, 1, coolDown: 99999),
                        new Spawn("Eidolon Segment Tail", 1, 1, coolDown: 99999),
                        new Grenade(50, 0, 50, effect: ConditionEffectIndex.Darkness, effectDuration: 999999, coolDown: 5000),
                        new TimedTransition(15000, "weirdmovement")
                        ),
                    new State("weirdmovement",
                        new ConditionalEffect(ConditionEffectIndex.Armored),
                        new SoundPlay(0),
                        new ChangeMusic("intensemusic"),
                        new Grenade(50, 0, 50, effect: ConditionEffectIndex.Darkness, effectDuration: 999999, coolDown: 5000),
                        new Follow(2, 70, 3),
                        new Shoot(70, 4, 15, 0, coolDown: 1000),
                        new Shoot(70, 6, 60, 1, 0, coolDown: 2000),
                        new HpLessTransition(0.9, "1")
                        ),
                    new State("1",
                        new ConditionalEffect(ConditionEffectIndex.Armored),
                        new SoundPlay(0),
                        new OrderOnce(70, "Eidolon Segment Tail", "go1"),
                        new Grenade(50, 0, 50, effect: ConditionEffectIndex.Darkness, effectDuration: 999999, coolDown: 5000),
                        new Swirl(2, 7, 70),
                        new StayCloseToSpawn(2, 30),
                        new Shoot(70, 4, 15, 0, coolDown: 1000),
                        new Shoot(70, 6, 60, 1, 0, coolDown: 2000),
                        new HpLessTransition(0.8, "2")
                        ),
                    new State("2",
                        new ConditionalEffect(ConditionEffectIndex.Armored),
                        new SoundPlay(0),
                        new Follow(1, 80, 5),
                        new Shoot(70, 3, 10, 2, coolDown: 1500),
                        new Shoot(70, 4, 15, 3, 0, coolDown: 2000),
                        new OrderOnce(50, "Eidolon Segment A", "go1"),
                        new OrderOnce(50, "Eidolon Segment B", "go1"),
                        new OrderOnce(50, "Eidolon Segment C", "go1"),
                        new OrderOnce(50, "Eidolon Segment D", "go1"),
                        new OrderOnce(50, "Eidolon Segment E", "go1"),
                        new OrderOnce(50, "Eidolon Segment F", "go1"),
                        new OrderOnce(50, "Eidolon Segment G", "go1"),
                        new OrderOnce(50, "Eidolon Segment H", "go1"),
                        new OrderOnce(50, "Eidolon Segment I", "go1"),
                        new Grenade(50, 0, 50, effect: ConditionEffectIndex.Darkness, effectDuration: 999999, coolDown: 5000),
                        new HpLessTransition(0.6, "pre3"),
                        new TimedTransition(15000, "2.5")
                        ),
                    new State("2.5",
                        new ConditionalEffect(ConditionEffectIndex.Armored),
                        new SoundPlay(0),
                        new StayCloseToSpawn(2, 30),
                        new Grenade(50, 0, 50, effect: ConditionEffectIndex.Darkness, effectDuration: 999999, coolDown: 5000),
                        new Swirl(2, 7, 70),
                        new Shoot(70, 4, 15, 0, coolDown: 1000),
                        new Shoot(70, 6, 60, 1, 0, coolDown: 2000),
                        new HpLessTransition(0.6, "pre3"),
                        new TimedTransition(15000, "pre3")
                        ),
                    new State("pre3",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new ChangeMusic("thefight"),
                        new SoundPlay(0),
                        new SetAltTexture(1),
                        new OrderOnce(50, "Eidolon Segment A", "invis"),
                        new OrderOnce(50, "Eidolon Segment B", "invis"),
                        new OrderOnce(50, "Eidolon Segment C", "invis"),
                        new OrderOnce(50, "Eidolon Segment D", "invis"),
                        new OrderOnce(50, "Eidolon Segment E", "invis"),
                        new OrderOnce(50, "Eidolon Segment F", "invis"),
                        new OrderOnce(50, "Eidolon Segment G", "invis"),
                        new OrderOnce(50, "Eidolon Segment H", "invis"),
                        new OrderOnce(50, "Eidolon Segment I", "invis"),
                        new OrderOnce(50, "Eidolon Segment Tail", "invis"),
                        new ReturnToSpawn(5, 100),
                        new TimedTransition(150, "3")
                        ),
                    new State("3",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new SetAltTexture(1),
                        new OrderOnce(50, "Eidolon Segment A", "invis"),
                        new OrderOnce(50, "Eidolon Segment B", "invis"),
                        new OrderOnce(50, "Eidolon Segment C", "invis"),
                        new OrderOnce(50, "Eidolon Segment D", "invis"),
                        new OrderOnce(50, "Eidolon Segment E", "invis"),
                        new OrderOnce(50, "Eidolon Segment F", "invis"),
                        new OrderOnce(50, "Eidolon Segment G", "invis"),
                        new OrderOnce(50, "Eidolon Segment H", "invis"),
                        new OrderOnce(50, "Eidolon Segment I", "invis"),
                        new OrderOnce(50, "Eidolon Segment Tail", "invis"),
                        new ReturnToSpawn(5, 100),
                        new TimedRandomTransition(1000, false, "3", "3.25", "3.5")
                        ),
                    new State("3.25",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new Follow(10, 70, 3),
                        new TimedTransition(1000, "3.5")
                        ),
                    new State("3.5",
                        new ConditionalEffect(ConditionEffectIndex.Armored),
                        new SoundPlay(0),
                        new HealSelf(2000, 25000),
                        new SetAltTexture(0),
                        new OrderOnce(50, "Eidolon Segment A", "go"),
                        new OrderOnce(50, "Eidolon Segment B", "go"),
                        new OrderOnce(50, "Eidolon Segment C", "go"),
                        new OrderOnce(50, "Eidolon Segment D", "go"),
                        new OrderOnce(50, "Eidolon Segment E", "go"),
                        new OrderOnce(50, "Eidolon Segment F", "go"),
                        new OrderOnce(50, "Eidolon Segment G", "go"),
                        new OrderOnce(50, "Eidolon Segment H", "go"),
                        new OrderOnce(50, "Eidolon Segment I", "go"),
                        new OrderOnce(50, "Eidolon Segment Tail", "go"),
                        new Follow(2, 50, 3),
                        new TimedTransition(1000, "3.75"),
                        new HpLessTransition(0.3, "4")
                        ),
                    new State("3.75",
                        new ConditionalEffect(ConditionEffectIndex.Armored),
                        new Grenade(50, 0, 50, effect: ConditionEffectIndex.Darkness, effectDuration: 999999, coolDown: 5000),
                        new HealSelf(2000, 25000),
                        new SetAltTexture(0),
                        new Follow(1, 50, 3),
                        new Shoot(70, 3, 10, 2, coolDown: 75),
                        new Shoot(70, 6, 60, 1, 0, coolDown: 10000),
                        new TimedTransition(1000, "3.8"),
                        new HpLessTransition(0.3, "4")
                        ),
                    new State("3.8",
                        new ConditionalEffect(ConditionEffectIndex.Armored),
                        new StayCloseToSpawn(2, 30),
                        new Grenade(50, 0, 50, effect: ConditionEffectIndex.Darkness, effectDuration: 999999, coolDown: 5000),
                        new HealSelf(2000, 25000),
                        new SetAltTexture(0),
                        new Follow(1, 50, 3),
                        new Shoot(70, 6, 60, 1, 0, coolDown: 10000),
                        new TimedTransition(1000, "3.9"),
                        new HpLessTransition(0.3, "4")
                        ),
                    new State("3.9",
                        new ConditionalEffect(ConditionEffectIndex.Armored),
                        new StayCloseToSpawn(2, 30),
                        new Grenade(50, 0, 50, effect: ConditionEffectIndex.Darkness, effectDuration: 999999, coolDown: 5000),
                        new HealSelf(2000, 25000),
                        new SetAltTexture(0),
                        new Follow(1, 50, 3),
                        new Shoot(70, 3, 10, 2, coolDown: 75),
                        new Shoot(70, 6, 60, 1, 0, coolDown: 10000),
                        new TimedRandomTransition(1000, false, "3.8", "3"),
                        new HpLessTransition(0.3, "4")
                        ),
                    new State("4",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new SoundPlay(0),
                        new ReturnToSpawn(3, 100),
                        new OrderOnce(50, "Eidolon Segment A", "go2"),
                        new OrderOnce(50, "Eidolon Segment B", "go2"),
                        new OrderOnce(50, "Eidolon Segment C", "go2"),
                        new OrderOnce(50, "Eidolon Segment D", "go2"),
                        new OrderOnce(50, "Eidolon Segment E", "go2"),
                        new OrderOnce(50, "Eidolon Segment F", "go2"),
                        new OrderOnce(50, "Eidolon Segment G", "go2"),
                        new OrderOnce(50, "Eidolon Segment H", "go2"),
                        new OrderOnce(50, "Eidolon Segment I", "go2"),
                        new OrderOnce(50, "Eidolon Segment Tail", "go2"),
                        new TimedTransition(1000, "5")
                        ),
                    new State("5",
                        new ConditionalEffect(ConditionEffectIndex.Armored),
                        new SoundPlay(0),
                        new HealSelf(1000, 10000),
                        new StayCloseToSpawn(2, 30),
                        new Swirl(3, 15, 50, false),
                        new Shoot(70, 2, 180, 3, rotateAngle: 10, fixedAngle: 0, coolDown: 500),
                        new HpLessTransition(0.1, "6")
                        ),
                    new State("6",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new OrderOnce(50, "Eidolon Segment A", "go"),
                        new OrderOnce(50, "Eidolon Segment B", "go"),
                        new OrderOnce(50, "Eidolon Segment C", "go"),
                        new OrderOnce(50, "Eidolon Segment D", "go"),
                        new OrderOnce(50, "Eidolon Segment E", "go"),
                        new OrderOnce(50, "Eidolon Segment F", "go"),
                        new OrderOnce(50, "Eidolon Segment G", "go"),
                        new OrderOnce(50, "Eidolon Segment H", "go"),
                        new OrderOnce(50, "Eidolon Segment I", "go"),
                        new OrderOnce(50, "Eidolon Segment Tail", "go"),
                        new SoundPlay(0),
                        new HealSelf(1000, 10000),
                        new StayCloseToSpawn(2, 30),
                        new Follow(2, 70, 5),
                        new Shoot(70, 2, 180, 3, rotateAngle: 10, fixedAngle: 0, coolDown: 500),
                        new Flash(0xff0000, 1000, 3),
                        new TimedTransition(3000, "end")
                        ),
                    new State("end",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new ChangeMusic("theabyss"),
                        new OrderOnce(50, "Eidolon Segment A", "d"),
                        new OrderOnce(50, "Eidolon Segment B", "d"),
                        new OrderOnce(50, "Eidolon Segment C", "d"),
                        new OrderOnce(50, "Eidolon Segment D", "d"),
                        new OrderOnce(50, "Eidolon Segment E", "d"),
                        new OrderOnce(50, "Eidolon Segment F", "d"),
                        new OrderOnce(50, "Eidolon Segment G", "d"),
                        new OrderOnce(50, "Eidolon Segment H", "d"),
                        new OrderOnce(50, "Eidolon Segment I", "d"),
                        new OrderOnce(50, "Eidolon Segment Tail", "d"),
                        new Shoot(50, 10, 36, 0, 0, coolDown: 100000),
                        new Shoot(50, 12, 30, 4, 0, coolDown: 100000),
                        new Suicide()
                        )
                    ),
                new Threshold(1,
                new ItemLoot("core of claamity", 1 / 2000f),
                new ItemLoot("Rod of Discord", 1 / 700f),
                new ItemLoot("soul edging", 1 / 700f),
                new ItemLoot("bahalicut onnac gun", 1 / 700f),
                new ItemLoot("Lumeneneyil", 1 / 700f),
                new ItemLoot("wyrm tooth neckalce", 1 / 700f),
                new ItemLoot("sirius", 1 / 700f),
                new ItemLoot("gauntlets of the abyss", 1 / 700f),
                new ItemLoot("Dreadcull T9 Upgrade", 1 / 1200f),
                new ItemLoot("Greater Potion of Wisdom", 1)
                )
            )
        .Init("Eidolon Segment A",
             new State(
                 new ConditionalEffect(ConditionEffectIndex.Invincible),
                 new ScaleHP(0.05),
                new State("go",
                    new SetAltTexture(0),
                   new Protect(3, "Eidolon Wyrm", protectionRange: 2)
                    ),
                new State("go1",
                    new SetAltTexture(0),
                   new Protect(3, "Eidolon Wyrm", protectionRange: 2),
                   new Shoot(50, 1, coolDown: 1500, coolDownOffset: 500)
                    ),
                new State("go2",
                    new SetAltTexture(0),
                   new Protect(3, "Eidolon Wyrm", 70, protectionRange: 2),
                   new TimedTransition(1000, "go3")
                    ),
               new State("go3",
                    new SetAltTexture(0),
                   new Protect(3, "Eidolon Wyrm", 70, protectionRange: 2),
                   new Shoot(50, 1, projectileIndex: 1, predictive: 0.5, coolDown: 1500, coolDownOffset: 500)
                    ),
                new State("invis",
                   new Protect(10, "Eidolon Wyrm", 100, protectionRange: 2),
                   new SetAltTexture(1)
                    ),
                new State("d",
                   new Suicide()
                    )
                 )
              )
         .Init("Eidolon Segment B",
             new State(
                 new ConditionalEffect(ConditionEffectIndex.Invincible),
                 new ScaleHP(0.05),
               new State("go",
                   new SetAltTexture(0),
                   new Protect(3, "Eidolon Segment A", protectionRange: 2)
                        ),
               new State("go1",
                    new SetAltTexture(0),
                   new Protect(3, "Eidolon Segment A", protectionRange: 2),
                   new Shoot(50, 1, coolDown: 1500, coolDownOffset: 500)
                    ),
               new State("go2",
                    new SetAltTexture(0),
                   new Protect(3, "Eidolon Segment A", 70, protectionRange: 2),
                   new TimedTransition(1000, "go3")
                    ),
               new State("go3",
                    new SetAltTexture(0),
                   new Protect(3, "Eidolon Segment A", 70, protectionRange: 2),
                   new Shoot(50, 1, projectileIndex: 1, predictive: 0.5, coolDown: 1500, coolDownOffset: 500)
                    ),
               new State("2plan",
                   new Protect(3, "Eidolon Wyrm", protectionRange: 2)
                        ),
               new State("invis",
                   new Protect(10, "Eidolon Segment A", 100, protectionRange: 2),
                   new SetAltTexture(1)
                    ),
               new State("d",
                   new Suicide()
                    )
                    )
                )
         .Init("Eidolon Segment C",
             new State(
                 new ConditionalEffect(ConditionEffectIndex.Invincible),
                 new ScaleHP(0.05),
               new State("go",
                   new SetAltTexture(0),
                   new Protect(3, "Eidolon Segment B", protectionRange: 2)
                        ),
               new State("go1",
                    new SetAltTexture(0),
                   new Protect(3, "Eidolon Segment B", protectionRange: 2),
                   new Shoot(50, 1, coolDown: 1500, coolDownOffset: 500)
                    ),
              new State("go2",
                    new SetAltTexture(0),
                   new Protect(3, "Eidolon Segment B", 70, protectionRange: 2),
                   new TimedTransition(1000, "go3")
                    ),
               new State("go3",
                    new SetAltTexture(0),
                   new Protect(3, "Eidolon Segment B", 70, protectionRange: 2),
                   new Shoot(50, 1, projectileIndex: 1, predictive: 0.5, coolDown: 1500, coolDownOffset: 500)
                    ),
               new State("2plan",
                   new Protect(3, "Eidolon Wyrm", protectionRange: 2)
                        ),
               new State("invis",
                   new Protect(10, "Eidolon Segment B", 100, protectionRange: 2),
                   new SetAltTexture(1)
                    ),
               new State("d",
                   new Suicide()
                    )
                     )
                )
         .Init("Eidolon Segment D",
             new State(
                 new ConditionalEffect(ConditionEffectIndex.Invincible),
                 new ScaleHP(0.05),
               new State("go",
                   new SetAltTexture(0),
                   new Protect(3, "Eidolon Segment C", protectionRange: 2)
                        ),
               new State("go1",
                    new SetAltTexture(0),
                   new Protect(3, "Eidolon Segment C", protectionRange: 2),
                   new Shoot(50, 1, coolDown: 1500, coolDownOffset: 500)
                    ),
               new State("go2",
                    new SetAltTexture(0),
                   new Protect(3, "Eidolon Segment C", 70, protectionRange: 2),
                   new TimedTransition(1000, "go3")
                    ),
               new State("go3",
                    new SetAltTexture(0),
                   new Protect(3, "Eidolon Segment C", 70, protectionRange: 2),
                   new Shoot(50, 1, projectileIndex: 1, predictive: 0.5, coolDown: 1500, coolDownOffset: 500)
                    ),
               new State("2plan",
                   new Protect(3, "Eidolon Wyrm", protectionRange: 2)
                        ),
               new State("invis",
                   new Protect(10, "Eidolon Segment C", 100, protectionRange: 2),
                   new SetAltTexture(1)
                    ),
               new State("d",
                   new Suicide()
                    )
                 )
            )
         .Init("Eidolon Segment E",
             new State(
                 new ConditionalEffect(ConditionEffectIndex.Invincible),
                 new ScaleHP(0.05),
               new State("go",
                   new SetAltTexture(0),
                   new Protect(3, "Eidolon Segment D", protectionRange: 2)
                        ),
               new State("go1",
                    new SetAltTexture(0),
                   new Protect(3, "Eidolon Segment D", protectionRange: 2),
                   new Shoot(50, 1, coolDown: 1500, coolDownOffset: 500)
                    ),
               new State("go2",
                    new SetAltTexture(0),
                   new Protect(3, "Eidolon Segment C", 70, protectionRange: 2),
                   new TimedTransition(1000, "go3")
                    ),
               new State("go3",
                    new SetAltTexture(0),
                   new Protect(3, "Eidolon Segment C", 70, protectionRange: 2),
                   new Shoot(50, 1, projectileIndex: 1, predictive: 0.5, coolDown: 1500, coolDownOffset: 500)
                    ),
               new State("2plan",
                   new Protect(3, "Eidolon Wyrm", protectionRange: 2)
                        ),
               new State("invis",
                   new Protect(10, "Eidolon Segment D", 100, protectionRange: 2),
                   new SetAltTexture(1)
                    ),
               new State("d",
                   new Suicide()
                    )
                 )
            )
         .Init("Eidolon Segment F",
             new State(
                 new ConditionalEffect(ConditionEffectIndex.Invincible),
                 new ScaleHP(0.05),
               new State("go",
                   new SetAltTexture(0),
                   new Protect(3, "Eidolon Segment E", protectionRange: 2)
                        ),
               new State("go1",
                    new SetAltTexture(0),
                   new Protect(3, "Eidolon Segment E", protectionRange: 2),
                   new Shoot(50, 1, coolDown: 1500, coolDownOffset: 500)
                    ),
               new State("go2",
                    new SetAltTexture(0),
                   new Protect(3, "Eidolon Segment E", 70, protectionRange: 2),
                   new TimedTransition(1000, "go3")
                    ),
               new State("go3",
                    new SetAltTexture(0),
                   new Protect(3, "Eidolon Segment E", 70, protectionRange: 2),
                   new Shoot(50, 1, projectileIndex: 1, predictive: 0.5, coolDown: 1500, coolDownOffset: 500)
                    ),
               new State("2plan",
                   new Protect(3, "Eidolon Wyrm", protectionRange: 2)
                        ),
               new State("invis",
                   new Protect(10, "Eidolon Segment E", 100, protectionRange: 2),
                   new SetAltTexture(1)
                    ),
               new State("d",
                   new Suicide()
                    )
                 )
            )
         .Init("Eidolon Segment G",
             new State(
                 new ConditionalEffect(ConditionEffectIndex.Invincible),
                 new ScaleHP(0.05),
               new State("go",
                   new SetAltTexture(0),
                   new Protect(3, "Eidolon Segment F", protectionRange: 2)
                        ),
               new State("go1",
                    new SetAltTexture(0),
                   new Protect(3, "Eidolon Segment F", protectionRange: 2),
                   new Shoot(50, 1, coolDown: 1500, coolDownOffset: 500)
                    ),
               new State("go2",
                    new SetAltTexture(0),
                   new Protect(3, "Eidolon Segment F", 70, protectionRange: 2),
                   new TimedTransition(1000, "go3")
                    ),
               new State("go3",
                    new SetAltTexture(0),
                   new Protect(3, "Eidolon Segment F", 70, protectionRange: 2),
                   new Shoot(50, 1, projectileIndex: 1, predictive: 0.5, coolDown: 1500, coolDownOffset: 500)
                    ),
               new State("2plan",
                   new Protect(3, "Eidolon Wyrm", protectionRange: 2)
                        ),
               new State("invis",
                   new Protect(10, "Eidolon Segment F", 100, protectionRange: 2),
                   new SetAltTexture(1)
                    ),
               new State("d",
                   new Suicide()
                    )
                 )
            )
         .Init("Eidolon Segment H",
             new State(
                 new ConditionalEffect(ConditionEffectIndex.Invincible),
                 new ScaleHP(0.05),
               new State("go",
                   new SetAltTexture(0),
                   new Protect(3, "Eidolon Segment G", protectionRange: 2)
                        ),
               new State("go1",
                    new SetAltTexture(0),
                   new Protect(3, "Eidolon Segment G", protectionRange: 2),
                   new Shoot(50, 1, coolDown: 1500, coolDownOffset: 500)
                    ),
               new State("go2",
                    new SetAltTexture(0),
                   new Protect(3, "Eidolon Segment G", 70, protectionRange: 2),
                   new TimedTransition(1000, "go3")
                    ),
               new State("go3",
                    new SetAltTexture(0),
                   new Protect(3, "Eidolon Segment G", 70, protectionRange: 2),
                   new Shoot(50, 1, projectileIndex: 1, predictive: 0.5, coolDown: 1500, coolDownOffset: 500)
                    ),
               new State("2plan",
                   new Protect(3, "Eidolon Wyrm", protectionRange: 2)
                        ),
               new State("invis",
                   new Protect(10, "Eidolon Segment G", 100, protectionRange: 2),
                   new SetAltTexture(1)
                    ),
               new State("d",
                   new Suicide()
                    )
                 )
            )
         .Init("Eidolon Segment I",
             new State(
                 new ConditionalEffect(ConditionEffectIndex.Invincible),
                 new ScaleHP(0.05),
               new State("go",
                   new SetAltTexture(0),
                   new Protect(3, "Eidolon Segment H", protectionRange: 2)
                        ),
               new State("go1",
                    new SetAltTexture(0),
                   new Protect(3, "Eidolon Segment H", protectionRange: 2),
                   new Shoot(50, 1, coolDown: 1500, coolDownOffset: 500)
                    ),
               new State("go2",
                    new SetAltTexture(0),
                   new Protect(3, "Eidolon Segment H", 70, protectionRange: 2),
                   new TimedTransition(1000, "go3")
                    ),
               new State("go3",
                    new SetAltTexture(0),
                   new Protect(3, "Eidolon Segment H", 70, protectionRange: 2),
                   new Shoot(50, 1, projectileIndex: 1, predictive: 0.5, coolDown: 1500, coolDownOffset: 500)
                    ),
               new State("2plan",
                   new Protect(3, "Eidolon Wyrm", protectionRange: 2)
                        ),
               new State("invis",
                   new Protect(10, "Eidolon Segment H", 100, protectionRange: 2),
                   new SetAltTexture(1)
                    ),
               new State("d",
                   new Suicide()
                    )
                 )
            )
        .Init("Eidolon Segment Tail",
             new State(
                 new ConditionalEffect(ConditionEffectIndex.Invincible),
                 new ScaleHP(0.05),
               new State("go",
                   new SetAltTexture(0),
                   new Protect(3, "Eidolon Segment I", protectionRange: 2)
                        ),
               new State("go1",
                   new SetAltTexture(0),
                   new Protect(3, "Eidolon Segment I", protectionRange: 2),
                   new Shoot(70, 4, 90, 2, coolDown: 2500),
                   new Shoot(70, 1, projectileIndex: 4, coolDown: 1000)
                        ),
               new State("go2",
                    new SetAltTexture(0),
                   new Protect(3, "Eidolon Segment I", 70, protectionRange: 2),
                   new TimedTransition(1000, "go3")
                    ),
               new State("go3",
                    new SetAltTexture(0),
                   new Protect(3, "Eidolon Segment I", 70, protectionRange: 2),
                   new Shoot(50, 1, projectileIndex: 3, predictive: 0.5, coolDown: 1500, coolDownOffset: 500)
                    ),
               new State("2plan",
                   new Protect(3, "Eidolon Wyrm", protectionRange: 2)
                        ),
               new State("invis",
                   new Protect(10, "Eidolon Segment I", 100, protectionRange: 2),
                   new SetAltTexture(1)
                    ),
               new State("d",
                   new Suicide()
                    )
                 )
            )
        .Init("Vault Chest",
                new State(
                    new Wander(3)

                )
            )
        .Init("FAVELA TELEPORT",
                new State("wait",
                    new PlayerWithinTransition(1, "Teleport", true),
                    new State("Teleport",
                    new TeleportPlayerTo(53, 28, 0.6),
                    new TimedTransition(100, "wait")
                    )
                )
            )
        .Init("FAVELA inhabitant",
            new State(
                new ScaleHP(1),
                new State("boom",
                    new Charge(speed: 0.8, range: 15),
                    new Wander(speed: 0.6),
                    new Shoot(20, projectileIndex: 0, count: 3, shootAngle: 25, coolDown: 2000),
                    new Shoot(20, projectileIndex: 1, count: 2, shootAngle: 35, coolDown: 1000)
                    )
                ),
            new Threshold(0.01,
                new ItemLoot("FAVELA Token", 1 / 1250f)
                )
            )
        .Init("chris the redeemed",
            new State(
                new ScaleHP(1),
                new State("Idle",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                    new PlayerWithinTransition(5, "1")
                    ),
                new State("1",
                    new Taunt("PORRA"),
                    new Flash(0xff0000, 1, 3),
                    new TimedTransition(3000, "2")
                    ),
                new State("2",
                    new RemoveConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new StayCloseToSpawn(speed: 0.6, range: 5),
                    new Wander(0.6),
                    new Shoot(15, projectileIndex: 0, count: 3, shootAngle: 6, coolDown: 1250),
                    new Shoot(15, projectileIndex: 1, count: 5, shootAngle: 9, coolDown: 2000),
                    new HpLessTransition(0.75, "3")
                    ),
                new State("3",
                    new Taunt("ah vtnc"),
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                    new TossObject("FAVELA inhabitant", 5, 0, coolDown: 999999),
                    new TossObject("FAVELA inhabitant", 5, 90, coolDown: 999999),
                    new TossObject("FAVELA inhabitant", 5, 180, coolDown: 999999),
                    new TossObject("FAVELA inhabitant", 5, 270, coolDown: 999999),
                    new TimedTransition(2000, "4")
                    ),
                new State("4",
                    new Shoot(15, projectileIndex: 0, count: 4, shootAngle: 90, coolDown: 3000),
                    new Shoot(15, projectileIndex: 1, count: 9, shootAngle: 45, fixedAngle: 0, coolDown: 2000, predictive: 0.25),
                    new EntitiesNotExistsTransition(25, "5", "FAVELA inhabitant")
                    ),
                new State("5",
                    new Flash(0xff0000, 1, 6),
                    new ChangeSize(13, 175),
                    new TimedTransition(3000, "6")
                    ),
                new State("6",
                    new Wander(0.75),
                    new Charge(speed: 0.6, range: 7),
                    new RemoveConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new Shoot(15, projectileIndex: 0, count: 9, shootAngle: 45, fixedAngle: 0, coolDown: 2000),
                    new Shoot(15, projectileIndex: 1, count: 5, shootAngle: 12, coolDown: 3000),
                    new HpLessTransition(0.5, "7")
                    ),
                new State("7",
                    new Flash(0xff0000, 0.5, 3),
                    new Taunt(":>"),
                    new TimedTransition(1000, "8")
                    ),
                new State("8",
                    new Wander(0.75),
                    new Charge(speed: 0.6, range: 7),
                    new Shoot(15, projectileIndex: 0, count: 3, shootAngle: 5, coolDown: 1000),
                    new Shoot(15, projectileIndex: 1, count: 12, shootAngle: 30, coolDown: 3000),
                    new HpLessTransition(0.25, "9")
                    ),
                new State("9",
                    new Flash(0xff0000, 0.5, 3),
                    new Taunt("pau no lombo sem refresco"),
                    new TimedTransition(1000, "10")
                    ),
                new State("10",
                    new Follow(0.75),
                    new Shoot(15, projectileIndex: 1, count: 3, shootAngle: 5, coolDown: 1000),
                    new Shoot(15, projectileIndex: 0, count: 12, shootAngle: 30, coolDown: 3000),
                    new HpLessTransition(0.1, "deaths")
                    ),
                 new State("deaths",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new Flash(0xff0000, 0.5, 3),
                    new Taunt("</3"),
                    new TimedTransition(1000, "deaths2")
                    ),
                 new State("deaths2",
                    new Shoot(15, projectileIndex: 1, count: 12, shootAngle: 30, coolDown: 300000),
                    new Suicide()
                    )
                ),
            new Threshold(0.01,
                                new ItemLoot("ASGARD Token", 1 / 175f),
                                new ItemLoot("FAVELA Token", 1 / 250f),
                                new ItemLoot("Deep Sea Token", 1 / 300f),
                                new ItemLoot("A Fallen Light (Token)", 1 / 500f),
                                new ItemLoot("Spaceship Token", 1 / 175f),
                                new ItemLoot("bling bling bling", 1 / 600f),
                                new ItemLoot("gun LEGENADRY", 1 / 600f),
                                new ItemLoot("the blood of chris", 1 / 600f),
                                new ItemLoot("golden rod of BRAZIL", 1 / 350f),
                                new ItemLoot("book of brazil i think", 1 / 1000f),
                                new ItemLoot("brazi lpride seals", 1 / 600f),
                                new ItemLoot("the rod", 1 / 2000f),
                                new ItemLoot("vest BULLETPROOF", 1 / 350f),
                                new ItemLoot("Dreadcull T9 Upgrade", 1 / 1000f)
            )
            )
        .Init("annihilator of galaxies",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                new State("waiting",
                    new PlayerWithinTransition(12, "begin")
                    ),
                new State("begin",
                    new ChangeSize(10, 200),
                    new Taunt("you are pathetic."),
                    new Flash(0xff0000, 0.5, 6),
                    new TimedTransition(3000, "begin2")
                    ),
                new State("begin2",
                    new ChangeSize(10, 200),
                    new Taunt("everything you know will cease to exist..."),
                    new Flash(0xff0000, 0.5, 6),
                    new TimedTransition(3000, "spray")
                    ),
                new State("spray",
                    new Charge(5, 100, 2000),
                    new Follow(2, 100, 0),
                    new Shoot(40, 6, projectileIndex:0, shootAngle: 10, coolDown: 500),
                    new Shoot(40, 6, projectileIndex: 1, shootAngle: 10, fixedAngle: 150, coolDown: 300, rotateAngle: 20),
                    new Shoot(40, 6, projectileIndex: 2, shootAngle: 10, fixedAngle: 240, coolDown: 300, rotateAngle: -10),
                    new Shoot(40, 6, projectileIndex: 0, shootAngle: 10, fixedAngle: 330, coolDown: 300, rotateAngle: 10),
                    new TimedTransition(10000, "rage")
                    ),
                new State("rage",
                    new Suicide()
                    )
                )
            )

        #region dreadcull 
        .Init("Catacombs Lich",
                new State(
                    new FamiliarFollow(),
                    new TalismanAttack2(1000, ConditionEffectIndex.Slowed, 2000)
                )
            )
        .Init("Catacombs Lich T1",
                new State(
                    new FamiliarFollow(),
                    new TalismanAttack2(2000, ConditionEffectIndex.Slowed, 2000)
                )
            )
        .Init("Catacombs Lich T2",
                new State(
                    new FamiliarFollow(),
                    new TalismanAttack2(4000, ConditionEffectIndex.Slowed, 2000)
                )
            )
        .Init("Catacombs Lich T3",
                new State(
                    new FamiliarFollow(),
                    new TalismanAttack2(10000, ConditionEffectIndex.Slowed, 2000)
                )
            )
        .Init("Catacombs Lich T4",
                new State(
                    new FamiliarFollow(),
                    new TalismanAttack2(25000, ConditionEffectIndex.Slowed, 2000)
                )
            )
        .Init("Catacombs Lich T5",
                new State(
                    new FamiliarFollow(),
                    new TalismanAttack2(100000, ConditionEffectIndex.Slowed, 2000)
                )
            )
        .Init("Catacombs Lich T6",
                new State(
                    new FamiliarFollow(),
                    new TalismanAttack2(200000, ConditionEffectIndex.Slowed, 2000)
                )
            )
        .Init("Catacombs Lich T7",
                new State(
                    new FamiliarFollow(),
                    new TalismanAttack2(500000, ConditionEffectIndex.Slowed, 2000)
                )
            )
        .Init("Catacombs Lich T8",
                new State(
                    new FamiliarFollow(),
                    new TalismanAttack2(1000000, ConditionEffectIndex.Slowed, 2000)
                )
            )
        .Init("Catacombs Lich T9",
                new State(
                    new FamiliarFollow(),
                    new TalismanAttack2(5000000, ConditionEffectIndex.Slowed, 200000)
                )
            )
        #endregion dreadcull
        #region Hermit God

            .Init("Hermit God",
                new State(
                    new ScaleHP(0.5),
                    new DropPortalOnDeath("Ocean Trench Portal", 1, null, 5, 5),
                    new CopyDamageOnDeath("Hermit God Drop"),
                    //new DropPortalOnDeath("Ocean Trench Portal", 100, XAdjustment: 5, YAdjustment: 5),
                    new State("invis",
                        new SetAltTexture(3),
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new InvisiToss("Hermit Minion", 9, 0, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 45, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 90, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 135, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 180, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 225, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 270, 90000001, coolDownOffset: 0),
                          new InvisiToss("Hermit Minion", 9, 315, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 15, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 30, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 90, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 120, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 150, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 180, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 210, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 240, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 50, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 100, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 150, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 200, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 250, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 300, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit God Tentacle", 5, 45, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit God Tentacle", 5, 90, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit God Tentacle", 5, 135, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit God Tentacle", 5, 180, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit God Tentacle", 5, 225, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit God Tentacle", 5, 270, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit God Tentacle", 5, 315, 90000001, coolDownOffset: 0),
                        new TimedTransition(1000, "check")
                        ),
                    new State("check",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new EntityNotExistsTransition("Hermit God Tentacle", 20, "active")
                        ),
                    new State("active",
                        new SetAltTexture(2),
                        new TimedTransition(500, "active2")
                        ),
                    new State("active2",
                        new SetAltTexture(0),
                        new Shoot(25, 3, 10, 0, coolDown: 400),
                        new Wander(.2),
                        new TossObject("Whirlpool", 6, 0, 90000001, 100),
                        new TossObject("Whirlpool", 6, 45, 90000001, 100),
                        new TossObject("Whirlpool", 6, 90, 90000001, 100),
                        new TossObject("Whirlpool", 6, 135, 90000001, 100),
                        new TossObject("Whirlpool", 6, 180, 90000001, 100),
                        new TossObject("Whirlpool", 6, 225, 90000001, 100),
                        new TossObject("Whirlpool", 6, 270, 90000001, 100),
                        new TossObject("Whirlpool", 6, 315, 90000001, 100),
                        new TimedTransition(10000, "rage")
                        ),
                    new State("rage",
                        new SetAltTexture(4),
                        new Order(20, "Whirlpool", "despawn"),
                        new Flash(0xfFF0000, .8, 9000001),
                        new Shoot(25, 8, projectileIndex: 1, coolDown: 2000),
                        new Shoot(25, 20, projectileIndex: 2, coolDown: 3000, coolDownOffset: 5000),
                        new TimedTransition(17000, "invis")
                        )
                    )
            )
            .Init("Whirlpool",
                new State(
                    new ScaleHP(0.2),
                    new State("active",
                        new Shoot(25, 8, projectileIndex: 0, coolDown: 1000),
                        new Orbit(.5, 4, target: "Hermit God", radiusVariance: 0),
                        new EntityNotExistsTransition("Hermit God", 50, "despawn")
                        ),
                    new State("despawn",
                        new Suicide()
                        )
                    )
            )
            .Init("Hermit God Tentacle",
                new State(
                    new ScaleHP(0.2),
                    new Prioritize(
                        new Orbit(.5, 5, target: "Hermit God", radiusVariance: 0.5),
                        new Follow(0.85, range: 1, duration: 2000, coolDown: 0)
                        ),
                    new Shoot(4, 8, projectileIndex: 0, coolDown: 1000)
                    )
            )
            .Init("Hermit Minion",
                new State(
                    new Prioritize(
                        new Wander(.5),
                        new Follow(0.85, 3, 1, 2000, 0)
                        ),
                    new Shoot(5, 1, 1, 1, coolDown: 2300),
                    new Shoot(5, 3, 1, 0, coolDown: 1000)
                    )
            )
            .Init("Hermit God Drop",
                new State(
                    new State("idle",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new EntityNotExistsTransition("Hermit God", 10, "despawn")
                        ),
                    new State("despawn",
                        new Suicide()
                        )
                    ),
                    new MostDamagers(3,
                        LootTemplates.Sor2Perc()
                    ),
                new Threshold(0.01,
                    new ItemLoot("Greater Potion of Dexterity", 1),
                    new ItemLoot("Greater Potion of Vitality", 1),
                    new ItemLoot("Sacred Essence", 0.0005),
                    new ItemLoot("Legendary Essence", 0.0005),
                    new TierLoot(12, ItemType.Armor, 0.05),
                    new TierLoot(12, ItemType.Weapon, 0.05),
                    new ItemLoot("Greater Potion of Life", 0.2),
                    new ItemLoot("Greater Potion of Mana", 0.2),
                    new ItemLoot("Helm of the Juggernaut", 0.02)

                )
            )
         .Init("Peepo",
            new State(
                new ScaleHP(0.5),
                new State("Idle",
                    new StayCloseToSpawn(0.1, 6),
                    new Wander(0.7),
                    new HpLessTransition(0.99999, "Uh oh")
                    ),
                new State("Uh oh",
                    new Flash(0x00ff00, 0.1, 50),
                    new Taunt("Feels cool man"),
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new TimedTransition(5000, "Go away")
                    ),
                new State("Go away",
                    new Shoot(10, projectileIndex: 0, count: 15, shootAngle: 24, coolDown: 1500),
                    new HpLessTransition(0.80, "Reee")
                    ),
                new State("Reee",
                    new Taunt("REEEEEE"),
                    new Prioritize(
                            new Follow(0.8, range: 7),
                            new Wander(0.5)
                            ),
                    new Shoot(20, projectileIndex: 3, count: 15, shootAngle: 24, coolDown: 1000),
                    new Shoot(15, projectileIndex: 1, count: 3, shootAngle: 15, coolDown: 400),
                    new HpLessTransition(0.65, "Kys1")
                    ),
                new State("Kys1",
                    new Flash(0x00ff00, 1, 2),
                    new Taunt("You are too weak to face me noobs! :lmao:"),
                    new ConditionalEffect(ConditionEffectIndex.Armored),
                    new TimedTransition(500, "Pause"),
                    new HpLessTransition(0.30, "Rage")
                    ),
                new State("Kys2",
                    new Flash(0x00ff00, 1, 2),
                    new Shoot(20, projectileIndex: 3, count: 15, shootAngle: 24, coolDown: 400),
                    new Shoot(15, projectileIndex: 1, count: 3, shootAngle: 15, coolDown: 500),
                    new TimedTransition(2000, "Pause"),
                    new HpLessTransition(0.30, "Rage")
                    ),
                new State("Pause",
                    new ConditionalEffect(ConditionEffectIndex.Armored),
                    new Shoot(15, projectileIndex: 2, count: 40, shootAngle: 9, coolDown: 1200),
                    new TimedTransition(1000, "Kys2"),
                    new HpLessTransition(0.30, "Rage")
                    ),
                new State("Rage",
                    new Flash(0xff0000, 5, 999999),
                    new Taunt("DIE!"),
                    new Prioritize(
                            new Follow(0.8, range: 7),
                            new Wander(0.5)
                            ),
                    new Shoot(10, projectileIndex: 0, count: 10, shootAngle: 36, coolDown: 1000),
                    new Shoot(20, projectileIndex: 3, count: 3, shootAngle: 24, coolDown: 1000),
                    new Shoot(15, projectileIndex: 1, count: 3, shootAngle: 15, coolDown: 400),
                    new HpLessTransition(0.005, "Die")
                    ),
                new State("Die",
                    new Taunt("Feels bad, man..."),
                    new Flash(0x0000ff, 0.3, 9999999)
                    )
                ),
                new MostDamagers(3,
                    LootTemplates.Sor2Perc()
                    ),
                new MostDamagers(5,
                    LootTemplates.StatPots()
                    ),
                new Threshold(0.06,
                    new ItemLoot("Potion of Speed", 1),
                    new ItemLoot("Potion of Speed", 0.5),
                    new ItemLoot("Potion of Speed", 0.5),
                    new ItemLoot("Sword of the Frog Bearer", 0.01),
                    new ItemLoot("Shield of Mucus", 0.01),
                    new ItemLoot("Sacred Essence", 0.0005),
                    new ItemLoot("Legendary Essence", 0.0005),

                    new TierLoot(10, ItemType.Weapon, 0.2),
                    new TierLoot(11, ItemType.Weapon, 0.1),
                    new TierLoot(12, ItemType.Weapon, 0.08),

                    new TierLoot(11, ItemType.Armor, 0.2),
                    new TierLoot(12, ItemType.Armor, 0.1),
                    new TierLoot(13, ItemType.Armor, 0.08),


                    new TierLoot(5, ItemType.Ring, 0.15),
                    new TierLoot(6, ItemType.Ring, 0.08)
                    )
            )
      .Init("Hellfire Lord",
          new State(
              new ScaleHP(0.5),
              new State("Idle",
                  new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                  new PlayerWithinTransition(12, "Uh oh")
                  ),
              new State("Uh oh",
                  new Taunt("Who arrives to challenge me?"),
                  new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                  new TimedTransition(2000, "Uh oh 2")
                  ),
              new State("Uh oh 2",
                  new Grenade(5, 250, 15, coolDown: 1250),
                  new HpLessTransition(0.97, "Uh oh 3")
                  ),
              new State("Uh oh 3",
                  new Taunt("I see, you seek adventure. But on the behalf of Oryx, I shall destroy you."),
                  new TossObject("Hellfire Turret", range: 9.89949, angle: 45, coolDown: 5000),
                  new TossObject("Hellfire Turret", range: 9.89949, angle: 135, coolDown: 5000),
                  new TossObject("Hellfire Turret", range: 9.89949, angle: 225, coolDown: 5000),
                  new TossObject("Hellfire Turret", range: 9.89949, angle: 315, coolDown: 5000),
                  new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                  new TimedTransition(2000, "Fire Stream")
                  ),
              new State("Fire Stream",
                  new Shoot(30, projectileIndex: 0, count: 3, coolDown: 400, shootAngle: 16),
                  new HpLessTransition(0.80, "Fire Rings")
                  ),
              new State("Fire Rings",
                  new Taunt("Be engulfed in flames!"),
                  new Shoot(30, projectileIndex: 2, count: 7, coolDown: 1000, shootAngle: 360 / 7),
                  new Shoot(30, projectileIndex: 3, count: 7, coolDown: 1000, shootAngle: 360 / 7),
                  new HpLessTransition(0.60, "Fire Tentacles")
                  ),
              new State("Fire Tentacles",
                  new Taunt("Turrets, obliterate them!"),
                  new Order(100, "Hellfire Turret", "Shooting2"),
                  new Shoot(50, projectileIndex: 1, count: 6, coolDown: 400 * 20, coolDownOffset: 0, fixedAngle: 0, shootAngle: 60),
                  new Shoot(50, projectileIndex: 1, count: 6, coolDown: 400 * 20, coolDownOffset: 200 * 1, fixedAngle: 5 * 1, shootAngle: 60),
                  new Shoot(50, projectileIndex: 1, count: 6, coolDown: 400 * 20, coolDownOffset: 200 * 2, fixedAngle: 5 * 2, shootAngle: 60),
                  new Shoot(50, projectileIndex: 1, count: 6, coolDown: 400 * 20, coolDownOffset: 200 * 3, fixedAngle: 5 * 3, shootAngle: 60),
                  new Shoot(50, projectileIndex: 1, count: 6, coolDown: 400 * 20, coolDownOffset: 200 * 4, fixedAngle: 5 * 4, shootAngle: 60),
                  new Shoot(50, projectileIndex: 1, count: 6, coolDown: 400 * 20, coolDownOffset: 200 * 5, fixedAngle: 5 * 5, shootAngle: 60),
                  new Shoot(50, projectileIndex: 1, count: 6, coolDown: 400 * 20, coolDownOffset: 200 * 6, fixedAngle: 5 * 6, shootAngle: 60),
                  new Shoot(50, projectileIndex: 1, count: 6, coolDown: 400 * 20, coolDownOffset: 200 * 7, fixedAngle: 5 * 7, shootAngle: 60),
                  new Shoot(50, projectileIndex: 1, count: 6, coolDown: 400 * 20, coolDownOffset: 200 * 8, fixedAngle: 5 * 8, shootAngle: 60),
                  new Shoot(50, projectileIndex: 1, count: 6, coolDown: 400 * 20, coolDownOffset: 200 * 9, fixedAngle: 5 * 9, shootAngle: 60),
                  new Shoot(50, projectileIndex: 1, count: 6, coolDown: 400 * 20, coolDownOffset: 200 * 10, fixedAngle: 5 * 10, shootAngle: 60),
                  new Shoot(50, projectileIndex: 1, count: 6, coolDown: 400 * 20, coolDownOffset: 200 * 11, fixedAngle: 5 * 11, shootAngle: 60),
                  new Shoot(50, projectileIndex: 1, count: 6, coolDown: 400 * 20, coolDownOffset: 200 * 12, fixedAngle: 5 * 12, shootAngle: 60),
                  new Shoot(50, projectileIndex: 1, count: 6, coolDown: 400 * 20, coolDownOffset: 200 * 13, fixedAngle: 5 * 13, shootAngle: 60),
                  new Shoot(50, projectileIndex: 1, count: 6, coolDown: 400 * 20, coolDownOffset: 200 * 14, fixedAngle: 5 * 14, shootAngle: 60),
                  new Shoot(50, projectileIndex: 1, count: 6, coolDown: 400 * 20, coolDownOffset: 200 * 15, fixedAngle: 5 * 15, shootAngle: 60),
                  new Shoot(50, projectileIndex: 1, count: 6, coolDown: 400 * 20, coolDownOffset: 200 * 16, fixedAngle: 5 * 16, shootAngle: 60),
                  new Shoot(50, projectileIndex: 1, count: 6, coolDown: 400 * 20, coolDownOffset: 200 * 17, fixedAngle: 5 * 17, shootAngle: 60),
                  new Shoot(50, projectileIndex: 1, count: 6, coolDown: 400 * 20, coolDownOffset: 200 * 18, fixedAngle: 5 * 18, shootAngle: 60),
                  new HpLessTransition(0.40, "Fire Waves")
                  ),
              new State("Fire Waves",
                  new Taunt("Gah! I have had enough!"),
                  new Order(100, "Hellfire Turret", "Shooting3"),
                  new Shoot(30, projectileIndex: 1, count: 1, coolDown: 1000),
                  new Shoot(50, projectileIndex: 4, count: 5, coolDown: 4000, coolDownOffset: 0, fixedAngle: 0, shootAngle: 9),
                  new Shoot(50, projectileIndex: 4, count: 7, coolDown: 4000, coolDownOffset: 400, fixedAngle: 0, shootAngle: 9),
                  new Shoot(50, projectileIndex: 4, count: 9, coolDown: 4000, coolDownOffset: 800, fixedAngle: 0, shootAngle: 9),
                  new Shoot(50, projectileIndex: 4, count: 9, coolDown: 4000, coolDownOffset: 1200, fixedAngle: 0, shootAngle: 9),
                  new Shoot(50, projectileIndex: 4, count: 9, coolDown: 4000, coolDownOffset: 1600, fixedAngle: 0, shootAngle: 9),
                  new Shoot(50, projectileIndex: 4, count: 5, coolDown: 4000, coolDownOffset: 0, fixedAngle: 180, shootAngle: 9),
                  new Shoot(50, projectileIndex: 4, count: 7, coolDown: 4000, coolDownOffset: 400, fixedAngle: 180, shootAngle: 9),
                  new Shoot(50, projectileIndex: 4, count: 9, coolDown: 4000, coolDownOffset: 800, fixedAngle: 180, shootAngle: 9),
                  new Shoot(50, projectileIndex: 4, count: 9, coolDown: 4000, coolDownOffset: 1200, fixedAngle: 180, shootAngle: 9),
                  new Shoot(50, projectileIndex: 4, count: 9, coolDown: 4000, coolDownOffset: 1600, fixedAngle: 180, shootAngle: 9),
                  new Shoot(50, projectileIndex: 4, count: 5, coolDown: 4000, coolDownOffset: 2000, fixedAngle: 90, shootAngle: 9),
                  new Shoot(50, projectileIndex: 4, count: 7, coolDown: 4000, coolDownOffset: 2400, fixedAngle: 90, shootAngle: 9),
                  new Shoot(50, projectileIndex: 4, count: 9, coolDown: 4000, coolDownOffset: 2800, fixedAngle: 90, shootAngle: 9),
                  new Shoot(50, projectileIndex: 4, count: 9, coolDown: 4000, coolDownOffset: 3200, fixedAngle: 90, shootAngle: 9),
                  new Shoot(50, projectileIndex: 4, count: 9, coolDown: 4000, coolDownOffset: 3600, fixedAngle: 90, shootAngle: 9),
                  new Shoot(50, projectileIndex: 4, count: 5, coolDown: 4000, coolDownOffset: 2000, fixedAngle: 270, shootAngle: 9),
                  new Shoot(50, projectileIndex: 4, count: 7, coolDown: 4000, coolDownOffset: 2400, fixedAngle: 270, shootAngle: 9),
                  new Shoot(50, projectileIndex: 4, count: 9, coolDown: 4000, coolDownOffset: 2800, fixedAngle: 270, shootAngle: 9),
                  new Shoot(50, projectileIndex: 4, count: 9, coolDown: 4000, coolDownOffset: 3200, fixedAngle: 270, shootAngle: 9),
                  new Shoot(50, projectileIndex: 4, count: 9, coolDown: 4000, coolDownOffset: 3600, fixedAngle: 270, shootAngle: 9),
                  new TossObject("Hellfire Bomb", range: 10, angle: 45, coolDown: 4000, coolDownOffset: 0),
                  new TossObject("Hellfire Bomb", range: 10, angle: 135, coolDown: 4000, coolDownOffset: 0),
                  new TossObject("Hellfire Bomb", range: 10, angle: 225, coolDown: 4000, coolDownOffset: 0),
                  new TossObject("Hellfire Bomb", range: 10, angle: 315, coolDown: 4000, coolDownOffset: 0),
                  new TossObject("Hellfire Bomb", range: 10, angle: 0, coolDown: 4000, coolDownOffset: 2000),
                  new TossObject("Hellfire Bomb", range: 10, angle: 90, coolDown: 4000, coolDownOffset: 2000),
                  new TossObject("Hellfire Bomb", range: 10, angle: 180, coolDown: 4000, coolDownOffset: 2000),
                  new TossObject("Hellfire Bomb", range: 10, angle: 270, coolDown: 4000, coolDownOffset: 2000),
                  new HpLessTransition(0.20, "Pre Rage")
                  ),
              new State("Pre Rage",
                  new Taunt("..."),
                  new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                  new TimedTransition(6000, "Rage")
                  ),
              new State("Rage",
                  new Taunt("YOU WILL DIE!!!"),
                  new Flash(0xff0000, 2, 999999),
                  new TossObject("Hellfire Bomb", range: 9, angle: 45, coolDown: 4000, coolDownOffset: 0),
                  new TossObject("Hellfire Bomb", range: 9, angle: 135, coolDown: 4000, coolDownOffset: 0),
                  new TossObject("Hellfire Bomb", range: 9, angle: 225, coolDown: 4000, coolDownOffset: 0),
                  new TossObject("Hellfire Bomb", range: 9, angle: 315, coolDown: 4000, coolDownOffset: 0),
                  new TossObject("Hellfire Bomb", range: 9, angle: 0, coolDown: 4000, coolDownOffset: 2000),
                  new TossObject("Hellfire Bomb", range: 9, angle: 90, coolDown: 4000, coolDownOffset: 2000),
                  new TossObject("Hellfire Bomb", range: 9, angle: 180, coolDown: 4000, coolDownOffset: 2000),
                  new TossObject("Hellfire Bomb", range: 9, angle: 270, coolDown: 4000, coolDownOffset: 2000),
                  new TossObject("Hellfire Bomb", range: 2, angle: 45, coolDown: 4000, coolDownOffset: 0),
                  new TossObject("Hellfire Bomb", range: 2, angle: 135, coolDown: 4000, coolDownOffset: 0),
                  new TossObject("Hellfire Bomb", range: 2, angle: 225, coolDown: 4000, coolDownOffset: 0),
                  new TossObject("Hellfire Bomb", range: 2, angle: 315, coolDown: 4000, coolDownOffset: 0),
                  new TossObject("Hellfire Bomb", range: 2, angle: 0, coolDown: 4000, coolDownOffset: 2000),
                  new TossObject("Hellfire Bomb", range: 2, angle: 90, coolDown: 4000, coolDownOffset: 2000),
                  new TossObject("Hellfire Bomb", range: 2, angle: 180, coolDown: 4000, coolDownOffset: 2000),
                  new TossObject("Hellfire Bomb", range: 2, angle: 270, coolDown: 4000, coolDownOffset: 2000),
                  new Shoot(30, projectileIndex: 0, count: 3, coolDown: 400, shootAngle: 16),
                  new Shoot(30, projectileIndex: 4, count: 1, coolDown: 400),
                  new HpLessTransition(0.05, "Ded")
                  ),
              new State("Ded",
                  new Taunt("Oryx, I have failed you..."),
                  new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                  new Flash(0x000000, 4, 1),
                  new TimedTransition(2000, "Oof")
                  ),
              new State("Oof",
                  new Shoot(30, projectileIndex: 2, count: 36, fixedAngle: 0, shootAngle: 10),
                  new Shoot(30, projectileIndex: 3, count: 36, fixedAngle: 5, shootAngle: 10),
                  new Suicide()
                  )
              ),
                new MostDamagers(3,
                    LootTemplates.Sor2Perc()
                    ),
                new MostDamagers(5,
                    LootTemplates.StatPots()
                    ),
              new Threshold(0.06,
                  new ItemLoot("Greater Potion of Life", 0.5),
                  new ItemLoot("Greater Potion of Mana", 0.5),
                  new ItemLoot("Greater Potion of Vitality", 1),
                  new ItemLoot("Greater Potion of Defense", 1),
                  new ItemLoot("Black Flame Staff", 0.01),
                  new ItemLoot("Hellfire Orb", 0.01),
                  new ItemLoot("Sacred Essence", 0.0005),
                  new ItemLoot("Legendary Essence", 0.0005),

                  new TierLoot(10, ItemType.Weapon, 0.3),
                  new TierLoot(11, ItemType.Weapon, 0.2),
                  new TierLoot(12, ItemType.Weapon, 0.1),

                  new TierLoot(11, ItemType.Armor, 0.3),
                  new TierLoot(12, ItemType.Armor, 0.2),
                  new TierLoot(13, ItemType.Armor, 0.1),

                  new TierLoot(5, ItemType.Ring, 0.15),
                  new TierLoot(6, ItemType.Ring, 0.08)
              )
          )
          .Init("Hellfire Turret",
          new State(
              new ConditionalEffect(ConditionEffectIndex.Invulnerable),
              new EntityNotExistsTransition("Hellfire Lord", 50, "Kaboom"),
              new State("Shooting1",
                  new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                  new Shoot(30, projectileIndex: 0, count: 5, coolDown: 3000, coolDownOffset: 0, shootAngle: 72),
                  new Shoot(30, projectileIndex: 1, count: 5, coolDown: 3000, coolDownOffset: 1500, shootAngle: 72)
                  ),
              new State("Shooting2",
                  new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                  new Shoot(30, projectileIndex: 1, count: 8, coolDown: 2000, coolDownOffset: 0, fixedAngle: 0, shootAngle: 45),
                  new Shoot(30, projectileIndex: 1, count: 8, coolDown: 2000, coolDownOffset: 1000, fixedAngle: 22.5, shootAngle: 45)
                  ),
              new State("Shooting3",
                  new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                  new Shoot(30, projectileIndex: 0, count: 1, coolDown: 1000)
                  ),
              new State("Kaboom",
                  new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                  new Shoot(30, projectileIndex: 1, count: 8, fixedAngle: 0, shootAngle: 45),
                  new Shoot(30, projectileIndex: 0, count: 8, fixedAngle: 22.5, shootAngle: 45),
                  new Decay(0)
                  )
              )
          )
          .Init("Hellfire Bomb",
          new State(
              new State("Idle",
                  new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                  new Flash(0xff0000, 2, 1),
                  new ChangeSize(100, 80),
                  new TimedTransition(800, "Kaboom2")
                  ),
              new State("Kaboom2",
                  new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                  new Shoot(30, projectileIndex: 0, count: 8, fixedAngle: 0, shootAngle: 45),
                  new Decay(0)
                  )
              )
            )

         .Init("Sanic",
          new State(
              new ScaleHP(0.5),
              new State("Idle",
                  new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                  new PlayerWithinTransition(5, "1")
                  ),
              new State("1",
                  new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                  new Taunt("No one can match my speed, haha!"),
                  new TimedTransition(4000, "2")
                  ),
              new State("2",
                  new Follow(speed: 2.5, range: 10),
                  new Shoot(50, count: 8, shootAngle: 45, projectileIndex: 0, predictive: 0.2, coolDown: 400),
                  new HpLessTransition(0.2, "YoUrEtOoSlOw")
                  ),
              new State("YoUrEtOoSlOw",
                  new Taunt("YoU'rE tOo SlOw!"),
                  new StayBack(speed: 3, distance: 30),
                  new TimedTransition(8000, "rest")
                  ),
              new State("rest",
                  new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                  new Shoot(50, count: 8, shootAngle: 45, projectileIndex: 0, predictive: 0.2, coolDown: 400),
                  new TimedTransition(5000, "YoUrEtOoSlOw")
                  )
              ),
                new MostDamagers(3,
                    LootTemplates.Sor2Perc()
                    ),
                new MostDamagers(5,
                    LootTemplates.StatPots()
                    ),
              new Threshold(0.06,
                  new ItemLoot("Sacred Essence", 0.0005),
                  new ItemLoot("Legendary Essence", 0.0005),
                  new ItemLoot("Greater Potion of Life", 0.5),
                  new ItemLoot("Greater Potion of Mana", 0.5),
                  new ItemLoot("Greater Potion of Vitality", 1),
                  new ItemLoot("Greater Potion of Defense", 1),
                  new ItemLoot("Sanic Helm", 0.002),

                  new TierLoot(10, ItemType.Weapon, 0.3),
                  new TierLoot(11, ItemType.Weapon, 0.2),
                  new TierLoot(12, ItemType.Weapon, 0.1),

                  new TierLoot(11, ItemType.Armor, 0.3),
                  new TierLoot(12, ItemType.Armor, 0.2),
                  new TierLoot(13, ItemType.Armor, 0.1),


                  new TierLoot(5, ItemType.Ring, 0.15),
                  new TierLoot(6, ItemType.Ring, 0.08)
              )
            )

        #endregion
        #region Transcendent Burrower

        .Init("Transcendent Burrower",
            new State(
                new ScaleHP(0.5),
                new State("idle",
                    new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                    new PlayerWithinTransition(5, "1")
                    ),
                new State("1",
                    new Flash(0xff0000, 1, 10),
                    new TimedTransition(5000, "2")
                    ),
                new State("2",
                    new RemoveConditionalEffect(ConditionEffectIndex.Invincible),
                    new Wander(0.8),
                    new Shoot(20, count: 5, shootAngle: 20, projectileIndex: 1, coolDown: 500),
                    new Shoot(20, projectileIndex: 4, count: 3, shootAngle: 25, coolDown: 2000),

                    new HpLessTransition(0.75, "prep3")
                    ),
                new State("prep3",
                    new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                    new Flash(0xff0000, 1, 10),
                    new Taunt("HISS*"),
                    new TimedTransition(3000, "a1")
                    ),
                new State("a1",
                    new SetAltTexture(2),
                    new TimedTransition(300, "a2")
                    ),
                new State("a2",
                    new SetAltTexture(3),
                    new TimedTransition(300, "a3")
                    ),
                new State("a3",
                    new SetAltTexture(4),
                    new TimedTransition(300, "a4")
                    ),
                new State("a4",
                    new SetAltTexture(5),
                    new TimedTransition(300, "a5")
                    ),
                new State("a5",
                    new SetAltTexture(6),
                    new TimedTransition(300, "a6")
                    ),
                new State("a6",
                    new SetAltTexture(7),
                    new TimedTransition(300, "a7")
                    ),
                new State("a7",
                    new SetAltTexture(8),
                    new TimedTransition(300, "a8")
                    ),
                new State("a8",
                    new SetAltTexture(9),
                    new TimedTransition(300, "3")
                    ),
                new State("3",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new SetAltTexture(1),
                    new Charge(speed: 1, range: 15),
                    new Wander(speed: 0.8),
                    new PlayerWithinTransition(2, "4")
                    ),
                new State("4",
                    new SetAltTexture(0),
                    new RemoveConditionalEffect(ConditionEffectIndex.Invincible),
                    new Shoot(20, count: 20, shootAngle: 18, projectileIndex: 0, coolDown: 500),
                    new Shoot(20, count: 8, shootAngle: 45, projectileIndex: 5, coolDown: 1800),
                    new HpLessTransition(0.4, "6prep"),
                    new TimedTransition(3600, "5")
                    ),
                new State("5",
                    new Wander(0.8),
                    new Shoot(20, count: 5, shootAngle: 20, projectileIndex: 1, coolDown: 500),
                    new Shoot(20, projectileIndex: 4, count: 3, shootAngle: 25, coolDown: 2000),

                    new HpLessTransition(0.4, "6prep"),
                    new TimedTransition(3000, "3")
                    ),
                new State("6prep",
                    new ChangeSize(13, 250),
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new Flash(0xff0000, 1, 10),
                    new Taunt("HISS*"),
                    new TimedTransition(3000, "6")
                    ),
                new State("6",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new SetAltTexture(1),
                    new Charge(speed: 1, range: 15),
                    new Wander(speed: 0.8),
                    new PlayerWithinTransition(2, "7")
                    ),
                new State("7",
                    new SetAltTexture(0),

                    new Grenade(radius: 3.5, range: 0, fixedAngle: 0, color: 0x01FCF6, coolDown: 2000, damage: 150, effect: ConditionEffectIndex.ArmorBroken, effectDuration: 2000),
                    new Shoot(20, count: 7, shootAngle: 14, projectileIndex: 3, coolDown: 1500),
                    new Shoot(20, count: 4, shootAngle: 90, fixedAngle: 0, projectileIndex: 6, coolDown: 3000),
                    new Shoot(20, count: 1, projectileIndex: 7, coolDown: 5000),
                    new TimedTransition(10000, "6")
                )
            )
          )
        .Init("Sandworms",
            new State(
                new ScaleHP(0.2),
                new State("boom",
                    new Charge(speed: 0.8, range: 15),
                    new Wander(speed: 0.6),
                    new Shoot(20, projectileIndex: 0, count: 3, shootAngle: 25, coolDown: 2000)
                    )
                )
            )
        #endregion
        #region Blaze-Born
        .Init("Blaze-Born",
            new State(
                new ScaleHP(0.5),
                new State("Idle",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                    new PlayerWithinTransition(5, "1")
                    ),
                new State("1",
                    new Taunt("You made a mistake coming here. Now, you will perish."),
                    new Flash(0xff0000, 1, 3),
                    new TimedTransition(3000, "2")
                    ),
                new State("2",
                    new RemoveConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new StayCloseToSpawn(speed: 0.6, range: 5),
                    new Wander(0.6),
                    new Shoot(15, projectileIndex: 0, count: 3, shootAngle: 6, coolDown: 1250),
                    new Shoot(15, projectileIndex: 1, count: 5, shootAngle: 9, coolDown: 2000),
                    new Shoot(15, projectileIndex: 2, count: 3, shootAngle: 30, coolDown: 1000),
                    new HpLessTransition(0.75, "3")
                    ),
                new State("3",
                    new Taunt("Minions, protect me!"),
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
 new TimedTransition(2000, "4")
                    ),
                new State("4",
                    new Shoot(15, projectileIndex: 3, count: 4, shootAngle: 90, coolDown: 3000),
                    new Shoot(15, projectileIndex: 4, count: 9, shootAngle: 45, fixedAngle: 0, coolDown: 2000, predictive: 0.25),
                    new EntitiesNotExistsTransition(15, "5", "Blazed Warrior")
                    ),
                new State("5",
                    new Flash(0xff0000, 1, 6),
                    new ChangeSize(13, 250),
                    new TimedTransition(3000, "6")
                    ),
                new State("6",
                    new Wander(0.75),
                    new Charge(speed: 0.6, range: 7),
                    new RemoveConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new Shoot(15, projectileIndex: 4, count: 9, shootAngle: 45, fixedAngle: 0, coolDown: 2000),
                    new Shoot(15, projectileIndex: 5, count: 5, shootAngle: 12, coolDown: 3000),
                    new TimedTransition(3500, "7")
                    ),
                new State("7",
                    new Flash(0xff0000, 0.5, 3),
                    new Taunt("Burn!"),
                    new TimedTransition(1000, "8")
                    ),
                new State("8",
                    new Shoot(15, projectileIndex: 6, count: 3, shootAngle: 5, coolDown: 5000),
                    new TimedTransition(2000, "6")
                    )
                )
            )
           .Init("Blaze-Born Anchor",
            new State(
                new State("1",
                    new ConditionalEffect(ConditionEffectIndex.Invincible, true)
                    )
                )
            )
           .Init("Blazed Warrior",
            new State(
                new ScaleHP(0.2),
                new State("boom",
                    new Orbit(speed: 0.6, radius: 5, target: "Blaze-Born Anchor", speedVariance: 0.2),
                    new Shoot(20, projectileIndex: 0, count: 3, shootAngle: 6, coolDown: 1000, predictive: 0.5)
                    )
                )
            )
        #endregion
        #region fallen rework concept
        /*
     .Init("Gatekeeper Chronus",
              new State(
                new State("1",
                  new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                  new PlayerWithinTransition(8, "Dialogue1")
                 ),
                new State("Dialogue1",
                  new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                  new Taunt("Welcome warriors... It's been an awfully long time since I've seen a group like you."),
                  new TimedTransition(3000, "Dialogue2")
                 ),
                new State("Dialogue2",
                  new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                  new Taunt("While Merganus has enslaved me and my kind, I cannot let you past. I would doom the others."),
                  new TimedTransition(3000, "Dialogue3")
                 ),
                new State("Dialogue3",
                  new Flash(0xff0000, 0.5, 3),
                  new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                  new Taunt("Prepare, as I will not let the others suffer at the hands of you brutes, no matter how noble you think you are."),
                  new TimedTransition(3000, "P1A")
                 ),
                new State("P1A",
                 new ConditionalEffect(ConditionEffectIndex.Invulnerable, false, 3000),
                 new InvisiToss("Chronus Orbiter", range: 5.5, angle: 0, coolDown: 999999),
                 new InvisiToss("Chronus Orbiter", range: 5.5, angle: 90, coolDown: 999999),
                 new InvisiToss("Chronus Orbiter", range: 5.5, angle: 180, coolDown: 999999),
                 new InvisiToss("Chronus Orbiter", range: 5.5, angle: 270, coolDown: 999999),
                 new Shoot(15, projectileIndex: 0, count: 8, shootAngle: 45, fixedAngle: 0, coolDown: 2000),
                 new Shoot(15, projectileIndex: 1, count: 3, shootAngle: 8, coolDown: 1000),
                 new HpLessTransition(0.80, "P1BTaunt")
                 ),
                new State("P1BTaunt",
                  new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                  new Taunt("Seems like those toys aren't working... I'll have to face you head on."),
                  new TimedTransition(3000, "P1B")
                 ),
                new State("P1B",
                 new ConditionalEffect(ConditionEffectIndex.Invulnerable, false, 3000),
                 new Wander(0.3),
                 new Charge(0.4, range: 10),
                 new Shoot(10, projectileIndex: 2, shootAngle: 90, count: 20, coolDown: 400),
                 new Shoot(10, projectileIndex: 3, shootAngle: 10, count: 3, coolDown: 1200),
                 new HpLessTransition(0.66, "3")
                 )

             )
         )
         */
        #endregion fallen rework concept
        #region Grotesque Serpent
        .Init("Grotesque Serpent",
            new State(
                new ScaleHP(0.5),
                new State("idle",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                    new PlayerWithinTransition(5, "1")
                    ),
                new State("1",
                    new Taunt("Hiss!"),
                    new Flash(0xff0000, 2, 3),
                    new TimedTransition(3000, "2")
                    ),
                new State("2",
                    new RemoveConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new Wander(0.3),
                    new Charge(0.4, range: 10),
                    new Shoot(10, projectileIndex: 0, shootAngle: 10, count: 3, coolDown: 500),
                    new Shoot(10, projectileIndex: 1, shootAngle: 10, count: 10, coolDown: 3000),
                    new HpLessTransition(0.66, "3")
                    ),
                new State("3",
                    new Shoot(10, projectileIndex: 2, shootAngle: 45, fixedAngle: 0, count: 8, coolDown: 5000),
                    new Shoot(10, projectileIndex: 3, shootAngle: 10, count: 4, coolDown: 1250),
                    new HpLessTransition(0.33, "4")
                    ),
                new State("4",
                    new Taunt("AAAAAAAAAA"),
                    new ChangeSize(13, 125),
                    new Wander(0.4),
                    new Charge(0.5, range: 10),
                    new Shoot(10, projectileIndex: 1, shootAngle: 10, count: 10, coolDown: 3000),
                    new Shoot(10, projectileIndex: 4, shootAngle: 18, count: 20, coolDown: 400),
                    new Shoot(10, projectileIndex: 5, shootAngle: 5, count: 5, coolDown: 750)
                    )
                )

        #endregion
        #region Grand Totem of Deception

        #endregion
        #region Fiery Fiend
        #endregion
        #region Kinetic Occultist
        #endregion

        #endregion stuff
                    );
    }
}