using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataModels
{
    public class OrdersDataModel
    {
        public OrdersDocumentViewModel OrderDocument { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string ShippingConstraint { get; set; }
        public string PurchaseOrder { get; set; }
        public string SalesOrderId { get; set; }
        public DateTime? ShipRange { get; set; }
        public int Shipped { get; set; }
        public int Total { get; set; }
        public string ProjectName { get; set; }
        public IList<OrderStatusEnum> OrderStatuses { get; set; }


        public class OrdersDocumentViewModel
        {
            public bool? IsDocumentOpened { get; set; }
            public string DocumentLink { get; set; }
        }
    }
}
