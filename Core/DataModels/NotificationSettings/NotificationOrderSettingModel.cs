using System;

namespace Core.DataModels.NotificationSettings
{
    public class NotificationOrderSettingModel
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public string DaysToNotify { get; set; }
        public bool ShippingAlertOrderNotification { get; set; }
        public int OrderStatus { get; set; }
        public DateTime? DateLastNotification { get; set; }
    }
}
