using Core.DataModels.NotificationSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataModels
{
    public class NotificationDocumentByUserDataModel
    {
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public IList<NotificationCompanyOrdersSettingModel> NotificationCompanyOrdersSetting { get; set; }
    }
}
