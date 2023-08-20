﻿using Business;
using DataBase.Core.Enums;
using DomainModels.DTO;
using GAMAX.Services.Dto;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;

namespace GAMAX.Services.Hubs
{
    public class SingalRHub : Hub
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserConnectionManager _userConnectionManager;
        private readonly HubContextNotify _hubContextNotify;
        public SingalRHub(IHttpContextAccessor httpContextAccessor, UserConnectionManager userConnectionManager , HubContextNotify hubContextNotify)
        {
            _httpContextAccessor = httpContextAccessor;
            _userConnectionManager = userConnectionManager;
            _hubContextNotify = hubContextNotify;
            
        }
        public override Task OnConnectedAsync()
        {
            var userInfo = UserClaimsHelper.GetClaimsFromHttpContext(_httpContextAccessor);
            string connectionId = Context.ConnectionId;
            _userConnectionManager.AddUserConnection(userInfo.Uid, connectionId);
            //OnTestConnection(connectionId);
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var userInfo = UserClaimsHelper.GetClaimsFromHttpContext(_httpContextAccessor);
            _userConnectionManager.RemoveUserConnection(userInfo.Uid);
            return base.OnDisconnectedAsync(exception);
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
