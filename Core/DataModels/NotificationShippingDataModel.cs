using Core.DataModels.NotificationSettings;
using System;
using System.Collections.Generic;

namespace Core.DataModels
{
    public class NotificationShippingDataModel
    {
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Type { get; set; }
        public int BaanId { get; set; }
        public IList<NotificationOrderSettingModel> NotificationOrderSetting { get; set; }
    }
}
