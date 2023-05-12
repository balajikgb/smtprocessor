using Core.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class OrderWithClientViewModel
    {
       public List<OrdersDataModel> ordersDataModels { get; set; }
       public ClientViewModel clientViewMode { get; set; }
    }
}
