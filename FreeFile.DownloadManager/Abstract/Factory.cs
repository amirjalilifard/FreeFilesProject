using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace FreeFile.DownloadManager.Abstract
{
    public sealed class Factory
    {
        Factory()
        {

            Assembly transferEngineAssembly = Assembly.LoadFile(Path.GetDirectoryName( Application.ExecutablePath) + "\\FreeFiles.TransferEngine.WCFPNRP.dll");            
            var tnaTypes = transferEngineAssembly.GetTypes();
            foreach (var item in tnaTypes)
            {
                if (item.GetInterface("ITransferEngineFactory") != null)
                {
                    ITransferEngineFactory ITransferEngineFactory = Activator.CreateInstance(item) as ITransferEngineFactory;
                    this.transferEngine = ITransferEngineFactory.CreateTransferEngine();
                    this.fileProviderServer = this.transferEngine as IFileProviderServer;
                    break;
                }
            }
            /* Create
             *searchEngine;             
            */
            this.searchEngine = new Searchengine();
        }
        static readonly object sharedObject = new object();
        static Factory instance;
        public static Factory Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (sharedObject)
                    {
                        if (instance == null)
                        {
                            instance = new Factory();
                        }
                    }
                }
                return instance;
            }
        }
        ISearchEngine searchEngine;
        ITransferEngine transferEngine;
        IFileProviderServer fileProviderServer;
        public ISearchEngine CreateSeachEngine()
        {
            return searchEngine;
        }
        public ITransferEngine CreateTransferEngine()
        {
            return transferEngine;
        }

        public IFileProviderServer CreateFileProviderServer()
        {
            return fileProviderServer;
        }
    }
}
