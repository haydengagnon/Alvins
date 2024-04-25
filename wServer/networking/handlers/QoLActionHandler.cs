using wServer.networking.packets;
using wServer.networking.packets.incoming;
using wServer.realm.entities;

namespace wServer.networking.handlers
{
    internal class QoLActionHandler : PacketHandlerBase<QoLAction>
    {
        public override PacketId ID => PacketId.QOLACTION;

        protected override void HandlePacket(Client client, QoLAction packet)
        {
            client.Manager.Core.addPendingAction(() => Handle(client.Player, packet));
        }

        private static void Handle(Player player, QoLAction packet)
        {
            switch (packet.ActionId)
            {
                case 1:
                    if (player.SorStorage >= 100)
                    {
                        var acc = player.Client.Account;
                        player.Client.Manager.Database.UpdateSorStorage(acc, -100);
                        player.SorStorage -= 100;
                        player.ForceUpdate(player.SorStorage);
                        player.SendHelp("You now have " + player.SorStorage + " sacred fragments left. A Sacred Essence has been sent to your vault!");
                        player.Client.Manager.Database.AddGift(acc, 0x7190);
                    }
                    else
                    {
                        player.SendError("You can't construct a Sacred Essence with less than 100 fragments.");
                    }
                    break;

                case 2:
                    if (player.AirStorage >= 100)
                    {
                        var acc = player.Client.Account;
                        player.Client.Manager.Database.UpdateAirStorage(acc, -100);
                        player.AirStorage -= 100;
                        player.ForceUpdate(player.AirStorage);
                        player.SendHelp("You now have " + player.AirStorage + "air fragments left. An Air Essence has been sent to your vault!");
                        player.Client.Manager.Database.AddGift(acc, 0x7190);
                    }
                    else
                    {
                        player.SendError("You can't construct an Air Essence with less than 100 fragments.");
                    }
                    break;

                case 3:
                    if (player.WaterStorage >= 100)
                    {
                        var acc = player.Client.Account;
                        player.Client.Manager.Database.UpdateWaterStorage(acc, -100);
                        player.WaterStorage -= 100;
                        player.ForceUpdate(player.WaterStorage);
                        player.SendHelp("You now have " + player.WaterStorage + " water fragments left. A Water Essence has been sent to your vault!");
                        player.Client.Manager.Database.AddGift(acc, 0x7190);
                    }
                    else
                    {
                        player.SendError("You can't construct a Water Essence with less than 100 fragments.");
                    }
                    break;

                case 4:
                    if (player.EarthStorage >= 100)
                    {
                        var acc = player.Client.Account;
                        player.Client.Manager.Database.UpdateEarthStorage(acc, -100);
                        player.EarthStorage -= 100;
                        player.ForceUpdate(player.EarthStorage);
                        player.SendHelp("You now have " + player.EarthStorage + " earth fragments left. An Earth Essence has been sent to your vault!");
                        player.Client.Manager.Database.AddGift(acc, 0x7190);
                    }
                    else
                    {
                        player.SendError("You can't construct an Earth Essence with less than 100 fragments.");
                    }
                    break;

                case 5:
                    if (player.FireStorage >= 100)
                    {
                        var acc = player.Client.Account;
                        player.Client.Manager.Database.UpdateFireStorage(acc, -100);
                        player.FireStorage -= 100;
                        player.ForceUpdate(player.FireStorage);
                        player.SendHelp("You now have " + player.FireStorage + " fire fragments left. A Fire Essence has been sent to your vault!");
                        player.Client.Manager.Database.AddGift(acc, 0x7190);
                    }
                    else
                    {
                        player.SendError("You can't construct a Fire Essence with less than 100 fragments.");
                    }
                    break;

                default:
                    player.SendError("Inproper action ID.");
                    break;
            }
        }
    }
}
