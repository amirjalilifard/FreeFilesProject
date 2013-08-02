using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeFile.DownloadManager.Abstract
{
    public interface ITransferEngine
    {        
        byte[] GetFile(string filename,  string hash,long partNumber,string hostName);
        byte[] GetFile(string filename, long partNumber, string hostName);
    }
}
