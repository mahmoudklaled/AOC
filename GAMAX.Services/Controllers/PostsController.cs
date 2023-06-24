using Business;
using Business.Accounts.Services;
using Business.Enums;
using Business.Posts.Models;
using Business.Posts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
        public PostsController(IHttpContextAccessor httpContextAccessor ,IPostService postService )
        {
            _httpContextAccessor = httpContextAccessor;
            _postService = postService;
        }
        [HttpPost("GetAllPosts")]  
        public async Task<IActionResult> GetAllPosts()
        {
            return Ok(_postService.GetAllPostAsync());
        }
        [HttpPost("GetAllQuestionPosts")]
        public async Task<IActionResult> GetAllQuestionPosts()
        {
            return Ok(_postService.GetAllQuestionPostAsync());
        }

        [HttpPost("GetAllPostTypes")]
        public async Task<IActionResult> GetAllPostTypes()
        {
            return Ok(_postService.GetAllPostTypesAsync());
        }

        [HttpPost("DeletePost")]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            HttpContext context = _httpContextAccessor.HttpContext;
            string email = context.User.FindFirst(ClaimTypes.Email)?.Value;
            return Ok(_postService.DeletePostAsync(id , email));
        }
        [HttpPost("DeleteQuestionPost")]
        public async Task<IActionResult> DeleteQuestionPost(Guid id)
        {
            HttpContext context = _httpContextAccessor.HttpContext;
            string email = context.User.FindFirst(ClaimTypes.Email)?.Value;
            return Ok(_postService.DeletePostAsync(id , email));
        }
        [HttpPost("AddPost")]
        public async Task<IActionResult> AddPost(AllPostsModel postmodel)
        {
            HttpContext context = _httpContextAccessor.HttpContext;
            string email = context.User.FindFirst(ClaimTypes.Email)?.Value;
            bool result;
            switch (postmodel.Type)
            {
                case PostsTypes.Post:
                    result = await _postService.AddPostAsync(postmodel, email);
                    break;
                case PostsTypes.Question:
                    result = await _postService.AddQuestionPostAsync(postmodel, email);
                    break;
                default:
                    result = false;
                    break;
            }
            return Ok(_postService);
        }
        
        [HttpPost("UpdatePostOrQuestion")]
        public async Task<IActionResult> UpdatePostOrQuestion(AllPostsModel postmodel)
        {
            HttpContext context = _httpContextAccessor.HttpContext;
            string email = context.User.FindFirst(ClaimTypes.Email)?.Value;
            bool result;
            switch(postmodel.Type)
            {
                case PostsTypes.Post:
                    result =await  _postService.UpdatePostAsync(postmodel, email);
                    break;
                case PostsTypes.Question:
                    result = await _postService.UpdateQuestionPostAsync(postmodel, email);
                    break;
                default:
                    result = false;  
                    break;
            }
            return Ok(result);
        }
    }
}
