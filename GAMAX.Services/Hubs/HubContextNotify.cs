using Business;
using DataBase.Core.Enums;
using DomainModels.DTO;
using Microsoft.AspNetCore.SignalR;

namespace GAMAX.Services.Hubs
{
    public class HubContextNotify
    {
        private readonly IHubContext<SingalRHub> _hubContext;
        private readonly UserConnectionManager _userConnectionManager;
        private readonly SignalRActions _signalRActions;
        public HubContextNotify(IHubContext<SingalRHub> hubContext , UserConnectionManager userConnectionManager , SignalRActions signalRActions)
        {
            _hubContext = hubContext;
            _userConnectionManager = userConnectionManager;
            _signalRActions = signalRActions;
            _signalRActions.OnAddingPostAction = OnAddingPost;
            _signalRActions.OnAddingCommentAction = OnAddingComment;
            _signalRActions.OnSendingFriendRequestAction = OnSendFriendRequest;
            _signalRActions.OnApprovedFriendRequestAction = OnApproveFriendRequest;
        }
        public async Task SendPrivateMessage(Guid RecivedUserId, string message)
        {
            var connID = _userConnectionManager.GetUserConnection(RecivedUserId);
            if (!string.IsNullOrEmpty(connID))
            {
                await _hubContext.Clients.Client(connID).SendAsync("ReceiveMessage", message);
            }
        }
        public async Task OnSendFriendRequest(Guid RecivedUserId, UserAccount userAccount)
        {
            var connID = _userConnectionManager.GetUserConnection(RecivedUserId);
            if (!string.IsNullOrEmpty(connID))
            {
                await _hubContext.Clients.Client(connID).SendAsync("OnSendFriendRequest", userAccount);
            }
        }
        public async Task OnApproveFriendRequest(Guid ApprovedUserId, UserAccount userAccount)
        {
            var connID = _userConnectionManager.GetUserConnection(ApprovedUserId);
            if (!string.IsNullOrEmpty(connID))
            {
                await _hubContext.Clients.Client(connID).SendAsync("OnApproveFriendRequest", userAccount);
            }
        }
        public async Task OnAddingPost(NotificationDTO notification)
        {
            await _hubContext.Clients.All.SendAsync("OnAddingPostOrQuestion", notification);
        }
        public async Task OnAddingComment(Guid PostOwnerUserId, CommentDTO comment, Guid POSTID, PostsTypes postsType)
        {
            var connID = _userConnectionManager.GetUserConnection(PostOwnerUserId);
            if (!string.IsNullOrEmpty(connID))
            {
                await _hubContext.Clients.Client(connID).SendAsync("OnAddingComment", new
                {
                    commentDTO = comment,
                    PostId = POSTID,
                    Type = postsType
                });
            }
        }
        public async Task OnAddingReactOnPost(Guid PostOwnerUserId, ReactsDTO react, Guid POSTID, PostsTypes postsType)
        {
            var connID = _userConnectionManager.GetUserConnection(PostOwnerUserId);
            if (!string.IsNullOrEmpty(connID))
            {
                await _hubContext.Clients.Client(connID).SendAsync("OnAddingReactOnPost", new
                {
                    reactDTO = react,
                    PostId = POSTID,
                    Type = postsType
                });
            }
        }
        public async Task OnAddingReactOnComment(Guid PostOwnerUserId, ReactsDTO react, Guid commentId, NotificatinTypes notificationType)
        {
            var connID = _userConnectionManager.GetUserConnection(PostOwnerUserId);
            if (!string.IsNullOrEmpty(connID))
            {
                await _hubContext.Clients.Client(connID).SendAsync("OnAddingReactOnComment", new
                {
                    reactDTO = react,
                    CommentId = commentId,
                    Type = notificationType
                });
            }
        }
    }
}
