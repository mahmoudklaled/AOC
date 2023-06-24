using Business.Enums;
using Business.Posts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Posts.Services
{
    public interface IReacts
    {
        Task<bool> DeleteReactAsync(Guid reactId, string userEmail);
        Task<bool> AddReactAsync(ReactRequest reactRequest, string userEmail);
        Task<List<ReactResponse>> GetAllReacts(ReactRequest reactRequest);
        Task<bool> UpdateReact(Guid Id , string userEmail , ReactsType reactType);

    }
}
