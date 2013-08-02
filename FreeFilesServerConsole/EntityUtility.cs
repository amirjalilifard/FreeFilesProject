using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeFilesServerConsole
{
    static class EntityUtility
    {
        public static EF.Peer externalPeerToEFPeer(Entities.Peer peer)
        {
            EF.Peer EFPeer = new EF.Peer();
            EFPeer.PeerHostName = peer.PeerHostName;
            EFPeer.Comments = peer.Comments;
            EFPeer.PeerID = peer.PeerID;
            return EFPeer;
        }
    }
}
