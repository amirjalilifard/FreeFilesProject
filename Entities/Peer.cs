using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public sealed class Peer
    {
        public Guid PeerID { get; set; }
        public string PeerHostName { get; set; }
        public string Comments { get; set; }
    }
}
