using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;

namespace FreeFile.DownloadManager.Abstract
{
    class Searchengine:ISearchEngine
    {
        public List<Entities.File> Search(string searchPattern)
        {
            FileServer.FilesServiceClient fileServiceClient = new FileServer.FilesServiceClient();
            
            List<Entities.File> filesList = new List<File>();
            foreach (var file in fileServiceClient.SearchAvaiableFiles(searchPattern))
            {
                Entities.File currentFile = new File();
                currentFile.FileName = file.FileName;
                currentFile.FileSize = file.FileSize;
                currentFile.FileType = file.FileType;
                currentFile.PeerID = file.PeerID;
                currentFile.PeerHostName = file.PeerHostName;
                filesList.Add(currentFile);
            }
            return filesList;
            //List<FileSearchResult> retlist = new List<FileSearchResult>();
            //retlist.Add(new FileSearchResult { FileName = "pooya.txt", 
            //    Hash = "BA-32-53-87-6A-ED-6B-C2-2D-4A-6F-F5-3D-84-06-C6-AD-86-41-95-ED-14-4A-B5-C8-76-21-B6-C2-33-B5-48-BA-EA-E6-95-6D-F3-46-EC-8C-17-F5-EA-10-F3-5E-E3-CB-C5-14-79-7E-D7-DD-D3-14-54-64-E2-A0-BA-B4-13",
            //                                   Size = 100,
            //                                   HostName = "FreeFile163b69882b3a4f18b0de878eb5f0b4b7"
            //});
            //return retlist;

        }

        public List<FileSearchResult> SearchByFileHashCode(string hashCode)
        {
            return null;
        }
    }
}
