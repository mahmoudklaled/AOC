using Business.Accounts.Models;
using Business.Accounts.Services;
using GAMAX.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.Identity.Client;
using System.Security.Claims;

namespace GAMAX.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAcountService _accountService;
        public AccountsController(IHttpContextAccessor httpContextAccessor ,IAcountService acountService)
        {
            _httpContextAccessor = httpContextAccessor;
            _accountService = acountService;
        }

        [HttpPost("GetProfileAcountData")]  //TODO: take email or username as params not token
        public async Task<IActionResult> GetProfileAcountData([FromBody] string? accessToken)
        {
            HttpContext context = _httpContextAccessor.HttpContext;
            string userNameId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            string email = context.User.FindFirst(ClaimTypes.Email)?.Value;
            var accountProfile = await _accountService.GetAccountProfileAsync(userNameId);
            if (accountProfile == null)
                return BadRequest(new
                {
                    message = "Account Not Found !"
                });
            return Ok(accountProfile);
        }
        [HttpPost("UpdateProfileAcountData")]
        public async Task<IActionResult> UpdateProfileAcountData([FromBody] ProfileUpdateModel profileUpdateModel)
        {
            

            var accountProfileUpdate = await _accountService.UpdateAccountProfileAsync(profileUpdateModel);
            if (!accountProfileUpdate)
                return BadRequest(new
                {
                    message = "something Went wrong!"
                });
            return Ok(accountProfileUpdate);
        }
    }

}
