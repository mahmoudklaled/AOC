using GAMAX.Services.Models;
using GAMAX.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GAMAX.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterAsync(model);

            return Ok(result);
        }

        [HttpGet("verify")]
        public async Task<IActionResult> Verify(string Email , string verificationCode)
        {
            VerificationModel model = new VerificationModel();
            model.VerificationCode = verificationCode;
            model.Email = Email;
            if (Email==null && verificationCode ==null)
                return BadRequest("wrong params");

            var result = await _authService.VerifyAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            //SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);
            string redirectUrl = "http://localhost:3000/login?verfiy=true";
            return Redirect(redirectUrl);
            
        }

        [HttpPost("token")]
        public async Task<IActionResult> GetTokenAsync([FromBody]string ? refreshToekn)
        {
            var refreshTokenFromCookies = Request.Cookies["refreshToken"];
            if (refreshTokenFromCookies == null && refreshToekn==null)
            {
                return BadRequest("Refresh Token Requird");
            }
            string NeededRefreshToken;
            if (refreshTokenFromCookies != null)
                  NeededRefreshToken = refreshTokenFromCookies;
            else
                NeededRefreshToken = refreshToekn;

            var result = await _authService.GetTokenAsync(NeededRefreshToken);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            if (!string.IsNullOrEmpty(result.RefreshToken))
                SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

            return Ok(result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] TokenRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _authService.GetTokenAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            if (!string.IsNullOrEmpty(result.RefreshToken))
                SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

            return Ok(result);
        }

        [HttpPost("addRole")]
        public async Task<IActionResult> AddRoleAsync([FromBody] AddRoleModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.AddRoleAsync(model);

            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);

            return Ok(model);
        }

        [HttpGet("refreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (refreshToken == null)
                return BadRequest("refresh Token Needed in Cookies");

            var result = await _authService.RefreshTokenAsync(refreshToken);

            if (!result.IsAuthenticated)
                return BadRequest(result);

            SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

            return Ok(result);
        }

        [HttpPost("revokeToken")]
        public async Task<IActionResult> RevokeToken([FromBody] RevokeToken model)
        {
            var token = Request.Cookies["refreshToken"] ?? model.Token  ;

            if (string.IsNullOrEmpty(token))
                return BadRequest("Token is required!");

            var result = await _authService.RevokeTokenAsync(token);
            
            if (!result)
                return BadRequest("Token is invalid!");

            RemoveRefreshTokenFromCookie();
            return Ok();
        }

        private void SetRefreshTokenInCookie(string refreshToken, DateTime expires)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = expires.ToLocalTime(),
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.None
            };

            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
        private void RemoveRefreshTokenFromCookie()
        {
            if (Request.Cookies.TryGetValue("refreshToken", out string refreshToken))
            {
                Response.Cookies.Delete("refreshToken");
            }
        }

    }
}
