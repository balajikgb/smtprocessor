using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataModels
{
    public class ItemDataModel
    {
        public string SalesOrderId { get; set; }
        public string LineItemId { get; set; }
        public string ItemCode { get; set; }
        public string TagCode { get; set; }
        public string TrackingNumber { get; set; }
        public string Carrier { get; set; }
        public string Description { get; set; }
        public string Ordered { get; set; }
        public string Shipped { get; set; }
        public double TotalPrice { get; set; }
        public DateTime? LastShippedDate { get; set; }
        public DateTime? OrigPlannedDate { get; set; }
        public DateTime? RevisedPlannedShipDate { get; set; }
        public bool IsComplete { get; set; }
    }
}
