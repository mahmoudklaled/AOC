using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;
using GAMAX.Services.Services;
using GAMAX.Services.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using GAMAX.Services.MiddleWare;
using Microsoft.Extensions.FileProviders;
using Business.Accounts.Services;
using Business.Posts.Services;
using DataBase.Core;
using DataBase.EF;
using DataBase.Core.Models.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);



builder.Services.AddHttpContextAccessor();
builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
     .AddEntityFrameworkStores<ApplicationDbContext>()
     .AddDefaultTokenProviders();

builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddTransient<IMailingService, MailingService>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAcountService, AcountService>();
builder.Services.AddScoped<IPostService, PostService>();

builder.Services.AddMailKit(config =>
{
    var configuration = builder.Configuration;
    config.UseMailKit(configuration.GetSection("Email").Get<MailKitOptions>());
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(o =>
    {
        o.RequireHttpsMetadata = false;
        o.SaveToken = false;
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddControllers();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSwaggerUI",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
    options.AddPolicy("AllowAnyOrigin",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});






// Retrieve the configuration from the builder
var configuration = builder.Configuration;

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));





// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();





var app = builder.Build();

var apiProjectPath = Directory.GetCurrentDirectory();
var solutionPath = Directory.GetParent(apiProjectPath)?.FullName;
var photosFolderPath = Path.Combine(solutionPath, "StaticFiles");

// Configure the static files middleware
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(photosFolderPath),
    RequestPath = "/Photos"
});

// Define the routes that should skip token validation
var routesToSkipTokenValidation = new List<string>
{ 
    "/api/Auth/register",
    "/api/Auth/verify",
    "/api/Auth/token",
    "/api/Auth/login",
    "/api/Auth/refreshToken",
    "/api/Auth/revokeToken",
    "/api/Auth/ResendConfirmMail",
    "/api/Auth/ResetPasswordCode",
    "/api/Auth/UpdatePassword",
    "/api/StaticFiles/download"
};

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseWhen(context => !routesToSkipTokenValidation.Contains(context.Request.Path.Value), builder =>
{
    builder.UseMiddleware<TokenValidationMiddleware>();
});

app.UseCors(builder =>
{
    builder.WithOrigins("*")
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
});

app.UseAuthentication();
app.UseAuthorization();
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();
//});
app.MapControllers();

app.Run();
