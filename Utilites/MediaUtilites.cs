using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilites
{
    public static class MediaUtilites
    {
        //notes path must inclue media exctention e.g filePath = "path/to/save/image.jpg";
        private static string FolderPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())?.FullName, "StaticFiles");

        public static async Task SaveMediaAsync(byte[] mediaBytes, string filePath)
        {
            await File.WriteAllBytesAsync(filePath, mediaBytes);
        }

        public static async Task SaveMediaAsync(Stream mediaStream, string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await mediaStream.CopyToAsync(fileStream);
            }
        }
        public static string ConverIformToPath(IFormFile formFile, string FileFolderPath)
        {
            if (formFile != null && formFile.Length > 0)
            {
                string fileExtension = Path.GetExtension(formFile.FileName);
                string newFileName = $"{Guid.NewGuid()}{fileExtension}";
                string newFilePath = Path.Combine(FolderPath, FileFolderPath, newFileName);

                using (var fileStream = new FileStream(newFilePath, FileMode.Create))
                {
                     formFile.CopyTo(fileStream);
                }

                return (Path.Combine("StaticFiles", FileFolderPath, newFileName));
            }
            return null;

        }
    }
}
