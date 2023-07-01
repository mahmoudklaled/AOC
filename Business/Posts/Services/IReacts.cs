using DataBase.Core.Models.Reacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Posts.Services
{
    public interface IReacts
    {
        Task<bool> DeletePostReactAsync(Guid reactId, string userEmail);
        Task<bool> DeleteQuestionPostReactAsync(Guid reactId, string userEmail);
        Task<bool> DeleteCommentPostReactAsync(Guid reactId, string userEmail);
        Task<bool> DeleteCommentQuestionReactAsync(Guid reactId, string userEmail);
        Task<bool> AddReactOnPostAsync(ReactRequest reactRequest, string userEmail);
        Task<bool> AddReactOnQuestionPostAsync(ReactRequest reactRequest, string userEmail);
        Task<bool> AddReactOnPostCommentAsync(ReactRequest reactRequest, string userEmail);
        Task<bool> AddReactOnQuestionPostCommentAsync(ReactRequest reactRequest, string userEmail);
        //Task<List<ReactResponse>> GetAllReactsOnPost(ReactRequest reactRequest);
        //Task<List<ReactResponse>> GetAllReactsOnQuestionPost(ReactRequest reactRequest);
        Task<bool> UpdatePostReact(ReactRequest reactRequest, string userEmail);
        Task<bool> UpdateQuestionReact(ReactRequest reactRequest, string userEmail);
        Task<bool> UpdatePostCommentReact(ReactRequest reactRequest, string userEmail);
        Task<bool> UpdateQuestionCommentReact(ReactRequest reactRequest, string userEmail);
    }
}
