using Business.Enums;
using Business.Posts.Models;
using Business.Posts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
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
        public async Task<IActionResult> GetPostComments(Guid postId, string postType,int countToSkip)
        {
            PostsTypes postsTypes;
            if(Enum.TryParse(postType, out postsTypes))
                return Ok(_commentServices.GetCommentsAsync(postId, postsTypes, countToSkip));
            return BadRequest(new
            {
                message = "Wrong Post Type !"
            }) ; 
        }
        [HttpPost("AddComment")]
        public async Task<IActionResult> AddComment([FromBody]CommentRequest comment ,string ? userEmail)
        {
            HttpContext context = _httpContextAccessor.HttpContext;
            string email = context.User.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null && userEmail==null)
                return BadRequest(new
                {
                    message = "Email Needed !"
                });
            if (email != null)
                return Ok(_commentServices.AddCommentAsync(comment, email));
            else if(userEmail!=null) return Ok(_commentServices.AddCommentAsync(comment, userEmail));
            return BadRequest(new
            {
                message = "Fail"
            });
        }
        [HttpPost("DeleteComment")]
        public async Task<IActionResult> DeleteComment([FromBody] Guid commentId, string? userEmail)
        {
            HttpContext context = _httpContextAccessor.HttpContext;
            string email = context.User.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null && userEmail == null)
                return BadRequest(new
                {
                    message = "Email Needed !"
                });
            if (email != null)
                return Ok(_commentServices.DeleteCommentAsync(commentId, email));
            else if (userEmail != null) return Ok(_commentServices.DeleteCommentAsync(commentId, userEmail));
            return BadRequest(new
            {
                message = "Fail!"
            });
        }
    }
}
