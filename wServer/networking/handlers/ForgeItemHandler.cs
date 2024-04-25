using System;
using System.Collections.Generic;
using System.Linq;
using common.resources;
using wServer.networking.packets;
using wServer.networking.packets.incoming;
using wServer.networking.packets.outgoing;
using wServer.realm.worlds;
using static wServer.realm.ForgeList;

namespace wServer.networking.handlers
{
    internal class ForgeItemHandler : PacketHandlerBase<ForgeItem>
    {
        public override PacketId ID => PacketId.FORGEITEM;
        private readonly Random rng = new Random();

        protected override void HandlePacket(Client client, ForgeItem packet)
        {
            Handle(client, packet);
        }

        private void Handle(Client client, ForgeItem packet)
        {
            if (client.Player.Inventory[packet.Top.SlotId].ObjectType != packet.Top.ObjectType || client.Player.Inventory[packet.Bottom.SlotId].ObjectType != packet.Bottom.ObjectType)
                return;
            if (packet.Top.SlotId == packet.Bottom.SlotId)
                return;
            if (packet.Top.SlotId < 4 || packet.Bottom.SlotId < 4)
            {
                client.Player.SendError("Hey, it looks like you tried to forge with an item from your equipment slot, unequip it if you want to forge with it!");
                return;
            }

            string ItemName = HandleForge(client, packet);

            if (ItemName == FailedForge)
            {
                client.Player.SendError("You were unable to forge anything with these...");
                return;
            }

            ushort ForgedItem = client.Player.Manager.Resources.GameData.DisplayIdToObjectType[ItemName];
            Item item = client.Player.Manager.Resources.GameData.Items[ForgedItem];

            client.Player.Inventory[packet.Top.SlotId] = null;
            client.Player.Inventory[packet.Bottom.SlotId] = null;

            for (int i = 4; i <= (client.Player.HasBackpack ? 19 : 11); i++)
                if (client.Player.Inventory[i] == null)
                {
                    client.Player.Inventory[i] = item;
                    break;
                }

            if (ItemName.Contains("Greater Potion of") || ItemName.Contains("Vial of"))
            {
                client.Player.SendInfoFormat("You just forged [{0}]", item.DisplayName);
                return;
            }

            foreach (var p in client.Player.Owner.Players.Values)
                if (item.Sacred)
                {
                    p.SendSacredAnnounce("[{0}] has just forged the sacred item [{1}]", client.Player.Name, item.ObjectId);
                }
                else if (item.Legendary)
                {
                    p.SendSacredAnnounce("[{0}] has just forged the legendary item [{1}]", client.Player.Name, item.ObjectId);
                }
                else if (item.GodSlayer)
                {
                    p.SendSacredAnnounce("[{0}] has ALVINED their weapon into [{1}]!", client.Player.Name, item.ObjectId);
                }
                else
                    p.SendInfoFormat("[{0}] has just forged [{1}]", client.Player.Name, item.ObjectId);

            if (client.Account.GuildId > 0)
                foreach (var w in client.Manager.Worlds.Values)
                    foreach (var p in w.Players.Values)
                        if (p.Client.Account.GuildId == client.Account.GuildId && p.Owner.Id != World.Nexus)
                            if (item.Sacred)
                                p.SendSacredAnnounce("[{0}] has just forged the sacred item [{1}]", client.Player.Name, item.ObjectId);
                            else if (item.Legendary)
                                p.SendSacredAnnounce("[{0}] has just forged the legendary item [{1}]", client.Player.Name, item.ObjectId);
                            else if (item.GodSlayer)
                                p.SendSacredAnnounce("[{0}] has ALVINED their weapon into [{1}]!", client.Player.Name, item.ObjectId);
                            else
                                p.SendInfoFormat("[{0}] has just forged [{1}]", client.Player.Name, item.ObjectId);
        }


        private string HandleForge(Client client, ForgeItem packet)
        {
            Item top = client.Player.Manager.Resources.GameData.Items[(ushort)packet.Top.ObjectType];
            Item bottom = client.Player.Manager.Resources.GameData.Items[(ushort)packet.Bottom.ObjectType];

            Dictionary<string[], Tuple<string, string>> List;
            ForgeData Type;
            switch (bottom.ObjectId)
            {
                case "Sacred Essence":
                    List = SacredList;
                    Type = ForgeData.Sacred;
                    break;
                case "Spirit of the Past":
                    List = SacredList;
                    Type = ForgeData.Rusted;
                    if (top.Legendary)
                    {
                        List = NightmareList;
                    }
                    break;
                case "Godly Remnant":
                    List = GodlyList;
                    Type = ForgeData.Godly;
                    break;
                case "Legendary Essence":
                    List = NightmareList;
                    Type = ForgeData.Nightmare;
                    break;
                case "Change of Heart":
                    if (top.Sacred)
                    {
                        List = SCHeart;
                        Type = ForgeData.HeartSC;
                    }
                    else if (top.Legendary)
                    {
                        List = LGHeart;
                        Type = ForgeData.HeartLG;
                    }
                    else
                    {
                        List = OtherList;
                        Type = ForgeData.Other;
                    }
                    break;
                default:
                    List = OtherList;
                    Type = ForgeData.Other;
                    break;
            }

            if (Type == ForgeData.Sacred || Type == ForgeData.Godly || Type == ForgeData.Other || Type == ForgeData.Nightmare)
                foreach (var r in List)
                    if (top.ObjectId == r.Value.Item1 && bottom.ObjectId == r.Value.Item2)
                        return r.Key[rng.Next(r.Key.Length)];

            if (Type == ForgeData.Rusted)
                foreach (var r in List)
                    if (r.Key.Contains(top.ObjectId))
                        return r.Value.Item1;

            if (Type == ForgeData.HeartSC || Type == ForgeData.HeartLG)
                foreach (var r in List)
                    if (r.Key.Contains(top.ObjectId))
                        return r.Key[rng.Next(r.Key.Length)];

            return FailedForge;
        }

    }
}