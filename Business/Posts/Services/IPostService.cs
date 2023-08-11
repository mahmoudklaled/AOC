using DomainModels.Models;

namespace Business.Posts.Services
{
    public interface IPostService
    {
        Task<List<DomainModels.DTO.PostDTO>> GetPostAsync(DateTime? Time);
        Task<List<DomainModels.DTO.QuestionPostDTO>> GetQuestionPostAsync(DateTime? Time);
        Task<List<DomainModels.DTO.AllPostDTO>> GetPostTypesAsync(DateTime? Time);
        Task<DomainModels.DTO.PostDTO> GetPostByIDAsync(Guid id);
        Task<DomainModels.DTO.QuestionPostDTO> GetQuestionPostByIdAsync(Guid id);
        Task<List<DomainModels.DTO.PostDTO>> GetPersonalPostAsync(int pageNumber,Guid userID);
        Task<List<DomainModels.DTO.QuestionPostDTO>> GetPersonalQuestionPostAsync(int pageNumber, Guid userID);
        Task<List<DomainModels.DTO.AllPostDTO>> GetPersonalPostTypesAsync(int pageNumber, Guid userID);

        Task<(bool, Guid)> AddQuestionPostAsync(UploadPost postmodel, string userEmail);
        Task<(bool, Guid)> AddPostAsync(UploadPost postmodel, string userEmail);
        Task<bool> UpdatePostAsync(UpdataPost postmodel, string userEmail);
        Task<bool> UpdateQuestionPostAsync(UpdataPost postmodel, string userEmail);
        Task<bool> DeletePostAsync(Guid id, string userEmail);
        Task<bool> DeleteQuestionPostAsync(Guid id, string userEmail);
        
    }
}
