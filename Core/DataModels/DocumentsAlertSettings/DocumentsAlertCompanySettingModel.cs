using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.DataModels.DocumentsAlertSettings
{
    public class DocumentsAlertCompanySettingModel
    {
        public List<DocumentsAlertOrderSettingModel> DocumentsAlertOrderSettings { get; set; }
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyType { get; set; }
        public int Baan { get; set; }
        public string Environment { get; set; }
        public DateTime SoonestOrderModifyDate { get; set; }
    }
}
