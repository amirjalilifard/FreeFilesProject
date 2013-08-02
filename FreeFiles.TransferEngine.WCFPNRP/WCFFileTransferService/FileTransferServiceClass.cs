using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;


namespace FreeFiles.TransferEngine.WCFPNRP.WCFFileTransferService
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single, UseSynchronizationContext = false)]
     class FileTransferServiceClass :IFileTransferService
    {

        public byte[] TransferFileByHash(string fileName,string hash, long partNumber)
        {
            return FileReader.GetFileBytes(fileName,hash, partNumber);
        }

        public byte[] TransferFile(string fileName, long partNumber)
        {
            return FileReader.GetFileBytes(fileName, partNumber);
        }
    }
}