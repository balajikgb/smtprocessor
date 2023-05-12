using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Config
{
    public class DestinationBlobConfig
    {
        public string destinationBlobConnectionString { get; set; }
        public string destinationBlobContainer { get; set; }
        public string startFolderTemplate { get; set; }
    }
}
