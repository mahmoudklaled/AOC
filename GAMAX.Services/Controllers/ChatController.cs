using Business.Services;
using DomainModels.DTO;
using GAMAX.Services.Dto;
using GAMAX.Services.Hubs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GAMAX.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ChatController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IChatServices _chatServices ;
        private readonly HubContextNotify _hubContextNotify;
        public ChatController(IHttpContextAccessor httpContextAccessor , IChatServices chatServices , HubContextNotify hubContextNotify)
        {
            _hubContextNotify = hubContextNotify;
            _httpContextAccessor = httpContextAccessor;
            _chatServices = chatServices;
        }
        [HttpPost("SendPrivateMessage")]
        public async Task<IActionResult> SearchAccount(ChatDTO chatDTO)
        {
            var userInfo = UserClaimsHelper.GetClaimsFromHttpContext(_httpContextAccessor);
            var result = await _chatServices.SendPrivateMessage(chatDTO);
            await _hubContextNotify.SendPrivateMessage(chatDTO.ReciveId, chatDTO);
            return Ok(result);
        }
        [HttpPost("GetUserChat")]
        public async Task<IActionResult> GetUserChat(Guid secoundUserId)
        {
            var userInfo = UserClaimsHelper.GetClaimsFromHttpContext(_httpContextAccessor);
            var result = await _chatServices.GetUserChat(userInfo.Uid ,secoundUserId);
            return Ok(result);
        }
        [HttpPost("MarkUserChatAsRead")]
        public async Task<IActionResult> MarkUserChatAsRead(Guid secoundUserId)
        {
            var userInfo = UserClaimsHelper.GetClaimsFromHttpContext(_httpContextAccessor);
            var result = await _chatServices.MarkUserChatAsRead(userInfo.Uid, secoundUserId);
            return Ok(result);
        }
        [HttpPost("GetFriendsWithLastMessage")]
        public async Task<IActionResult> GetFriendsWithLastMessage()
        {
            var userInfo = UserClaimsHelper.GetClaimsFromHttpContext(_httpContextAccessor);
            var result = await _chatServices.GetFriendsWithLastMessage(userInfo.Uid);
            return Ok(result);
        }
    }
}
