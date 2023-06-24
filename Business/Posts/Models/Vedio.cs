using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Posts.Models
{
    public class Vedio
    {
        [Key]
        public Guid Id { get; set; }
        public string VedioPath { get; set; }

        // Foreign key for Post
        public Guid? PostId { get; set; }
        public Post Post { get; set; }

        // Foreign key for QuestionPost
        public Guid? QuestionPostId { get; set; }
        public QuestionPost QuestionPost { get; set; }

        public Guid? CommentId { get; set; }
        public Comment Comment { get; set; }
    }
}
