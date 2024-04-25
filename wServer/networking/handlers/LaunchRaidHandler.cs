using wServer.networking.packets;
using wServer.networking.packets.incoming;
using wServer.networking.packets.outgoing;
using wServer.realm;
using wServer.realm.entities;

namespace wServer.networking.handlers
{
    internal class LaunchRaidHandler : PacketHandlerBase<LaunchRaid>
    {
        public override PacketId ID => PacketId.LAUNCH_RAID;

        protected override void HandlePacket(Client client, LaunchRaid packet)
        {
            client.Manager.Core.addPendingAction(() => Handle(client.Player, packet));
        }

        public void launchRaid(Player player, int gold, bool ultra, int raidId)
        {
            var playerSvr = player.Manager.Config.serverInfo.name;
            var Manager = player.Manager;
            var gameData = Manager.Resources.GameData;

            if (player.Credits >= gold && player.Owner.Name.Equals("Tavern"))
            {
                switch (raidId)
                {
                    case 1:
                        if (ultra == false)
                        {
                            if (player.startRaid1(player) == false)
                            {
                                player.Client.Manager.Database.UpdateCredit(player.Client.Account, -gold);
                                player.Credits = player.Client.Account.Credits - gold;
                                player.ForceUpdate(player.Credits);
                                player.Manager.Chat.Announce("The Zol Awakening Raid has been launched on " + playerSvr + "!");

                                Manager._isRaidLaunched = true;

                                if (!gameData.IdToObjectType.TryGetValue("Aldragine's Hideout Portal", out var objType) ||
                                        !gameData.Portals.ContainsKey(objType))
                                    return;
                                var timeoutTime = gameData.Portals[objType].Timeout;
                                var entity = Entity.Resolve(Manager, objType);
                                entity.Move(6f, 16f);
                                player.Owner.EnterWorld(entity);

                                (entity as Portal).PlayerOpened = true;
                                (entity as Portal).Opener = player.Name;

                                player.Owner.Timers.Add(new WorldTimer(timeoutTime * 2000, (w) =>
                                {
                                    if (w == null || w.Deleted || entity == null) return;

                                    w.LeaveWorld(entity);
                                }));
                                player.Owner.Timers.Add(new WorldTimer(60000, (w) =>
                                {
                                    if (w == null || w.Deleted) return;

                                    w.Manager._isRaidLaunched = false;
                                }));
                                player.Owner.BroadcastPacket(new Notification
                                {
                                    Color = new ARGB(0xFF00FF00),
                                    ObjectId = player.Id,
                                    Message = player.Name + " has launched the Zol Awakening Raid!"
                                }, null);
                                player.Owner.Opener = player.Name;
                            }
                            else
                            {
                                player.SendError("You need the correct token in your inventory to launch this raid.");
                            }
                        }
                        else
                        {
                            if (player.startRaid1(player) == false)
                            {
                                player.Client.Manager.Database.UpdateCredit(player.Client.Account, -gold);
                                player.Credits = player.Client.Account.Credits - gold;
                                player.ForceUpdate(player.Credits);
                                player.Manager.Chat.Announce("The Ultra Zol Awakening Raid has been launched on " + playerSvr + "!");

                                Manager._isRaidLaunched = true;
                                if (!gameData.IdToObjectType.TryGetValue("Ultra Aldragine's Hideout Portal", out ushort objType) ||
                                    !gameData.Portals.ContainsKey(objType))
                                    return;
                                var entity = Entity.Resolve(Manager, objType);

                                entity.Move(6f, 16f);
                                player.Owner.EnterWorld(entity);

                                (entity as Portal).PlayerOpened = true;
                                (entity as Portal).Opener = player.Name;
                                var timeoutTime = gameData.Portals[objType].Timeout;
                                player.Owner.Timers.Add(new WorldTimer(timeoutTime * 2000, (w) =>
                                {
                                    if (w == null || w.Deleted || entity == null) return;

                                    w.LeaveWorld(entity);
                                }));
                                player.Owner.Timers.Add(new WorldTimer(60000, (w) =>
                                {
                                    if (w == null || w.Deleted) return;

                                    w.Manager._isRaidLaunched = false;
                                }));
                                player.Owner.BroadcastPacket(new Notification
                                {
                                    Color = new ARGB(0xFF00FF00),
                                    ObjectId = player.Id,
                                    Message = player.Name + " has launched the Ultra Zol Awakening Raid!"
                                }, null);
                                //set raid opener
                                player.Owner.Opener = player.Name;
                            }
                            else
                            {
                                player.SendError("You need the correct token in your inventory to launch this raid.");
                            }
                        }
                        break;

                    case 2:
                        if (ultra == false)
                        {
                            if (player.startRaid2(player) == false)
                            {
                                player.Client.Manager.Database.UpdateCredit(player.Client.Account, -gold);
                                player.Credits = player.Client.Account.Credits - gold;
                                player.ForceUpdate(player.Credits);
                                player.Manager.Chat.Announce("The Calling of the Titan Raid has been launched on " + playerSvr + "!");

                                Manager._isRaidLaunched = true;

                                if (!gameData.IdToObjectType.TryGetValue("Bastille of Drannol Portal", out ushort objType) ||
                                        !gameData.Portals.ContainsKey(objType))
                                    return;
                                var timeoutTime = gameData.Portals[objType].Timeout;
                                var entity = Entity.Resolve(Manager, objType);
                                entity.Move(11f, 10f);
                                player.Owner.EnterWorld(entity);

                                (entity as Portal).PlayerOpened = true;
                                (entity as Portal).Opener = player.Name;

                                player.Owner.Timers.Add(new WorldTimer(timeoutTime * 2000, (w) =>
                                {
                                    if (w == null || w.Deleted || entity == null) return;

                                    w.LeaveWorld(entity);
                                }));
                                player.Owner.Timers.Add(new WorldTimer(60000, (w) =>
                                {
                                    if (w == null || w.Deleted) return;

                                    w.Manager._isRaidLaunched = false;
                                }));
                                player.Owner.BroadcastPacket(new Notification
                                {
                                    Color = new ARGB(0xFF00FF00),
                                    ObjectId = player.Id,
                                    Message = player.Name + " has launched the Calling of the Titan Raid!"
                                }, null);
                                //set raid opener
                                player.Owner.Opener = player.Name;
                            }
                            else
                            {
                                player.SendError("You need the correct token in your inventory to launch this raid.");
                            }
                        }
                        else
                        {
                            if (player.startRaid2(player) == false)
                            {
                                player.Client.Manager.Database.UpdateCredit(player.Client.Account, -gold);
                                player.Credits = player.Client.Account.Credits - gold;
                                player.ForceUpdate(player.Credits);
                                player.Manager.Chat.Announce("The Ultra Calling of the Titan Raid has been launched on " + playerSvr + "!");

                                Manager._isRaidLaunched = true;
                                if (!gameData.IdToObjectType.TryGetValue("Ultra Bastille of Drannol Portal", out ushort objType) ||
                                    !gameData.Portals.ContainsKey(objType))
                                    return;
                                var entity = Entity.Resolve(Manager, objType);

                                entity.Move(11f, 10f);
                                player.Owner.EnterWorld(entity);

                                (entity as Portal).PlayerOpened = true;
                                (entity as Portal).Opener = player.Name;
                                var timeoutTime = gameData.Portals[objType].Timeout;
                                player.Owner.Timers.Add(new WorldTimer(timeoutTime * 2000, (w) =>
                                {
                                    if (w == null || w.Deleted || entity == null) return;

                                    w.LeaveWorld(entity);
                                }));
                                player.Owner.Timers.Add(new WorldTimer(60000, (w) =>
                                {
                                    if (w == null || w.Deleted) return;

                                    w.Manager._isRaidLaunched = false;
                                }));
                                player.Owner.BroadcastPacket(new Notification
                                {
                                    Color = new ARGB(0xFF00FF00),
                                    ObjectId = player.Id,
                                    Message = player.Name + " has launched the Ultra Calling of the Titan Raid!"
                                }, null);
                                //set raid opener
                                player.Owner.Opener = player.Name;
                            }
                            else
                            {
                                player.SendError("You need the correct token in your inventory to launch this raid.");
                            }
                        }
                        break;
                }
            }
            else
            {
                player.SendError("You must be in the Tavern to pop this raid.");
            }
        }

        private void Handle(Player player, LaunchRaid packet)
        {
            var acc = player.Client.Account;
            if (player.Manager._isRaidLaunched == false)
            {
                if (player.Stars >= 0)
                {
                    switch (packet.RaidId)
                    {
                        case 1:
                            if (packet.Ultra == false)
                            {
                                launchRaid(player, 10000, false, 1);
                            }
                            else
                            {
                                launchRaid(player, 10000, true, 1);
                            }
                            break;

                        case 2:
                            if (packet.Ultra == false)
                            {
                                launchRaid(player, 10000, false, 2);
                            }
                            else
                            {
                                launchRaid(player, 10000, true, 2);
                            }
                            break;
                    }
                }
                else
                {
                    player.SendError("You need at least 20 stars to launch a raid.");
                }
            }
            else
            {
                player.SendError("A raid has already been launched earlier.");
            }
        }
    }
}
