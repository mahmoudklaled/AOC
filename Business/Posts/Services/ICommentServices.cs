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
        Task<(bool, Guid)> AddPostCommentAsync(AddCommentRequest comment, string userEmail);
        Task<bool> UpdatePostCommentAsync(CommentUpdateRequest comment, string userEmail);
        Task<bool> DeleteQuestionCommentAsync(Guid commentId, string userEmail);
        Task<(bool, Guid)> AddQuestionCommentAsync(AddCommentRequest comment, string userEmail);
        Task<bool> UpdateQuestionCommentAsync(CommentUpdateRequest comment, string userEmail);
        Task<List<PostComment>> GetPostCommentsAsync(Guid postId ,int cntToSkip);
        Task<List<QuestionComment>> GetQuestionCommentsAsync(Guid postId,  int cntToSkip);
        Task<PostComment> GetPostCommentByIdAsync(Guid commentId);
        Task<QuestionComment> GetQuestionCommentByIdAsync(Guid commentId);
    }
}
