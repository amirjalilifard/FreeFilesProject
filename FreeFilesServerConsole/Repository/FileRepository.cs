using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeFilesServerConsole.EF.Repository
{
    class FileRepository:IFilesRepository
    {
        private FreeFilesEntitiesContext _freeFilesObjectContext;
        public FileRepository(IUnitOfWork unitOfWork)
        {
            
            _freeFilesObjectContext = unitOfWork as FreeFilesEntitiesContext;
        }
        public List<FreeFilesServerConsole.EF.File> SearchAvaiableFiles(string fileName)
        {
            var filesList = from files in _freeFilesObjectContext.Files
                            join peers in _freeFilesObjectContext.Peers on files.PeerID equals peers.PeerID
                            where files.FileName.Contains(fileName)
                            select new {files,peers };
            List<FreeFilesServerConsole.EF.File> List = new List<File>();
            foreach (var item in filesList)
            {
                File file = new File();
                file.FileName = item.files.FileName;
                file.FileSize = item.files.FileSize;
                file.FileType = item.files.FileType;
                file.PeerHostName = item.peers.PeerHostName;
                List.Add(file);
            }
            return List;
        }

        public void AddFiles(List<FreeFilesServerConsole.EF.File> FilesList)
        {
            //_freeFilesObjectContext = new FreeFilesEntitiesContext();
            try
            {
                foreach (FreeFilesServerConsole.EF.File file in FilesList)
                {
                    _freeFilesObjectContext.Files.AddObject(file);
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.InnerException.Message);
            }
        }

        public void AddPeer(FreeFilesServerConsole.EF.Peer Peer)
        {
            //_freeFilesObjectContext = new FreeFilesEntitiesContext();
            try
            {
                _freeFilesObjectContext.Peers.AddObject(Peer);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.InnerException.Message);
            }
        }

        public void Save()
        {
            _freeFilesObjectContext.Save();            
        }
    }
}
