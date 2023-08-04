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
        Task<List<Post>> GetPostAsync(int pageNumber);
        Task<List<QuestionPost>> GetQuestionPostAsync(int pageNumber);
        Task<Post> GetPostByIDAsync(Guid id);
        Task<QuestionPost> GetQuestionPostByIdAsync(Guid id);
        Task<List<AllPostsModel>> GetPostTypesAsync(int pageNumber);
        Task<List<Post>> GetPersonalPostAsync(int pageNumber,Guid userID);
        Task<List<QuestionPost>> GetPersonalQuestionPostAsync(int pageNumber, Guid userID);
        Task<List<AllPostsModel>> GetPersonalPostTypesAsync(int pageNumber, Guid userID);
        Task<(bool, Guid)> AddQuestionPostAsync(UploadPost postmodel, string userEmail);
        Task<(bool, Guid)> AddPostAsync(UploadPost postmodel, string userEmail);
        Task<bool> UpdatePostAsync(UpdataPost postmodel, string userEmail);
        Task<bool> UpdateQuestionPostAsync(UpdataPost postmodel, string userEmail);
        Task<bool> DeletePostAsync(Guid id, string userEmail);
        Task<bool> DeleteQuestionPostAsync(Guid id, string userEmail);
        
    }
}
