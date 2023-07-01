using Business.Posts.Helper;
using DataBase.Core;
using DataBase.Core.Models.CommentModels;
using DataBase.Core.Models.PhotoModels;
using DataBase.Core.Models.VedioModels;

namespace Business.Posts.Services
{
    public class CommentServices:ICommentServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public CommentServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork= unitOfWork;
        }
        public async Task<bool> DeletePostCommentAsync(Guid commentId, string userEmail)
        {
            string[] includes = { "PostCommentPhoto", "PostCommentVedio", "PostCommentReacts" };
            var user = await _unitOfWork.ProfileAccount.FindAsync(p=>p.Email==userEmail);
            var comment = await _unitOfWork.PostComment.FindAsync(p=>p.Id ==commentId, includes);
            if (comment == null || user == null)
                return false;
            if (comment.ProfileAccountId != user.Id) return false;
            if (comment.PostCommentPhoto != null)
                _unitOfWork.PostCommentPhoto.Delete(comment.PostCommentPhoto);
            if (comment.PostCommentVedio != null)
                _unitOfWork.PostCommentVedio.Delete(comment.PostCommentVedio);
            if (comment.PostCommentReacts != null)
                _unitOfWork.PostCommentReact.DeleteRange(comment.PostCommentReacts);
            _unitOfWork.PostComment.Delete(comment);
            return _unitOfWork.Complete()>0;
        }

        public async Task<bool> AddPostCommentAsync(CommentRequest comment, string userEmail)
        {
            var user = await _unitOfWork.ProfileAccount.FindAsync(p=>p.Email== userEmail);
            if (user == null) return false;
            var post = await _unitOfWork.Post.FindAsync(p=>p.Id==comment.PostId);
            if (post == null) return false;
            var Newcomment = new PostComment()
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
                Newcomment.PostCommentPhoto = new PostCommentPhoto()
                {
                    Id = new Guid(),
                    PhotoPath = PostHelper.ConverIformToPath(comment.Photo, "CommentsPhoto"),
                    PostCommentId = Newcomment.Id
                };
            }
            if (comment.Vedio != null)
            {
                Newcomment.PostCommentVedio = new PostCommentVedio()
                {
                    Id = new Guid(),
                    VedioPath = PostHelper.ConverIformToPath(comment.Vedio, "CommentsVedio"),
                    PostCommentId = Newcomment.Id
                };
            }
            await _unitOfWork.PostComment.AddAsync(Newcomment);
            return _unitOfWork.Complete() > 0;
        }

        public async Task<bool> UpdatePostCommentAsync(CommentRequest comment, string userEmail)
        {
            string[] includes = { "PostCommentPhoto", "PostCommentVedio"};
            var user = await _unitOfWork.ProfileAccount.FindAsync(p => p.Email == userEmail);
            var cmnt = await _unitOfWork.PostComment.FindAsync(p => p.Id == comment.Id, includes);
            if (cmnt == null || user == null)
                return false;
            if (cmnt.ProfileAccountId != user.Id) return false;
            cmnt.comment = comment.comment;
            if (comment.Photo != null)
            {
                if (cmnt.PostCommentPhoto != null)
                {
                    _unitOfWork.PostCommentPhoto.Delete(cmnt.PostCommentPhoto);
                }
                cmnt.PostCommentPhoto = new PostCommentPhoto()
                {
                    Id = new Guid(),
                    PhotoPath = PostHelper.ConverIformToPath(comment.Photo, "CommentsPhoto"),
                    PostCommentId = cmnt.Id
                };
            }
            if (comment.Vedio != null)
            {
                if (cmnt.PostCommentVedio != null)
                {
                    _unitOfWork.PostCommentVedio.Delete(cmnt.PostCommentVedio);
                }
                cmnt.PostCommentVedio = new PostCommentVedio()
                {
                    Id = new Guid(),
                    VedioPath = PostHelper.ConverIformToPath(comment.Vedio, "CommentsVedio"),
                    PostCommentId = cmnt.Id
                };
            }
            _unitOfWork.PostComment.Update(cmnt);
            return _unitOfWork.Complete() > 0;
        }

        public async Task<bool> DeleteQuestionCommentAsync(Guid commentId, string userEmail)
        {
            string[] includes = { "QuestionCommentPhoto", "QuestionCommentVedio", "QuestionCommentReacts" };
            var user = await _unitOfWork.ProfileAccount.FindAsync(p => p.Email == userEmail);
            var comment = await _unitOfWork.QuestionComment.FindAsync(p => p.Id == commentId , includes);
            if (comment == null || user == null)
                return false;
            if (comment.ProfileAccountId != user.Id) return false;
            if (comment.QuestionCommentPhoto != null)
               _unitOfWork.QuestionCommentPhoto.Delete(comment.QuestionCommentPhoto);
            if (comment.QuestionCommentVedio != null)
                _unitOfWork.QuestionCommentVedio.Delete(comment.QuestionCommentVedio);
            if (comment.QuestionCommentReacts != null)
                _unitOfWork.QuestionCommentReact.DeleteRange(comment.QuestionCommentReacts);
            _unitOfWork.QuestionComment.Delete(comment);
            return _unitOfWork.Complete() > 0;
        }

        public async Task<bool> AddQuestionCommentAsync(CommentRequest comment, string userEmail)
        {
            var user = await _unitOfWork.ProfileAccount.FindAsync(p => p.Email == userEmail);
            if (user == null) return false;
            var post = await _unitOfWork.QuestionPost.FindAsync(p => p.Id == comment.PostId);
            if (post == null) return false;
            var Newcomment = new QuestionComment()
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
                Newcomment.QuestionCommentPhoto = new QuestionCommentPhoto()
                {
                    Id = new Guid(),
                    PhotoPath = PostHelper.ConverIformToPath(comment.Photo, "CommentsPhoto"),
                    QuestionCommentId = Newcomment.Id
                };
            }
            if (comment.Vedio != null)
            {
                Newcomment.QuestionCommentVedio = new QuestionCommentVedio()
                {
                    Id = new Guid(),
                    VedioPath = PostHelper.ConverIformToPath(comment.Vedio, "CommentsVedio"),
                    QuestionCommentId = Newcomment.Id
                };
            }
            await _unitOfWork.QuestionComment.AddAsync(Newcomment);
            return _unitOfWork.Complete() > 0;
        }

        public async Task<bool> UpdateQuestionCommentAsync(CommentRequest comment, string userEmail)
        {
            string[] includes = { "QuestionCommentPhoto", "QuestionCommentVedio" };
            var user = await _unitOfWork.ProfileAccount.FindAsync(p => p.Email == userEmail);
            var cmnt = await _unitOfWork.QuestionComment.FindAsync(p => p.Id == comment.Id, includes);
            if (cmnt == null || user == null)
                return false;
            if (cmnt.ProfileAccountId != user.Id) return false;
            cmnt.comment = comment.comment;
            if (comment.Photo != null)
            {
                if(cmnt.QuestionCommentPhoto != null) {
                    _unitOfWork.QuestionCommentPhoto.Delete(cmnt.QuestionCommentPhoto);
                }
                cmnt.QuestionCommentPhoto = new QuestionCommentPhoto()
                {
                    Id = new Guid(),
                    PhotoPath = PostHelper.ConverIformToPath(comment.Photo, "CommentsPhoto"),
                    QuestionCommentId = cmnt.Id
                };
            }
            if (comment.Vedio != null)
            {
                if (cmnt.QuestionCommentVedio != null)
                {
                    _unitOfWork.QuestionCommentVedio.Delete(cmnt.QuestionCommentVedio);
                }
                cmnt.QuestionCommentVedio = new QuestionCommentVedio()
                {
                    Id = new Guid(),
                    VedioPath = PostHelper.ConverIformToPath(comment.Vedio, "CommentsVedio"),
                    QuestionCommentId = cmnt.Id
                };
            }
            _unitOfWork.QuestionComment.Update(cmnt);
            return _unitOfWork.Complete() > 0;
        }

        public async Task<List<PostComment>> GetPostCommentsAsync(Guid postId, int cntToSkip)
        {
            string[] includes = { "PostCommentPhoto", "PostCommentVedio", "PostCommentReacts" };
            if (cntToSkip < 0)
                cntToSkip = 0;
            var result = await _unitOfWork.PostComment.FindAllAsync(p => p.PostId == postId, cntToSkip * 5, 5, includes);                   
            return result.ToList();
        }

        public async Task<List<QuestionComment>> GetQuestionCommentsAsync(Guid postId, int cntToSkip)
        {
            string[] includes = { "QuestionCommentPhoto", "QuestionCommentVedio", "QuestionCommentReacts" };
            if (cntToSkip < 0)
                cntToSkip = 0;
            var result = await _unitOfWork.QuestionComment.FindAllAsync(p => p.QuestionPostId == postId, cntToSkip * 5, 5, includes);
            return result.ToList();
        }
    }
    
}
