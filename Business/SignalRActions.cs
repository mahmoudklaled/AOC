using BDataBase.Core.Models.Accounts;
using DataBase.Core.Enums;
using DataBase.Core.Models.Notifications;
using DataBase.Core.Models.Posts;
using DomainModels.DTO;

namespace Business
{
    public class SignalRActions
    {
        public SignalRActions()
        {
            
        }
        public Func<NotificationDTO, Task> OnAddingPostAction { get; set; }
        public Func<Guid , CommentDTO , Guid , PostsTypes , Task> OnAddingCommentAction { get; set; }
        public Func<Guid , UserAccount , Task> OnSendingFriendRequestAction { get; set; }
        public Func<Guid , UserAccount , Task> OnApprovedFriendRequestAction { get; set; }
        public Func<Guid, ReactsDTO, Guid, PostsTypes, Task> OnAddingReactOnPostAction { get; set; }
        public Func<Guid, ReactsDTO, Guid, PostsTypes, Task> OnAddingReactOnCommentAction { get; set; }
    }
}
