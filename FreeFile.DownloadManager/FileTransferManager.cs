using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeFile.DownloadManager.Abstract;

namespace FreeFile.DownloadManager
{
    public sealed class FileTransferManager
    {
        public class FilePartData
        {
            internal FilePartData(DownloadParameter downloadParameter, byte[] data)
            {
                DownloadParameter = downloadParameter;
               FileBytes = data;
            }
            public DownloadParameter  DownloadParameter { get; private set; }            
            public byte[] FileBytes{ get; private set; }
        }

        public sealed class DownloadParameter
        {

            public long AllPartsCount { get; set; }
            public long Part { get; set; }
            public string Host { get; set; }
            public Entities.File FileSearchResult { get; set; }
        }
        
        const long FilePartSizeInByte = 10240;
        
        ITransferEngine transferEngine;
        
        ISearchEngine searchEngine;
        
        List<FileSearchResult> resultOfSameHashSearch;        

        public FileTransferManager()
        {
            this.transferEngine = Factory.Instance.CreateTransferEngine();
            this.searchEngine = Factory.Instance.CreateSeachEngine();
        }


        private void StartDownload(object state)
        {
            Entities.File fileSearchResult = state as Entities.File;
            //Wee need to aply multiThreading to use multi host to download diferent part of file cuncurently max number of thread could be 5 thread per host in all of the application;
            long partcount = fileSearchResult.FileSize / FilePartSizeInByte;
            long mod = fileSearchResult.FileSize % FilePartSizeInByte;
            if (mod > 0) partcount++;
            for (int i = 1; i <=partcount; i++)
            {
                downloadFilePart(new DownloadParameter { FileSearchResult = fileSearchResult, Host = fileSearchResult.PeerHostName, Part = i, AllPartsCount = partcount });    
            }
            
                      
        }
     
        public void CancelDownloads()
        {        
        }
        
        private void downloadFilePart(DownloadParameter downloadParameter)
        {
            try
            {
                var data = transferEngine.GetFile(downloadParameter.FileSearchResult.FileName, downloadParameter.Part, downloadParameter.Host);
                onFilePartDownloaded(new FilePartData(downloadParameter, data));
                //Error
            }
            catch(Exception ex)
            {
                throw new FileDownloadException(downloadParameter.Part, downloadParameter.FileSearchResult.FileName, downloadParameter.Host, ex);
            }
        }
    
        private void searchForSameFileBaseOnHash(object state)
        {
            var fileSearchResult =state as FileSearchResult;
            resultOfSameHashSearch = searchEngine.SearchByFileHashCode(fileSearchResult.Hash);
            onSynchronizedReadOnlyCollection(resultOfSameHashSearch);
        }
       
        public event EventHandler<DataContainerEventArg<List<FileSearchResult>>> ExtraServerHostFondBaseOnHashSearch;
        
        private void onSynchronizedReadOnlyCollection(List<FileSearchResult> searchResult)
        {
            if (ExtraServerHostFondBaseOnHashSearch != null)
            {
                ExtraServerHostFondBaseOnHashSearch(this, new DataContainerEventArg<List<FileSearchResult>>(searchResult));
            }
        }

        public event EventHandler<DataContainerEventArg<FilePartData>> FilePartDownloaded;
        
        private void onFilePartDownloaded(FilePartData filePartData)
        {
            if (FilePartDownloaded != null)
            {
                FilePartDownloaded(this, new DataContainerEventArg<FilePartData>(filePartData));
            }
        }

        public List<Entities.File> SearchFileByName(string fileName)
        {
           return this.searchEngine.Search(fileName);
        }

        public void Download(Entities.File fileSearchResult)
        {
            //var action =new Action<object>(searchForSameFileBaseOnHash);
            //Task searchForSameFileBaseOnHashTask = new Task(action, fileSearchResult);
            //searchForSameFileBaseOnHashTask.Start();

            var downloadAction = new Action<object>(StartDownload);
            Task downloadActionTask = new Task(downloadAction, fileSearchResult);
            downloadActionTask.Start();
        }

        public void AddFiles(List<Entities.File> files,Entities.Peer peer)
        {
            FileServer.FilesServiceClient fsc = new FileServer.FilesServiceClient();
            fsc.AddFiles(files.ToArray(), peer);
        }
    }
}
