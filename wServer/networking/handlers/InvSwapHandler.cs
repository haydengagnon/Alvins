using common.resources;
using Mono.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using wServer.networking.packets;
using wServer.networking.packets.incoming;
using wServer.networking.packets.outgoing;
using wServer.realm;
using wServer.realm.entities;

namespace wServer.networking.handlers
{
    internal class InvSwapHandler : PacketHandlerBase<InvSwap>
    {
        private static readonly Random Rand = new Random();
        public Inventory Inv => null;
        public override PacketId ID => PacketId.INVSWAP;

        protected override void HandlePacket(Client client, InvSwap packet)
        {
            Handle(
                client.Player,
                client.Player.Owner.GetEntity(packet.SlotObj1.ObjectId),
                client.Player.Owner.GetEntity(packet.SlotObj2.ObjectId),
                packet.SlotObj1.SlotId, packet.SlotObj2.SlotId);
        }

        private void Handle(
            Player player,
            Entity a, Entity b,
            int slotA, int slotB)
        {
            if (player?.Owner == null)
                return;
            if (slotA != slotB
                && slotB != 255 && slotB != 254
                && slotA != 255 && slotA != 254
                && slotA < 12 && slotB < 12
                         || player.HasBackpack && slotA < 20 && slotB < 20)
            {
                Player playerA = null, playerB = null;
                if (a is Player plrA) playerA = plrA;
                if (b is Player plrB) playerB = plrB;

                if (slotA >= 0 && slotA < 4)
                {
                    playerA?.OnUnequip(playerA.Inventory[slotA]);
                    if (playerB?.Inventory[slotB] != null)
                        playerB.OnEquip(playerB.Inventory[slotB]);
                }
                if (slotA > 4 && slotA >= 20)
                {
                    playerA?.OnUnequipInv(playerA.Inventory[slotA]);
                    if (playerB?.Inventory[slotB] != null)
                        playerB.InInv(playerB.Inventory[slotB]);
                }
                if (slotB > 4 && slotB >= 20)
                {
                    playerA?.InInv(playerA.Inventory[slotA]);
                    if (playerB?.Inventory[slotB] != null)
                        playerB.OnUnequipInv(playerB.Inventory[slotB]);
                }
                else if (slotB >= 0 && slotB < 4)
                {
                    playerA?.OnEquip(playerA.Inventory[slotA]);
                    if (playerB?.Inventory[slotB] != null)
                        playerB.OnUnequip(playerB.Inventory[slotB]);
                }
            }

            if (!ValidateEntities(player, a, b) || player.tradeTarget != null)
            {
                a.ForceUpdate(slotA);
                b.ForceUpdate(slotB);
                player.Client.SendPacket(new InvResult() { Result = 1 });
                return;
            }

            var conA = (IContainer)a;
            var conB = (IContainer)b;

            // check if stacking operation
            if (b == player)
                foreach (var stack in player.Stacks)
                    if (stack.Slot == slotB)
                    {
                        var stackTrans = conA.Inventory.CreateTransaction();
                        var item = stack.Put(stackTrans[slotA]);
                        if (item == null) // success
                        {
                            // if a stackable item ends up in a gift chest it becomes infinite if not removed
                            if (a is GiftChest && stackTrans[slotA] != null)
                            {
                                var trans = player.Manager.Database.Conn.CreateTransaction();
                                player.Manager.Database.RemoveGift(player.Client.Account, stackTrans[slotA].ObjectType, trans);
                                trans.Execute();
                            }

                            stackTrans[slotA] = null;
                            Inventory.Execute(stackTrans);
                            player.Client.SendPacket(new InvResult() { Result = 0 });
                            return;
                        }
                    }

            // not stacking operation, continue on with normal swap

            // validate slot types
            if (!ValidateSlotSwap(player, conA, conB, slotA, slotB))
            {
                a.ForceUpdate(slotA);
                b.ForceUpdate(slotB);
                player.Client.SendPacket(new InvResult() { Result = 1 });
                return;
            }

            // setup swap
            var queue = new Queue<Action>();
            var conATrans = conA.Inventory.CreateTransaction();
            var conBTrans = conB.Inventory.CreateTransaction();
            var itemA = conATrans[slotA];
            var itemB = conBTrans[slotB];
            conBTrans[slotB] = itemA;
            conATrans[slotA] = itemB;
            // validate that soulbound items are not placed in public bags (includes any swaped item from admins)
            if (!ValidateItemSwap(player, a, itemB))
            {
                queue.Enqueue(() => DropInSoulboundBag(player, itemB));
                conATrans[slotA] = null;
            }
            if (!ValidateItemSwap(player, b, itemA))
            {
                queue.Enqueue(() => DropInSoulboundBag(player, itemA));
                conBTrans[slotB] = null;
            }
            //If both items are greater potions turn it into vial instead of switching
            if (itemA == itemB && !(conA == conB && slotA == slotB))
            {
                string switchTo = null;
                switch (itemA.ObjectId)
                {
                    case "Legendsary Token 1":
                        switchTo = "Legendsary Token 2";
                        break;
                    case "Legendsary Token 2":
                        switchTo = "Legendsary Token 4";
                        break;
                    case "Legendsary Token 4":
                        switchTo = "Legendsary Token 8";
                        break;
                    case "Legendsary Token 8":
                        switchTo = "Legendsary Token 16";
                        break;
                    case "Legendsary Token 16":
                        switchTo = "Legendsary Token 32";
                        break;
                    case "Legendsary Token 32":
                        switchTo = "Legendsary Token 64";
                        break;
                    case "Necrospoilis Token 1":
                        switchTo = "Necrospoilis Token 2";
                        break;
                    case "Necrospoilis Token 2":
                        switchTo = "Necrospoilis Token 4";
                        break;
                    case "Necrospoilis Token 4":
                        switchTo = "Necrospoilis Token 8";
                        break;
                    case "Necrospoilis Token 8":
                        switchTo = "Necrospoilis Token 16";
                        break;
                    case "Necrospoilis Token 16":
                        switchTo = "Necrospoilis Token 32";
                        break;
                    case "Necrospoilis Token 32":
                        switchTo = "Necrospoilis Token 64";
                        break;
                        /*
                    case "welding":
                        switchTo = "welding2";
                        break;
                    case "welding2":
                        switchTo = "welding4";
                        break;
                        
                    case "welding4":
                        switchTo = "welding8";
                        break;
                    case "welding8":
                        switchTo = "welding16";
                        break;
                    case "welding16":
                        switchTo = "welding32";
                        break;
                    case "welding32":
                        switchTo = "welding64";
                        break;
                    case "welding64":
                        switchTo = "welding128";
                        break;
                    case "welding128":
                        switchTo = "welding3";
                        break;
                        */



                }

                if (switchTo != null)
                {
                    if (a is GiftChest || b is GiftChest)
                    {
                        player.SendError("You can't combine items from or to a gift chest!");
                        player.Client.SendPacket(new InvResult() { Result = 1 });
                        return;
                    }

                    player.Manager.Resources.GameData.IdToObjectType.TryGetValue(switchTo, out var objType);
                    conB.Inventory[slotB] = player.Manager.Resources.GameData.Items[objType];
                    conA.Inventory[slotA] = null;

                    player.Client.SendPacket(new InvResult() { Result = 0 });

                    player.SaveToCharacter();
                    player.Client.Character.FlushAsync();

                    a.ForceUpdate(slotA);
                    b.ForceUpdate(slotB);
                    return;
                }
            }

            // swap items
            if (Inventory.Execute(conATrans, conBTrans))
            {
                // remove gift if from gift chest
                var db = player.Manager.Database;
                var trans = db.Conn.CreateTransaction();
                if (a is GiftChest && itemA != null)
                    db.RemoveGift(player.Client.Account, itemA.ObjectType, trans);
                if (b is GiftChest && itemB != null)
                    db.RemoveGift(player.Client.Account, itemB.ObjectType, trans);
                if (trans.Execute())
                {
                    while (queue.Count > 0)
                        queue.Dequeue()();

                    player.Client.SendPacket(new InvResult() { Result = 0 });
                    return;
                }
            }
            a.ForceUpdate(slotA);
            b.ForceUpdate(slotB);
            player.Client.SendPacket(new InvResult() { Result = 1 });
        }

        private bool ValidateEntities(Player p, Entity a, Entity b)
        { // returns false if bad input
            if (a == null || b == null)
                return false;

            if ((a as IContainer) == null ||
                (b as IContainer) == null)
                return false;

            if (a is Player && a != p ||
                b is Player && b != p)
                return false;

            if (a is Container contA &&
                contA.BagOwners.Length > 0 &&
                !contA.BagOwners.Contains(p.AccountId))
                return false;

            if (b is Container contB &&
                contB.BagOwners.Length > 0 &&
                !contB.BagOwners.Contains(p.AccountId))
                return false;

            if (a is OneWayContainer && b != p ||
                b is OneWayContainer && a != p)
                return false;

            var aPos = new Vector2(a.X, a.Y);
            var bPos = new Vector2(b.X, b.Y);
            if (Vector2.DistanceSquared(aPos, bPos) > 1)
                return false;

            return true;
        }

        private bool ValidateSlotSwap(Player player, IContainer conA, IContainer conB, int slotA, int slotB)
        {
            return
                (slotA < 12 && slotB < 12 || player.HasBackpack) &&
                conB.AuditItem(conA.Inventory[slotA], slotB) &&
                conA.AuditItem(conB.Inventory[slotB], slotA);
        }

        private bool ValidateItemSwap(Player player, Entity c, Item item)
        {
            return c == player ||
                   item == null ||
                   !item.Soulbound && !player.Client.Account.Admin ||
                   IsSoleContainerOwner(player, c as IContainer);
        }

        private static bool IsSoleContainerOwner(Player player, IContainer con)
        {
            int[] owners = null;
            if (con is Container container)
                owners = container.BagOwners;

            return owners != null && owners.Length == 1 && owners.Contains(player.AccountId);
        }

        private static void DropInSoulboundBag(Player player, Item item)
        {
            var container = new Container(player.Manager, 0x0503, 1000 * 60, true)
            {
                BagOwners = new[] { player.AccountId },
                Inventory = { [0] = item }
            };
            container.Move(player.X + (float)((Rand.NextDouble() * 2 - 1) * 0.5),
                           player.Y + (float)((Rand.NextDouble() * 2 - 1) * 0.5));
            container.SetDefaultSize(75);
            player.Owner.EnterWorld(container);
        }
    }
}