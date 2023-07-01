using DataBase.Core.Models.Posts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Posts.Helper
{
    public static class PostHelper
    {
        private static string FolderPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())?.FullName, "StaticFiles");
        
        public static List<string> SaveFiles(List<IFormFile> files, string savePath ,AllPostsModel allPostsModel , ref List<string> photos , ref List<string> vedios)
        {
            List<string> filePaths = new List<string>();

            if (files != null && files.Any())
            {
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        string fileName = Path.GetFileName(file.FileName);
                        string filePath = Path.Combine(savePath, fileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }

                        filePaths.Add(filePath);
                    }
                }
            }

            return filePaths;
        }
        public  static AllPostsModel ConvertToPaths(AllPostsModel model, string photoFolderPath, string videoFolderPath)
        {
            if (model.Photos != null)
            {
                List<string> photoPaths = new List<string>();
                foreach (var photo in model.Photos)
                {
                    if (photo != null && photo.Length > 0)
                    {
                        string photoExtension = Path.GetExtension(photo.FileName);
                        string newPhotoFileName = $"{Guid.NewGuid()}{photoExtension}";
                        string photoPath = Path.Combine(FolderPath, photoFolderPath, newPhotoFileName);

                        using (var fileStream = new FileStream(photoPath, FileMode.Create))
                        {
                            photo.CopyTo(fileStream);
                        }

                        photoPaths.Add(Path.Combine("StaticFiles", photoFolderPath, newPhotoFileName));
                    }
                }

                model.PhotosPath = photoPaths;
            }

            if (model.Vedios != null)
            {
                List<string> videoPaths = new List<string>();
                foreach (var video in model.Vedios)
                {
                    if (video != null && video.Length > 0)
                    {
                        string videoExtension = Path.GetExtension(video.FileName);
                        string newVideoFileName = $"{Guid.NewGuid()}{videoExtension}";
                        string videoPath = Path.Combine(FolderPath, videoFolderPath, newVideoFileName);

                        using (var fileStream = new FileStream(videoPath, FileMode.Create))
                        {
                            video.CopyTo(fileStream);
                        }

                        videoPaths.Add(Path.Combine("StaticFiles", videoFolderPath, newVideoFileName));
                    }
                }

                model.VediosPath = videoPaths;
            }

            return model;
        }
        public static string ConverIformToPath(IFormFile formFile , string FileFolderPath)
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
