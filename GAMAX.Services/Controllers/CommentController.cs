using Business.Posts.Services;
using DataBase.Core.Models.CommentModels;
using GAMAX.Services.Dto;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using System.Security.Claims;

namespace GAMAX.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICommentServices _commentServices;
        public CommentController(IHttpContextAccessor httpContextAccessor, ICommentServices commentServices)
        {
            _httpContextAccessor = httpContextAccessor;
            _commentServices = commentServices;
        }
        [HttpPost("GetPostComments")]
        public async Task<IActionResult> GetPostComments(Guid postId, int PageNumber)
        {
            return Ok(_commentServices.GetPostCommentsAsync(postId , PageNumber));
        }
        [HttpPost("GetQuestionComments")]
        public async Task<IActionResult> GetQuestionComments(Guid postId, int PageNumber)
        {
           return Ok(_commentServices.GetQuestionCommentsAsync(postId, PageNumber));
        }
        [HttpPost("AddPostComment")]
        public async Task<IActionResult> AddPostComment([FromBody] AddCommentRequest comment)
        {
            var userInfo = UserClaimsHelper.GetClaimsFromHttpContext(_httpContextAccessor);
            var result = await _commentServices.AddPostCommentAsync(comment, userInfo.Email);
            if (result)
                return Ok();

            return BadRequest(new {
                Message = "Fail"
            });
        }
        [HttpPost("AddQuestionComment")]
        public async Task<IActionResult> AddQuestionComment([FromBody] AddCommentRequest comment)
        {
            var userInfo = UserClaimsHelper.GetClaimsFromHttpContext(_httpContextAccessor);
            var result = await _commentServices.AddQuestionCommentAsync(comment, userInfo.Email);
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
        public async Task<IActionResult> UpdatePostComment([FromBody] CommentUpdateRequest comment)
        {
            var userInfo = UserClaimsHelper.GetClaimsFromHttpContext(_httpContextAccessor);
            var result = await _commentServices.UpdatePostCommentAsync(comment, userInfo.Email);
            if (result)
                return Ok();

            return BadRequest(new
            {
                Message = "Fail"
            });

        }
        [HttpPost("UpdateQuestionComment")]
        public async Task<IActionResult> UpdateQuestionComment([FromBody] CommentUpdateRequest comment)
        {
            var userInfo = UserClaimsHelper.GetClaimsFromHttpContext(_httpContextAccessor);
            var result = await _commentServices.UpdateQuestionCommentAsync(comment, userInfo.Email);
            if (result)
                return Ok();

            return BadRequest(new
            {
                Message = "Fail"
            });
        }
    }
}
