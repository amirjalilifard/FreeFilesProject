using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeFile.DownloadManager
{
   public sealed class HostUnreachableException:ApplicationException
    {
        public string HostName { get; private set; }
       public HostUnreachableException(string hostName, string message = "Host is unreachable for geting data", Exception innerexception = null)
           :base(message,innerexception)
       {
           HostName = hostName;

       }

    }
}
