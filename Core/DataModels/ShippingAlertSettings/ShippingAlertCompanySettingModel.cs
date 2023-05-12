using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataModels.ShippingAlertSettings
{
    public class ShippingAlertCompanySettingModel
    {
        public List<ShippingAlertOrderSettingModel> ShippingAlertOrderSettings { get; set; }
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyType { get; set; }
        public int Baan { get; set; }
        public string Environment { get; set; }
        public DateTime SoonestOrderModifyDate { get; set; }
        public string CustomerId { get; set; }
    }
}
