using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace FreeFilesServerConsole.WCFServices
{
    public interface IServiceInitializer
    {
        void InitializeServiceHost();
    }
}
