using Business.Posts.Services;
using DataBase.Core.Enums;
using DataBase.Core.Models.CommentModels;
using DataBase.Core.Models.PhotoModels;
using DataBase.Core.Models.Posts;
using DataBase.Core.Models.Reacts;
using DataBase.Core.Models.VedioModels;
using GAMAX.Services.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GAMAX.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPostService _postService;
        public PostsController(IHttpContextAccessor httpContextAccessor, IPostService postService )
        {
            _httpContextAccessor = httpContextAccessor;
            _postService = postService;
        }
        [HttpPost("GetAllPosts")]  
        public async Task<IActionResult> GetAllPosts(int pageNumber)
        {
            var result = await _postService.GetPostAsync(pageNumber);
            List<Dto.Post> posts = new List<Dto.Post>();
            foreach (var post in result)
            {
                posts.Add(new Dto.Post
                {
                    Id = post.Id,
                    Description = post.Description,
                    TimeCreated = (DateTime.UtcNow) - (post.TimeCreated),
                    UserAccountsId = post.UserAccountsId,
                    PostUserLastName = post.UserAccounts.LastName,
                    PostUserFirstName = post.UserAccounts.FirstName,
                    Photos =post.Photos.Select(pp => new BasePhoto { Id = pp.Id, PhotoPath = pp.PhotoPath }).ToList().ToList(),
                    Vedios=post.Vedios.Select(pp => new BaseVedio { Id = pp.Id, VedioPath = pp.VedioPath }).ToList(),
                    Comments=post.Comments.Select(pp => new BaseComment { Id = pp.Id, comment = pp.comment, Date = pp.Date, UserAccountsId = pp.UserAccountsId }).ToList(),
                    Reacts=post.Reacts.Select(pp => new BaseReact { Id = pp.Id, reacts = pp.reacts, UserAccountsId = pp.UserAccountsId }).ToList(),
                });
            }
            return Ok(posts);
        }
        [HttpPost("GetAllQuestionPosts")]
        public async Task<IActionResult> GetAllQuestionPosts(int pageNumber)
        {
            var result = await _postService.GetQuestionPostAsync(pageNumber);
            List<Dto.QuestionPost> posts = new List<Dto.QuestionPost>();
            foreach (var post in result)
            {
                posts.Add(new Dto.QuestionPost
                {
                    Id = post.Id,
                    Answer=post.Answer,
                    Question=post.Question,
                    TimeCreated = (DateTime.UtcNow) - (post.TimeCreated),
                    UserAccountsId = post.UserAccountsId,
                    PostUserLastName = post.UserAccounts.LastName,
                    PostUserFirstName = post.UserAccounts.FirstName,
                    Photos = post.Photos.Select(pp => new BasePhoto { Id = pp.Id, PhotoPath = pp.PhotoPath }).ToList(),
                    Vedios = post.Vedios.Select(pp => new BaseVedio { Id = pp.Id, VedioPath = pp.VedioPath }).ToList(),
                    Comments = post.Comments.Select(pp => new BaseComment { Id = pp.Id, comment = pp.comment, Date = pp.Date, UserAccountsId = pp.UserAccountsId }).ToList(),
                    Reacts = post.Reacts.Select(pp => new BaseReact { Id = pp.Id, reacts = pp.reacts, UserAccountsId = pp.UserAccountsId }).ToList(),

                });
            }
            return Ok(posts);
        }

        [HttpPost("GetAllPostTypes")]
        public async Task<IActionResult> GetAllPostTypes(int pageNumber)
        {
            var result = await _postService.GetPostTypesAsync(pageNumber);
            List<Dto.AllPost> posts = new List<Dto.AllPost>();
            foreach (var post in result)
            {
                switch(post.Type)
                {
                    case PostsTypes.Post:
                        posts.Add(new Dto.AllPost
                        {
                            Type = post.Type,
                            Id = post.Id,
                            Description = post.Description,
                            TimeCreated = (DateTime.UtcNow) - (post.TimeCreated),
                            UserAccountsId = post.UserAccountsId,
                            PostUserLastName = post.PostUserLastName,
                            PostUserFirstName = post.PostUserFirstName,
                            Photos = post.Photo,
                            Vedios = post.Vedio,
                            Comments = post.comments,
                            Reacts = post.reacts,
                        });
                        break;
                    case PostsTypes.Question:
                        posts.Add(new Dto.AllPost
                        {
                            Type= post.Type,
                            Id = post.Id,
                            Answer = post.Answer,
                            Question = post.Question,
                            TimeCreated = (DateTime.UtcNow) - (post.TimeCreated),
                            UserAccountsId = post.UserAccountsId,
                            PostUserLastName = post.PostUserLastName,
                            PostUserFirstName = post.PostUserFirstName,
                            Photos = post.Photo,
                            Vedios = post.Vedio,
                            Comments = post.comments,
                            Reacts = post.reacts,

                        });
                        break;
                    default:break;

                }
                
            }
            return Ok();
        }

        [HttpPost("DeletePost")]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            var userInfo = UserClaimsHelper.GetClaimsFromHttpContext(_httpContextAccessor);
            return Ok(await _postService.DeletePostAsync(id, userInfo.Email));
        }
        [HttpPost("DeleteQuestionPost")]
        public async Task<IActionResult> DeleteQuestionPost(Guid id)
        {
            var userInfo = UserClaimsHelper.GetClaimsFromHttpContext(_httpContextAccessor);
            return Ok(await _postService.DeleteQuestionPostAsync(id, userInfo.Email));
        }
        [HttpPost("AddPost")]
        public async Task<IActionResult> AddPost([FromForm] Dto.UploadPost postModel )
        {
            var userInfo = UserClaimsHelper.GetClaimsFromHttpContext(_httpContextAccessor);
            var uploadPost = new DataBase.Core.Models.Posts.UploadPost
            {
                Type = postModel.Type,
                Photos = postModel.Photos,
                Vedios = postModel.Vedios,
                Description = postModel.Description
            };
            bool result = await _postService.AddPostAsync(uploadPost, userInfo.Email); 
            return Ok(result);
        }
        [HttpPost("AddQuestionPost")]
        public async Task<IActionResult> AddQuestionPost( Dto.UploadQuestionPost questionPostModel)
        {
            var userInfo = UserClaimsHelper.GetClaimsFromHttpContext(_httpContextAccessor);
            var uploadPost = new DataBase.Core.Models.Posts.UploadPost
            {
                Answer = questionPostModel.Answer,
                Question = questionPostModel.Question,
                Photos = questionPostModel.Photos,
                Vedios = questionPostModel.Vedios,
                Type = questionPostModel.Type,

            };
            bool result = await _postService.AddQuestionPostAsync(uploadPost, userInfo.Email); 
            return Ok(result);
        }

        [HttpPost("UpdateQuestion")]
        public async Task<IActionResult> UpdateQuestion( UpdateQuestion questionPostModel)
        {
            var userInfo = UserClaimsHelper.GetClaimsFromHttpContext(_httpContextAccessor);
            var uploadPost = new DataBase.Core.Models.Posts.UpdataPost
            {
                Id = questionPostModel.Id,
                Question = questionPostModel.Question,
                Answer=questionPostModel.Answer,
                DeletedPhotoIds=questionPostModel.DeletedPhotoIds,
                DeletedVedioIds=questionPostModel.DeletedVedioIds,
                NewPhotos=questionPostModel.Photos,
                NewVedios=questionPostModel.Vedios,
                Type = questionPostModel.Type,
                
            };
            bool result = await _postService.UpdateQuestionPostAsync(uploadPost, userInfo.Email);
            return Ok(result);
        }
        [HttpPost("UpdatePost")]
        public async Task<IActionResult> UpdatePost( [FromForm] Dto.UpdatePost postModel)
        {
            var userInfo = UserClaimsHelper.GetClaimsFromHttpContext(_httpContextAccessor);
            var uploadPost = new DataBase.Core.Models.Posts.UpdataPost
            {
                Id = postModel.Id,
                DeletedPhotoIds = postModel.DeletedPhotoIds,
                DeletedVedioIds = postModel.DeletedVedioIds,
                NewPhotos = postModel.Photos,
                NewVedios = postModel.Vedios,
                Type = postModel.Type,
                Description=postModel.Description,

            };
            bool result = await _postService.UpdatePostAsync(uploadPost, userInfo.Email); 
            return Ok(result);
        }
    }
}
