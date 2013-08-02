using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Collections.Specialized;

namespace FreeFilesServerConsole
{
    static class Config
    {
        const string FileServiceEndPointAddressKey = "FileServiceEndPointAddress";
        public static string GetEndPointAddress()
        {
            return ConfigurationSettings.AppSettings[FileServiceEndPointAddressKey].ToString();
        }
    }
}
