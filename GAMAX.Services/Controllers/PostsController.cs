using Business.Posts.Services;
using DataBase.Core.Enums;
using DataBase.Core.Models.CommentModels;
using DataBase.Core.Models.PhotoModels;
using DataBase.Core.Models.Posts;
using DataBase.Core.Models.Reacts;
using DataBase.Core.Models.VedioModels;
using GAMAX.Services.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Security.Claims;
using Utilites;

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
                    TimeCreated = TimeHelper.ConvertTimeCreateToString(post.TimeCreated),
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
                    TimeCreated = TimeHelper.ConvertTimeCreateToString(post.TimeCreated),
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
        [HttpPost("GetPostByID")]
        public async Task<IActionResult> GetPostByID(Guid id)
        {
            var result = await _postService.GetPostByIDAsync(id);
                var post =  
                (new Dto.Post
                {
                    Id = result.Id,
                    Description = result.Description,
                    TimeCreated = TimeHelper.ConvertTimeCreateToString(result.TimeCreated),
                    UserAccountsId = result.UserAccountsId,
                    PostUserLastName = result.UserAccounts.LastName,
                    PostUserFirstName = result.UserAccounts.FirstName,
                    Photos = result.Photos.Select(pp => new BasePhoto { Id = pp.Id, PhotoPath = pp.PhotoPath }).ToList().ToList(),
                    Vedios = result.Vedios.Select(pp => new BaseVedio { Id = pp.Id, VedioPath = pp.VedioPath }).ToList(),
                    Comments = result.Comments.Select(pp => new BaseComment { Id = pp.Id, comment = pp.comment, Date = pp.Date, UserAccountsId = pp.UserAccountsId }).ToList(),
                    Reacts = result.Reacts.Select(pp => new BaseReact { Id = pp.Id, reacts = pp.reacts, UserAccountsId = pp.UserAccountsId }).ToList(),
                });
            
            return Ok(post);
        }
        [HttpPost("GetQuestionByID")]
        public async Task<IActionResult> GetQuestionByID(Guid id)
        {
            var result = await _postService.GetQuestionPostByIdAsync(id);
            var question=   
                (new Dto.QuestionPost
                {
                    Id = result.Id,
                    Answer = result.Answer,
                    Question = result.Question,
                    TimeCreated = TimeHelper.ConvertTimeCreateToString(result.TimeCreated),
                    UserAccountsId = result.UserAccountsId,
                    PostUserLastName = result.UserAccounts.LastName,
                    PostUserFirstName = result.UserAccounts.FirstName,
                    Photos = result.Photos.Select(pp => new BasePhoto { Id = pp.Id, PhotoPath = pp.PhotoPath }).ToList(),
                    Vedios = result.Vedios.Select(pp => new BaseVedio { Id = pp.Id, VedioPath = pp.VedioPath }).ToList(),
                    Comments = result.Comments.Select(pp => new BaseComment { Id = pp.Id, comment = pp.comment, Date = pp.Date, UserAccountsId = pp.UserAccountsId }).ToList(),
                    Reacts = result.Reacts.Select(pp => new BaseReact { Id = pp.Id, reacts = pp.reacts, UserAccountsId = pp.UserAccountsId }).ToList(),

                });
            return Ok(question);
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
                            Type = PostsTypes.Post,
                            Id = post.Id,
                            Description = post.Description,
                            TimeCreated = TimeHelper.ConvertTimeCreateToString(post.TimeCreated),
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
                            Type= PostsTypes.Question,
                            Id = post.Id,
                            Answer = post.Answer,
                            Question = post.Question,
                            TimeCreated = TimeHelper.ConvertTimeCreateToString(post.TimeCreated),
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
            return Ok(posts);
        }
        [HttpPost("GetAllPersonalPosts")]
        public async Task<IActionResult> GetAllPersonalPosts(int pageNumber)
        {
            var userInfo = UserClaimsHelper.GetClaimsFromHttpContext(_httpContextAccessor);
            var result = await _postService.GetPersonalPostAsync(pageNumber,userInfo.Uid);
            List<Dto.Post> posts = new List<Dto.Post>();
            foreach (var post in result)
            {
                posts.Add(new Dto.Post
                {
                    Id = post.Id,
                    Description = post.Description,
                    TimeCreated = TimeHelper.ConvertTimeCreateToString(post.TimeCreated),
                    UserAccountsId = post.UserAccountsId,
                    PostUserLastName = post.UserAccounts.LastName,
                    PostUserFirstName = post.UserAccounts.FirstName,
                    Photos = post.Photos.Select(pp => new BasePhoto { Id = pp.Id, PhotoPath = pp.PhotoPath }).ToList().ToList(),
                    Vedios = post.Vedios.Select(pp => new BaseVedio { Id = pp.Id, VedioPath = pp.VedioPath }).ToList(),
                    Comments = post.Comments.Select(pp => new BaseComment { Id = pp.Id, comment = pp.comment, Date = pp.Date, UserAccountsId = pp.UserAccountsId }).ToList(),
                    Reacts = post.Reacts.Select(pp => new BaseReact { Id = pp.Id, reacts = pp.reacts, UserAccountsId = pp.UserAccountsId }).ToList(),
                });
            }
            return Ok(posts);
        }
        [HttpPost("GetAllPersonalQuestionPosts")]
        public async Task<IActionResult> GetAllPersonalQuestionPosts(int pageNumber)
        {
            var userInfo = UserClaimsHelper.GetClaimsFromHttpContext(_httpContextAccessor);
            var result = await _postService.GetPersonalQuestionPostAsync(pageNumber, userInfo.Uid);
            List<Dto.QuestionPost> posts = new List<Dto.QuestionPost>();
            foreach (var post in result)
            {
                posts.Add(new Dto.QuestionPost
                {
                    Id = post.Id,
                    Answer = post.Answer,
                    Question = post.Question,
                    TimeCreated = TimeHelper.ConvertTimeCreateToString(post.TimeCreated),
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

        [HttpPost("GetAllPersonalPostTypes")]
        public async Task<IActionResult> GetAllPersonalPostTypes(int pageNumber)
        {
            var userInfo = UserClaimsHelper.GetClaimsFromHttpContext(_httpContextAccessor); 
            var result = await _postService.GetPersonalPostTypesAsync(pageNumber, userInfo.Uid);
            List<Dto.AllPost> posts = new List<Dto.AllPost>();
            foreach (var post in result)
            {
                switch (post.Type)
                {
                    case PostsTypes.Post:
                        posts.Add(new Dto.AllPost
                        {
                            Type = PostsTypes.Post,
                            Id = post.Id,
                            Description = post.Description,
                            TimeCreated = TimeHelper.ConvertTimeCreateToString(post.TimeCreated),
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
                            Type = PostsTypes.Question,
                            Id = post.Id,
                            Answer = post.Answer,
                            Question = post.Question,
                            TimeCreated = TimeHelper.ConvertTimeCreateToString(post.TimeCreated),
                            UserAccountsId = post.UserAccountsId,
                            PostUserLastName = post.PostUserLastName,
                            PostUserFirstName = post.PostUserFirstName,
                            Photos = post.Photo,
                            Vedios = post.Vedio,
                            Comments = post.comments,
                            Reacts = post.reacts,

                        });
                        break;
                    default: break;

                }

            }
            return Ok(posts);
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
            (bool result, Guid id) = await _postService.AddPostAsync(uploadPost, userInfo.Email); 
            if(result)
            {
                var  post = await _postService.GetPostByIDAsync(id);
                var postResult = new Dto.Post
                            {
                                Id = post.Id,
                                Description = post.Description,
                                TimeCreated = TimeHelper.ConvertTimeCreateToString(post.TimeCreated),
                                UserAccountsId = post.UserAccountsId,
                                PostUserLastName = post.UserAccounts.LastName,
                                PostUserFirstName = post.UserAccounts.FirstName,
                                Photos = post.Photos.Select(pp => new BasePhoto { Id = pp.Id, PhotoPath = pp.PhotoPath }).ToList().ToList(),
                                Vedios = post.Vedios.Select(pp => new BaseVedio { Id = pp.Id, VedioPath = pp.VedioPath }).ToList(),
                                Comments = post.Comments.Select(pp => new BaseComment { Id = pp.Id, comment = pp.comment, Date = pp.Date, UserAccountsId = pp.UserAccountsId }).ToList(),
                                Reacts = post.Reacts.Select(pp => new BaseReact { Id = pp.Id, reacts = pp.reacts, UserAccountsId = pp.UserAccountsId }).ToList(),
                            };
                return Ok(post);
            }
            return BadRequest(result);
        }
        [HttpPost("AddQuestionPost")]
        public async Task<IActionResult> AddQuestionPost([FromForm] Dto.UploadQuestionPost questionPostModel)
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
            (bool result ,Guid id ) = await _postService.AddQuestionPostAsync(uploadPost, userInfo.Email);
            if( result )
            {
                var post = await _postService.GetQuestionPostByIdAsync(id);
                var question =
                (new Dto.QuestionPost
                {
                    Id = post.Id,
                    Answer = post.Answer,
                    Question = post.Question,
                    TimeCreated = TimeHelper.ConvertTimeCreateToString(post.TimeCreated),
                    UserAccountsId = post.UserAccountsId,
                    PostUserLastName = post.UserAccounts.LastName,
                    PostUserFirstName = post.UserAccounts.FirstName,
                    Photos = post.Photos.Select(pp => new BasePhoto { Id = pp.Id, PhotoPath = pp.PhotoPath }).ToList(),
                    Vedios = post.Vedios.Select(pp => new BaseVedio { Id = pp.Id, VedioPath = pp.VedioPath }).ToList(),
                    Comments = post.Comments.Select(pp => new BaseComment { Id = pp.Id, comment = pp.comment, Date = pp.Date, UserAccountsId = pp.UserAccountsId }).ToList(),
                    Reacts = post.Reacts.Select(pp => new BaseReact { Id = pp.Id, reacts = pp.reacts, UserAccountsId = pp.UserAccountsId }).ToList(),

                });
                return Ok(question);
            }
            return BadRequest(result);
        }

        [HttpPost("UpdateQuestion")]
        public async Task<IActionResult> UpdateQuestion( [FromForm] UpdateQuestion questionPostModel)
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
            if (result)
            {
                //return Ok();
                var post = await _postService.GetQuestionPostByIdAsync(questionPostModel.Id);
                var question =
                (new Dto.QuestionPost
                {
                    Id = post.Id,
                    Answer = post.Answer,
                    Question = post.Question,
                    TimeCreated = TimeHelper.ConvertTimeCreateToString(post.TimeCreated),
                    UserAccountsId = post.UserAccountsId,
                    PostUserLastName = post.UserAccounts.LastName,
                    PostUserFirstName = post.UserAccounts.FirstName,
                    Photos = post.Photos.Select(pp => new BasePhoto { Id = pp.Id, PhotoPath = pp.PhotoPath }).ToList(),
                    Vedios = post.Vedios.Select(pp => new BaseVedio { Id = pp.Id, VedioPath = pp.VedioPath }).ToList(),
                    Comments = post.Comments.Select(pp => new BaseComment { Id = pp.Id, comment = pp.comment, Date = pp.Date, UserAccountsId = pp.UserAccountsId }).ToList(),
                    Reacts = post.Reacts.Select(pp => new BaseReact { Id = pp.Id, reacts = pp.reacts, UserAccountsId = pp.UserAccountsId }).ToList(),

                });
                return Ok(question);
            }
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
            bool susscuss = await _postService.UpdatePostAsync(uploadPost, userInfo.Email); 
            if(susscuss) {
                var result = await _postService.GetPostByIDAsync(uploadPost.Id);
                var post =
                (new Dto.Post
                {
                    Id = result.Id,
                    Description = result.Description,
                    TimeCreated = TimeHelper.ConvertTimeCreateToString(result.TimeCreated),
                    UserAccountsId = result.UserAccountsId,
                    PostUserLastName = result.UserAccounts.LastName,
                    PostUserFirstName = result.UserAccounts.FirstName,
                    Photos = result.Photos.Select(pp => new BasePhoto { Id = pp.Id, PhotoPath = pp.PhotoPath }).ToList().ToList(),
                    Vedios = result.Vedios.Select(pp => new BaseVedio { Id = pp.Id, VedioPath = pp.VedioPath }).ToList(),
                    Comments = result.Comments.Select(pp => new BaseComment { Id = pp.Id, comment = pp.comment, Date = pp.Date, UserAccountsId = pp.UserAccountsId }).ToList(),
                    Reacts = result.Reacts.Select(pp => new BaseReact { Id = pp.Id, reacts = pp.reacts, UserAccountsId = pp.UserAccountsId }).ToList(),
                });
                //return Ok();
                return Ok(post);
            }

            return BadRequest(susscuss);
        }
        private string ConvertTimeCreateToString(DateTime timeCreated)
        {
            TimeSpan time = (DateTime.UtcNow) - (timeCreated);
            if (time.Days >= 365)
            {
                int years = time.Days / 365;
                return $"{years} Y";
            }
            if (time.Days >= 30)
            {
                int months = time.Days / 30;
                return $"{months} M";
            }
            if (time.Days > 0)
                return $"{time.Days} D";
            if (time.Hours > 0)
                return $"{time.Hours} H";
            if (time.Minutes > 0)
                return $"{time.Minutes} Minutes";
            if (time.Seconds > 0)
                return $"{time.Seconds} S";
            return "";
        }
    }
}
