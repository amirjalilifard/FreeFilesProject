using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeFile.LocalFileManager
{
    public interface IFileInfoServerManager
    {
        void RegisterSharedFileInfo(string localServerName,string fileName, string hash, long size);
        void RemoveSharedFile(string localServerName, string fileName, string hash);
    }
}
