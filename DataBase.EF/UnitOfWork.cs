using BDataBase.Core.Models.Accounts;
using DataBase.Core;
using DataBase.Core.Interfaces;
using DataBase.Core.Models.CommentModels;
using DataBase.Core.Models.PhotoModels;
using DataBase.Core.Models.Posts;
using DataBase.Core.Models.Reacts;
using DataBase.Core.Models.VedioModels;
using DataBase.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IBaseRepository<ProfileAccounts> ProfileAccount { get; private set; }
        public IBaseRepository<QuestionPost> QuestionPost { get; private set; }
        public IBaseRepository<Post> Post { get; private set; }
        public IBaseRepository<PostPhoto> PostPhoto { get; private set; }
        public IBaseRepository<PostVedio> PostVedio { get; private set; }
        public IBaseRepository<QuestionPhoto> QuestionPhoto { get; private set; }
        public IBaseRepository<QuestionVedio> QuestionVedio { get; private set; }
        public IBaseRepository<PostComment> PostComment { get; private set; }
        public IBaseRepository<QuestionComment> QuestionComment { get; private set; }
        public IBaseRepository<PostReact> PostReact { get; private set; }
        public IBaseRepository<QuestionReact> QuestionReact { get; private set; }
        public IBaseRepository<PostCommentReact> PostCommentReact { get; private set; }
        public IBaseRepository<QuestionCommentReact> QuestionCommentReact { get; private set; }
        public IBaseRepository<QuestionCommentVedio> QuestionCommentVedio { get; private set; }
        public IBaseRepository<QuestionCommentPhoto> QuestionCommentPhoto { get; private set; }
        public IBaseRepository<PostCommentVedio> PostCommentVedio { get; private set; }
        public IBaseRepository<PostCommentPhoto> PostCommentPhoto { get; private set; }


        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            ProfileAccount= new BaseRepository<ProfileAccounts>(_context);
            QuestionPost= new BaseRepository<QuestionPost>(_context);
            Post = new BaseRepository<Post>(_context);
            PostComment= new BaseRepository<PostComment>(_context);
            PostReact = new BaseRepository<PostReact>(_context);
            QuestionComment = new BaseRepository<QuestionComment>(_context);
            QuestionReact = new BaseRepository<QuestionReact>(_context);
            PostCommentReact = new BaseRepository<PostCommentReact>(_context);
            QuestionCommentReact = new BaseRepository<QuestionCommentReact>(_context);

        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
