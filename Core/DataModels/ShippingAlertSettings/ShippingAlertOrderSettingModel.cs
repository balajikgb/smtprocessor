using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataModels.ShippingAlertSettings
{
    public class ShippingAlertOrderSettingModel
    {
        public string PurchaseNumber { get; set; }
        public string OrderNumber { get; set; }
        public string DaysToNotify { get; set; }
        public bool ShippingAlertOrderNotification { get; set; }
        public DateTime LastModifyDate { get; set; }
    }
}
