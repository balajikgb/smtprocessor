using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.RepositoryModels
{
    public class SalesOrderDetailModel : BaseRepositoryModel
    {
        public int Position { get; set; }
        public string Item { get; set; }
        public string ItemDescription { get; set; }
        public double QtyOrderd { get; set; }
        public double QtyShipped { get; set; }
        public string QtyUnit { get; set; }
        public DateTime? FirstOriginalPlannedShipDate { get; set; }
        public DateTime? LastOriginalPlannedShipDate { get; set; }
        public DateTime? FirstRevisedPlannedShipDate { get; set; }
        public DateTime? LastRevisedPlannedShipDate { get; set; }
        public DateTime? LastShippedDate { get; set; }
        public int PurchaseLeadTimeDays { get; set; }
        public string Carrier { get; set; }
        public double NetAmount { get; set; }
        public string TrackingNumber { get; set; }
        //was in XML, not in reference
        public double QtyOnDock { get; set; }
        public string TagCode { get; set; }
        public string CustomerServerReasonDescription { get; set; }
        public bool Cancelled { get; set; }
        public bool Blocked { get; set; }

    }
}
