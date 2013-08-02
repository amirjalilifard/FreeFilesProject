using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace FreeFiles.TransferEngine.WCFPNRP.WCFFileTransferService
{    
    [ServiceContractAttribute]
     interface IFileTransferService
    {
        [OperationContractAttribute(IsOneWay = false)]
        byte[] TransferFileByHash(string fileName,string hash, long partNumber);
        
        [OperationContractAttribute(IsOneWay = false)]
        byte[] TransferFile(string fileName, long partNumber);
    }
}
