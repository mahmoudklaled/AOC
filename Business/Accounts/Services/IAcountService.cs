using Business.Accounts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Accounts.Services
{
    public interface IAcountService
    {
        Task<ProfileAccounts> GetAccountProfileAsync(string userId);
        Task<bool> UpdateAccountProfileAsync(ProfileUpdateModel profileUpdateModel);
    }
}
