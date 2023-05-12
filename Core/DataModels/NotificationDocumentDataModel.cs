using Core.DataModels.NotificationSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataModels
{
    public class NotificationDocumentDataModel
    {
        public int IdOrder { get; set; }
        public string OrderNumber { get; set; }
        public IList<NotificationDocumentSettingModel> NotificationDocumentSetting { get; set; }
    }
}
