﻿using DomainModels.DTO;
using DomainModels.Models;


namespace Business.Posts.Services
{
    public interface IReactServices
    {
        Task<bool> DeletePostReactAsync(Guid reactId, string userEmail);
        Task<bool> DeleteQuestionPostReactAsync(Guid reactId, string userEmail);
        Task<bool> DeleteCommentPostReactAsync(Guid reactId, string userEmail);
        Task<bool> DeleteCommentQuestionReactAsync(Guid reactId, string userEmail);
        Task<(bool, Guid)> AddReactOnPostAsync(AddReactRequest reactRequest, string userEmail);
        Task<(bool, Guid)> AddReactOnQuestionPostAsync(AddReactRequest reactRequest, string userEmail);
        Task<(bool, Guid)> AddReactOnPostCommentAsync(AddReactRequest reactRequest, string userEmail);
        Task<(bool, Guid)> AddReactOnQuestionPostCommentAsync(AddReactRequest reactRequest, string userEmail);
        Task<bool> UpdatePostReact(ReactUpdateRequest reactRequest, string userEmail);
        Task<bool> UpdateQuestionReact(ReactUpdateRequest reactRequest, string userEmail);
        Task<bool> UpdatePostCommentReact(ReactUpdateRequest reactRequest, string userEmail);
        Task<bool> UpdateQuestionCommentReact(ReactUpdateRequest reactRequest, string userEmail);
        Task<List<DomainModels.DTO.ReactsDTO>> GetPostCommentReacts(Guid postCommentId);
        Task<List<DomainModels.DTO.ReactsDTO>> GetQuestionCommentReacts(Guid questionPostCommentId);
        Task<List<DomainModels.DTO.ReactsDTO>> GetPostReacts(Guid postId);
        Task<List<DomainModels.DTO.ReactsDTO>> GetQuestionReacts(Guid questionId);
        Task<ReactsDTO> GetReactByIdOnQuestionComment(Guid reactId);
        Task<ReactsDTO> GetReactByIdOnQuestionPost(Guid reactId);
        Task<ReactsDTO> GetReactByIdOnPostComment(Guid reactId);
        Task<ReactsDTO> GetReactByIdOnPost(Guid reactId);
        

    }
}
