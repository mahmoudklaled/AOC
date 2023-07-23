using Business.Accounts.Services;
using DataBase.Core.Models.Accounts;
using GAMAX.Services.Dto;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost("GetProfileAcountData")]  
        public async Task<IActionResult> GetProfileAcountData()
        {
            var userInfo = UserClaimsHelper.GetClaimsFromHttpContext(_httpContextAccessor);
            var accountProfile = await _accountService.GetAccountProfileAsync(userInfo.Email);
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
                    Message = "something Went wrong!"
                });
            return Ok(accountProfileUpdate);
        }
        
        [HttpPost("AddProfilePhoto")]
        public async Task<IActionResult> UpdateProfilePhoto(IFormFile formFile)
        {
            var userInfo = UserClaimsHelper.GetClaimsFromHttpContext(_httpContextAccessor);
            var result = await _accountService.UpdateProfilePhotoAsync(formFile, userInfo.Email);
            if(result)
                return Ok(result);
            return BadRequest(new { message = "something wend wrong" });
        }
        
        [HttpPost("AddProfileCover")]
        public async Task<IActionResult> UpdateProfileCover(IFormFile formFile)
        {
            var userInfo = UserClaimsHelper.GetClaimsFromHttpContext(_httpContextAccessor);
            var result = await _accountService.UpdateProfileCoverAsync(formFile, userInfo.Email);
            if (result)
                return Ok(result);
            return BadRequest(new { message = "something wend wrong" });
        }
        
        [HttpPost("SearchAccount")]
        public async Task<IActionResult> SearchAccount(string searchString)
        {
            var searchResult = await _accountService.SearchAccountsAsync(searchString);
            return Ok(searchResult);
        }

    }

}
