using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
namespace FreeFilesServerConsole.EF
{
    class FreeFilesEntitiesContext:ObjectContext,IUnitOfWork
    {
        private ObjectSet<FreeFilesServerConsole.EF.File> _files;
        private ObjectSet<FreeFilesServerConsole.EF.Peer> _peers;
        public FreeFilesEntitiesContext()
            : base("name=FreeFilesEntities", "FreeFilesEntities1")
        {
            _files = CreateObjectSet<FreeFilesServerConsole.EF.File>();
            _peers = CreateObjectSet<FreeFilesServerConsole.EF.Peer>();
        }

        public ObjectSet<FreeFilesServerConsole.EF.File> Files
        {
            get { return _files; }
        }

        public ObjectSet<FreeFilesServerConsole.EF.Peer> Peers
        {
            get { return _peers; }
        }

        public void Save()
        {
            try
            {
                SaveChanges();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.InnerException.Message);
            }
        }
    }
}
