using DataBase.Core.Enums;
using DataBase.Core.Models.PhotoModels;
using DataBase.Core.Models.Posts;
using System.ComponentModel.DataAnnotations;


namespace BDataBase.Core.Models.Accounts
{
    public class ProfileAccounts
    {
        [Key]
        public string Id { get; set; }
        [Key]
        public string Email { get; set; }
        //[Key]
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public string City { get; set; } = null;
        public string Country { get; set; } = null;
        public ProfileTypes Type { get; set; }
        public CoverPhoto CoverPhoto { get; set; }
        public ProfilePhoto ProfilePohot { get; set; }
        public ICollection<Post> Posts { get; set; } // Represents the posts added by the user
        public ICollection<QuestionPost> QuestionPosts { get; set; } // Represents the question posts added by the user
        

    }
}
