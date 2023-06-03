using GAMAX.Services.Models;
using System.IdentityModel.Tokens.Jwt;

namespace GAMAX.Services.Services
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterModel model);
        //Task<JwtSecurityToken> Login(Models.LoginModel model);
        Task<AuthModel> VerifyAsync(VerificationModel model);
        Task<AuthModel> GetTokenAsync(TokenRequestModel model);
        Task<AuthModel> GetTokenAsync(string refreshToken);
        Task<string> AddRoleAsync(AddRoleModel model);
        Task<AuthModel> RefreshTokenAsync(string token);
        Task<bool> RevokeTokenAsync(string token);
    }
    
}
