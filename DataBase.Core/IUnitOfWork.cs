using BDataBase.Core.Models.Accounts;
using DataBase.Core.Interfaces;
using DataBase.Core.Models.CommentModels;
using DataBase.Core.Models.PhotoModels;
using DataBase.Core.Models.Posts;
using DataBase.Core.Models.Reacts;
using DataBase.Core.Models.VedioModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<ProfileAccounts> ProfileAccount { get; }
        IBaseRepository<QuestionPost> QuestionPost { get; }
        IBaseRepository<Post> Post { get; }
        IBaseRepository<PostPhoto> PostPhoto { get; }
        IBaseRepository<PostVedio> PostVedio { get; }
        IBaseRepository<QuestionPhoto> QuestionPhoto { get; }
        IBaseRepository<QuestionVedio> QuestionVedio { get; }
        IBaseRepository<PostComment> PostComment { get; }
        IBaseRepository<QuestionComment> QuestionComment { get; }
        IBaseRepository<PostReact> PostReact { get; }
        IBaseRepository<QuestionReact> QuestionReact { get; }
        IBaseRepository<PostCommentReact> PostCommentReact { get; }
        IBaseRepository<QuestionCommentReact> QuestionCommentReact { get; }
        IBaseRepository<QuestionCommentVedio> QuestionCommentVedio { get; }
        IBaseRepository<QuestionCommentPhoto> QuestionCommentPhoto { get; }
        IBaseRepository<PostCommentVedio> PostCommentVedio { get; }
        IBaseRepository<PostCommentPhoto> PostCommentPhoto { get; }
        int Complete();
    }
}
