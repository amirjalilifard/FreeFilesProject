using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FreeFile.DownloadManager.Abstract;
using System.ServiceModel;
using FreeFile.DownloadManager;
using FreeFiles.TransferEngine.WCFPNRP.WCFFileTransferService;
using FreeFiles.TransferEngine.WCFPNRP.WCFFileTransferClient;

namespace FreeFiles.TransferEngine.WCFPNRP
{
    public class PnrpTransferEngine : ITransferEngine, IFileProviderServer
    {
        static readonly object SharedObject = new object();
        static PNRPManager pnrpManager;
        public PnrpTransferEngine(string localServerClassifier, int localServerPort)
        {
            if (pnrpManager == null)
            {
                lock (SharedObject)
                {
                    if (pnrpManager == null)
                    {

                        pnrpManager = new PNRPManager(localServerClassifier, localServerPort);
                    }
                }
            }
        }

        byte[] ITransferEngine.GetFile(string filename, string hash, long partNumber, string hostName)
        {
            var peers = pnrpManager.ResolveByPeerHostName(hostName);
            byte[] data = null;
            if (peers != null && peers.Count > 0)
            {
                bool dataretrived = false;
                FileTransferServiceClientClass Client = null;
                System.ServiceModel.Channels.Binding netBinding = new NetTcpBinding(SecurityMode.None);

                //foreach (var peer in peers)
                //{
                EndpointAddress endpointAddress = new EndpointAddress(string.Format("net.tcp://{0}:{1}/TransferEngine", peers.FirstOrDefault().HostName, peers.FirstOrDefault().Port));
                    Client = new FileTransferServiceClientClass(netBinding, endpointAddress);
                    try
                    {
                        data = Client.TransferFile(filename, hash, partNumber);
                        dataretrived = true;
                        //break;
                    }
                    catch
                    {
                    }
                //}
                if (!dataretrived) throw new HostUnreachableException(hostName);
            }
            else throw new HostUnreachableException(hostName);
            return data;

        }

        byte[] ITransferEngine.GetFile(string filename, long partNumber, string hostName)
        {
            var peers = pnrpManager.ResolveByPeerHostName(hostName);
            byte[] data = null;
            if (peers != null && peers.Count > 0)
            {
                bool dataretrived = false;
                FileTransferServiceClientClass Client = null;
                System.ServiceModel.Channels.Binding netBinding = new NetTcpBinding(SecurityMode.None);

                //foreach (var peer in peers)
                //{
                EndpointAddress endpointAddress = new EndpointAddress(string.Format("net.tcp://{0}:{1}/TransferEngine", peers.FirstOrDefault().HostName, peers.FirstOrDefault().Port));
                    Client = new FileTransferServiceClientClass(netBinding, endpointAddress);
                    try
                    {
                        //########
                        data = Client.TransferFile(filename, partNumber);
                        dataretrived = true;
                        //break;
                    }
                    catch
                    {
                    }
                //}
                if (!dataretrived) throw new HostUnreachableException(hostName);
            }
            else throw new HostUnreachableException(hostName);
            return data;

        }


        void IFileProviderServer.SetupFileServer()
        {

            var peers = pnrpManager.Register();
            if (peers == null || peers.Count == 0) throw new Exception("Host not registerd!");
            var fileTransferServiceHost = new FileTransferServiceHost();
            fileTransferServiceHost.DoHost(peers);
        }
    }
}
