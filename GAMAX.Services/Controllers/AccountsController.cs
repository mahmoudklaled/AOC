using Business.Accounts.Services;
using DataBase.Core.Enums;
using DataBase.Core.Models.Accounts;
using GAMAX.Services.Dto;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
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
            var profileInfo = new Dto.UserAccounts
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
            return Ok(profileInfo);
        }
        
        [HttpPost("UpdateProfileAcountData")]
        public async Task<IActionResult> UpdateProfileAcountData([FromBody] Dto.ProfileUpdateModel profileUpdateModel)
        {
            var userInfo = UserClaimsHelper.GetClaimsFromHttpContext(_httpContextAccessor);
            if (userInfo.Uid != profileUpdateModel.Id)
                return BadRequest();
            Gender type;
            Enum.TryParse(profileUpdateModel.gender, out type);
            var updateModel = new DataBase.Core.Models.Accounts.ProfileUpdateModel
            {
                Id = profileUpdateModel.Id,
                Bio = profileUpdateModel.Bio,
                City = profileUpdateModel.City,
                Country = profileUpdateModel.Country,
                FirstName = profileUpdateModel.FirstName,
                LastName = profileUpdateModel.LastName,
                Birthdate = profileUpdateModel.Birthdate,
                gender=type
            };
            var accountProfileUpdate = await _accountService.UpdateAccountProfileAsync(updateModel);
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
