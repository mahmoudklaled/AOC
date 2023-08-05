using DataBase.Core.Enums;
using DataBase.Core.Models.PhotoModels;
using DataBase.Core.Models.Reacts;
using DataBase.Core.Models.VedioModels;
using Microsoft.AspNetCore.Http;

namespace DomainModels.DTO
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
    public record QuestionPostDTO
    {
        public Guid Id { get; set; }
        public string Question { get; set; }
        public string? Answer { get; set; }
        public string TimeCreated { get; set; }
        public ICollection<BasePhoto> Photos { get; set; }
        public ICollection<BaseVedio> Vedios { get; set; }
        public ICollection<CommentDTO> Comments { get; set; }
        public ICollection<BaseReact> Reacts { get; set; }
        public int commentsCount { get; set; }
        public Guid UserAccountsId { get; set; }
        public string PostUserFirstName { get; set; }
        public string PostUserLastName { get; set; }
    }
    public record PostDTO
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string TimeCreated { get; set; }
        public ICollection<BasePhoto> Photos { get; set; }
        public ICollection<BaseVedio> Vedios { get; set; }
        public ICollection<CommentDTO> Comments { get; set; }
        public ICollection<BaseReact> Reacts { get; set; }
        public int commentsCount { get; set; }
        public Guid UserAccountsId { get; set; }
        public string PostUserFirstName { get; set; }
        public string PostUserLastName { get; set; }

    }
    public record AllPostDTO
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public string TimeCreated { get; set; }
        public string? Question { get; set; }
        public string? Answer { get; set; }
        public PostsTypes Type { get; set; }
        public ICollection<BasePhoto> Photos { get; set; }
        public ICollection<BaseVedio> Vedios { get; set; }
        public ICollection<CommentDTO> Comments { get; set; }
        public ICollection<BaseReact> Reacts { get; set; }
        public int commentsCount { get; set; }
        public Guid UserAccountsId { get; set; }
        public string PostUserFirstName { get; set; }
        public string PostUserLastName { get; set; }
    }
    
    #endregion


    #region comments
    public record CommentUpdateRequest
    {
        public Guid Id { get; set; }
        public string? comment { get; set; }
        public IFormFile? Photo { get; set; }
        public IFormFile? Vedio { get; set; }

    }
    public record AddCommentRequest
    {
        public string? comment { get; set; }
        public Guid PostId { get; set; }
        public IFormFile? Photo { get; set; }
        public IFormFile? Vedio { get; set; }

    }
    public record CommentDTO
    {
        public Guid Id { get; set; }
        public string? comment { get; set; }
        public string Date { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public Guid UserId { get; set; }
        public BasePhoto? CommentPhoto { get; set; }
        public BaseVedio? CommentVedio { get; set; }
        public List<BaseReact> CommentReacts { get; set; }

    }
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

    #region UserProfile
    public record UserAccounts
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Bio { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public DateTime? Birthdate { get; set; }
        public string? gender { get; set; }
        public string Type { get; set; }
    }
    public record ProfileUpdateModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Bio { get; set; }
        public DateTime? Birthdate { get; set; }
        public string? gender { get; set; } = "Unknown";
    }
    #endregion
}
