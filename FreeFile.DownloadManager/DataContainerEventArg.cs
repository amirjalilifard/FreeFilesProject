using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeFile.DownloadManager
{
    public sealed class DataContainerEventArg<T>:EventArgs
    {
        public DataContainerEventArg(T data) : base() { this.Data = data; }
        public T Data { get; set; }
    }
}
