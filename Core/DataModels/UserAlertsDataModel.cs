using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataModels
{
    public class UserAlertsDataModel
    {
        public AlertsDataModel Alerts { get; set; }
        public string Username { get; set; }
        public List<UserCompaniesDataModel> SelectedCompanies { get; set; }
        public List<UserCompaniesDataModel> UserCompanies { get; set; }
    }
    public class UserCompaniesDataModel
    {
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
    }
}
