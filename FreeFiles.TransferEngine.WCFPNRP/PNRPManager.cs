using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.PeerToPeer;
using System.ServiceModel;
using System.IO;

namespace FreeFiles.TransferEngine.WCFPNRP
{
    class PNRPManager
    {       
        public PNRPManager(string classifierName, int port)
        {
            this.ClassifierName = classifierName;
            this.Port= port;
            peerName = new PeerName(classifierName, PeerNameType.Unsecured);
            registrations = new List<PeerNameRegistration>();
            registrations.Add(new PeerNameRegistration(peerName, this.Port, Cloud.AllLinkLocal) { UseAutoEndPointSelection=true });
            //registrations.Add(new PeerNameRegistration(peerName, this.Port, Cloud.Global) { UseAutoEndPointSelection = true });
        }

        public string ClassifierName { get; private set; }
        public  int Port { get; private set; }        
        List<PeerNameRegistration> registrations = null;
        PeerName peerName = null;
        public List<PeerInfo> CurrentPOeerRegistrationInfo { get; private set; }

        public List<PeerInfo>  Register()
        {
            List<PeerInfo>  registerdPeer = new List<PeerInfo>();
            foreach (var registration in registrations)
            {
                string timeStamp = string.Format("FreeFile Peer Crearted at : {0}", DateTime.Now.ToShortTimeString());
                registration.Comment = timeStamp;
                try
                {
                    try
                    {
                        registration.Start();
                        if (registerdPeer.FirstOrDefault(x => x.HostName == registration.PeerName.PeerHostName) == null)
                        {
                            PeerInfo peerInfo = new PeerInfo(registration.PeerName.PeerHostName, registration.PeerName.Classifier, registration.Port);
                            peerInfo.Comment = registration.Comment;
                            registerdPeer.Add(peerInfo);
                        }
                    }
                    catch { }
                }
                catch (PeerToPeerException PEX)
                {
                    throw new Exception(PEX.InnerException.Message);
                }
            }
            this.CurrentPOeerRegistrationInfo = registerdPeer;                    
            return registerdPeer;
        }      

        public void Leave()
        {
            foreach (var registration in registrations)
            {
                registration.Stop();   
            }            
        }
      

        public List<PeerInfo> ResolveByPeerHostName(string peerHostName)
        {
            try
            {
                if (string.IsNullOrEmpty(peerHostName))
                    throw new ArgumentException("Cannot have a null or empty host peer name.");

                PeerNameResolver resolver = new PeerNameResolver();
                List<PeerInfo> foundPeers = new List<PeerInfo>();
                var resolvedName = resolver.Resolve(new PeerName(peerHostName, PeerNameType.Unsecured), Cloud.AllLinkLocal);                
                foreach (var foundItem in resolvedName)
                {
                    foreach (var endPointInfo in foundItem.EndPointCollection)
                    {
                        PeerInfo peerInfo = new PeerInfo(foundItem.PeerName.PeerHostName, foundItem.PeerName.Classifier,endPointInfo.Port);
                        peerInfo.Comment = foundItem.Comment;
                        foundPeers.Add(peerInfo);
                    }

                }
                return foundPeers;
               
            }
            catch (PeerToPeerException px)
            {
                throw new Exception(px.InnerException.Message);
            }

        }        
    }
}
