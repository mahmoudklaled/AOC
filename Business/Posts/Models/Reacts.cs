using Business.Accounts.Models;
using Business.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Posts.Models
{
    public class Reacts
    {
        [Key]
        public Guid Id { get; set; }
        public string?  ProfileAccountId { get; set; }
        public ReactsType reacts { get; set; }
        public Guid? PostID { get; set; }
        public Guid? QuestionID { get; set; }
        public Guid? CommentID { get; set; }

        // Navigation property
        public ProfileAccounts? ProfileAccount { get; set; }
        public Post? Post { get; set; }
        public QuestionPost? QuestionPost { get; set; }
        public Comment? Comment { get; set; }


    }
}
