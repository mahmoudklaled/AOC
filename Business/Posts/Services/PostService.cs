using BDataBase.Core.Models.Accounts;
using Business.Posts.Helper;
using DataBase.Core;
using DataBase.Core.Enums;
using DataBase.Core.Models.PhotoModels;
using DataBase.Core.Models.Posts;
using DataBase.Core.Models.VedioModels;
using System.Collections.ObjectModel;
using Utilites;

namespace Business.Posts.Services
{
    public class PostService : IPostService
    {

        private readonly IUnitOfWork _unitOfWork;
        public PostService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<List<Post>> GetPostAsync(int ? take,int ? skip)
        {
            string[] includes = { "Photos", "Vedios" , "Reacts" };
            var result = await _unitOfWork.Post.FindAllAsync(null,take ??0, skip ?? 0, includes);
            return result.ToList();
        }
        public async Task<List<QuestionPost>> GetQuestionPostAsync(int? take, int? skip)
        {
            string[] includes = { "Photos", "Vedios" , "Reacts" };
            var result = await _unitOfWork.QuestionPost.FindAllAsync(null, take ?? 0, skip ?? 0,  includes);
            return  result.ToList();
        }
        public async Task<List<AllPostsModel>> GetPostTypesAsync(int? take, int? skip)
        {
            var posts = await GetPostAsync(take,skip);
            var questions = await GetQuestionPostAsync(take, skip);

            var allPostsModel = new List<AllPostsModel>();

            List<PostPhoto> postPhotos = new List<PostPhoto>();
            // Populate the postPhotos list with data

            List<BasePhoto> basePhotos = postPhotos.Select(pp => new BasePhoto
            {
                Id = pp.Id,
                PhotoPath = pp.PhotoPath
            }).ToList();
            // Add posts with type 'Post'
            allPostsModel.AddRange(posts.Select(post => new AllPostsModel
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
                TimeCreated = post.TimeCreated,
                Photo = post.Photos.Select(pp => new BasePhoto{Id = pp.Id,PhotoPath = pp.PhotoPath}).ToList(),
                Vedio = post.Vedios.Select(pp => new BaseVedio { Id = pp.Id, VedioPath = pp.VedioPath }).ToList(),
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
                Photo = question.Photos.Select(pp => new BasePhoto { Id = pp.Id, PhotoPath = pp.PhotoPath }).ToList().ToList(),
                Vedio = question.Vedios.Select(pp => new BaseVedio { Id = pp.Id, VedioPath = pp.VedioPath }).ToList(),
                Type = PostsTypes.Question,
                Question = question.Question, // Set the Question property for questions
                Answer = question.Answer // Set the Answer property for questions
            }));

            // Sort the combined list by TimeCreated in descending order
            allPostsModel = allPostsModel.OrderByDescending(p => p.TimeCreated).ToList();

            return allPostsModel;
        }
        public async Task<bool> AddPostAsync(UploadPost postmodel, string userEmail)
        {
            var user = await _unitOfWork.UserAccounts.FindAsync(p=>p.Email ==userEmail);
            if (user == null) return false;
            var newpostmodel = PostHelper.ConvertToPaths(postmodel, "PostPhoto", "PostVedios");
            var post = PreparePostModel(newpostmodel, user);
            await _unitOfWork.Post.AddAsync(post);
            var saveresult = _unitOfWork.Complete();
            return await saveresult > 0;
        }
        public async Task<bool> AddQuestionPostAsync(UploadPost postmodel, string userEmail)
        {
            var user = await _unitOfWork.UserAccounts.FindAsync(p=>p.Email==userEmail);
            if (user == null) return false;
            var newpostmodel = PostHelper.ConvertToPaths(postmodel, "PostPhoto", "PostVedios");
            var post = PrepareQuestonPostModel(newpostmodel, user);
            await _unitOfWork.QuestionPost.AddAsync(post);
            var saveresult =  _unitOfWork.Complete();
            return await saveresult > 0;
        }
        public async Task<bool> UpdatePostAsync(UpdataPost postmodel, string userEmail)
        {
            
            var post = await _unitOfWork.Post.FindAsync(p => p.Id == postmodel.Id);
            if (post == null)
                return false;
            var user = await _unitOfWork.UserAccounts.FindAsync(p => p.Email == userEmail);
            if (user == null)
                return false;
            if (post.UserAccountsId != user.Id)
                return false;
            DeletePostPhotoAndVedio(postmodel);
            AddNewPostPhotoAndVedio(postmodel);
            post.Description= postmodel.Description;
            post.Title=postmodel.Title;
            _unitOfWork.Post.Update(post);
            var update = _unitOfWork.Complete();
            return await update > 0;
        }
        public async Task<bool> UpdateQuestionPostAsync(UpdataPost postmodel, string userEmail)
        {
            var questionpost = await _unitOfWork.QuestionPost.FindAsync(p => p.Id == postmodel.Id);
                                        
            if (questionpost == null)
                return false;

            var user = await _unitOfWork.UserAccounts.FindAsync(p => p.Email == userEmail);
            if (user == null)
                return false;
            if (questionpost.UserAccountsId != user.Id)
                return false;
            DeletePostPhotoAndVedio(postmodel);
            AddNewPostPhotoAndVedio(postmodel);
            questionpost.Description= postmodel.Description;
            questionpost.Title= postmodel.Title;
            questionpost.Question = postmodel.Question;
            questionpost.Answer= postmodel.Answer;
            _unitOfWork.QuestionPost.Update(questionpost);
            var update =  _unitOfWork.Complete();
            return await update > 0;
        }
        public async Task<bool> DeletePostAsync(Guid id, string userEmail)
        {
            var post = await _unitOfWork.Post.FindAsync(p=>p.Id==id);
            if (post == null)
                return false;
            var user = await _unitOfWork.UserAccounts.FindAsync(p=>p.Email==userEmail);
            if (user == null)
                return false;
            if (post.UserAccountsId != user.Id)
                return false;
            _unitOfWork.Post.Delete(post);
            var result =  _unitOfWork.Complete();
            if (await result > 0)
                return true;
            return false;

        }
        public async Task<bool> DeleteQuestionPostAsync(Guid id, string userEmail)
        {
            var questionpost = await _unitOfWork.QuestionPost.FindAsync(q=>q.Id==id);
            if (questionpost == null)
                return false;
            var user = await _unitOfWork.UserAccounts.FindAsync(p => p.Email == userEmail);
            if (user == null)
                return false;
            if (questionpost.UserAccountsId != user.Id)
                return false;
            _unitOfWork.QuestionPost.Delete(questionpost);
            var result =  _unitOfWork.Complete();
            if (await result > 0)
                return true;
            return false;
        }
        private QuestionPost PrepareQuestonPostModel(AllPostsModel postmodel, UserAccounts user, QuestionPost post = null)
        {
            if (post == null)
            {
                post = new QuestionPost();
                post.Id = Guid.NewGuid();
                post.UserAccounts = user;
                post.UserAccountsId = user.Id;
            }

            if (post.Photos == null)
                post.Photos = new Collection<QuestionPhoto>();
            foreach (var photo in postmodel.PhotosPath)
            {
                var photoModel = new QuestionPhoto()
                {
                    Id = new Guid(),
                    PhotoPath = photo,
                    QuestionPost = post,
                    QuestionId = post.Id
                };
                post.Photos.Add(photoModel);
            }
            if (post.Vedios == null)
                post.Vedios = new Collection<QuestionVedio>();
            foreach (var vedio in postmodel.VediosPath)
            {
                var vedioModel = new QuestionVedio()
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
        private Post PreparePostModel(AllPostsModel postmodel, UserAccounts user, Post post = null)
        {

            if (post == null)
            {
                post = new Post();
                post.Id = Guid.NewGuid();
                post.UserAccounts = user;
                post.UserAccountsId = user.Id;
            }
            if (post.Photos == null)
                post.Photos = new Collection<PostPhoto>();
            foreach (var photo in postmodel.PhotosPath)
            {
                var photoModel = new PostPhoto()
                {
                    Id = new Guid(),
                    PhotoPath = photo,
                    Post = post,
                    PostId = post.Id
                };
                post.Photos.Add(photoModel);
            }
            if (post.Vedios == null)
                post.Vedios = new Collection<PostVedio>();
            foreach (var vedio in postmodel.VediosPath)
            {
                var vedioModel = new PostVedio()
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
        private async void AddNewPostPhotoAndVedio(UpdataPost postmodel)
        {
            switch (postmodel.Type)
            {
                case PostsTypes.Post:
                    foreach (var item in postmodel.NewPhotos)
                    {
                        var path = MediaUtilites.ConverIformToPath(item, "PostPhoto");
                        var photo = new PostPhoto
                        {
                            Id = Guid.NewGuid(),
                            PhotoPath = path,
                            PostId = postmodel.Id
                        };
                        await _unitOfWork.PostPhoto.AddAsync(photo);
                    }
                    foreach (var item in postmodel.NewVedios)
                    {
                        var path = MediaUtilites.ConverIformToPath(item, "PostVedios");
                        var vedio = new PostVedio
                        {
                            Id = Guid.NewGuid(),
                            VedioPath = path,
                            PostId = postmodel.Id
                        };
                        await _unitOfWork.PostVedio.AddAsync(vedio);
                    }
                    break;
                case PostsTypes.Question:
                    foreach (var item in postmodel.NewPhotos)
                    {
                        var path = MediaUtilites.ConverIformToPath(item, "PostPhoto");
                        var photo = new QuestionPhoto
                        {
                            Id = Guid.NewGuid(),
                            PhotoPath = path,
                            QuestionId = postmodel.Id
                        };
                        await _unitOfWork.QuestionPhoto.AddAsync(photo);
                    }
                    foreach (var item in postmodel.NewVedios)
                    {
                        var path = MediaUtilites.ConverIformToPath(item, "PostVedios");
                        var vedio = new QuestionVedio
                        {
                            Id = Guid.NewGuid(),
                            VedioPath = path,
                            QuestionPostId = postmodel.Id
                        };
                        await _unitOfWork.QuestionVedio.AddAsync(vedio);
                    }
                    break;
                default: break;


            }
            _unitOfWork.Complete();
        }
        private async void DeletePostPhotoAndVedio(UpdataPost postmodel)
        {
            switch (postmodel.Type)
            {
                case PostsTypes.Post:
                    foreach (var item in postmodel.DeletedPhotoIds)
                    {
                        var photo = await _unitOfWork.PostPhoto.FindAsync(p => p.Id == item);
                        if (photo != null)
                            _unitOfWork.PostPhoto.Delete(photo);
                    }
                    foreach (var item in postmodel.DeletedVedioIds)
                    {
                        var vedio = await _unitOfWork.PostVedio.FindAsync(p => p.Id == item);
                        if (vedio != null)
                            _unitOfWork.PostVedio.Delete(vedio);
                    }
                    break;
                case PostsTypes.Question:
                    foreach (var item in postmodel.DeletedPhotoIds)
                    {
                        var photo = await _unitOfWork.QuestionPhoto.FindAsync(p => p.Id == item);
                        if (photo != null)
                            _unitOfWork.QuestionPhoto.Delete(photo);
                    }
                    foreach (var item in postmodel.DeletedVedioIds)
                    {
                        var vedio = await _unitOfWork.QuestionVedio.FindAsync(p => p.Id == item);
                        if (vedio != null)
                            _unitOfWork.QuestionVedio.Delete(vedio);
                    }
                    break;
                default: break;


            }
            _unitOfWork.Complete();
        }

    }
}
