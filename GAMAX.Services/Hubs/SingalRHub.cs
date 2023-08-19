using Business;
using DataBase.Core.Enums;
using DomainModels.DTO;
using GAMAX.Services.Dto;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;

namespace GAMAX.Services.Hubs
{
    public class SingalRHub:Hub
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Dictionary<Guid, string> _userConnectionMap = new Dictionary<Guid, string>();
        private readonly SignalRActions _signalRActions;
        public SingalRHub(IHttpContextAccessor httpContextAccessor ,SignalRActions signalRActions)
        {
            _httpContextAccessor = httpContextAccessor;
            _signalRActions = signalRActions;
            _signalRActions.OnAddingPostAction = OnAddingPost;
            _signalRActions.OnAddingCommentAction = OnAddingComment;
            _signalRActions.OnSendingFriendRequestAction = OnSendFriendRequest;
            _signalRActions.OnApprovedFriendRequestAction = OnApproveFriendRequest;
        }
        public override Task OnConnectedAsync()
        {
            var userInfo = UserClaimsHelper.GetClaimsFromHttpContext(_httpContextAccessor);
            string connectionId = Context.ConnectionId;

            _userConnectionMap[userInfo.Uid] = connectionId;
            //OnTestConnection(connectionId);
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var userInfo = UserClaimsHelper.GetClaimsFromHttpContext(_httpContextAccessor);
            if (_userConnectionMap.ContainsKey(userInfo.Uid))
            {
                _userConnectionMap.Remove(userInfo.Uid);
            }
            return base.OnDisconnectedAsync(exception);
        }
        public async Task SendPrivateMessage(Guid RecivedUserId, string message)
        {
            if (_userConnectionMap.TryGetValue(RecivedUserId, out string targetConnectionId))
            {
                await Clients.Client(targetConnectionId).SendAsync("ReceiveMessage", message);
            }
            else
            {
                // Handle the case where the target user is not currently connected
            }
        }
        public async Task OnSendFriendRequest(Guid RecivedUserId, UserAccount userAccount)
        {
            if (_userConnectionMap.TryGetValue(RecivedUserId, out string targetConnectionId))
            {
                await Clients.Client(targetConnectionId).SendAsync("OnSendFriendRequest", userAccount);
            }
        }
        public async Task OnApproveFriendRequest(Guid ApprovedUserId, UserAccount userAccount)
        {
            if (_userConnectionMap.TryGetValue(ApprovedUserId, out string targetConnectionId))
            {
                await Clients.Client(targetConnectionId).SendAsync("OnApproveFriendRequest", userAccount);
            }
        }
        public async Task OnAddingPost(NotificationDTO notification)
        {
            await Clients.All.SendAsync("OnAddingPostOrQuestion", notification);
        }
        public async Task OnAddingComment(Guid PostOwnerUserId, CommentDTO comment ,Guid POSTID ,PostsTypes postsType)
        {
            if (_userConnectionMap.TryGetValue(PostOwnerUserId, out string targetConnectionId))
            {
                await Clients.Client(targetConnectionId).SendAsync("OnAddingComment", new {
                commentDTO = comment,
                PostId = POSTID,
                Type = postsType
                });
            }
        }
        public async Task OnAddingReactOnPost(Guid PostOwnerUserId, ReactsDTO react, Guid POSTID, PostsTypes postsType)
        {
            if (_userConnectionMap.TryGetValue(PostOwnerUserId, out string targetConnectionId))
            {
                await Clients.Client(targetConnectionId).SendAsync("OnAddingReactOnPost", new
                {
                    reactDTO = react,
                    PostId = POSTID,
                    Type = postsType
                });
            }
        }
        public async Task OnAddingReactOnComment(Guid PostOwnerUserId, ReactsDTO react, Guid commentId, NotificatinTypes notificationType)
        {
            if (_userConnectionMap.TryGetValue(PostOwnerUserId, out string targetConnectionId))
            {
                await Clients.Client(targetConnectionId).SendAsync("OnAddingReactOnComment", new
                {
                    reactDTO = react,
                    CommentId = commentId,
                    Type = notificationType
                });
            }
        }
        //public Task OnTestConnection(string id)
        //{
        //    Clients.Client(id).SendAsync("OnTestConnection", "Hello youre connected!");
        //    return Task.CompletedTask;
        //}
        //public Task OnInvokeConnection(string Message)
        //{
        //    Clients.All.SendAsync("OnInvokeConnection", "Hello youre OnInvokeConnection!" + Message);
        //    return Task.CompletedTask;
        //}
        //public async Task OnAddingQuestionPost(Guid postId)
        //{
        //    await Clients.All.SendAsync("OnAddingQuestionPost", postId);

        //}
    }
}
