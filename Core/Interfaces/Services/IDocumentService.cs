using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.ViewModels;
using static Core.DataModels.OrdersDataModel;

namespace Core.Interfaces.Services
{
    public interface IDocumentService
    {
        OrdersDocumentViewModel GetDocument();

    }
}
