using AutoMapper;
using Business.Accounts.Models;
using Business.Authentication.Models;
using Business.Enums;
using Business.Posts.Helper;
using Business.Posts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Collections.ObjectModel;

namespace Business.Posts.Services
{
    public class PostService : IPostService
    {

        private readonly ApplicationDbContext _dbContext;
        public PostService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        
        public async Task<List<Post>> GetAllPostAsync()
        {
            var result  = await _dbContext.Posts.Include(p=>p.Photos).Include(v=>v.Vedios).ToListAsync();
            return result;
        }
        public async Task<List<AllPostsModel>> GetAllPostTypesAsync()
        {
            var posts = await GetAllPostAsync();
            var questions = await GetAllQuestionPostAsync();

            var allPostsModel = new List<AllPostsModel>();

            // Add posts with type 'Post'
            allPostsModel.AddRange(posts.Select(post => new AllPostsModel
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
                TimeCreated = post.TimeCreated,
                Photo = post.Photos.ToList(),
                Vedio = post.Vedios.ToList(),
                Type = PostsTypes.Post,
                Question = null, // Set to null for posts
                Answer = null // Set to null for posts
            }));

            // Add questions with type 'Question' and populate Question and Answer properties
            allPostsModel.AddRange(questions.Select(question => new AllPostsModel
            {
                Id = question.Id,
                Title = question.Title,
                Description = question.Description,
                TimeCreated = question.TimeCreated,
                Photo = question.Photos.ToList(),
                Vedio= question.Vedios.ToList(),
                Type = PostsTypes.Question,
                Question = question.Question, // Set the Question property for questions
                Answer = question.Answer // Set the Answer property for questions
            }));

            // Sort the combined list by TimeCreated in descending order
            allPostsModel = allPostsModel.OrderByDescending(p => p.TimeCreated).ToList();

            return allPostsModel;
        }
        public async Task<List<QuestionPost>> GetAllQuestionPostAsync()
        {
            var result = await _dbContext.QuestionPosts.Include(p => p.Photos).Include(v=>v.Vedios).ToListAsync();
            return result;
        }
        public async Task<bool> AddPostAsync(AllPostsModel postmodel, string userEmail)
        {
            var user = await _dbContext.ProfileAccounts.FindAsync(userEmail);
            if (user == null) return false;
            postmodel = PostHelper.ConvertToPaths(postmodel, "PostPhoto", "PostVedios");
            var post = PreparePostModel(postmodel, user);
            await _dbContext.Posts.AddAsync(post);
            var saveresult = await _dbContext.SaveChangesAsync();
            return saveresult > 0;
        }
        public async Task<bool> AddQuestionPostAsync(AllPostsModel postmodel, string userEmail)
        {
            var user = await _dbContext.ProfileAccounts.FindAsync(userEmail);
            if (user == null) return false;
            postmodel = PostHelper.ConvertToPaths(postmodel, "PostPhoto", "PostVedios");
            var post = PrepareQuestonPostModel(postmodel, user);
            await _dbContext.QuestionPosts.AddAsync(post);
            var saveresult = await _dbContext.SaveChangesAsync();
            return saveresult > 0;
        }
        public async Task<bool> UpdatePostAsync(AllPostsModel postmodel , string userEmail)
        {
            var post = await _dbContext.Posts
                                        .Include(p => p.Photos)
                                        .Include(p => p.Vedios)
                                        .FirstOrDefaultAsync(p => p.Id == postmodel.Id);

            if (post == null)
                return false;
            
            var user = await _dbContext.ProfileAccounts.FindAsync(userEmail);
            if(user == null) 
                return false;  
            if (post.ProfileAccountId != user.Id)
                return false;
            UpdateOldPhotoAndVedio(postmodel, post);
            post = PreparePostModel(postmodel, user , post);
            _dbContext.Posts.Update(post);
            var update = await _dbContext.SaveChangesAsync();
            return update > 0;
        }
        public async Task<bool> UpdateQuestionPostAsync(AllPostsModel postmodel , string userEmail)
        {
            var questionpost = await _dbContext.QuestionPosts
                                        .Include(p => p.Photos)
                                        .Include(p => p.Vedios)
                                        .FirstOrDefaultAsync(p => p.Id == postmodel.Id);

            if (questionpost == null)
                return false;

            var user = await _dbContext.ProfileAccounts.FindAsync(userEmail);
            if (user == null)
                return false;
            if (questionpost.ProfileAccountId != user.Id)
                return false;
            UpdateOldPhotoAndVedio(postmodel, questionpost);
            questionpost = PrepareQuestonPostModel(postmodel, user, questionpost);
            _dbContext.QuestionPosts.Update(questionpost);
            var update = await _dbContext.SaveChangesAsync();
            return update > 0;
        }
        public async Task<bool> DeletePostAsync(Guid id , string userEmail)
        {
            var post = await _dbContext.Posts.FindAsync(id);
            if (post == null)
                return false;
            var user = await _dbContext.ProfileAccounts.FindAsync(userEmail);
            if (user == null)
                return false;
            if (post.ProfileAccountId != user.Id)
                return false;
            _dbContext.Posts.Remove(post);
            var result =await _dbContext.SaveChangesAsync();
            if(result>0)
                return true;
            return false;

        }
        public async Task<bool> DeleteQuestionPostAsync(Guid id , string userEmail)
        {
            var questionpost = await _dbContext.QuestionPosts.FindAsync(id);
            if (questionpost == null)
                return false;
            var user = await _dbContext.ProfileAccounts.FindAsync(userEmail);
            if(user==null)
                return false;
            if (questionpost.ProfileAccountId != user.Id)
                return false;
            _dbContext.QuestionPosts.Remove(questionpost);
            var result = await _dbContext.SaveChangesAsync();
            if (result > 0)
                return true;
            return false;
        }
        

        private QuestionPost PrepareQuestonPostModel(AllPostsModel postmodel, ProfileAccounts user, QuestionPost post = null)
        {
            if (post == null)
            {
                post = new QuestionPost();
                post.Id = Guid.NewGuid();
                post.ProfileAccount = user;
                post.ProfileAccountId = user.Id;
            }

            if (post.Photos == null)
                post.Photos = new Collection<Photo>();
            foreach (var photo in postmodel.PhotosPath)
            {
                var photoModel = new Photo()
                {
                    Id = new Guid(),
                    PhotoPath = photo,
                    QuestionPost = post,
                    QuestionPostId = post.Id
                };
                post.Photos.Add(photoModel);
            }
            if (post.Vedios == null)
                post.Vedios = new Collection<Vedio>();
            foreach (var vedio in postmodel.VediosPath)
            {
                var vedioModel = new Vedio()
                {
                    Id = new Guid(),
                    VedioPath = vedio,
                    QuestionPost = post,
                    QuestionPostId = post.Id
                };
                post.Vedios.Add(vedioModel);
            }
            post.Title = postmodel.Title;
            post.Description = postmodel.Description;
            post.Answer = postmodel.Answer;
            post.Question = postmodel.Question;
            return post;
        }
        private Post PreparePostModel(AllPostsModel postmodel, ProfileAccounts user, Post post = null)
        {

            if (post == null)
            {
                post = new Post();
                post.Id = Guid.NewGuid();
                post.ProfileAccount = user;
                post.ProfileAccountId = user.Id;
            }
            if (post.Photos == null)
                post.Photos = new Collection<Photo>();
            foreach (var photo in postmodel.PhotosPath)
            {
                var photoModel = new Photo()
                {
                    Id = new Guid(),
                    PhotoPath = photo,
                    Post = post,
                    PostId = post.Id
                };
                post.Photos.Add(photoModel);
            }
            if (post.Vedios == null)
                post.Vedios = new Collection<Vedio>();
            foreach (var vedio in postmodel.VediosPath)
            {
                var vedioModel = new Vedio()
                {
                    Id = new Guid(),
                    VedioPath = vedio,
                    Post = post,
                    PostId = post.Id
                };
                post.Vedios.Add(vedioModel);
            }
            post.Title = postmodel.Title;
            post.Description = postmodel.Description;
            return post;
        }
        private void UpdateOldPhotoAndVedio(AllPostsModel postmodel, object post)
        {
            if (post is Post)
            {
                var actualPost = post as Post;

                if (postmodel.Photo.Count != actualPost.Photos.Count)
                {
                    var photosToRemove = new List<Photo>();
                    foreach (var photo in actualPost.Photos)
                    {
                        var result = postmodel.Photo.FirstOrDefault(p => p.Id == photo.Id);
                        if (result == null)
                        {
                            photosToRemove.Add(photo);
                        }
                    }

                    foreach (var photoToRemove in photosToRemove)
                    {
                        actualPost.Photos.Remove(photoToRemove);
                        _dbContext.Photos.Remove(photoToRemove);
                    }
                }
            }
            else if (post is QuestionPost)
            {
                var actualQuestionPost = post as QuestionPost;

                if (postmodel.Photo.Count != actualQuestionPost.Photos.Count)
                {
                    var photosToRemove = new List<Photo>();
                    foreach (var photo in actualQuestionPost.Photos)
                    {
                        var result = postmodel.Photo.FirstOrDefault(p => p.Id == photo.Id);
                        if (result == null)
                        {
                            photosToRemove.Add(photo);
                        }
                    }

                    foreach (var photoToRemove in photosToRemove)
                    {
                        actualQuestionPost.Photos.Remove(photoToRemove);
                        _dbContext.Photos.Remove(photoToRemove);
                    }
                }
            }
        }
        
    }
}
