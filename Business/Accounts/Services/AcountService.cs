using BDataBase.Core.Models.Accounts;
using DataBase.Core.Models.Accounts;
using DataBase.Core;
using Microsoft.AspNetCore.Http;
using Utilites;

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

        public async Task<bool> UpdateProfileCoverAsync(IFormFile formFile , string email)
        {
            var photoPath = MediaUtilites.ConverIformToPath(formFile, "ProfilePhoto");
            string[] includes = { "ProfilePohot" };
            var profileAccount = await _unitOfWork.ProfileAccount.FindAsync(p => p.Email == email, includes);
            if (profileAccount== null) return false;
            if (profileAccount.ProfilePohot != null)
                 _unitOfWork.ProfilePhoto.Delete(profileAccount.ProfilePohot);
            profileAccount.ProfilePohot = new DataBase.Core.Models.PhotoModels.ProfilePhoto
            {
                Id = Guid.NewGuid(),
                PhotoPath = photoPath,
                ProfileId = profileAccount.Id
            };
            var result = await _unitOfWork.Complete();
            return result > 0;
             
        }

        public async Task<bool> UpdateProfilePhotoAsync(IFormFile formFile, string email)
        {
            var photoPath = MediaUtilites.ConverIformToPath(formFile, "ProfilePhoto");
            string[] includes = { "ProfilePohot" };
            var profileAccount = await _unitOfWork.ProfileAccount.FindAsync(p => p.Email == email, includes);
            if (profileAccount == null) return false;
            if (profileAccount.CoverPhoto != null)
                _unitOfWork.CoverPhoto.Delete(profileAccount.CoverPhoto);
            profileAccount.CoverPhoto = new DataBase.Core.Models.PhotoModels.CoverPhoto
            {
                Id = Guid.NewGuid(),
                PhotoPath = photoPath,
                ProfileId = profileAccount.Id
            };
            var result = await _unitOfWork.Complete();
            return result > 0;
        }
    }
}
