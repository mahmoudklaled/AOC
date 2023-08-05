using DataBase.Core.Models.PhotoModels;
using DataBase.Core.Models.Posts;
using DataBase.Core.Models.Reacts;
using DataBase.Core.Models.VedioModels;
using GAMAX.Services.Dto;
using Microsoft.AspNetCore.Mvc;
using Utilites;

namespace GAMAX.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Business.Posts.Services.ICommentServices _commentServices;
        public CommentController(IHttpContextAccessor httpContextAccessor, Business.Posts.Services.ICommentServices commentServices)
        {
            _httpContextAccessor = httpContextAccessor;
            _commentServices = commentServices;
        }
        [HttpPost("GetPostComments")]
        public async Task<IActionResult> GetPostComments(Guid postId, int PageNumber)
        {
            var result = await _commentServices.GetPostCommentsAsync(postId, PageNumber);
            var commentsInfo = new List<CommentData>();
            foreach (var item in result)
            {
                commentsInfo.Add(new CommentData
                {
                    Id = item.Id,
                    comment = item.comment,
                    CommentPhoto = item.PostCommentPhoto,
                    CommentVedio = item.PostCommentVedio,
                    Date = TimeHelper.ConvertTimeCreateToString(item.Date),
                    UserFirstName = item.UserAccounts.FirstName,
                    UserLastName = item.UserAccounts.LastName,
                    CommentReacts = item.PostCommentReacts.Select(pp => new BaseReact { Id = pp.Id, reacts = pp.reacts }).ToList()
                });
            }
            return Ok(commentsInfo);
        }
        [HttpPost("GetQuestionComments")]
        public async Task<IActionResult> GetQuestionComments(Guid postId, int PageNumber)
        {
            var result = await _commentServices.GetQuestionCommentsAsync(postId, PageNumber);
            var commentsInfo = new List<CommentData>();
            foreach (var item in result)
            {
                commentsInfo.Add(new CommentData
                {
                    Id= item.Id,
                    comment= item.comment,
                    CommentPhoto =item.QuestionCommentPhoto,
                    CommentVedio=item.QuestionCommentVedio,
                    Date= TimeHelper.ConvertTimeCreateToString(item.Date),
                    UserFirstName=item.UserAccounts.FirstName,
                    UserLastName=item.UserAccounts.LastName,
                    CommentReacts= item.QuestionCommentReacts.Select(pp => new BaseReact { Id = pp.Id, reacts = pp.reacts }).ToList()
                });
            }
           return Ok(commentsInfo);
        }
        [HttpPost("AddPostComment")]
        public async Task<IActionResult> AddPostComment([FromForm] AddCommentRequest requestModel)
        {
            var userInfo = UserClaimsHelper.GetClaimsFromHttpContext(_httpContextAccessor);
            var cmmnt = new DataBase.Core.Models.CommentModels.AddCommentRequest
            {
                comment = requestModel.comment,
                Photo = requestModel.Photo,
                Vedio = requestModel.Vedio,
                PostId = requestModel.PostId,
            };
            var result = await _commentServices.AddPostCommentAsync(cmmnt, userInfo.Email);
            if (result)
                return Ok();

            return BadRequest(new {
                Message = "Fail"
            });
        }
        [HttpPost("AddQuestionComment")]
        public async Task<IActionResult> AddQuestionComment([FromForm] AddCommentRequest requestModel)
        {
            var userInfo = UserClaimsHelper.GetClaimsFromHttpContext(_httpContextAccessor);
            var cmmnt = new DataBase.Core.Models.CommentModels.AddCommentRequest
            {
                comment = requestModel.comment,
                Photo = requestModel.Photo,
                Vedio = requestModel.Vedio,
                PostId = requestModel.PostId,
            };
            var result = await _commentServices.AddQuestionCommentAsync(cmmnt, userInfo.Email);
            if (result)
                return Ok();

            return BadRequest(new
            {
                Message = "Fail"
            });
        }
        [HttpPost("DeletePostComment")]
        public async Task<IActionResult> DeletePostComment( Guid commentId)
        {
            var userInfo = UserClaimsHelper.GetClaimsFromHttpContext(_httpContextAccessor);
            var result = await _commentServices.DeletePostCommentAsync(commentId, userInfo.Email);
            if (result)
                return Ok();

            return BadRequest(new
            {
                Message = "Fail"
            });
        }
        [HttpPost("DeleteQuestionComment")]
        public async Task<IActionResult> DeleteQuestionComment( Guid commentId)
        {
            var userInfo = UserClaimsHelper.GetClaimsFromHttpContext(_httpContextAccessor);
            var result = await _commentServices.DeleteQuestionCommentAsync(commentId, userInfo.Email);
            if (result)
                return Ok();

            return BadRequest(new
            {
                Message = "Fail"
            });
        }
        [HttpPost("UpdatePostComment")]
        public async Task<IActionResult> UpdatePostComment([FromForm] CommentUpdateRequest requestModel)
        {
            var userInfo = UserClaimsHelper.GetClaimsFromHttpContext(_httpContextAccessor);
            var cmmnt = new DataBase.Core.Models.CommentModels.CommentUpdateRequest
            {
                comment = requestModel.comment,
                Photo = requestModel.Photo,
                Vedio = requestModel.Vedio,
                Id= requestModel.Id,
                
            };
            var result = await _commentServices.UpdatePostCommentAsync(cmmnt, userInfo.Email);
            if (result)
                return Ok();

            return BadRequest(new
            {
                Message = "Fail"
            });

        }
        [HttpPost("UpdateQuestionComment")]
        public async Task<IActionResult> UpdateQuestionComment([FromForm] CommentUpdateRequest requestModel)
        {
            var userInfo = UserClaimsHelper.GetClaimsFromHttpContext(_httpContextAccessor);
            var cmmnt = new DataBase.Core.Models.CommentModels.CommentUpdateRequest
            {
                comment = requestModel.comment,
                Photo = requestModel.Photo,
                Vedio = requestModel.Vedio,
                Id = requestModel.Id,

            };
            var result = await _commentServices.UpdateQuestionCommentAsync(cmmnt, userInfo.Email);
            if (result)
                return Ok();

            return BadRequest(new
            {
                Message = "Fail"
            });
        }
    }
}
