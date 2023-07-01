using Business.Posts.Services;
using DataBase.Core.Models.CommentModels;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetPostComments(Guid postId, int requestcount)
        {
            return Ok(_commentServices.GetPostCommentsAsync(postId ,requestcount));
        }
        [HttpPost("GetQuestionComments")]
        public async Task<IActionResult> GetQuestionComments(Guid postId, int requestcount)
        {
           return Ok(_commentServices.GetQuestionCommentsAsync(postId,requestcount));
        }
        [HttpPost("AddPostComment")]
        public async Task<IActionResult> AddPostComment([FromBody]CommentRequest comment ,string ? userEmail)
        {
            HttpContext context = _httpContextAccessor.HttpContext;
            string email = context.User.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null && userEmail==null)
                return BadRequest(new
                {
                    message = "Email Needed !"
                });
            if (email != null)
                return Ok(_commentServices.AddPostCommentAsync(comment, email));
            else if(userEmail!=null) return Ok(_commentServices.AddPostCommentAsync(comment, userEmail));
            return BadRequest(new
            {
                message = "Fail"
            });
        }
        [HttpPost("AddQuestionComment")]
        public async Task<IActionResult> AddQuestionComment([FromBody] CommentRequest comment, string? userEmail)
        {
            HttpContext context = _httpContextAccessor.HttpContext;
            string email = context.User.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null && userEmail == null)
                return BadRequest(new
                {
                    message = "Email Needed !"
                });
            if (email != null)
                return Ok(_commentServices.AddQuestionCommentAsync(comment, email));
            else if (userEmail != null) return Ok(_commentServices.AddQuestionCommentAsync(comment, userEmail));
            return BadRequest(new
            {
                message = "Fail"
            });
        }
        [HttpPost("DeletePostComment")]
        public async Task<IActionResult> DeletePostComment([FromBody] Guid commentId, string? userEmail)
        {
            HttpContext context = _httpContextAccessor.HttpContext;
            string email = context.User.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null && userEmail == null)
                return BadRequest(new
                {
                    message = "Email Needed !"
                });
            if (email != null)
                return Ok(_commentServices.DeletePostCommentAsync(commentId, email));
            else if (userEmail != null) return Ok(_commentServices.DeletePostCommentAsync(commentId, userEmail));
            return BadRequest(new
            {
                message = "Fail!"
            });
        }
        [HttpPost("DeleteQuestionComment")]
        public async Task<IActionResult> DeleteQuestionComment([FromBody] Guid commentId, string? userEmail)
        {
            HttpContext context = _httpContextAccessor.HttpContext;
            string email = context.User.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null && userEmail == null)
                return BadRequest(new
                {
                    message = "Email Needed !"
                });
            if (email != null)
                return Ok(_commentServices.DeleteQuestionCommentAsync(commentId, email));
            else if (userEmail != null) return Ok(_commentServices.DeleteQuestionCommentAsync(commentId, userEmail));
            return BadRequest(new
            {
                message = "Fail!"
            });
        }
        [HttpPost("UpdatePostComment")]
        public async Task<IActionResult> UpdatePostComment([FromBody] CommentRequest comment, string? userEmail)
        {
            HttpContext context = _httpContextAccessor.HttpContext;
            string email = context.User.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null && userEmail == null)
                return BadRequest(new
                {
                    message = "Email Needed !"
                });
            if (email != null)
                return Ok(_commentServices.UpdatePostCommentAsync(comment, email));
            else if (userEmail != null) return Ok(_commentServices.UpdatePostCommentAsync(comment, userEmail));
            return BadRequest(new
            {
                message = "Fail"
            });
        }
        [HttpPost("UpdateQuestionComment")]
        public async Task<IActionResult> UpdateQuestionComment([FromBody] CommentRequest comment, string? userEmail)
        {
            HttpContext context = _httpContextAccessor.HttpContext;
            string email = context.User.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null && userEmail == null)
                return BadRequest(new
                {
                    message = "Email Needed !"
                });
            if (email != null)
                return Ok(_commentServices.UpdateQuestionCommentAsync(comment, email));
            else if (userEmail != null) return Ok(_commentServices.UpdateQuestionCommentAsync(comment, userEmail));
            return BadRequest(new
            {
                message = "Fail"
            });
        }
    }
}
