using System.ComponentModel.DataAnnotations;
using Business.Enums;
using Business.Posts.Models;

namespace Business.Accounts.Models
{
    public class ProfileAccounts
    {
        [Key]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        [Key]
        public string Email { get; set; }
        public string City { get; set; } = null;
        public string Country { get; set; } = null;
        public ProfileTypes Type { get; set; }
        public string CoverPhoto { get; set; }
        public string ProfilePohot { get; set; }

        public ICollection<Post> Posts { get; set; } // Represents the posts added by the user
        public ICollection<QuestionPost> QuestionPosts { get; set; } // Represents the question posts added by the user
        public ICollection<Comment> Comments { get; set; } // All User Comments
        public ICollection<Reacts> Reacts { get; set; } //All user Reacts

    }
}
