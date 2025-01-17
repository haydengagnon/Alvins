﻿using common;
using System;
using System.Linq;
using wServer.networking.packets;
using wServer.networking.packets.incoming;
using wServer.networking.packets.outgoing;
using wServer.realm;

namespace wServer.networking.handlers
{
    internal class HelloHandler : PacketHandlerBase<Hello>
    {
        public override PacketId ID => PacketId.HELLO;

        protected override void HandlePacket(Client client, Hello packet)
        {
            //client.Manager.Logic.AddPendingAction(t => Handle(client, packet));
            Handle(client, packet);
        }

        private void Handle(Client client, Hello packet)
        {
            // validate connection eligibility and get acc info
            var acc = VerifyConnection(client, packet);

            if (acc == null)
            {
                client.SendFailure("You must be registered to play.");
                return;
            }

            // log ip
            client.Manager.Database.LogAccountByIp(client.IP, acc.AccountId);
            acc.IP = client.IP;
            acc.FlushAsync();

            client.Account = acc;
            client.GUID = packet.GUID;
            client.IgnoreAllyProjectiles = packet.IgnoreAllyProjectiles;
            client.IgnoreAllyDamageText = packet.IgnoreAllyDamageText;
            client.Manager.Core.addPendingAction(() => client.Manager.ConMan.Add(new ConInfo(client, packet)));
        }

        private DbAccount VerifyConnection(Client client, Hello packet)
        {
            ServerSettings ss = client.Manager.Config.serverSettings;
            var version = client.Manager.Config.serverSettings.version;
            if (!version.Equals(packet.BuildVersion))
            {
                client.SendFailure("Invalid client version, CHECK #client CHANNEL", Failure.MessageWithDisconnect);
                return null;
            }

            if(Program.CP.IsEnabled)
            {
                if (!client.IsAir)
                {
                    client.SendFailure("Invalid client version, CHECK #client CHANNEL", Failure.MessageWithDisconnect);
                    return null;
                } else
                {
                    if (Program.CP.Hashs[client.TokenId] != packet.Sig)
                    {
                        client.SendFailure("Invalid client version, CHECK #client CHANNEL", Failure.MessageWithDisconnect);
                        return null;
                    }
                }

                /*Console.WriteLine("Time: " + packet.Time);
                Console.WriteLine("Sig: " + packet.Sig);
                Console.WriteLine("Token Id: " + client.TokenId);
                Console.WriteLine("Expected Sig: " + ss.sigs[client.TokenId]);*/
            }

            var s1 = client.Manager.Database.Verify(packet.GUID, packet.Password, out DbAccount acc);
            if (s1 == LoginStatus.AccountNotExists)
            {
                var s2 = client.Manager.Database.Register(packet.GUID, packet.Password, true, out acc);
                if (s2 != RegisterStatus.OK)
                {
                    client.SendFailure("Bad Login", Failure.MessageWithDisconnect);
                    return null;
                }
            }
            else if (s1 == LoginStatus.InvalidCredentials)
            {
                client.SendFailure("Bad Login", Failure.MessageWithDisconnect);
                return null;
            }

            if (acc.NameChosen != true)
            {
                client.SendFailure("You must choose a name before playing.", Failure.MessageWithDisconnect);
                return null;
            }

            if (acc.Banned)
            {
                client.SendFailure("Failed to log in, account is banned.", Failure.MessageWithDisconnect);
                return null;
            }

            if (client.Manager.Database.IsIpBanned(client.IP))
            {
                client.SendFailure("Failed to log in, account is banned.", Failure.MessageWithDisconnect);
                return null;
            }

            if (!acc.Admin && client.Manager.Config.serverInfo.adminOnly)
            {
                client.SendFailureDialog("Admin Only Server", $"Only admins can play on {client.Manager.Config.serverInfo.name}.");
                return null;
            }

            var minRank = client.Manager.Config.serverInfo.minRank;
            if (acc.Rank < minRank)
            {
                client.SendFailureDialog("Rank Required Server", $"You need a minimum server rank of {minRank} to play on {client.Manager.Config.serverInfo.name}.");
                return null;
            }
            /*
            var excludeRank = client.Manager.Config.serverInfo.excludeRank;
            if (acc.Rank == excludeRank)
            {
                client.SendFailureDialog("Rank Required Server", $"This type of account is not allowed to play on {client.Manager.Config.serverInfo.name}.");
                return null;
            }
            */
            if (client.Manager.Clients.Where(c => c.Key.IP.Equals(client.IP)).ToArray().Length > 5)
            {
                client.SendFailureDialog("IP in use", "Cannot connect to the server from your ip.");
                return null;
            }
            return acc;
        }
    }
}
