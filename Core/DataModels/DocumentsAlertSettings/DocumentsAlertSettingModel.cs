using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.DataModels.DocumentsAlertSettings
{
    public class DocumentsAlertSettingModel
    {
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public List<DocumentsAlertCompanySettingModel> DocumentsalertCompanySettings { get; set; }
    }
}
