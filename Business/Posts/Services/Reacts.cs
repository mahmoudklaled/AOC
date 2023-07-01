using DataBase.Core;
using DataBase.Core.Models.Reacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Posts.Services
{
    public class Reacts : IReacts
    {
        private readonly IUnitOfWork _unitOfWork;
        public Reacts(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddReactOnPostAsync(ReactRequest reactRequest, string userEmail)
        {
            var user = await _unitOfWork.ProfileAccount.FindAsync(p => p.Email == userEmail);
            var post = await _unitOfWork.Post.FindAsync(p => p.Id == reactRequest.ObjectId);
            if(user == null || post==null) return false;
            var react = new PostReact
            {
                Id = new Guid(),
                PostId = post.Id,
                reacts = reactRequest.ReactType
            };
            await _unitOfWork.PostReact.AddAsync(react);
            return _unitOfWork.Complete() > 0;
        }

        public async Task<bool> AddReactOnPostCommentAsync(ReactRequest reactRequest, string userEmail)
        {
            var user = await _unitOfWork.ProfileAccount.FindAsync(p => p.Email == userEmail);
            var Comment = await _unitOfWork.PostComment.FindAsync(p => p.Id == reactRequest.ObjectId);
            if (user == null || Comment == null) return false;
            var react = new PostCommentReact
            {
                Id = new Guid(),
                PostCommentId = Comment.Id,
                reacts = reactRequest.ReactType
            };
            await _unitOfWork.PostCommentReact.AddAsync(react);
            return _unitOfWork.Complete() > 0;
        }

        public async Task<bool> AddReactOnQuestionPostAsync(ReactRequest reactRequest, string userEmail)
        {
            var user = await _unitOfWork.ProfileAccount.FindAsync(p => p.Email == userEmail);
            var post = await _unitOfWork.QuestionPost.FindAsync(p => p.Id == reactRequest.ObjectId);
            if (user == null || post == null) return false;
            var react = new QuestionReact
            {
                Id = new Guid(),
                QuestionPostId = post.Id,
                reacts = reactRequest.ReactType
            };
            await _unitOfWork.QuestionReact.AddAsync(react);
            return _unitOfWork.Complete() > 0;
        }

        public async Task<bool> AddReactOnQuestionPostCommentAsync(ReactRequest reactRequest, string userEmail)
        {
            var user = await _unitOfWork.ProfileAccount.FindAsync(p => p.Email == userEmail);
            var Comment = await _unitOfWork.QuestionComment.FindAsync(p => p.Id == reactRequest.ObjectId);
            if (user == null || Comment == null) return false;
            var react = new QuestionCommentReact
            {
                Id = new Guid(),
                QuestionCommentId = Comment.Id,
                reacts = reactRequest.ReactType
            };
            await _unitOfWork.QuestionCommentReact.AddAsync(react);
            return _unitOfWork.Complete() > 0;
        }

        public async Task<bool> DeleteCommentPostReactAsync(Guid reactId, string userEmail)
        {
            var user = await _unitOfWork.ProfileAccount.FindAsync(p => p.Email == userEmail);
            var react = await _unitOfWork.PostCommentReact.FindAsync(r => r.Id == reactId);
            if ((react == null || user == null) || react.ProfileAccountId != user.Id)
                return false;
            _unitOfWork.PostCommentReact.Delete(react);
            return _unitOfWork.Complete() > 0;
        }

        public async Task<bool> DeleteCommentQuestionReactAsync(Guid reactId, string userEmail)
        {
            var user = await _unitOfWork.ProfileAccount.FindAsync(p => p.Email == userEmail);
            var react = await _unitOfWork.QuestionCommentReact.FindAsync(r => r.Id == reactId);
            if ((react == null || user == null) || react.ProfileAccountId != user.Id)
                return false;
            _unitOfWork.QuestionCommentReact.Delete(react);
            return _unitOfWork.Complete() > 0;
        }

        public async Task<bool> DeletePostReactAsync(Guid reactId, string userEmail)
        {
            var user = await _unitOfWork.ProfileAccount.FindAsync(p => p.Email == userEmail);
            var react = await _unitOfWork.PostReact.FindAsync(r => r.Id == reactId);
            if ((react == null || user == null) || react.ProfileAccountId != user.Id)
                return false;
            _unitOfWork.PostReact.Delete(react);
            return _unitOfWork.Complete() > 0;
        }

        public async Task<bool> DeleteQuestionPostReactAsync(Guid reactId, string userEmail)
        {
            var user = await _unitOfWork.ProfileAccount.FindAsync(p => p.Email == userEmail);
            var react = await _unitOfWork.QuestionReact.FindAsync(r => r.Id == reactId);
            if ((react == null || user == null) || react.ProfileAccountId != user.Id)
                return false;
            _unitOfWork.QuestionReact.Delete(react);
            return _unitOfWork.Complete() > 0;
        }

        public async Task<bool> UpdatePostCommentReact(ReactRequest reactRequest, string userEmail)
        {
            var user = await _unitOfWork.ProfileAccount.FindAsync(p => p.Email == userEmail);
            var react = await _unitOfWork.PostCommentReact.FindAsync(r => r.Id == reactRequest.ReactId);
            if ((react == null || user == null) || react.ProfileAccountId != user.Id)
                return false;
            react.reacts = reactRequest.ReactType;
            _unitOfWork.PostCommentReact.Update(react);
            return _unitOfWork.Complete() > 0;
        }

        public async Task<bool> UpdatePostReact(ReactRequest reactRequest, string userEmail)
        {
            var user = await _unitOfWork.ProfileAccount.FindAsync(p => p.Email == userEmail);
            var react = await _unitOfWork.PostReact.FindAsync(r => r.Id == reactRequest.ReactId);
            if ((react == null || user == null) || react.ProfileAccountId != user.Id)
                return false;
            react.reacts = reactRequest.ReactType;
            _unitOfWork.PostReact.Update(react);
            return _unitOfWork.Complete() > 0;
        }

        public async Task<bool> UpdateQuestionCommentReact(ReactRequest reactRequest, string userEmail)
        {
            var user = await _unitOfWork.ProfileAccount.FindAsync(p => p.Email == userEmail);
            var react = await _unitOfWork.QuestionCommentReact.FindAsync(r => r.Id == reactRequest.ReactId);
            if ((react == null || user == null) || react.ProfileAccountId != user.Id)
                return false;
            react.reacts = reactRequest.ReactType;
            _unitOfWork.QuestionCommentReact.Update(react);
            return _unitOfWork.Complete() > 0;
        }

        public async Task<bool> UpdateQuestionReact(ReactRequest reactRequest, string userEmail)
        {
            var user = await _unitOfWork.ProfileAccount.FindAsync(p => p.Email == userEmail);
            var react = await _unitOfWork.QuestionReact.FindAsync(r => r.Id == reactRequest.ReactId);
            if ((react == null || user == null) || react.ProfileAccountId != user.Id)
                return false;
            react.reacts = reactRequest.ReactType;
            _unitOfWork.QuestionReact.Update(react);
            return _unitOfWork.Complete() > 0;
        }
    }
}
