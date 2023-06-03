using GAMAX.Services.Models;
using System.IdentityModel.Tokens.Jwt;

namespace GAMAX.Services.Services
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterModel model);
        Task<AuthModel> VerifyAsync(VerificationModel model);
        Task<AuthModel> GetTokenAsync(TokenRequestModel model);
        Task<AuthModel> GetTokenAsync(string refreshToken);
        Task<string> AddRoleAsync(AddRoleModel model);
        Task<AuthModel> RefreshTokenAsync(string token);
        Task<bool> RevokeTokenAsync(string token);
        Task<string> SendNewConfirmMail(ApplicationUser user);
        Task<string> SendResetPasswordMail(ApplicationUser user);
        Task<bool> ResetPassword(RessetPassword user);
    }
    
}
