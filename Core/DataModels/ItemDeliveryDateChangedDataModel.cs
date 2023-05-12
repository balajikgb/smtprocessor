using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataModels
{
    public class ItemDeliveryDateChangedDataModel
    {
        public string LineItemId { get; set; }
        public string OrderId { get; set; }
        public DateTime? OriginalPlannedDate { get; set; }
        public DateTime? OriginalRevisedPlannedShipDate { get; set; }
        public DateTime? NewRevisedPlannedShipDate { get; set; }
        public bool IsClosed { get; set; }
    }
}
