using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeFile.DownloadManager.LocalFileManagement
{
    namespace FreeFile.DownloadManager.Abstract
    {
        public interface IFileInfoServerManager
        {
            void RegisterSharedFileInfo(string localServerName, string fileName, string hash, long size);
            void RemoveSharedFile(string localServerName, string fileName, string hash);
        }
    }
}
