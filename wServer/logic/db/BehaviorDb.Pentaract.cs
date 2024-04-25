using common.resources;
using wServer.logic.behaviors;
using wServer.logic.loot;
using wServer.logic.transitions;

namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ Pentaract = () => Behav()
            .Init("Pentaract Eye",
                new State(
                    new Prioritize(
                        new Swirl(2, 8, 20, true),
                        new Protect(2, "Pentaract Tower", 20, 6, 4)
                        ),
                    new Shoot(9, 1, coolDown: 1000)
                    )
            )
            .Init("Pentaract Tower",
                new State(
                    new Spawn("Pentaract Eye", 5, coolDown: 5000, givesNoXp: false),
                    new Grenade(4, 100, 8, coolDown: 5000),
                    new TransformOnDeath("Pentaract Tower Corpse"),
                    new TransferDamageOnDeath("Pentaract"),
                    // needed to avoid crash, Oryx.cs needs player name otherwise hangs server (will patch that later)
                    new TransferDamageOnDeath("Pentaract Tower Corpse")
                    )
            )
            .Init("Pentaract",
                new State(
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new State("Waiting",
                        new EntityNotExistsTransition("Pentaract Tower", 50, "Die")
                        ),
                    new State("Die",
                        new Suicide()
                        )
                    )
            )
            .Init("Pentaract Tower Corpse",
                new State(
                    new DropPortalOnDeath("The Unspeakable Portal", 0.33),
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new State("Waiting",
                        new TimedTransition(15000, "Spawn"),
                        new EntityNotExistsTransition("Pentaract Tower", 50, "Die")
                        ),
                    new State("Spawn",
                        new Transform("Pentaract Tower")
                        ),
                    new State("Die",
                        new Suicide()
                        )
                    ),
                new MostDamagers(3,
                    LootTemplates.SorRare()
                    ),
                new Threshold(0.01,
                    new TierLoot(10, ItemType.Weapon, .17),
                    new TierLoot(11, ItemType.Weapon, .15),
                    new TierLoot(9, ItemType.Armor, .15),
                    new TierLoot(10, ItemType.Armor, .17),
                    new TierLoot(11, ItemType.Armor, .17),
                    new TierLoot(12, ItemType.Armor, 0.05),
                    new TierLoot(12, ItemType.Weapon, 0.05),
                    new ItemLoot("Potion of Life", 0.2),
                    new ItemLoot("Potion of Mana", 0.2),
                    new ItemLoot("Potion of Defense", .1),
                    new ItemLoot("Potion of Attack", .1),
                    new ItemLoot("Potion of Vitality", .1),
                    new ItemLoot("Potion of Wisdom", .1),
                    new ItemLoot("Potion of Speed", .1),
                    new ItemLoot("Potion of Dexterity", .1),
                    new ItemLoot("Sacred Essence", 0.0005),
                    new ItemLoot("Seal of Blasphemous Prayer", .08)
                    )
            )
            ;
    }
}