using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Entities;

namespace FreeFilesServerConsole.WCFServices
{
    public interface IFileService
    {
        void AddFiles(List<FreeFilesServerConsole.EF.File> FilesList,Peer Peer);
        List<FreeFilesServerConsole.EF.File> SearchAvaiableFiles(string fileName);
    }
}
