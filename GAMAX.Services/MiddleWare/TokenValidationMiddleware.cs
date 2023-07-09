using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace GAMAX.Services.MiddleWare
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Retrieve the access token from the request headers
            string accessToken = context.Request.Cookies["Authorization"];//.FirstOrDefault()?.Split(" ").Last();
            //accessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Ik1haG1vdWRLaGFsZWQ1MzY3MjI2NSIsImp0aSI6IjZjNWZmMWJhLTc5ZmMtNDg5ZC04YzY5LTAxOGFjNGNlOWQ0OCIsImVtYWlsIjoiTWFobW91ZC5raGFsZWQxOTY0MEBnbWFpbC5jb20iLCJ1aWQiOiJmZjRkMmRmMS04ODU4LTQwZTctYjg5Mi01OGMwZTIxZWM3NzgiLCJyb2xlcyI6IlVzZXIiLCJleHAiOjE2ODg3NTE3NzMsImlzcyI6IlNlY3VyZUFwaSIsImF1ZCI6IlNlY3VyZUFwaVVzZXIifQ.KyRT5ylz8cHxoNMuHQiBpwdnt7Esa_PtJ5_C2BcrnD4";
            if (!string.IsNullOrEmpty(accessToken))
            {
                try
                {
                    // Validate the access token
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var validationParameters = GetTokenValidationParameters();

                    // Validate the token
                    ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(accessToken, validationParameters, out SecurityToken validatedToken);

                    // Set the validated claims principal on the current request
                    context.User= claimsPrincipal;

                    // Get the roles from the token and set them in the context
                    var roles = claimsPrincipal.FindAll("roles").Select(c => c.Value).ToList();
                    context.Items["Roles"] = roles;
                }
                catch (SecurityTokenException)
                {
                    // Token is invalid or expired
                    var responseMessage = new { Message = "Access Token is invalid or Expired" };
                    var responseJson = JsonSerializer.Serialize(responseMessage);
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(responseJson, Encoding.UTF8);
                    return;
                }
            }
            else
            {
                // Access token is missing
                var responseMessage = new { Message = "Access Token is Missing!" };
                var responseJson = JsonSerializer.Serialize(responseMessage);
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(responseJson, Encoding.UTF8);
                return;
            }

            // Call the next middleware or controller
            await _next(context);
        }

        private TokenValidationParameters GetTokenValidationParameters()
        {
            var configuration = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json")
                            .Build();

            // Read the values from the configuration file
            string validIssuer = configuration["JWT:Issuer"];
            string validAudience = configuration["JWT:Audience"];
            string key = configuration["JWT:Key"];
            // Create and configure the token validation parameters
            var validationParameters = new TokenValidationParameters
            {
                // Set your token validation settings here (e.g., issuer, audience, signing key)
                ValidIssuer = validIssuer,
                ValidAudience = validAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
            };

            return validationParameters;
        }
    }


}
