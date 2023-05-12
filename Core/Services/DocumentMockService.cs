using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces.Services;
using Core.ViewModels;
using static Core.DataModels.OrdersDataModel;

namespace Core.Services
{
    public class DocumentMockService : IDocumentService
    {
        public OrdersDocumentViewModel GetDocument()
        {
            return new OrdersDocumentViewModel() { IsDocumentOpened = true, DocumentLink = "blabal" };
        }
    }
}
