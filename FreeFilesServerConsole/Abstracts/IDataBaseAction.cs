using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities;

namespace FreeFiles.Abstracts
{
    public interface IDataBaseAction
    {
        void AddFiles(List<FreeFilesServerConsole.EF.File> FilesList, Peer Peer);
        List<FreeFilesServerConsole.EF.File> SearchAvaiableFiles(string fileName);
    }
}