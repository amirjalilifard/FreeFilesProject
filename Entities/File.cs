using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public sealed class File
    {
        public string FileName { get; set; }
        public int FileSize { get; set; }
        public string FileType { get; set; }
        public Guid PeerID { get; set; }
        public string PeerHostName { get; set; }
    }
}
