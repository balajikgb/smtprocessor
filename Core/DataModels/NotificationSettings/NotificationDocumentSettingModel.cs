using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataModels.NotificationSettings
{
    public class NotificationDocumentSettingModel
    {
        public string SO { get; set; }
        public decimal Fid { get; set; }
        public string FileName { get; set; }
        public int OrderDocumentsId { get; set; }
    }
}
