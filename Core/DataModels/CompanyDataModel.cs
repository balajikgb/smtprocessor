using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataModels
{
    public class CompanyDataModel
    {
        public string Baan { get; set; }
        public int BaanId { get; set; }
        public string Acct { get; set; }
        public int CountOpenOrders { get; set; }
        public string Company { get; set; }
        public string City { get; set; }
        public string Telephone { get; set; }
        public string TypeId { get; set; }
    }
}
