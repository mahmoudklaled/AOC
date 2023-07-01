using DataBase.Core.Enums;
using DataBase.Core.Models.CommentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Posts.Services
{
    public interface ICommentServices
    {
        Task<bool> DeletePostCommentAsync(Guid commentId, string userEmail);
        Task<bool> AddPostCommentAsync(CommentRequest comment, string userEmail);
        Task<bool> UpdatePostCommentAsync(CommentRequest comment, string userEmail);
        Task<bool> DeleteQuestionCommentAsync(Guid commentId, string userEmail);
        Task<bool> AddQuestionCommentAsync(CommentRequest comment, string userEmail);
        Task<bool> UpdateQuestionCommentAsync(CommentRequest comment, string userEmail);
        Task<List<PostComment>> GetPostCommentsAsync(Guid postId ,int cntToSkip);
        Task<List<QuestionComment>> GetQuestionCommentsAsync(Guid postId,  int cntToSkip);
    }
}
