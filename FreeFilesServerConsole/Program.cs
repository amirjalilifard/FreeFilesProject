using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Entities;

namespace FreeFilesServerConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            #region tests
            //FreeFilesServerConsole.WCFServices.FilesService af = new FreeFilesServerConsole.WCFServices.FilesService();
            //FreeFilesServerConsole.EF.File ff = new FreeFilesServerConsole.EF.File();
            //ff.FileName = "111";
            //ff.FileSize = 2000;
            //ff.FileType = "pdf";
            //ff.PeerID = Guid.NewGuid();
            //List<FreeFilesServerConsole.EF.File> fileList = new List<FreeFilesServerConsole.EF.File>();
            //fileList.Add(ff);

            //Peer pp = new Peer();
            //pp.Comments = "www";
            //pp.PeerHostName = "ss.pnrp.net";
            //pp.PeerID = ff.PeerID;
            //List<FreeFilesServerConsole.EF.Peer> peerList = new List<FreeFilesServerConsole.EF.Peer>();
            //fileList.Add(ff);
            //af.AddFiles(fileList, pp);

            //af.SaveFile();
            //Console.ReadKey();
            #endregion
            FreeFilesServerConsole.WCFServices.ServiceInitializer si = new WCFServices.ServiceInitializer();
            si.InitializeServiceHost();
            Console.Write("started");
            Console.ReadKey();
        }
    }
}
