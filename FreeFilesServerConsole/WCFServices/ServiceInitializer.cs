using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;
using System.Configuration;

namespace FreeFilesServerConsole.WCFServices
{
    //[FileServiceAttributes(typeof(FilesService), ) ]
    
    public class ServiceInitializer : IServiceInitializer
    {
        private string _endPointAddress = string.Empty;
        public ServiceInitializer()
        {
            _endPointAddress = ConfigurationSettings.AppSettings["FileServiceEndPointAddress"].ToString();
        }
        public void InitializeServiceHost()
        {
        //    FileServiceAttributes serviceAttributes = FileServiceAttributes.FileServiceAttributeInit();
            Uri[] baseAddresses = new Uri[]{
                new Uri(_endPointAddress),
            };
            ServiceHost Host = new ServiceHost(typeof(FilesService),baseAddresses);

            Host.AddServiceEndpoint(typeof(FilesService),
                new BasicHttpBinding(),"");
            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = true;
            Host.Description.Behaviors.Add(smb);
            Host.Open();
        }
    }

    public class FileServiceAttributes : Attribute
    {
        public FileServiceAttributes(Type ImplementedContract, string ServiceAddress)
        {
            this.ImplementedContract = ImplementedContract;
            this.ServiceAddress=ServiceAddress;
        }
        
        public Type ImplementedContract { get; set; }
        public Type ImplemenetdBinding { get; set; }
        public string ServiceAddress { get; set; }

        public static FileServiceAttributes FileServiceAttributeInit()
        {
            Type type = typeof(IServiceInitializer);
            object[] attributes = type.GetCustomAttributes(
            typeof(FileServiceAttributes), true);
            return attributes[0] as FileServiceAttributes;
        }
    }
}
