using Core.DataModels;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface INotificationService
    {
        Task CheckOrdersForNotificationEmailShippingAsync();
        Task<NotificationShippingByUserDataModel> CheckOrdersForNotificationAlertUserShippingAsync(string userName, string environment);
        Task CheckDocumentsForNotificationEmailDocumentAsync();
        Task<NotificationDocumentByUserDataModel> CheckDocumentsForNotificationAlertUserDocumentAsync(string userName, string environment);
        void ChangeNotifiedOrderDocumentsDb(string username, decimal fid, bool Notified);
        Task SendEmailAsync(string fromAddress, List<string> toAddress, string ccAddress, string subject, string type, string bodyContent, bool isHtmlBody, string attachment);
    }
}
