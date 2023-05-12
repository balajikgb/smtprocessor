using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataModels.NotificationSettings
{
    public class NotificationCompanyOrdersSettingModel
    {
        public IList<NotificationDocumentDataModel> NotificationDocumentData { get; set; }
    }
}
