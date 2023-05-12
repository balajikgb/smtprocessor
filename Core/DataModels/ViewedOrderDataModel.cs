using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataModels
{
	public class ViewedOrderDataModel
	{
		public string OrderID { get; set; }
		public int UserId { get; set; }
		public DateTime CreatedDate { get; set; }
		public int BaanId { get; set; }
		public string CompanyId { get; set; }
		public string Environment { get; set; }
		public string TypeId { get; set; }
	}
}
