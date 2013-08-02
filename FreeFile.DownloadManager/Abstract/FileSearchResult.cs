using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeFile.DownloadManager.Abstract
{
    public sealed class FileSearchResult
    {
        public string FileName { get; set; }
        public long Size { get; set; }
        public string Hash { get; set; }
        public string HostName { get; set; }
    }
}
