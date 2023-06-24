using Business.Accounts.Models;
using Business.Authentication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Linq;
using AutoMapper;
using Elfie.Serialization;

namespace Business.Accounts.Services
{
    public class AcountService : IAcountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _dbContext;
        public AcountService(UserManager<ApplicationUser> userManager , ApplicationDbContext applicationDbContext )
        {
            _dbContext = applicationDbContext;
            _userManager = userManager;
        }
        public async Task<ProfileAccounts> GetAccountProfileAsync(string userNameId)
        {
            var accountProfile = await _dbContext.ProfileAccounts
                                                    .Where(a => a.UserName.Equals(userNameId))
                                                    .Include(p => p.Posts)
                                                    .Include(q => q.QuestionPosts)
                                                    .FirstOrDefaultAsync();

            return accountProfile;
        }

        public async Task<bool> UpdateAccountProfileAsync(ProfileUpdateModel profileUpdateModel)
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProfileUpdateModel, ProfileAccounts>();
            });
            var mapper = mapperConfig.CreateMapper();
            var result = await _dbContext.ProfileAccounts.FindAsync(profileUpdateModel.Id);
            if (result == null) return false;
            mapper.Map(profileUpdateModel, result);
            var update  = await _dbContext.SaveChangesAsync();
            if(update>0)
                return true;
            return false;
        }
    }
}
