using Business.Posts.Services;
using GAMAX.Services.Dto;
using Microsoft.AspNetCore.Mvc;

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
            var posts = await _postService.GetPostAsync(pageNumber);
            return Ok(posts);
        }
        [HttpPost("GetAllQuestionPosts")]
        public async Task<IActionResult> GetAllQuestionPosts(int pageNumber)
        {
            var posts = await _postService.GetQuestionPostAsync(pageNumber);
            return Ok(posts);
        }
        [HttpPost("GetPostByID")]
        public async Task<IActionResult> GetPostByID(Guid id)
        {
            var post = await _postService.GetPostByIDAsync(id);
            return Ok(post);
        }
        [HttpPost("GetQuestionByID")]
        public async Task<IActionResult> GetQuestionByID(Guid id)
        {
            var question = await _postService.GetQuestionPostByIdAsync(id);
            return Ok(question);
        }
        [HttpPost("GetAllPostTypes")]
        public async Task<IActionResult> GetAllPostTypes(int pageNumber)
        {
            var Posts = await _postService.GetPostTypesAsync(pageNumber);
            return Ok(Posts);
        }
        [HttpPost("GetAllPersonalPosts")]
        public async Task<IActionResult> GetAllPersonalPosts(int pageNumber)
        {
            var userInfo = UserClaimsHelper.GetClaimsFromHttpContext(_httpContextAccessor);
            var posts = await _postService.GetPersonalPostAsync(pageNumber,userInfo.Uid);
            return Ok(posts);
        }
        [HttpPost("GetAllPersonalQuestionPosts")]
        public async Task<IActionResult> GetAllPersonalQuestionPosts(int pageNumber)
        {
            var userInfo = UserClaimsHelper.GetClaimsFromHttpContext(_httpContextAccessor);
            var posts = await _postService.GetPersonalQuestionPostAsync(pageNumber, userInfo.Uid);
            return Ok(posts);
        }

        [HttpPost("GetAllPersonalPostTypes")]
        public async Task<IActionResult> GetAllPersonalPostTypes(int pageNumber)
        {
            var userInfo = UserClaimsHelper.GetClaimsFromHttpContext(_httpContextAccessor); 
            var posts = await _postService.GetPersonalPostTypesAsync(pageNumber, userInfo.Uid);
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
        public async Task<IActionResult> AddPost([FromForm] DomainModels.DTO.UploadPost postModel )
        {
            var userInfo = UserClaimsHelper.GetClaimsFromHttpContext(_httpContextAccessor);
            var uploadPost = new DomainModels.Models.UploadPost
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
                return Ok(post);
            }
            return BadRequest(result);
        }
        [HttpPost("AddQuestionPost")]
        public async Task<IActionResult> AddQuestionPost([FromForm] DomainModels.DTO.UploadQuestionPost questionPostModel)
        {
            var userInfo = UserClaimsHelper.GetClaimsFromHttpContext(_httpContextAccessor);
            var uploadPost = new DomainModels.Models.UploadPost
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
                return Ok(post);
            }
            return BadRequest(result);
        }

        [HttpPost("UpdateQuestion")]
        public async Task<IActionResult> UpdateQuestion( [FromForm] DomainModels.DTO.UpdateQuestion questionPostModel)
        {
            var userInfo = UserClaimsHelper.GetClaimsFromHttpContext(_httpContextAccessor);
            var uploadPost = new DomainModels.Models.UpdataPost
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
                var post = await _postService.GetQuestionPostByIdAsync(questionPostModel.Id);
                return Ok(post);
            }
            return Ok(result);
        }
        [HttpPost("UpdatePost")]
        public async Task<IActionResult> UpdatePost( [FromForm] DomainModels.DTO.UpdatePost postModel)
        {
            var userInfo = UserClaimsHelper.GetClaimsFromHttpContext(_httpContextAccessor);
            var uploadPost = new DomainModels.Models.UpdataPost
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
                var post = await _postService.GetPostByIDAsync(uploadPost.Id);
                return Ok(post);
            }
            return BadRequest(susscuss);
        }
        
    }
}
