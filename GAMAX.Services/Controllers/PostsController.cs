using Business.Posts.Services;
using DataBase.Core.Enums;
using DataBase.Core.Models.Posts;
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
        public async Task<IActionResult> GetAllPosts(int take , int skip)
        {
            return Ok( await _postService.GetPostAsync(take,skip));
        }
        [HttpPost("GetAllQuestionPosts")]
        public async Task<IActionResult> GetAllQuestionPosts(int take, int skip)
        {
            //var result = ;
            return Ok(await _postService.GetQuestionPostAsync(take, skip));
        }

        [HttpPost("GetAllPostTypes")]
        public async Task<IActionResult> GetAllPostTypes(int take, int skip)
        {
            return Ok(await _postService.GetPostTypesAsync(take, skip));
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
        public async Task<IActionResult> AddPost([FromBody] UploadPost postmodel )
        {
            var userInfo = UserClaimsHelper.GetClaimsFromHttpContext(_httpContextAccessor);
            bool result;
            switch (postmodel.Type)
            {
                case PostsTypes.Post:
                    result = await _postService.AddPostAsync(postmodel, userInfo.Email);
                    break;
                case PostsTypes.Question:
                    result = await _postService.AddQuestionPostAsync(postmodel, userInfo.Email);
                    break;
                default:
                    result = false;
                    break;
            }
            return Ok(result);
        }
        
        [HttpPost("UpdatePostOrQuestion")]
        public async Task<IActionResult> UpdatePostOrQuestion([FromBody] UpdataPost postmodel)
        {
            var userInfo = UserClaimsHelper.GetClaimsFromHttpContext(_httpContextAccessor);
            bool result;
            switch(postmodel.Type)
            {
                case PostsTypes.Post:
                    result =await  _postService.UpdatePostAsync(postmodel, userInfo.Email);
                    break;
                case PostsTypes.Question:
                    result = await _postService.UpdateQuestionPostAsync(postmodel, userInfo.Email);
                    break;
                default:
                    result = false;  
                    break;
            }
            return Ok(result);
        }
    }
}
