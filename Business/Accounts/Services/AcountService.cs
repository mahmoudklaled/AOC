using BDataBase.Core.Models.Accounts;
using DataBase.Core.Models.Accounts;
using DataBase.Core;

namespace Business.Accounts.Services
{
    public class AcountService : IAcountService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AcountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ProfileAccounts> GetAccountProfileAsync(string email)
        {
            string[] includes = { "Posts", "QuestionPosts" };
            var accountProfile = await _unitOfWork.ProfileAccount.FindAllAsync(p => p.Email == email, includes);
            return accountProfile.FirstOrDefault();
        }

        public async Task<bool> UpdateAccountProfileAsync(ProfileUpdateModel profileUpdateModel)
        {
            var profileAccount= await _unitOfWork.ProfileAccount.FindAsync(p=>p.Email==profileUpdateModel.Email);
            var UpdateProfileAccount = new ProfileAccounts
            {
                Id = profileAccount.Id,
                UserName= profileAccount.UserName,
                Email = profileAccount.Email,
                FirstName = profileUpdateModel.FirstName,
                LastName = profileUpdateModel.LastName,
                City = profileUpdateModel.City,
                Country = profileUpdateModel.Country
            };
             _unitOfWork.ProfileAccount.Update(UpdateProfileAccount);
            var UpdateResult = _unitOfWork.Complete();
            return await UpdateResult > 0;
        }

        public Task<bool> UpdateProfileCoverAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateProfilePhotoAsync()
        {
            throw new NotImplementedException();
        }
    }
}
