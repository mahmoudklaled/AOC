using Business.Enums;
using Business.Posts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Posts.Services
{
    public interface ICommentServices
    {
        Task<bool> DeleteCommentAsync(Guid commentId, string userEmail);
        Task<bool> AddCommentAsync(CommentRequest comment, string userEmail);
        Task<List<Comment>> GetCommentsAsync(Guid postId, PostsTypes postsType, int cntToSkip);
    }
}
