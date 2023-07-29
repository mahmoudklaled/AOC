using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Accounts.LogicBusiness
{
    public static class AccountHelpers
    {
        private static string parentFolder = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
        private static string PhotoPath = Path.Combine("StaticFiles", "Photos");

        public static string GetDefaultProfilePohot(Guid id)
        {
            var sourceFile = Path.Combine(parentFolder, PhotoPath); 
            CopyAndRenamePhoto(sourceFile, Path.Combine(sourceFile, "ProfilePhoto"), "Profile.jpg", id.ToString() + "Profile.jpg");
            return Path.Combine(PhotoPath, "ProfilePhoto", id.ToString()+"Profile.jpg");
        }
        public static string GetDefaultCoverPohot(Guid id)
        {
            var sourceFile = Path.Combine(parentFolder, PhotoPath);
            CopyAndRenamePhoto(sourceFile, Path.Combine(sourceFile, "CoverPhoto"), "Cover.jpg", id.ToString() + "Cover.jpg");
            return Path.Combine(PhotoPath, "CoverPhoto", id.ToString()+"Cover.jpg");
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
        public static void CopyAndRenamePhoto(string sourceFolderPath, string destinationFolderPath, string photoName, string newPhotoName)
        {
            try
            {
                string sourceFilePath = Path.Combine(sourceFolderPath, photoName);
                string destinationDirectoryPath = Path.Combine(destinationFolderPath);
                string destinationFilePath = Path.Combine(destinationDirectoryPath, newPhotoName);

                if (File.Exists(sourceFilePath))
                {
                    File.Copy(sourceFilePath, destinationFilePath,true);

                }
            }
            catch (Exception ex)
            {
            }
        }

    }
}
