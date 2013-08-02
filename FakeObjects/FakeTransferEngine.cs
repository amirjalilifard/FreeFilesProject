using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FreeFile.DownloadManager.Abstract;

namespace FakeObjects
{
    public class FakeTransferEngine : ITransferEngine
    {
        public byte[] GetFile(string filePath, string fileName, string hostURI = "", string hash = "", long partNumber = 1)
        {
            if (!filePath.EndsWith(@"\"))
            {
                filePath += @"\";
            }
            var data=File.ReadAllBytes(filePath + fileName);
            return data;
        }

        public byte[] GetFile(string filename, string hash, long partNumber, string hostName)
        {
            throw new NotImplementedException();
        }

        public byte[] GetFile(string filename, long partNumber, string hostName)
        {
            throw new NotImplementedException();
        }

        public void SetupFileServer()
        {
            throw new NotImplementedException();
        }
    }
}
