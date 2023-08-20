using DomainModels.DTO;
using DomainModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface INotificationServices
    {
        void AddNotification(NotificationModel notification);
        void RemoveAllUserNotification(Guid Id);
        Task<IEnumerable<NotificationDTO>> GetAllUserNotifications(Guid userId);
    }
}
