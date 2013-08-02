using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FreeFilesServerConsole.EF.Repository;
using FreeFilesServerConsole.EF;
using Entities;

namespace FreeFiles.Abstracts
{
    public class SQLLite:IDataBaseAction
    {
        private FreeFilesEntitiesContext _freeFilesObjectContext = new FreeFilesEntitiesContext();
        public void AddFiles(List<FreeFilesServerConsole.EF.File> FilesList, Entities.Peer peer)
        {
            SaveFile();
        }
        public List<FreeFilesServerConsole.EF.File> SearchAvaiableFiles(string fileName)
        {
            return SearchAvaiableFiles(fileName);
        }

        private void SaveFile()
        {
            _freeFilesObjectContext.Save();
        }
    }
}