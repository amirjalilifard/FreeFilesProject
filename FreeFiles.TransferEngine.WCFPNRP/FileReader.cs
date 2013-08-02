using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FreeFile.DownloadManager;

namespace FreeFiles.TransferEngine.WCFPNRP
{
     static class FileReader
    {
        
        internal static byte[] GetFileBytes(string fileName, string hash, long partNumber)
        {
            var file=FileUtility.FindFileByHash(Config.SharedFolder, fileName, hash);
            return FileUtility.ReadFilePart(file, partNumber);                        
        }

        internal static byte[] GetFileBytes(string fileName, long partNumber)
        {
            return FileUtility.ReadFilePart(fileName, partNumber);
        }

    }
}
