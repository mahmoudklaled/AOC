using DataBase.Core.Enums;
using DataBase.Core.Models.CommentModels;
using DataBase.Core.Models.PhotoModels;
using DataBase.Core.Models.Reacts;
using DataBase.Core.Models.VedioModels;

namespace GAMAX.Services.Dto
{
    #region Posts
    public record UpdateQuestion
    {
        public Guid Id { get; set; } 
        public List<Guid>? DeletedPhotoIds { get; set; }
        public List<Guid>? DeletedVedioIds { get; set; }
        public List<IFormFile>? Photos { get; set; }
        public List<IFormFile>? Vedios { get; set; }
        public PostsTypes Type { get; set; } = PostsTypes.Question;
        public string Question { get; set; }
        public string? Answer { get; set; }
    }
    public record UpdatePost
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public List<Guid>? DeletedPhotoIds { get; set; }
        public List<Guid>? DeletedVedioIds { get; set; }
        public List<IFormFile>? Photos { get; set; }
        public List<IFormFile>? Vedios { get; set; }
        public PostsTypes Type { get; set; } = PostsTypes.Post;
    }
    public record UploadPost
    {
        public string Description { get; set; }
        public List<IFormFile>? Photos { get; set; }
        public List<IFormFile>? Vedios { get; set; }
        public PostsTypes Type { get; set; } = PostsTypes.Post;
    }
    public record UploadQuestionPost
    {
        public List<IFormFile>? Photos { get; set; }
        public List<IFormFile>? Vedios { get; set; }
        public PostsTypes Type { get; set; } = PostsTypes.Question;
        public string Question { get; set; }
        public string? Answer { get; set; }
    }
    public record QuestionPost
    {
        public Guid Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string TimeCreated { get; set; } 
        public ICollection<BasePhoto> Photos { get; set; }
        public ICollection<BaseVedio> Vedios { get; set; }
        public ICollection<BaseComment> Comments { get; set; }
        public ICollection<BaseReact> Reacts { get; set; }
        public Guid UserAccountsId { get; set; }
        public string PostUserFirstName { get; set; }
        public string PostUserLastName { get; set; }
    }
    public record Post
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string TimeCreated { get; set; }
        public ICollection<BasePhoto> Photos { get; set; }
        public ICollection<BaseVedio> Vedios { get; set; }
        public ICollection<BaseComment> Comments { get; set; }
        public ICollection<BaseReact> Reacts { get; set; }
        public Guid UserAccountsId { get; set; }
        public string PostUserFirstName { get; set; }
        public string PostUserLastName { get; set; }
    }
    public record AllPost
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string TimeCreated { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public PostsTypes Type { get; set; }
        public ICollection<BasePhoto> Photos { get; set; }
        public ICollection<BaseVedio> Vedios { get; set; }
        public ICollection<BaseComment> Comments { get; set; }
        public ICollection<BaseReact> Reacts { get; set; }
        public Guid UserAccountsId { get; set; }
        public string PostUserFirstName { get; set; }
        public string PostUserLastName { get; set; }
    }
    public record Photo { 
        public Guid Id { get; set; } public string Path { get; set; }
    }
    public record Vedio
    {
        public Guid Id { get; set; }
        public string Path { get; set; }
    }
    public record React
    {
        public Guid Id { get; set; }
        public ReactsType react { get; set; }
        public Guid UserAccountsId { get; set; }
    }
    public record Comment
    {
        public Guid Id { get; set; }
        public string comment { get; set; }
        public DateTime Date { get; set; }
        public Photo Photo { get; set; }
        public Vedio Vedio { get; set; }
        public List<React> PostCommentReacts { get; set; }
    }
    #endregion


    #region comments

    public record CommentUpdate
    {
        public Guid Id { get; set; }
        public string comment { get; set; }
        public Guid PostId { get; set; }
        public PostsTypes PostsType { get; set; }
        public IFormFile? Photo { get; set; }
        public IFormFile? Vedio { get; set; }
    }

    #endregion


    #region React
        #endregion

}
