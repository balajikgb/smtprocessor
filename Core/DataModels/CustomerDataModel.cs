using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataModels
{
    public class CustomerDataModel
    {
        public IEnumerable<CustomerCompanyDataModel> Companies { get; set; }
        public int OrderCount { get; set; }
        public string Environment { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string City { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public int BaanId { get; set; }
        public string Baan { get; set; }
    }
    public class CustomerCompanyDataModel
    {
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int BaanId { get; set; }
    }
   
}
