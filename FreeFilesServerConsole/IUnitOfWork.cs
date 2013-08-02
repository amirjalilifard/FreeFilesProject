using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeFilesServerConsole
{
    public interface IUnitOfWork
    {
        void Save();
    }
}
