using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace FreeFile.DownloadManager
{
    public static class Config
    {
        const string FreeFileLocalHostNameKey = "FreeFileLocalHostName";
        const string FreeFileLocalPortKey = "FreeFileLocalPort";
        const string SharedFolderNameKey = "SharedFolderName ";

        public static string LocalHostyName
        {
            get
            {
                //var localHostName = Registry.CurrentUser.GetValue(FreeFileLocalHostNameKey);
                var localHostName = Registry.CurrentUser.GetValue(FreeFileLocalHostNameKey);
                string hostname = string.Empty;
                if (localHostName == null)
                {
                    hostname = "FreeFile" + Guid.NewGuid().ToString().Replace("-", "");
                    Registry.CurrentUser.SetValue(FreeFileLocalHostNameKey, hostname);
                }
                else
                {
                    hostname = localHostName.ToString();
                }
                return hostname;
            }
        }

        public static string SharedFolder
        {
            get
            {
                var sharedFolderName = Registry.CurrentUser.GetValue(SharedFolderNameKey);
                string folder = string.Empty;
                if (sharedFolderName == null)
                {
                    folder = @"C:\FreeFileDirectory"; ;
                    Registry.CurrentUser.SetValue(SharedFolderNameKey, folder);
                }
                else
                {
                    folder = sharedFolderName.ToString();
                }
                return folder;
            }
        }

        public static int LocalPort
        {
            get
            {
                var localPort = Registry.CurrentUser.GetValue(FreeFileLocalPortKey);
                int port = 20388;
                if (localPort == null)
                {
                    Registry.CurrentUser.SetValue(FreeFileLocalPortKey, port);
                }
                return port;
            }
        }
    }
}
