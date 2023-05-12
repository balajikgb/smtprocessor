using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataModels
{
    public class SingleOrderDataModel
    {
        public string OrderNumber { get; set; }
        public CustomerDataModel Company { get; set; }
        public string CustomerName { get; set; }
        public string Project { get; set; }
        public string Currency { get; set; }
        public int QuantityShipped { get; set; }
        public int QuantityTotal { get; set; }
        public int QuantityCancelled { get; set; }
        public string ShippingConstraint { get; set; }
        public DateTime OrderDate { get; set; }
        public string PurchaseOrder { get; set; }
        public List<OrderItem> ItemsList { get; set; }

        public class OrderItem
        {
            public DateTime? Date { get; set; }
            public string Position { get; set; }
            public string QuantityShippedTotal { get; set; }
            public string Description { get; set; }
            public string TagCode { get; set; }
            public string Carrier { get; set; }
            public string TrackingNumber { get; set; }
        }

        public class CustomerDataModel
        {
            public string CustomerID { get; set; }
            public string CustomerName { get; set; }
            public string Address { get; set; }
            public string Phone { get; set; }
            public string Fax { get; set; }
        }
    }
}
