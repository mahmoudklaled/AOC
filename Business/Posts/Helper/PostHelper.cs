using DataBase.Core.Models.Posts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilites;

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
        public static AllPostsModel ConvertToPaths(UploadPost post, string photoFolderPath, string videoFolderPath)
        {
            var model = mappingModel(post);
            if (model.Photos != null)
            {
                List<string> photoPaths = new List<string>();
                foreach (var photo in model.Photos)
                {
                    var path = MediaUtilites.ConverIformToPath(photo, photoFolderPath);
                    if(path != null)
                        photoPaths.Add(path);
                    
                }
                model.PhotosPath = photoPaths;
            }

            if (model.Vedios != null)
            {
                List<string> videoPaths = new List<string>();
                foreach (var video in model.Vedios)
                {
                    var path =  MediaUtilites.ConverIformToPath(video, videoFolderPath);
                    if (path != null)
                        videoPaths.Add(path);
                }
                model.VediosPath = videoPaths;
            }

             return model;
        }
        public static AllPostsModel mappingModel(UploadPost post)
        {
            var model = new AllPostsModel()
            {
                Answer = post.Answer,
                Description = post.Description,
                Photos = post.Photos,
                Question = post.Question,
                TimeCreated = DateTime.UtcNow,
                Title = post.Title,
                Type = post.Type,
                Vedios = post.Vedios,
            };
            return model;
        }

        //public static string ConverIformToPath(IFormFile formFile , string FileFolderPath)
        //{
            
        //    if (formFile != null && formFile.Length > 0)
        //    {
        //        string fileExtension = Path.GetExtension(formFile.FileName);
        //        string newFileName = $"{Guid.NewGuid()}{fileExtension}";
        //        string newFilePath = Path.Combine(FolderPath, FileFolderPath, newFileName);

        //        using (var fileStream = new FileStream(newFilePath, FileMode.Create))
        //        {
        //            formFile.CopyTo(fileStream);
        //        }

        //        return (Path.Combine("StaticFiles", FileFolderPath, newFileName));
        //    }
        //    return null;
                
        //}


    }
}
