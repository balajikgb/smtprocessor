using Core.DataModels.NotificationSettings;
using System;
using System.Collections.Generic;

namespace Core.DataModels
{
    public class NotificationCompanyUsersDataModel
    {
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Type { get; set; }
        public int Baan { get; set; }
        public IList<NotificationUserSettingModel> NotificationUserSetting { get; set; }
    }
}
