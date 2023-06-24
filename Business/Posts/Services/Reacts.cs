using Business.Enums;
using Business.Posts.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Posts.Services
{
    public class Reacts : IReacts
    {
        private readonly ApplicationDbContext _dbContext;
        public Reacts(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }
        public async Task<bool> AddReactAsync(ReactRequest reactRequest , string userEmail)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
            if (user == null) return false;
            if (reactRequest.IsOnComment == true)
            {
                await _dbContext.Reacts.AddAsync(new Models.Reacts
                {
                    Id = new Guid(),
                    CommentID = reactRequest.commentId
                }) ;
            }
            else if (reactRequest.IsOnPost == true)
            {
                switch (reactRequest.postsType)
                {
                    case PostsTypes.Post:
                        await _dbContext.Reacts.AddAsync(new Models.Reacts
                        {
                            Id = new Guid(),
                            PostID = reactRequest.postId
                        });
                        break;
                    case PostsTypes.Question:
                        await _dbContext.Reacts.AddAsync(new Models.Reacts
                        {
                            Id = new Guid(),
                            PostID = reactRequest.postId
                        });
                        break;
                    default:
                        break;
                }
            }
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteReactAsync(Guid reactId, string userEmail)
        {
            var isAuth = await IsAuthorizedToDoActions(reactId, userEmail);
            if (!isAuth)return false;
            _dbContext.Reacts.Remove(await _dbContext.Reacts.FindAsync(reactId));
            _dbContext.SaveChanges();
            return true;
        }

        public async Task<List<ReactResponse>> GetAllReacts(ReactRequest reactRequest)
        {
            List<Models.Reacts> reacts = new List<Models.Reacts>(); 
            if (reactRequest.IsOnComment == true)
            {
                reacts = await _dbContext.Reacts.Where(r=> r.CommentID == reactRequest.commentId).ToListAsync();
            }
            else if(reactRequest.IsOnPost == true)
            {
                switch (reactRequest.postsType)
                {
                    case PostsTypes.Post:
                        reacts = await _dbContext.Reacts.Where(r => r.PostID == reactRequest.postId).ToListAsync();
                        break;
                    case PostsTypes.Question:
                        reacts = await _dbContext.Reacts.Where(r => r.QuestionID == reactRequest.postId).ToListAsync();
                        break;
                    default:
                        break;
                }
            }
            List<ReactResponse> result = new List<ReactResponse>();
            foreach (var react in reacts)
            {
                result.Add(new ReactResponse
                {
                    Id = react.Id,
                    Type = react.reacts
                });
            }
            return result;
        }

        public async Task<bool> UpdateReact(Guid Id, string userEmail,ReactsType reactType)
        {
            var isAuth = await IsAuthorizedToDoActions(Id, userEmail);
            if (!isAuth) return false;
            var react = await _dbContext.Reacts.FindAsync(Id);
            if (react == null) return false;
            react.reacts = reactType;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        private async Task<bool> IsAuthorizedToDoActions(Guid Id,string email)
        {
            var user = await _dbContext.ProfileAccounts.FirstOrDefaultAsync(p=>p.Email == email);
            if (user == null)return false;
            var react = await _dbContext.Reacts.FirstOrDefaultAsync(p=>p.Id == Id);
            if (react == null) return false;
            if (user.Id == react.ProfileAccountId) return true;
            return false;
        }
    }
}
