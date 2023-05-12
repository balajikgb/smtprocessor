using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Config
{
    public class AzureAdConfigOptions
    {
        public string Instance { get; set; }
        public string Domain { get; set; }
        public string ClientId { get; set; }
        public string TenantId { get; set; }
        public string CallbackPath { get; set; }
        public string MetadataAddress { get; set; }
    }
}
