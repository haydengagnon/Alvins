﻿using common.resources;
using System;
using System.Collections.Generic;
using System.Linq;
using wServer.realm.entities;
using wServer.realm.setpieces;
using wServer.realm.terrain;
using wServer.realm.worlds.logic;

namespace wServer.realm
{
    internal class Oryx
    {
        public bool Closing;

        private readonly Realm _world;
        private readonly Random _rand = new Random();
        private readonly int[] _enemyMaxCounts = new int[12];
        private readonly int[] _enemyCounts = new int[12];
        private long _prevTick;
        private int _tenSecondTick;

        private struct TauntData
        {
            public string[] Spawn;
            public string[] NumberOfEnemies;
            public string[] Final;
            public string[] Killed;
        }

        private readonly List<Tuple<string, ISetPiece>> _events = new List<Tuple<string, ISetPiece>>()
        {
            Tuple.Create("Pear", (ISetPiece) new Filisha()),
            Tuple.Create("Tong Shau Ping", (ISetPiece) new Hellfire()),
            Tuple.Create("octahedron LEGS", (ISetPiece) new Bedlam()),
            Tuple.Create("Black Bloon", (ISetPiece) new TheFallen()),
            Tuple.Create("PVZ ZOMBIE", (ISetPiece) new Cheesehead()),
            Tuple.Create("Nightmare Orange", (ISetPiece) new QueenofHearts()),
            Tuple.Create("ARCANUICA image slopy", (ISetPiece) new Sanic()),
            Tuple.Create("EL BANDITONIO rodriguez", (ISetPiece) new SkullShrine()),
            Tuple.Create("Zucky Zucc", (ISetPiece) new SkullShrine()),
            Tuple.Create("gemgemgem gem gem", (ISetPiece) new CubeGod()),
            Tuple.Create("BIG MAN ORTAR", (ISetPiece) new Pentaract()),
            Tuple.Create("rock man i forgot name", (ISetPiece) new Sphinx()),
            Tuple.Create("Lord of the Lost Lands", (ISetPiece) new LordOfTheLostLands()),
            Tuple.Create("crymbal", (ISetPiece) new Hermit()),
            Tuple.Create("PVZ Peashooter", (ISetPiece) new GhostShip()),
            Tuple.Create("MOAB", (ISetPiece) new Avatar()),
            Tuple.Create("DDT", (ISetPiece) new Yazanahar()),
            Tuple.Create("DEEZ NUTS GUY", (ISetPiece) new BloodGod()),
            Tuple.Create("Banan PHON", (ISetPiece) new Mothership()),
            Tuple.Create("chinese egghead", (ISetPiece) new Elemental()),
            Tuple.Create("talking toilet", (ISetPiece) new MadJester()),
            Tuple.Create("godes", (ISetPiece) new GarnetJade()),
            Tuple.Create("SANS", (ISetPiece) new Gargoyle()),
            Tuple.Create("Annoying Orange", (ISetPiece) new TimePiece()),
            Tuple.Create("Apple", (ISetPiece) new RockDragon()),
            Tuple.Create("Pickle Rick", (ISetPiece) new TestChicken()),
            Tuple.Create("golden freddy", (ISetPiece) new TheHorrific()),
            Tuple.Create("Marshmallow", (ISetPiece) new Tod()),
            Tuple.Create("baldi", (ISetPiece) new Gargoyle()),
            Tuple.Create("Billy Herrington", (ISetPiece) new TimePiece()),
            Tuple.Create("Herobrine", (ISetPiece) new RockDragon()),
            Tuple.Create("soulja boi", (ISetPiece) new TestChicken()),
            Tuple.Create("shoop da woop", (ISetPiece) new TheHorrific()),
            Tuple.Create("dancing triangle", (ISetPiece) new Tod()),
            Tuple.Create("finnish warrior", (ISetPiece) new Gargoyle()),
            Tuple.Create("sun baby", (ISetPiece) new TimePiece()),
            Tuple.Create("puggsy", (ISetPiece) new RockDragon()),
            Tuple.Create("Mindy", (ISetPiece) new TestChicken()),
            Tuple.Create("Vera", (ISetPiece) new TheHorrific()),
            Tuple.Create("Nadine", (ISetPiece) new Tod()),
            Tuple.Create("Interactive Buddy", (ISetPiece) new LordOfTheLostLands()),
            Tuple.Create("Aldraginowski, polak Zola", (ISetPiece) new Hermit()),
            
        };

        private readonly List<Tuple<string, ISetPiece>> _rareEvents = new List<Tuple<string, ISetPiece>>()
        {
            Tuple.Create("Sorgigas, the Sor Giant", (ISetPiece) new SorGiant())
        };

        #region "Taunt data"

        private static readonly Tuple<string, TauntData>[] CriticalEnemies = new Tuple<string, TauntData>[]
        {
            Tuple.Create("Lich", new TauntData()
            {
                NumberOfEnemies = new string[] {
                    "I am invincible while my {COUNT} Liches still stand!",
                    "My {COUNT} Liches will feast on your essence!"
                },
                Final = new string[] {
                    "My final Lich shall consume your souls!",
                    "My final Lich will protect me forever!"
                }
            }),
            Tuple.Create("Ent Ancient", new TauntData()
            {
                NumberOfEnemies = new string[] {
                    "Mortal scum! My {COUNT} Ent Ancients will defend me forever!",
                    "My forest of {COUNT} Ent Ancients is all the protection I need!"
                },
                Final = new string[] {
                    "My final Ent Ancient will destroy you all!",
                    "My final Ent Ancient shall crush you!"
                }
            }),
            Tuple.Create("Oasis Giant", new TauntData()
            {
                NumberOfEnemies = new string[] {
                    "My {COUNT} Oasis Giants will feast on your flesh!",
                    "You have no hope against my {COUNT} Oasis Giants!"
                },
                Final = new string[] {
                    "A powerful Oasis Giant still fights for me!",
                    "You will never defeat me while an Oasis Giant remains!"
                }
            }),
            Tuple.Create("Phoenix Lord", new TauntData()
            {
                NumberOfEnemies = new string[] {
                    "Maggots! My {COUNT} Phoenix Lord will burn you to ash!",
                    "My {COUNT} Phoenix Lords will serve me forever!"
                },
                Final = new string[] {
                    "My final Phoenix Lord will never fall!",
                    "My last Phoenix Lord will blacken your bones!"
                }
            }),
            Tuple.Create("Ghost King", new TauntData()
            {
                NumberOfEnemies = new string[] {
                    "My {COUNT} Ghost Kings give me more than enough protection!",
                    "Pathetic humans! My {COUNT} Ghost Kings shall destroy you utterly!"
                },
                Final = new string[] {
                    "A mighty Ghost King remains to guard me!",
                    "My final Ghost King is untouchable!"
                }
            }),
            Tuple.Create("Cyclops God", new TauntData()
            {
                NumberOfEnemies = new string[] {
                    "Cretins! I have {COUNT} Cyclops Gods to guard me!",
                    "My {COUNT} powerful Cyclops Gods will smash you!"
                },
                Final = new string[] {
                    "My last Cyclops God will smash you to pieces!",
                    "My final Cyclops God shall crush your puny skulls!"
                }
            }),
            Tuple.Create("Red Demon", new TauntData()
            {
                NumberOfEnemies = new string[] {
                    "Fools! There is no escape from my {COUNT} Red Demons!",
                    "My legion of {COUNT} Red Demons live only to serve me!"
                },
                Final = new string[] {
                    "My final Red Demon is unassailable!",
                    "A Red Demon still guards me!"
                }
            }),

            Tuple.Create("Skull Shrine", new TauntData()
            {
                Spawn = new string[] {
                    "Your futile efforts are no match for a Skull Shrine!"
                },
                NumberOfEnemies = new string[] {
                    "Insects!  {COUNT} Skull Shrines still protect me",
                    "You hairless apes will never overcome my {COUNT} Skull Shrines!",
                    "You frail humans will never defeat my {COUNT} Skull Shrines!",
                    "Miserable worms like you cannot stand against my {COUNT} Skull Shrines!",
                    "Imbeciles! My {COUNT} Skull Shrines make me invincible!"
                },
                Final = new string[] {
                    "Pathetic fools!  A Skull Shrine guards me!",
                    "Miserable scum!  My Skull Shrine is invincible!"
                },
                Killed = new string[] {
                    "You defaced a Skull Shrine!  Minions, to arms!",
                    "{PLAYER} razed one of my Skull Shrines -- I WILL HAVE MY REVENGE!",
                    "{PLAYER}, you will rue the day you dared to defile my Skull Shrine!",
                    "{PLAYER}, you contemptible pig! Ruining my Skull Shrine will be the last mistake you ever make!",
                    "{PLAYER}, you insignificant cur! The penalty for destroying a Skull Shrine is death!"
                }
            }),
            Tuple.Create("Cube God", new TauntData()
            {
                Spawn = new string[] {
                    "Your meager abilities cannot possibly challenge a Cube God!"
                },
                NumberOfEnemies = new string[] {
                    "Filthy vermin! My {COUNT} Cube Gods will exterminate you!",
                    "Loathsome slugs! My {COUNT} Cube Gods will defeat you!",
                    "You piteous cretins! {COUNT} Cube Gods still guard me!",
                    "Your pathetic rabble will never survive against my {COUNT} Cube Gods!",
                    "You feeble creatures have no hope against my {COUNT} Cube Gods!"
                },
                Final = new string[] {
                    "Worthless mortals! A mighty Cube God defends me!",
                    "Wretched mongrels!  An unconquerable Cube God is my bulwark!"
                },
                Killed = new string[] {
                    "You have dispatched my Cube God, but you will never escape my Realm!",
                    "{PLAYER}, you pathetic swine! How dare you assault my Cube God?",
                    "{PLAYER}, you wretched dog! You killed my Cube God!",
                    "{PLAYER}, you may have destroyed my Cube God but you will never defeat me!",
                    "I have many more Cube Gods, {PLAYER}!",
                }
            }),
            Tuple.Create("The Horrific", new TauntData()
            {
                Spawn = new string[] {
                    "Frightening..what is this abomination doing in my realm?",
                    "My Horrific will suck the life out of you fools!"
                },
                NumberOfEnemies = new string[] {
                    "Disgusting warriors! My {COUNT} Horrifics will shred you to bits!"
                },
                Killed = new string[] {
                    "How could you just slay my Horrific Creature with such ease, {PLAYER}?"
                }
            }),
            Tuple.Create("Tod", new TauntData()
            {
                Spawn = new string[] {
                    "Tod? What are you doing here?"
                },
                NumberOfEnemies = new string[] {
                    "Stupid cretins! My {COUNT} Tods still prevail!"
                },
                Killed = new string[] {
                    "Tod was my best friend.."
                }
            }),
            Tuple.Create("Test Egg", new TauntData()
            {
                Spawn = new string[] {
                    "There seems to be an egg in my realm."
                },
                Killed = new string[] {
                    "I'm going to eat you for breakfast, {PLAYER}!",
                }
            }),
            Tuple.Create("Pentaract", new TauntData()
            {
                Spawn = new string[] {
                    "Behold my Pentaract, and despair!"
                },
                NumberOfEnemies = new string[] {
                    "Wretched creatures! {COUNT} Pentaracts remain!",
                    "You detestable humans will never defeat my {COUNT} Pentaracts!",
                    "My {COUNT} Pentaracts will protect me forever!",
                    "Your weak efforts will never overcome my {COUNT} Pentaracts!",
                    "Defiance is useless! My {COUNT} Pentaracts will crush you!"
                },
                Final = new string[] {
                    "I am invincible while my Pentaract stands!",
                    "Ignorant fools! A Pentaract guards me still!"
                },
                Killed = new string[] {
                    "That was but one of many Pentaracts!",
                    "You have razed my Pentaract, but you will die here in my Realm!",
                    "{PLAYER}, you lowly scum!  You'll regret that you ever touched my Pentaract!",
                    "{PLAYER}, you flea-ridden animal! You destoryed my Pentaract!",
                    "{PLAYER}, by destroying my Pentaract you have sealed your own doom!"
                }
            }),
            Tuple.Create("Grand Sphinx", new TauntData()
            {
                Spawn = new string[] {
                    "At last, a Grand Sphinx will teach you to respect!"
                },
                NumberOfEnemies = new string[] {
                    "You dull-spirited apes! You shall pose no challenge for {COUNT} Grand Sphinxes!",
                    "Regret your choices, blasphemers! My {COUNT} Grand Sphinxes will teach you respect!",
                    "My {COUNT} Grand Sphinxes protect my Chamber with their lives!",
                    "My Grand Sphinxes will bewitch you with their beauty!"
                },
                Final = new string[] {
                    "A Grand Sphinx is more than a match for this rabble.",
                    "You festering rat-catchers! A Grand Sphinx will make you doubt your purpose!",
                    "Gaze upon the beauty of the Grand Sphinx and feel your last hopes drain away."
                },
                Killed = new string[] {
                    "The death of my Grand Sphinx shall be avenged!",
                    "My Grand Sphinx, she was so beautiful. I will kill you myself, {PLAYER}!",
                    "My Grand Sphinx had lived for thousands of years! You, {PLAYER}, will not survive the day!",
                    "{PLAYER}, you up-jumped goat herder! You shall pay for defeating my Grand Sphinx!",
                    "{PLAYER}, you pestiferous lout! I will not forget what you did to my Grand Sphinx!",
                    "{PLAYER}, you foul ruffian! Do not think I forget your defiling of my Grand Sphinx!"
                }
            }),
            Tuple.Create("Lord of the Lost Lands", new TauntData()
            {
                Spawn = new string[] {
                    "Cower in fear of my Lord of the Lost Lands!",
                    "My Lord of the Lost Lands will make short work of you!"
                },
                NumberOfEnemies = new string[] {
                    "Cower before your destroyer! You stand no chance against {COUNT} Lords of the Lost Lands!",
                    "Your pathetic band of fighters will be crushed under the might feet of my {COUNT} Lords of the Lost Lands!",
                    "Feel the awesome might of my {COUNT} Lords of the Lost Lands!",
                    "Together, my {COUNT} Lords of the Lost Lands will squash you like a bug!",
                    "Do not run! My {COUNT} Lords of the Lost Lands only wish to greet you!"
                },
                Final = new string[] {
                    "Give up now! You stand no chance against a Lord of the Lost Lands!",
                    "Pathetic fools! My Lord of the Lost Lands will crush you all!",
                    "You are nothing but disgusting slime to be scraped off the foot of my Lord of the Lost Lands!"
                },
                Killed = new string[] {
                    "How dare you foul-mouthed hooligans treat my Lord of the Lost Lands with such indignity!",
                    "What trickery is this?! My Lord of the Lost Lands was invincible!",
                    "You win this time, {PLAYER}, but mark my words:  You will fall before the day is done.",
                    "{PLAYER}, I will never forget you exploited my Lord of the Lost Lands' weakness!",
                    "{PLAYER}, you have done me a service! That Lord of the Lost Lands was not worthy of serving me.",
                    "You got lucky this time {PLAYER}, but you stand no chance against me!",
                }
            }),
            Tuple.Create("The Mothership", new TauntData
                {
                    Spawn = new string[] {
                        "The coordinates must have been off...that's kind of weird...",
                        "The invaders have invaded us. Aaah!",
                        "This big flying saucer is sure to take you dumb fools down for good!"
                    },
                    Killed = new string[] {
                        "Well...maybe aliens can't handle realmers like you.",
                        "{PLAYER}, that thing cost me about 99999999 gold man. Pay up!"
                    }
                }),
            Tuple.Create("Zaragon, the Blood Mage", new TauntData
                {
                    Spawn = new string[] {
                        "They will know his name..ZARAGON SHOW THEM YOUR POWER!"
                    },
                    Killed = new string[] {
                        "The Catacombs has been UNVEILED?!"
                    }
                }),
            Tuple.Create("Sorgigas, the Sor Giant", new TauntData
                {
                    Spawn = new string[] {
                        "The power of the Sor should be given to just anyone...my great giant shall protect that ultimate power!"
                    },
                    NumberOfEnemies = new string[] {
                        "My Sor Giants are untouchable!"
                    },
                    Killed = new string[] {
                        "Mere mortals..the power must not be in your filthy hands!"
                    }
                }),
            Tuple.Create("Hermit God", new TauntData()
            {
                Spawn = new string[] {
                    "My Hermit God's thousand tentacles shall drag you to a watery grave!"
                },
                NumberOfEnemies = new string[] {
                    "You will make a tasty snack for my Hermit Gods!",
                    "I will enjoy watching my {COUNT} Hermit Gods fight over your corpse!"
                },
                Final = new string[] {
                    "You will be pulled to the bottom of the sea by my mighty Hermit God.",
                    "Flee from my Hermit God, unless you desire a watery grave!",
                    "My Hermit God awaits more sacrifices for the majestic Thessal.",
                    "My Hermit God will pull you beneath the waves!",
                    "You will make a tasty snack for my Hermit God!",
                },
                Killed = new string[] {
                    "This is preposterous!  There is no way you could have defeated my Hermit God!",
                    "You were lucky this time, {PLAYER}!  You will rue this day that you killed my Hermit God!",
                    "You naive imbecile, {PLAYER}! Without my Hermit God, Dreadstump is free to roam the seas without fear!",
                    "My Hermit God was more than you'll ever be, {PLAYER}. I will kill you myself!",
                }
            }),
            Tuple.Create("Ghost Ship", new TauntData()
            {
                Spawn = new string[] {
                    "My Ghost Ship will terrorize you pathetic peasants!",
                    "A Ghost Ship has entered the Realm."
                },
                Final = new string[] {
                    "My Ghost Ship will send you to a watery grave.",
                    "You filthy mongrels stand no chance against my Ghost Ship!",
                    "My Ghost Ship's cannonballs will crush your pathetic Knights!"
                },
                Killed = new string[] {
                    "My Ghost Ship will return!",
                    "Alas, my beautiful Ghost Ship has sunk!",
                    "{PLAYER}, you foul creature.  I shall see to your death personally!",
                    "{PLAYER}, has crossed me for the last time! My Ghost Ship shall be avenged.",
                    "{PLAYER} is such a jerk!",
                    "How could a creature like {PLAYER} defeat my dreaded Ghost Ship?!",
                    "The spirits of the sea will seek revenge on your worthless soul, {PLAYER}!"
                }
            }),
            Tuple.Create("Dragon Head", new TauntData()
            {
                Spawn = new string[] {
                    "The Rock Dragon has been summoned.",
                    "Beware my Rock Dragon. All who face him shall perish.",
                },
                Final = new string[] {
                    "My Rock Dragon will end your pathetic existence!",
                    "Fools, no one can withstand the power of my Rock Dragon!",
                    "The Rock Dragon will guard his post until the bitter end.",
                    "The Rock Dragon will never let you enter the Lair of Draconis.",
                },
                Killed = new string[] {
                    "My Rock Dragon will return!",
                    "The Rock Dragon has failed me!",
                    "{PLAYER} knows not what he has done.  That Lair was guarded for the Realm's own protection!",
                    "{PLAYER}, you have angered me for the last time!",
                    "{PLAYER} will never survive the trials that lie ahead.",
                    "A filthy weakling like {PLAYER} could never have defeated my Rock Dragon!!!",
                    "You shall not live to see the next sunrise, {PLAYER}!",
                }
            }),
            Tuple.Create("shtrs Defense System", new TauntData()
            {
                Spawn = new string[] {
                    "The Shatters has been discovered!?!",
                    "The Forgotten King has raised his Avatar!",
                },
                Final = new string[] {
                    "Attacking the Avatar of the Forgotten King would be...unwise.",
                    "Kill the Avatar, and you risk setting free an abomination.",
                    "Before you enter the Shatters you must defeat the Avatar of the Forgotten King!",
                },
                Killed = new string[] {
                    "The Avatar has been defeated!",
                    "How could simpletons kill The Avatar of the Forgotten King!?",
                    "{PLAYER} has unleashed an evil upon this Realm.",
                    "{PLAYER}, you have awoken the Forgotten King. Enjoy a slow death!",
                    "{PLAYER} will never survive what lies in the depths of the Shatters.",
                    "Enjoy your little victory while it lasts, {PLAYER}!"
                }
            }),
           Tuple.Create("Yazanahar", new TauntData
                {
                    Spawn = new string[] {
                        "The ancient guardian will not rise to you fools."
                    },
                    Killed = new string[] {
                        "You have released power beyond your comprehension, {PLAYER}."
                    }
                }),
           Tuple.Create("Truvix, the Lord Wanderer", new TauntData
                {
                    Spawn = new string[] {
                        "A lost wanderer somehow escaped his prison.."
                    },
                    Killed = new string[] {
                        "Time is beyond your control, {PLAYER}!"
                    }
                }),
            Tuple.Create("Zombie Horde", new TauntData()
            {
                Spawn = new string[] {
                    "At last, my Zombie Horde will eradicate you like the vermin that you are!",
                    "The full strength of my Zombie Horde has been unleashed!",
                    "Let the apocalypse begin!",
                    "Quiver with fear, peasants, my Zombie Horde has arrived!",
                },
                Final = new string[] {
                    "A small taste of my Zombie Horde should be enough to eliminate you!",
                    "My Zombie Horde will teach you the meaning of fear!",
                },
                Killed = new string[] {
                    "The death of my Zombie Horde is unacceptable! You will pay for your insolence!",
                    "{PLAYER}, I will kill you myself and turn you into the newest member of my Zombie Horde!",
                }
            }),
            Tuple.Create("Encounter Altar", new TauntData
                {
                    Spawn = new string[] {
                        "Kill them my great statues!"
                    },
                    Killed = new string[] {
                        "ARGH!"
                    }
                }),
            Tuple.Create("Mad Jester", new TauntData
                {
                    Spawn = new string[] {
                        "Silly fools. I have summoned the TRUE FOOL!"
                    },
                    Killed = new string[] {
                        "That joke got old anyway."
                    }
                }),
            Tuple.Create("Lord Stone Gargoyle", new TauntData
                {
                    Spawn = new string[] {
                        "These ancient creatures are fierce..",
                        "Don't tamper with something you don't understand. Heed my warning."
                    },
                    Killed = new string[] {
                        "Your life will soon crumble away by my hand, {PLAYER}!"
                    }
                }),
            Tuple.Create("Elemental Phantom", new TauntData
                {
                    Spawn = new string[] {
                        "The elements are now in union!",
                        "My elemental phantom shall burn you into ashes!",
                        "My elemental phantom thinks you will look good in a glacier."
                    },
                    Killed = new string[] {
                        "Soon you will become my new servant, {PLAYER}.",
                        "Your lives were nothing to the Elemental Phantom...how could it fall?"
                    }
                }),
            Tuple.Create("Frog of Filisa", new TauntData
                {
                    Spawn = new string[] {
                        "I have unleashed a grand evil, none shall survive!"
                    },
                    Killed = new string[] {
                        "Jebaited, I'm glad that stupid frog is finally dead"
                    }
                }),
            Tuple.Create("Hellfire Lord", new TauntData
                {
                    Spawn = new string[] {
                        "The Hellfire Lord will purge all of you to hell!"
                    },
                    Killed = new string[] {
                        "Hades will not stand for this!"
                    }
                }),
            Tuple.Create("BedlamGod", new TauntData
                {
                    Spawn = new string[] {
                        "Fools! Bedlam will annihilate all of you!"
                    },
                    Killed = new string[] {
                        "You made a grave mistake killing Bedlam. The Asylum will be after you next."
                    }
                }),
            Tuple.Create("Sanic", new TauntData
                {
                    Spawn = new string[] {
                        "You may be fast, but are you faster than Sanic?"
                    },
                    Killed = new string[] {
                        "Impossible! Sanic was supposed to be faster than everyone!"
                    }
                }),
            Tuple.Create("TF The Fallen", new TauntData
                {
                    Spawn = new string[] {
                        "What is The Fallen doing here in my realm???"
                    },
                    Killed = new string[] {
                        "Bah, he was already dead anyways."
                    }
                }),
            Tuple.Create("Queen of Hearts", new TauntData
                {
                    Spawn = new string[] {
                        "A game of cards should trump you all!"
                    },
                    Killed = new string[] {
                        "She's been flushed!!!"
                    }
                }),
            Tuple.Create("The Cheesehead", new TauntData
                {
                    Spawn = new string[] {
                        "Free cheese for everyone!"
                    },
                    Killed = new string[] {
                        "Aww, no more cheese..."
                    }
                }),
            Tuple.Create("Boshy", new TauntData()),
            Tuple.Create("The Kid", new TauntData()),
        };

        #endregion "Taunt data"

        #region "Spawn data"

        private static readonly Dictionary<WmapTerrain, Tuple<int, Tuple<string, double>[]>> RegionMobs =
            new Dictionary<WmapTerrain, Tuple<int, Tuple<string, double>[]>>()
        {
            { WmapTerrain.ShoreSand, Tuple.Create(
                1500, new []
                {
                    Tuple.Create("Pirate", 0.3),
                    Tuple.Create("Piratess", 0.1),
                    Tuple.Create("Snake", 0.2),
                    Tuple.Create("Scorpion Queen", 0.4),
                })
            },
            { WmapTerrain.ShorePlains, Tuple.Create(
                1550, new []
                {
                    Tuple.Create("Bandit Leader", 0.4),
                    Tuple.Create("Red Gelatinous Cube", 0.2),
                    Tuple.Create("Purple Gelatinous Cube", 0.2),
                    Tuple.Create("Green Gelatinous Cube", 0.2),
                })
            },
            { WmapTerrain.LowPlains, Tuple.Create(
                1400, new []
                {
                    Tuple.Create("Hobbit Mage", 0.5),
                    Tuple.Create("Undead Hobbit Mage", 0.4),
                    Tuple.Create("Sumo Master", 0.1),
                })
            },
            { WmapTerrain.LowForest, Tuple.Create(
                1400, new []
                {
                    Tuple.Create("Elf Wizard", 0.2),
                    Tuple.Create("Goblin Mage", 0.2),
                    Tuple.Create("Easily Enraged Bunny", 0.3),
                    Tuple.Create("Forest Nymph", 0.3),
                })
            },
            { WmapTerrain.LowSand, Tuple.Create(
                1400, new []
                {
                    Tuple.Create("Sandsman King", 0.4),
                    Tuple.Create("Giant Crab", 0.2),
                    Tuple.Create("Sand Devil", 0.4),
                })
            },
            { WmapTerrain.MidPlains, Tuple.Create(
                1550, new []
                {
                    Tuple.Create("Fire Sprite", 0.1),
                    Tuple.Create("Ice Sprite", 0.1),
                    Tuple.Create("Magic Sprite", 0.1),
                    Tuple.Create("Pink Blob", 0.07),
                    Tuple.Create("Gray Blob", 0.07),
                    Tuple.Create("Earth Golem", 0.04),
                    Tuple.Create("Paper Golem", 0.04),
                    Tuple.Create("Big Green Slime", 0.08),
                    Tuple.Create("Swarm", 0.05),
                    Tuple.Create("Wasp Queen", 0.2),
                    Tuple.Create("Shambling Sludge", 0.03),
                    Tuple.Create("Orc King", 0.06),
                    Tuple.Create("Candy Gnome", 0.02)
                })
            },
            { WmapTerrain.MidForest, Tuple.Create(
                1550, new []
                {
                    Tuple.Create("Dwarf King", 0.3),
                    Tuple.Create("Metal Golem", 0.05),
                    Tuple.Create("Clockwork Golem", 0.05),
                    Tuple.Create("Werelion", 0.1),
                    Tuple.Create("Horned Drake", 0.3),
                    Tuple.Create("Red Spider", 0.1),
                    Tuple.Create("Black Bat", 0.1)
                })
            },
            { WmapTerrain.MidSand, Tuple.Create(
                1400, new []
                {
                    Tuple.Create("Desert Werewolf", 0.25),
                    Tuple.Create("Fire Golem", 0.1),
                    Tuple.Create("Darkness Golem", 0.1),
                    Tuple.Create("Sand Phantom", 0.2),
                    Tuple.Create("Nomadic Shaman", 0.25),
                    Tuple.Create("Great Lizard", 0.1),
                })
            },
            { WmapTerrain.HighPlains, Tuple.Create(
                900, new []
                {
                    Tuple.Create("Shield Orc Key", 0.2),
                    Tuple.Create("Urgle", 0.2),
                    Tuple.Create("Undead Dwarf God", 0.6)
                })
            },
            { WmapTerrain.HighForest, Tuple.Create(
                750, new []
                {
                    Tuple.Create("Ogre King", 0.4),
                    Tuple.Create("Dragon Egg", 0.1),
                    Tuple.Create("Lizard God", 0.5),
                    Tuple.Create("Beer God", 0.1)
                })
            },
            { WmapTerrain.HighSand, Tuple.Create(
                1000, new []
                {
                    Tuple.Create("Minotaur", 0.4),
                    Tuple.Create("Flayer God", 0.4),
                    Tuple.Create("Flamer King", 0.2)
                })
            },
            { WmapTerrain.Mountains, Tuple.Create(
                200, new []
                {
                    Tuple.Create("White Demon", 0.09),
                    Tuple.Create("Sprite God", 0.08),
                    Tuple.Create("Medusa", 0.08),
                    Tuple.Create("Ent God", 0.1),
                    Tuple.Create("Beholder", 0.09),
                    Tuple.Create("Flying Brain", 0.09),
                    Tuple.Create("Slime God", 0.08),
                    Tuple.Create("Ghost God", 0.07),
                    Tuple.Create("Rock Bot", 0.05),
                    Tuple.Create("Djinn", 0.09),
                    Tuple.Create("Leviathan", 0.07),
                    Tuple.Create("Arena Headless Horseman", 0.01),
                    Tuple.Create("Thunder God", 0.025),
                    Tuple.Create("Angelic Commander", 0.021),
                    Tuple.Create("Frost Knight", 0.19),
                    Tuple.Create("Elemental Flame", 0.19),
                })
            },
        };

        #endregion "Spawn data"

        public Oryx(Realm world)
        {
            _world = world;
            Init();
        }

        private static double GetUniform(Random rand)
        {
            // 0 <= u < 2^32
            var u = (uint)(rand.NextDouble() * uint.MaxValue);
            // The magic number below is 1/(2^32 + 2).
            // The result is strictly between 0 and 1.
            return (u + 1.0) * 2.328306435454494e-10;
        }

        private static double GetNormal(Random rand)
        {
            // Use Box-Muller algorithm
            var u1 = GetUniform(rand);
            var u2 = GetUniform(rand);
            var r = Math.Sqrt(-2.0 * Math.Log(u1));
            var theta = 2.0 * Math.PI * u2;
            return r * Math.Sin(theta);
        }

        private static double GetNormal(Random rand, double mean, double standardDeviation) => mean + standardDeviation * GetNormal(rand);

        private ushort GetRandomObjType(IEnumerable<Tuple<string, double>> dat)
        {
            double p = _rand.NextDouble();
            double n = 0;
            ushort objType = 0;
            foreach (var k in dat)
            {
                n += k.Item2;
                if (n > p)
                {
                    objType = _world.Manager.Resources.GameData.IdToObjectType[k.Item1];
                    break;
                }
            }
            return objType;
        }

        private int Spawn(ObjectDesc desc, WmapTerrain terrain, int w, int h)
        {
            Entity entity;

            var ret = 0;
            var pt = new IntPoint();

            if (desc.Spawn != null)
            {
                var num = (int)GetNormal(_rand, desc.Spawn.Mean, desc.Spawn.StdDev);

                if (num > desc.Spawn.Max)
                    num = desc.Spawn.Max;
                else if (num < desc.Spawn.Min)
                    num = desc.Spawn.Min;

                do
                {
                    pt.X = _rand.Next(0, w);
                    pt.Y = _rand.Next(0, h);
                } while (_world.Map[pt.X, pt.Y].Terrain != terrain || !_world.IsPassable(pt.X, pt.Y) || _world.AnyPlayerNearby(pt.X, pt.Y));

                for (var k = 0; k < num; k++)
                {
                    entity = Entity.Resolve(_world.Manager, desc.ObjectType);
                    entity.Move(pt.X + (float)(_rand.NextDouble() * 2 - 1) * 5, pt.Y + (float)(_rand.NextDouble() * 2 - 1) * 5);

                    (entity as Enemy).Terrain = terrain;

                    _world.EnterWorld(entity);

                    ret++;
                }
                return ret;
            }

            do
            {
                pt.X = _rand.Next(0, w);
                pt.Y = _rand.Next(0, h);
            } while (_world.Map[pt.X, pt.Y].Terrain != terrain || !_world.IsPassable(pt.X, pt.Y) || _world.AnyPlayerNearby(pt.X, pt.Y));

            entity = Entity.Resolve(_world.Manager, desc.ObjectType);
            entity.Move(pt.X, pt.Y);

            (entity as Enemy).Terrain = terrain;

            _world.EnterWorld(entity);

            ret++;

            return ret;
        }

        public void Init()
        {
            var w = _world.Map.Width;
            var h = _world.Map.Height;
            var stats = new int[12];

            for (var y = 0; y < h; y++)
                for (var x = 0; x < w; x++)
                {
                    var tile = _world.Map[x, y];
                    if (tile.Terrain != WmapTerrain.None)
                        stats[(int)tile.Terrain - 1]++;
                }

            foreach (var i in RegionMobs)
            {
                var terrain = i.Key;
                var idx = (int)terrain - 1;
                var enemyCount = stats[idx] / i.Value.Item1;
                _enemyMaxCounts[idx] = enemyCount;
                _enemyCounts[idx] = 0;

                for (var j = 0; j < enemyCount; j++)
                {
                    var objType = GetRandomObjType(i.Value.Item2);

                    if (objType == 0)
                        continue;

                    _enemyCounts[idx] += Spawn(_world.Manager.Resources.GameData.ObjectDescs[objType], terrain, w, h);

                    if (_enemyCounts[idx] >= enemyCount)
                        break;
                }
            }
        }

        public void Tick()
        {
            if (Program.Uptime.ElapsedMilliseconds - _prevTick <= 10000)
                return;

            if (_tenSecondTick % 2 == 0)
                HandleAnnouncements();

            if (_tenSecondTick % 6 == 0)
                EnsurePopulation();

            _tenSecondTick++;
            _prevTick = Program.Uptime.ElapsedMilliseconds;
        }

        private void EnsurePopulation()
        {
            RecalculateEnemyCount();

            var state = new int[12];
            var diff = new int[12];
            var c = 0;

            for (var i = 0; i < state.Length; i++)
            {
                if (_enemyCounts[i] > _enemyMaxCounts[i] * 1.5) //Kill some
                {
                    state[i] = 1;
                    diff[i] = _enemyCounts[i] - _enemyMaxCounts[i];
                    c++;
                    continue;
                }

                if (_enemyCounts[i] < _enemyMaxCounts[i] * 0.75) //Add some
                {
                    state[i] = 2;
                    diff[i] = _enemyMaxCounts[i] - _enemyCounts[i];
                    continue;
                }

                state[i] = 0;
            }

            foreach (var i in _world.Enemies) //Kill
            {
                var idx = (int)i.Value.Terrain - 1;
                var entity = i.Value.GetNearestEntity(10, true);

                if (idx == -1 || state[idx] == 0 || entity != null || diff[idx] == 0)
                    continue;

                if (state[idx] == 1)
                {
                    _world.LeaveWorld(i.Value);

                    diff[idx]--;
                    if (diff[idx] == 0)
                        c--;
                }

                if (c == 0)
                    break;

                entity = null;
            }

            var w = _world.Map.Width;
            var h = _world.Map.Height;

            for (var i = 0; i < state.Length; i++) //Add
            {
                if (state[i] != 2)
                    continue;

                var x = diff[i];
                var t = (WmapTerrain)(i + 1);
                for (var j = 0; j < x;)
                {
                    var objType = GetRandomObjType(RegionMobs[t].Item2);

                    if (objType == 0)
                        continue;

                    j += Spawn(_world.Manager.Resources.GameData.ObjectDescs[objType], t, w, h);
                }
            }
            RecalculateEnemyCount();
        }

        private void RecalculateEnemyCount()
        {
            for (var i = 0; i < _enemyCounts.Length; i++)
                _enemyCounts[i] = 0;

            foreach (var i in _world.Enemies)
            {
                if (i.Value.Terrain == WmapTerrain.None)
                    continue;

                _enemyCounts[(int)i.Value.Terrain - 1]++;
            }
        }

        private void HandleAnnouncements()
        {
            if (_world.Closed)
                return;

            var taunt = CriticalEnemies[_rand.Next(0, CriticalEnemies.Length)];
            var count = 0;
            foreach (var i in _world.Enemies)
            {
                var desc = i.Value.ObjectDesc;
                if (desc == null || desc.ObjectId != taunt.Item1)
                    continue;

                count++;
            }

            if (count == 0)
                return;

            if ((count == 1 && taunt.Item2.Final != null) || (taunt.Item2.Final != null && taunt.Item2.NumberOfEnemies == null))
            {
                var arr = taunt.Item2.Final;
                var msg = arr[_rand.Next(0, arr.Length)];
                BroadcastMsg(msg);
            }
            else
            {
                var arr = taunt.Item2.NumberOfEnemies;
                if (arr == null)
                    return;

                var msg = arr[_rand.Next(0, arr.Length)];
                msg = msg.Replace("{COUNT}", count.ToString());
                BroadcastMsg(msg);
            }
        }

        private void BroadcastMsg(string message)
        {
            _world.Manager.Chat.Oryx(_world, message);
        }

        public void OnPlayerEntered(Player player)
        {
            player.SendInfo("Welcome to UT Realms!");
            player.SendInfo("Use [WASD] to move, [QE] to rotate; click to shoot!");
            player.SendInfo("Type /glands to enter the God Lands!");
        }

        private void SpawnEvent(string name, ISetPiece setpiece)
        {
            var pt = new IntPoint();
            do
            {
                pt.X = _rand.Next(0, _world.Map.Width);
                pt.Y = _rand.Next(0, _world.Map.Height);
            } while ((_world.Map[pt.X, pt.Y].Terrain < WmapTerrain.Mountains || _world.Map[pt.X, pt.Y].Terrain > WmapTerrain.MidForest) || !_world.IsPassable(pt.X, pt.Y, true) ||
                      _world.AnyPlayerNearby(pt.X, pt.Y));

            pt.X -= (setpiece.Size - 1) / 2;
            pt.Y -= (setpiece.Size - 1) / 2;
            setpiece.RenderSetPiece(_world, pt);
        }

        public void OnEnemyKilled(Enemy enemy, Player killer)
        {
            if (enemy.ObjectDesc == null || !enemy.ObjectDesc.Quest)
                return;

            TauntData? dat = null;
            foreach (var i in CriticalEnemies)
                if (enemy.ObjectDesc.ObjectId == i.Item1)
                {
                    dat = i.Item2;
                    break;
                }

            if (dat == null)
                return;

            if (dat.Value.Killed != null)
            {
                var arr = dat.Value.Killed;
                if (killer == null)
                    arr = arr.Where(m => !m.Contains("{PLAYER}")).ToArray();

                if (arr.Length > 0)
                {
                    var msg = arr[_rand.Next(0, arr.Length)];
                    msg = msg.Replace("{PLAYER}", (killer != null) ? killer.Name : "");
                    BroadcastMsg(msg);
                }
            }

            // 25% for new event ???
            //if (_rand.NextDouble() > 0.25)
            //    return;

            var events = _events;
            if (_rand.NextDouble() <= 0.01)
                events = _rareEvents;

            var evt = events[_rand.Next(0, events.Count)];

            var gameData = _world.Manager.Resources.GameData;
            if (gameData.ObjectDescs[gameData.IdToObjectType[evt.Item1]].PerRealmMax == 1)
                events.Remove(evt);

            SpawnEvent(evt.Item1, evt.Item2);

            dat = null;
            foreach (var i in CriticalEnemies)
                if (evt.Item1 == i.Item1)
                {
                    dat = i.Item2;
                    break;
                }

            if (dat == null)
                return;

            if (dat.Value.Spawn != null)
            {
                var arr = dat.Value.Spawn;
                string msg = arr[_rand.Next(0, arr.Length)];
                BroadcastMsg(msg);
            }

            foreach (var player in _world.Players)
                player.Value.HandleQuest(true);
        }

        public void InitCloseRealm()
        {
            Closing = true;

            _world.Manager.Chat.Announce("SPACESHIP LAUNCHING IN 30 SECONDS!", true);

            _world.Timers.Add(new WorldTimer(30000, (w) =>
            {
                if (w == null || w.Deleted) 
                    return;

                CloseRealm();
            }));
        }

        private void CloseRealm()
        {
            _world.Closed = true;
            BroadcastMsg("WE'RE GOING BRO");
            BroadcastMsg("LETS FUCKING BLAST");

            _world.Timers.Add(new WorldTimer(10000, (w) =>
            {
                if (w == null || w.Deleted) return;

                SendToCastle();
            }));
        }

        private void SendToCastle()
        {
            BroadcastMsg("MY MINIONS HAVE FAILED ME!");
            BroadcastMsg("BUT NOW YOU SHALL FEEL MY WRATH!");
            BroadcastMsg("COME MEET YOUR DOOM AT THE WALLS OF MY CASTLE!");

            if (_world.Players.Count <= 0)
                return;

            var castle = _world.Manager.AddWorld(new worlds.logic.Castle(_world.Manager.Resources.Worlds.Data["Castle"], playersEntering: _world.Players.Count));
            _world.QuakeToWorld(castle);
        }
    }
}
