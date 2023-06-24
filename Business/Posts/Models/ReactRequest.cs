using Business.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Posts.Models
{
    public class ReactRequest
    {
        public bool IsOnComment { get; set; }
        public bool IsOnPost { get; set; }
        public Guid postId { get; set; }
        public Guid commentId { get; set; }
        public ReactsType ReactType { get; set; }
        public PostsTypes postsType { get; set; }
    }
}
