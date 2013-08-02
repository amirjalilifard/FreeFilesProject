using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.PeerToPeer;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Diagnostics;
using System.IO;
using FreeFile.DownloadManager.Abstract;
using FreeFile.DownloadManager;
using Entities;
using FreeFiles.TransferEngine.WCFPNRP;

namespace TestConsole
{
    class Program
    {

        static void Main(string[] args)
        {
            #region Getting file test
            Console.WriteLine("ServerName:");
            string servername = Console.ReadLine();
            Console.WriteLine("Port:");
            int port = int.Parse(Console.ReadLine());
            PnrpTransferEngine engine = new PnrpTransferEngine(servername, port);
            (engine as IFileProviderServer).SetupFileServer();
            Console.WriteLine("Engine is running now:");
            //Console.WriteLine("Destname:");
            //string Destname = Console.ReadLine();
            //var data = (engine as ITransferEngine).GetFile("pooya.txt", "", 0, Destname);
            //string filename = @"C:\From" + Destname + DateTime.Now.TimeOfDay.ToString("T").Replace(":", "") + ".txt";
            ////File.WriteAllBytes(filename, data);
            //Console.WriteLine("File downloaded " + filename);
            #endregion
            //#region Test connection to the WCF Service

            //Entities.File ff = new Entities.File();
            //ff.FileName = "111";
            //ff.FileSize = 2000;
            //ff.FileType = "pdf";
            //ff.PeerID = Guid.NewGuid();
            //List<Entities.File> fileList = new List<Entities.File>();
            //fileList.Add(ff);

            //Peer pp = new Peer();
            //pp.Comments = "www";
            //pp.PeerHostName = "ss.pnrp.net";
            //pp.PeerID = ff.PeerID;


            ////FileActionsService.FilesServiceClient fsc = new FileActionsService.FilesServiceClient();
            //////fsc.AddFiles(fileList,pp);

            //FileActionsService2.FilesServiceClient fsc = new FileActionsService2.FilesServiceClient();
            //fsc.AddFiles(fileList.ToArray(), pp);
            
            //#endregion
            Console.ReadKey();
        }

    }   
}
