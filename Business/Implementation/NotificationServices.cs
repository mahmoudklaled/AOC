using Business.Services;
using DataBase.Core;
using DataBase.Core.Enums;
using DataBase.Core.Models.Notifications;
using DataBase.Core.Models.Posts;
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
        private readonly SignalRActions _signalRActions;
        public NotificationServices(IUnitOfWork unitOfWork, SignalRActions signalRActions)
        {
            _unitOfWork = unitOfWork;
            _signalRActions = signalRActions;
        }
        public void AddNotification(NotificationDTO notification)
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
        public void NotifyOnAddingComment(CommentDTO commentDTO, Guid postId, PostsTypes postsType)
        {
            Task.Run(() => {
                Guid userPostOwnerId;
                var notificationDTO = new NotificationDTO()
                {
                    ActionedUserId = commentDTO.UserId,
                    ActionUserFirstName = commentDTO.UserFirstName,
                    ActionUserLastName = commentDTO.UserLastName,
                    ItemId = commentDTO.Id,
                };
                if (postsType == PostsTypes.Post)
                {
                    notificationDTO.NotificatinType = NotificatinTypes.AddComment;
                    userPostOwnerId = _unitOfWork.Post.Find(p=>p.Id == postId).UserAccountsId;
                }
                else
                {
                    notificationDTO.NotificatinType = NotificatinTypes.AddQuestion;
                    userPostOwnerId = _unitOfWork.QuestionPost.Find(p => p.Id == postId).UserAccountsId;
                }
                AddNotification(notificationDTO);
                SendCommentNotification(userPostOwnerId, commentDTO, postId, postsType);

            });

            return;
        }
        public void NotifyOnAddingPost(PostDTO postDTO)
        {
            var notificationDTO = new NotificationDTO()
            {
                ActionedUserId = postDTO.UserId,
                ActionUserFirstName = postDTO.UserFirstName,
                ActionUserLastName = postDTO.UserLastName,
                ItemId = postDTO.Id,
                NotificatinType = NotificatinTypes.AddPost
            };
            Task.Run(() => { AddNotification(notificationDTO); SendPostNotification(notificationDTO); });
            return;
        }
        public void NotifyOnAddingQuestion(QuestionPostDTO postDTO)
        {
            var notificationDTO = new NotificationDTO()
            {
                ActionedUserId = postDTO.UserId,
                ActionUserFirstName = postDTO.UserFirstName,
                ActionUserLastName = postDTO.UserLastName,
                ItemId = postDTO.Id,
                NotificatinType = NotificatinTypes.AddQuestion
            };
            Task.Run(() => { AddNotification(notificationDTO); SendPostNotification(notificationDTO); });
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
        public void NotifyOnApproveFriendRequest(Guid ApprovedUserId, Guid userId)
        {
            Task.Run(async () => { 
                var accountProfile = await _unitOfWork.UserAccounts.FindAsync(p => p.Id == userId);
                var userAccount = new DomainModels.DTO.UserAccount
                {
                    Id = accountProfile.Id,
                    Email = accountProfile.Email,
                    UserName = accountProfile.UserName,
                    FirstName = accountProfile.FirstName,
                    LastName = accountProfile.LastName,
                    City = accountProfile.City,
                    Country = accountProfile.Country,
                    Bio = accountProfile.Bio,
                    Birthdate = accountProfile.Birthdate,
                    gender = accountProfile.gender.ToString(),
                    Type = accountProfile.Type.ToString(),
                };

                ApproveFriendRequestNotification(ApprovedUserId, userAccount);
            });
        }

        public void NotifyOnSendFriendRequest(Guid RecivedUserId, Guid userId)
        {
            Task.Run(async () => {
                var accountProfile = await _unitOfWork.UserAccounts.FindAsync(p => p.Id == userId);
                var userAccount = new DomainModels.DTO.UserAccount
                {
                    Id = accountProfile.Id,
                    Email = accountProfile.Email,
                    UserName = accountProfile.UserName,
                    FirstName = accountProfile.FirstName,
                    LastName = accountProfile.LastName,
                    City = accountProfile.City,
                    Country = accountProfile.Country,
                    Bio = accountProfile.Bio,
                    Birthdate = accountProfile.Birthdate,
                    gender = accountProfile.gender.ToString(),
                    Type = accountProfile.Type.ToString(),
                };

                SendFriendRequestNotification(RecivedUserId, userAccount);
            });
        }
        private void SendPostNotification(NotificationDTO notification)
        {
            _signalRActions.OnAddingPostAction?.Invoke(notification);
        }
        private void SendCommentNotification(Guid userOwnerId, CommentDTO commentDTO, Guid postId, PostsTypes postsType)
        {
            _signalRActions.OnAddingCommentAction?.Invoke(userOwnerId, commentDTO, postId, postsType);
        }
        private void SendFriendRequestNotification(Guid RecivedUserId, UserAccount userAccount)
        {
            _signalRActions.OnSendingFriendRequestAction?.Invoke(RecivedUserId, userAccount);
        }
        private void ApproveFriendRequestNotification(Guid ApprovedUserId, UserAccount userAccount)
        {
            _signalRActions.OnApprovedFriendRequestAction?.Invoke(ApprovedUserId, userAccount);
        }
    }
}
