using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataModels.DocumentsAlertSettings;
using Core.DataModels.ShippingAlertSettings;

namespace Core.DataModels
{
    public class AlertsDataModel
    {
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public int UserId { get; set; }
        public List<DocumentsAlertCompanySettingModel> DocumentsAlertCompanySettings { get; set; }
        public List<ShippingAlertCompanySettingModel> ShippingAlertCompanySettings { get; set; }
    }
}
