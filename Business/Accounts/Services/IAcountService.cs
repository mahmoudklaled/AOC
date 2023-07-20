using BDataBase.Core.Models.Accounts;
using DataBase.Core.Models.Accounts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Accounts.Services
{
    public interface IAcountService
    {
        Task<ProfileAccounts> GetAccountProfileAsync(string email);
        Task<bool> UpdateAccountProfileAsync(ProfileUpdateModel profileUpdateModel);
        Task<bool> UpdateProfilePhotoAsync(IFormFile formFile, string email);
        Task<bool> UpdateProfileCoverAsync(IFormFile formFile, string email);
    }
}
