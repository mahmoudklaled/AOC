using Business.Services;
using GAMAX.Services.Dto;
using GAMAX.Services.Hubs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GAMAX.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendsController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAcountService _accountService;
        private readonly INotificationServices _notificationServices;
        private readonly HubContextNotify _hubContextNotify;
        public FriendsController(IHttpContextAccessor httpContextAccessor, IAcountService acountService, INotificationServices notificationServices , HubContextNotify hubContextNotify)
        {
            _httpContextAccessor = httpContextAccessor;
            _accountService = acountService;
            _notificationServices = notificationServices;
            _hubContextNotify = hubContextNotify;
        }
        [HttpPost("SearchAccount")]
        public async Task<IActionResult> SearchAccount(string searchString)
        {
            var searchResult = await _accountService.SearchAccountsAsync(searchString);
            return Ok(searchResult);
        }
        [HttpPost("SendFriendRequest")]
        public async Task<IActionResult> SendFriendRequest(Guid userId)
        {
            var userInfo = UserClaimsHelper.GetClaimsFromHttpContext(_httpContextAccessor);
            var searchResult = await _accountService.SendFriendRequest(userInfo.Uid, userId);
            _notificationServices.NotifyOnSendFriendRequest(userId, userInfo.Uid);
            return Ok(searchResult);
        }
        [HttpPost("GetPendingFriendRequest")]
        public async Task<IActionResult> GetPendingFriendRequest()
        {
            var userInfo = UserClaimsHelper.GetClaimsFromHttpContext(_httpContextAccessor);
            var pendingRequests = await _accountService.GetPendingFriendRequest(userInfo.Uid);
            return Ok(pendingRequests);
        }
        [HttpPost("AproveFriendRequest")]
        public async Task<IActionResult> AproveFriendRequest(Guid RequestId)
        {
            var userInfo = UserClaimsHelper.GetClaimsFromHttpContext(_httpContextAccessor);
            var searchResult = await _accountService.AproveFriendRequest(RequestId);
            _notificationServices.NotifyOnApproveFriendRequest(searchResult.Item2, userInfo.Uid);
            return Ok(searchResult.Item1);
        }
        [HttpPost("DeneyFriendRequest")]
        public async Task<IActionResult> DeneyFriendRequest(Guid RequestId)
        {
            var userInfo = UserClaimsHelper.GetClaimsFromHttpContext(_httpContextAccessor);
            var searchResult = await _accountService.DeneyFriendRequest(RequestId);
            return Ok(searchResult);
        }
        [HttpPost("DeleteFriend")]
        public async Task<IActionResult> DeleteFriend(Guid UserId)
        {
            var userInfo = UserClaimsHelper.GetClaimsFromHttpContext(_httpContextAccessor);
            var searchResult = await _accountService.DeleteFriend(userInfo.Uid, UserId);
            return Ok(searchResult);
        }
    }
}
