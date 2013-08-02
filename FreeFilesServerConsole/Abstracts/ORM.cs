using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FreeFilesServerConsole.EF.Repository;
using Entities;
using FreeFiles.Abstracts;
using FreeFilesServerConsole.EF;

namespace FreeFiles.Abstracts
{
    public class ORM:IDataBaseAction
    {
        FileRepository fileRepository;
        FreeFilesEntitiesContext _freeFilesObjectContext;
        public ORM()
        {
            fileRepository = new FileRepository(_freeFilesObjectContext as FreeFilesServerConsole.IUnitOfWork);
            _freeFilesObjectContext = new FreeFilesEntitiesContext();
        }

        public void AddFiles(List<FreeFilesServerConsole.EF.File> FilesList, Entities.Peer peer)
        {
            
            fileRepository.AddFiles(FilesList);
            PeerRepository peerRepository = new PeerRepository(_freeFilesObjectContext as FreeFilesServerConsole.IUnitOfWork);
            peerRepository.AddPeer(FreeFilesServerConsole.EntityUtility.externalPeerToEFPeer(peer));
            SaveFile();
        }

        public List<FreeFilesServerConsole.EF.File> SearchAvaiableFiles(string fileName)
        {
            return fileRepository.SearchAvaiableFiles(fileName).ToList();
        }

        private void SaveFile()
        {
            _freeFilesObjectContext.Save();
        }
    }
}