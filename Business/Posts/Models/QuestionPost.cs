using Business.Accounts.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Posts.Models
{
    public class QuestionPost
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public DateTime TimeCreated { get; set; } = DateTime.UtcNow;
        public ICollection<Photo> Photos { get; set; }
        public ICollection<Vedio> Vedios { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Reacts> Reacts { get; set; }

        //Forign-key
        public ProfileAccounts ProfileAccount { get; set; }
        public string ProfileAccountId { get; set; }
    }
}
