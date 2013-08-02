using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeFilesServerConsole.EF.Repository
{
    class PeerRepository
    {
        private FreeFilesEntitiesContext _freeFilesObjectContext;
        public PeerRepository(IUnitOfWork unitOfWork)
        {
            
            _freeFilesObjectContext = unitOfWork as FreeFilesEntitiesContext;
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
    }
}
