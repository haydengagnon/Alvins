using wServer.networking.packets;
using wServer.networking.packets.incoming;
using Player = wServer.realm.entities.Player;

namespace wServer.networking.handlers
{
    internal class UnboxRequestHandler : PacketHandlerBase<UnboxRequest>
    {
        public override PacketId ID => PacketId.UNBOXREQUEST;

        protected override void HandlePacket(Client client, UnboxRequest packet)
        {
            client.Manager.Core.addPendingAction(() => Handle(client.Player, packet));
        }

        private void Handle(Player player, UnboxRequest packet)
        {
            var acc = player.Client.Account;

            switch (packet.lootboxType)
            {
                case 1:
                    if (player.BronzeLootbox >= 1)
                    {
                        player.Client.Manager.Database.UpdateBronzeLootbox(acc, -1);
                        player.BronzeLootbox -= 1;
                        player.ForceUpdate(player.BronzeLootbox);
                        player.Unbox(1);
                    }
                    else
                    {
                        player.SendError("You do not have any lootboxes to open!");
                    }
                    break;

                case 2:
                    if (player.SilverLootbox >= 1)
                    {
                        player.Client.Manager.Database.UpdateSilverLootbox(acc, -1);
                        player.SilverLootbox -= 1;
                        player.ForceUpdate(player.SilverLootbox);
                        player.Unbox(2);
                    }
                    else
                    {
                        player.SendError("You do not have any lootboxes to open!");
                    }
                    break;

                case 3:
                    if (player.GoldLootbox >= 1)
                    {
                        player.Client.Manager.Database.UpdateGoldLootbox(acc, -1);
                        player.GoldLootbox -= 1;
                        player.ForceUpdate(player.GoldLootbox);
                        player.Unbox(3);
                    }
                    else
                    {
                        player.SendError("You do not have any lootboxes to open!");
                    }
                    break;

                case 4:
                    if (player.EliteLootbox >= 1)
                    {
                        player.Client.Manager.Database.UpdateEliteLootbox(acc, -1);
                        player.EliteLootbox -= 1;
                        player.ForceUpdate(acc.EliteLootbox);
                        player.Unbox(4);
                    }
                    else
                    {
                        player.SendError("You do not have any lootboxes to open or you don't have the sufficient amount of onrane!");
                    }
                    break;

                case 5:
                    if (player.Kantos >= 5000)
                    {
                        player.Client.Manager.Database.UpdateKantos(acc, -5000);
                        player.Kantos -= 5000;
                        player.ForceUpdate(player.Kantos);
                        player.Unbox(5);
                    }
                    else
                    {
                        player.SendError("You do not have the sufficient amount of Kantos to open this box.");
                    }
                    break;

                case 6:
                    if (acc.Credits >= 10000000)
                    {
                        player.Client.Manager.Database.UpdateCredit(acc, -10000000);
                        player.Credits -= 10000000;
                        player.ForceUpdate(acc.Credits);
                        player.Unbox(6);
                    }
                    else
                    {
                        player.SendError("You do not have the sufficient amount of Gold to open this box.");
                    }
                    break;

                case 7:
                    if (player.EventLootbox >= 1)
                    {
                        player.Client.Manager.Database.UpdateEventLootbox(acc, -1);
                        player.EventLootbox -= 1;
                        player.ForceUpdate(player.EventLootbox);
                        player.Unbox(7);
                    }
                    else
                    {
                        player.SendError("You do not have any lootboxes to open!");
                    }
                    break;
            }
        }
    }
}
