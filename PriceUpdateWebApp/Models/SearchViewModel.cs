using Core.DataModels;
using Core.DataModels.NotificationSettings;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ArasPLMWebAp.Models
{
    public class SearchViewModel
    {
        public string PartialViewModelName { get; set; }
        public string MarketId { get; set; }
        public string CompanyNameOrId { get; set; }
        public string TypeId { get; set; }
        public bool OpenOrdersOnly { get; set; }
        public string OrderNumber { get; set; }
        public int BaanId { get; set; }
        public string CompanyId { get; set; }
        public NotificationShippingByUserDataModel ShippingAlerts { get; set; }
        public NotificationDocumentByUserDataModel DocumentsAlerts { get; set; }
        public string Environment { get; set; }
        public List<ViewedOrderDataModel> ViewedSalesOrdersInfo { get; set; }

		public SearchViewModel()
		{
            ViewedSalesOrdersInfo = new List<ViewedOrderDataModel>();
		}
    }
}
