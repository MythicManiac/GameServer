﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENet;
using LeagueSandbox.GameServer.Logic.Packets;

namespace LeagueSandbox.GameServer.Core.Logic.PacketHandlers.Packets
{
    class HandleClick : IPacketHandler
    {
        public bool HandlePacket(Peer peer, byte[] data, Game game)
        {
            var click = new Click(data);
            Logger.LogCoreInfo("Object " + game.GetPeerInfo(peer).GetChampion().getNetId() + " clicked on " + click.targetNetId);

            return true;
        }
    }
}
