using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace FreeFiles.TransferEngine.WCFPNRP.WCFFileTransferService
{
     sealed class FileTransferServiceHost
    {
        public void DoHost(List<PeerInfo> peers)
        {
            Uri[] Uris = new Uri[peers.Count];

            string Address = string.Empty;
            for (int i = 0; i < peers.Count; i++)
            {
                Address = string.Format("net.tcp://{0}:{1}/TransferEngine", peers[i].HostName, peers[i].Port);
                Uris[i] = new Uri(Address);
            }

            FileTransferServiceClass currentPeerServiceProxy = new FileTransferServiceClass();
            ServiceHost _serviceHost = new ServiceHost(currentPeerServiceProxy, Uris);
            NetTcpBinding tcpBinding = new NetTcpBinding(SecurityMode.None);
            _serviceHost.AddServiceEndpoint(typeof(IFileTransferService), tcpBinding, "");

            _serviceHost.Open();
        }
    }
}
