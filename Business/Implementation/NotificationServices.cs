using Business.Services;
using DataBase.Core;
using DataBase.Core.Models.Notifications;
using DomainModels;
using DomainModels.DTO;
using DomainModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Implementation
{
    public class NotificationServices : INotificationServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public NotificationServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void AddNotification(NotificationModel notification)
        {
            var _notification = new Notifications
            {
                Id = Guid.NewGuid(),
                ActionedUserId = notification.ActionedUserId,
                ItemId = notification.ItemId,
                NotificatinType = notification.NotificatinType,
                NotifiedUserId = notification.NotifiedUserId,
            };
            Task.Run(async () =>
            {
                await _unitOfWork.Notification.AddAsync(_notification);
                await _unitOfWork.Complete();
            });
            return;

        }

        public async Task<IEnumerable<NotificationDTO>> GetAllUserNotifications(Guid userId)
        {
            string[] includes = { "ActionedUser" };
            var notifications = await _unitOfWork.Notification.FindAllAsync(n => n.NotifiedUserId == userId, includes);
            var notificationDTO = OMapper.Mapper.Map<List<DomainModels.DTO.NotificationDTO>>(notifications);
            return notificationDTO;
        }

        public void RemoveAllUserNotification(Guid Id)
        {
            Task.Run(async () =>
            {
                var notifications =  await _unitOfWork.Notification.FindAllAsync(n=>n.NotifiedUserId==Id);
                _unitOfWork.Notification.DeleteRange(notifications);
                await _unitOfWork.Complete();
            });
            return;
        }
    }
}
