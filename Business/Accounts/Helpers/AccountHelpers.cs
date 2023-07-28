using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Accounts.LogicBusiness
{
    public static class AccountHelpers
    {
        //Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
        private static string PhotoPath = Path.Combine("StaticFiles", "Photos");

        public static string GetDefaultProfilePohot(Guid id)
        {
            return Path.Combine(PhotoPath, "ProfilePhoto", id.ToString()+"Profile.png");
        }
        public static string GetDefaultCoverPohot(Guid id)
        {
            return Path.Combine(PhotoPath, "CoverPhoto", id.ToString()+"Cover.png");
        }
        public static string IformToProfilePath(IFormFile formFile,  Guid userId)
        {
            if (formFile != null && formFile.Length > 0)
            {
                string fileExtension = Path.GetExtension(formFile.FileName);
                string newFileName = $"{userId}Profile.{fileExtension}";
                string newFilePath = Path.Combine(PhotoPath, "ProfilePhoto", newFileName);

                using (var fileStream = new FileStream(newFilePath, FileMode.Create))
                {
                    formFile.CopyTo(fileStream);
                }

                return (Path.Combine(PhotoPath, "ProfilePhoto", newFileName));
            }
            return string.Empty;

        }
        public static string IformToCoverPath(IFormFile formFile, Guid userId)
        {
            if (formFile != null && formFile.Length > 0)
            {
                string fileExtension = Path.GetExtension(formFile.FileName);
                string newFileName = $"{userId}Cover.{fileExtension}";
                string newFilePath = Path.Combine(PhotoPath, "CoverPhoto", newFileName);

                using (var fileStream = new FileStream(newFilePath, FileMode.Create))
                {
                    formFile.CopyTo(fileStream);
                }

                return (Path.Combine(PhotoPath, "CoverPhoto", newFileName));
            }
            return string.Empty;

        }
    }
}
