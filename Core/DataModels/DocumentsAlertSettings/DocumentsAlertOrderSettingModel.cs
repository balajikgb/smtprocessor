using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.DataModels.DocumentsAlertSettings
{
    public class DocumentsAlertOrderSettingModel
    {
        public string PurchaseNumber { get; set; }
        public string OrderNumber { get; set; }
        public bool AlertOrderNotification { get; set; }
        public DateTime LastModifyDate { get; set; }
        public string CustomerId { get; set; }
        public int OrderStatus { get; set; }
    }
}
