using Business.Enums;
using Business.Posts.Helper;
using Business.Posts.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Posts.Services
{
    public class CommentServices:ICommentServices
    {
        private readonly ApplicationDbContext _dbContext;
        public CommentServices(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> DeleteCommentAsync(Guid commentId, string userEmail)
        {
            var user = await _dbContext.ProfileAccounts.FindAsync(userEmail);
            var comment = await _dbContext.Comments.FindAsync(commentId);
            if (comment == null || user == null)
                return false;
            if (comment.ProfileAccountId != user.Id) return false;

            _dbContext.Comments.Remove(comment);
            await _dbContext.SaveChangesAsync();

            return true;

        }
        public async Task<bool> AddCommentAsync(CommentRequest comment, string userEmail)
        {
            switch (comment.PostsType)
            {
                case PostsTypes.Post:
                    return await AddPostComment(comment, userEmail);
                case PostsTypes.Question:
                    return await AddQuestionComment(comment, userEmail);
                default: break;
            }
            return false;
        }
        public async Task<List<Comment>> GetCommentsAsync(Guid postId, PostsTypes postsType, int cntToSkip)
        {
            switch (postsType)
            {
                case PostsTypes.Post:
                    return await GetPostComment(postId, cntToSkip);
                case PostsTypes.Question:
                    return await GetQuestionComment(postId, cntToSkip);
                default: break;
            }
            return new List<Comment>();

        }
        private async Task<bool> AddPostComment(CommentRequest comment, string userEmail)
        {
            var user = await _dbContext.ProfileAccounts.FindAsync(userEmail);
            if (user == null) return false;
            var post = await _dbContext.Posts.FindAsync(comment.PostId);
            if (post == null) return false;
            var Newcomment = new Comment()
            {
                Id = new Guid(),
                ProfileAccount = user,
                ProfileAccountId = user.Id,
                PostId = comment.PostId,
                Post = post,
                comment = comment.comment
            };
            if (comment.Photo != null)
            {
                Newcomment.Photo = new Photo()
                {
                    Id = new Guid(),
                    PhotoPath = PostHelper.ConverIformToPath(comment.Photo, "CommentsPhoto"),
                    CommentId = Newcomment.Id
                };
            }
            if (comment.Vedio != null)
            {
                Newcomment.Vedio = new Vedio()
                {
                    Id = new Guid(),
                    VedioPath = PostHelper.ConverIformToPath(comment.Photo, "CommentsVedio"),
                    CommentId = Newcomment.Id
                };
            }
            await _dbContext.Comments.AddAsync(Newcomment);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;

        }
        private async Task<bool> AddQuestionComment(CommentRequest comment, string userEmail)
        {
            var user = await _dbContext.ProfileAccounts.FindAsync(userEmail);
            if (user == null) return false;
            var post = await _dbContext.QuestionPosts.FindAsync(comment.PostId);
            if (post == null) return false;
            var Newcomment = new Comment()
            {
                Id = new Guid(),
                ProfileAccount = user,
                ProfileAccountId = user.Id,
                QuestionPostId = comment.PostId,
                QuestionPost = post,
                comment = comment.comment
            };
            if (comment.Photo != null)
            {
                Newcomment.Photo = new Photo()
                {
                    Id = new Guid(),
                    PhotoPath = PostHelper.ConverIformToPath(comment.Photo, "CommentsPhoto"),
                    CommentId = Newcomment.Id
                };
            }
            if (comment.Vedio != null)
            {
                Newcomment.Vedio = new Vedio()
                {
                    Id = new Guid(),
                    VedioPath = PostHelper.ConverIformToPath(comment.Photo, "CommentsVedio"),
                    CommentId = Newcomment.Id
                };
            }
            await _dbContext.Comments.AddAsync(Newcomment);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;

        }
        private async Task<List<Comment>> GetQuestionComment(Guid QuestionPostId, int cntToSkip)
        {
            if (cntToSkip < 0)
                cntToSkip = 0;
            var result = await _dbContext.Comments
                                    .Where(p => p.QuestionPostId == QuestionPostId)
                                    .Skip(cntToSkip * 5)
                                    .Take(5)
                                    .ToListAsync();
            return result;

        }
        private async Task<List<Comment>> GetPostComment(Guid PostId, int cntToSkip)
        {
            if (cntToSkip < 0)
                cntToSkip = 0;
            var result = await _dbContext.Comments
                                    .Where(p => p.PostId == PostId)
                                    .Skip(cntToSkip * 5)
                                    .Take(5)
                                    .ToListAsync();
            return result;
        }
    }
    
}
