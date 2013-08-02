using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FreeFile.DownloadManager.Abstract;

namespace FreeFile.DownloadManager
{
    public static  class FileProviderServerManager
    {
        static IFileProviderServer FileProviderServer;
        public static void StartFileProviderServer()
        {
           FileProviderServer= Factory.Instance.CreateFileProviderServer();
           FileProviderServer.SetupFileServer();
        }
    }
}
