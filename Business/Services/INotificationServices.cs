using DataBase.Core.Enums;
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
        Task<bool> AddNotification(NotificationDTO notification);
        void NotifyOnAddingComment(CommentDTO commentDTO, Guid postId, PostsTypes postsType);
        void NotifyOnAddingPost(PostDTO postDTO);
        void NotifyOnAddingQuestion(QuestionPostDTO postDTO);
        void NotifyOnAddingReactPost(ReactsDTO reactDTO , AddReactRequest reactRequest);
        void NotifyOnAddingReactQuestionPost(ReactsDTO reactDTO, AddReactRequest reactRequest);
        void NotifyOnAddingReactOnComment(ReactsDTO reactDTO, AddReactRequest reactRequest);
        void NotifyOnAddingReactOnAnswer(ReactsDTO reactDTO, AddReactRequest reactRequest);
        void NotifyOnApproveFriendRequest(Guid ApprovedUserId, Guid userId);
        void NotifyOnSendFriendRequest(Guid RecivedUserId, Guid userId);
        void RemoveAllUserNotification(Guid Id);
        Task<IEnumerable<NotificationDTO>> GetAllUserNotifications(Guid userId);
    }
}
