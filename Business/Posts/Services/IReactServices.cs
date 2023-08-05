using DomainModels.Models;


namespace Business.Posts.Services
{
    public interface IReactServices
    {
        Task<bool> DeletePostReactAsync(Guid reactId, string userEmail);
        Task<bool> DeleteQuestionPostReactAsync(Guid reactId, string userEmail);
        Task<bool> DeleteCommentPostReactAsync(Guid reactId, string userEmail);
        Task<bool> DeleteCommentQuestionReactAsync(Guid reactId, string userEmail);
        Task<bool> AddReactOnPostAsync(AddReactRequest reactRequest, string userEmail);
        Task<bool> AddReactOnQuestionPostAsync(AddReactRequest reactRequest, string userEmail);
        Task<bool> AddReactOnPostCommentAsync(AddReactRequest reactRequest, string userEmail);
        Task<bool> AddReactOnQuestionPostCommentAsync(AddReactRequest reactRequest, string userEmail);
        Task<bool> UpdatePostReact(ReactUpdateRequest reactRequest, string userEmail);
        Task<bool> UpdateQuestionReact(ReactUpdateRequest reactRequest, string userEmail);
        Task<bool> UpdatePostCommentReact(ReactUpdateRequest reactRequest, string userEmail);
        Task<bool> UpdateQuestionCommentReact(ReactUpdateRequest reactRequest, string userEmail);
    }
}
