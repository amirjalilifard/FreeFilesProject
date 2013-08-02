using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FreeFilesServerConsole.EF.Repository;
using FreeFilesServerConsole.EF;
using Entities;
using System.ServiceModel;

namespace FreeFilesServerConsole.WCFServices
{
    [ServiceContract]
    public class FilesService
    {
        private FreeFilesEntitiesContext _freeFilesObjectContext=new FreeFilesEntitiesContext();
        [OperationContract]
        public void AddFiles(List<Entities.File> FilesList,Entities.Peer peer)
        {
            FileRepository fileRepository = new FileRepository(_freeFilesObjectContext as FreeFilesServerConsole.IUnitOfWork);
            this.AddPeer(externalPeerToEFPeer(peer));
            fileRepository.AddFiles(externalFileToEFFile(FilesList));
            
            SaveFile();
        }
        [OperationContract]
        public void AddPeer(FreeFilesServerConsole.EF.Peer Peer)
        {
            FileRepository fileRepository = new FileRepository(_freeFilesObjectContext as FreeFilesServerConsole.IUnitOfWork);
            fileRepository.AddPeer(Peer);

        }
        [OperationContract]
        public List<Entities.File> SearchAvaiableFiles(string fileName)
        {
            FileRepository fileRepository = new FileRepository(_freeFilesObjectContext as FreeFilesServerConsole.IUnitOfWork);
            return internalFileToEntityFile(fileRepository.SearchAvaiableFiles(fileName));
        }

        public void SaveFile()
        {
            _freeFilesObjectContext.Save();
        }

        private EF.Peer externalPeerToEFPeer(Entities.Peer peer)
        {
            EF.Peer EFPeer=new EF.Peer();
            EFPeer.PeerHostName = peer.PeerHostName;
            EFPeer.Comments = peer.Comments;
            EFPeer.PeerID = peer.PeerID;
            return EFPeer;
        }

        private List<FreeFilesServerConsole.EF.File> externalFileToEFFile(List<Entities.File> fileList)
        {
            List<FreeFilesServerConsole.EF.File> nativeFileTypeList = new List<FreeFilesServerConsole.EF.File>();
            foreach (var file in fileList)
            {
                EF.File EFFile = new EF.File();
                EFFile.FileID = Guid.NewGuid();
                EFFile.FileName = file.FileName;
                EFFile.FileSize = file.FileSize;
                EFFile.FileType = file.FileType;
                EFFile.PeerID = file.PeerID;
                EFFile.PeerHostName = file.PeerHostName;
                nativeFileTypeList.Add(EFFile);
            }
            return nativeFileTypeList;
        }

        private List<Entities.File> internalFileToEntityFile(List<FreeFilesServerConsole.EF.File> fileList)
        {
            List<Entities.File> entityFileTypeList = new List<Entities.File>();
            foreach (var file in fileList)
            {
                Entities.File EFFile = new Entities.File();
                EFFile.FileName = file.FileName;
                EFFile.FileSize = file.FileSize;
                EFFile.FileType = file.FileType;
                EFFile.PeerHostName = file.PeerHostName;
                EFFile.PeerID = file.PeerID;
                entityFileTypeList.Add(EFFile);
            }
            return entityFileTypeList;
        }
    }
}
