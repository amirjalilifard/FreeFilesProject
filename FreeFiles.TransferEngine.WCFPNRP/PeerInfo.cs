using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace FreeFiles.TransferEngine.WCFPNRP
{
    sealed class PeerInfo
    {
        public string HostName { get; private set; }
        
        public int Port { get; private set; }
        
        public string Comment { get; set; }
                
        public string Classifier { get; set; }

        public PeerInfo(string hostName,string Classifier, int port)
        {
            this.HostName = hostName;            
            this.Port = port;
            this.Classifier = Classifier;
        }
    }
}
