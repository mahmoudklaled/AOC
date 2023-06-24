using Business.Accounts.Models;
using Business.Enums;
using Business.Posts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Posts.Services
{
    public interface IPostService
    {
        Task<List<Models.Post>> GetAllPostAsync();
        Task<List<Models.QuestionPost>> GetAllQuestionPostAsync();
        Task<List<Models.AllPostsModel>> GetAllPostTypesAsync();
        Task<bool> AddQuestionPostAsync(AllPostsModel postmodel, string userEmail);
        Task<bool> AddPostAsync(AllPostsModel postmodel, string userEmail);
        Task<bool> UpdatePostAsync(AllPostsModel postmodel, string userEmail);
        Task<bool> UpdateQuestionPostAsync(AllPostsModel postmodel, string userEmail);
        Task<bool> DeletePostAsync(Guid id, string userEmail);
        Task<bool> DeleteQuestionPostAsync(Guid id, string userEmail);
        
    }
}
