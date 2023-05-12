using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataModels.ShippingAlertSettings
{
    public class ShippingAlertSettingModel
    {
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public List<ShippingAlertCompanySettingModel> ShippingAlertCompanySettings { get; set; }
    }
}
