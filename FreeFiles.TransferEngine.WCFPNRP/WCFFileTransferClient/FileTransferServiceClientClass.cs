using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using FreeFiles.TransferEngine.WCFPNRP.WCFFileTransferService;

namespace FreeFiles.TransferEngine.WCFPNRP.WCFFileTransferClient
{
        [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single, UseSynchronizationContext = false)]
         class FileTransferServiceClientClass : System.ServiceModel.ClientBase<IFileTransferService>
        {
            public FileTransferServiceClientClass() :base()
            {
            }

            public FileTransferServiceClientClass(string endpointConfigurationName) : 
                    base(endpointConfigurationName)
            {
            }

            public FileTransferServiceClientClass(string endpointConfigurationName, string remoteAddress) : 
                    base(endpointConfigurationName, remoteAddress)
            {
            }

            public FileTransferServiceClientClass(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                    base(endpointConfigurationName, remoteAddress)
            {
            }

            public FileTransferServiceClientClass(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                    base(binding, remoteAddress)
            {

            }

            public byte[] TransferFile(string fileName,string hash, long partNumber)
            {
                byte[] _bytes = base.Channel.TransferFileByHash(fileName, hash, partNumber);
                return _bytes;
            }

            public byte[] TransferFile(string fileName, long partNumber)
            {
                //##
                byte[] _bytes = base.Channel.TransferFile(fileName, partNumber);
                return _bytes;
            }
        }
    
}
