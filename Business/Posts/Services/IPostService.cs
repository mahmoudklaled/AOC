using DataBase.Core.Models.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Posts.Services
{
    public interface IPostService
    {
        Task<List<Post>> GetPostAsync(int? take, int? skip);
        Task<List<QuestionPost>> GetQuestionPostAsync(int? take, int? skip);
        Task<List<AllPostsModel>> GetPostTypesAsync(int? take, int? skip);
        Task<bool> AddQuestionPostAsync(UploadPost postmodel, string userEmail);
        Task<bool> AddPostAsync(UploadPost postmodel, string userEmail);
        Task<bool> UpdatePostAsync(UpdataPost postmodel, string userEmail);
        Task<bool> UpdateQuestionPostAsync(UpdataPost postmodel, string userEmail);
        Task<bool> DeletePostAsync(Guid id, string userEmail);
        Task<bool> DeleteQuestionPostAsync(Guid id, string userEmail);
        
    }
}
