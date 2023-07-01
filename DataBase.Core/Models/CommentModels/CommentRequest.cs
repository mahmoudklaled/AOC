using DataBase.Core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Core.Models.CommentModels
{
    public class CommentRequest
    {
        public Guid Id { get; set; }
        public string comment { get; set; }
        public Guid PostId { get; set; }
        public PostsTypes PostsType { get; set; }
        public IFormFile? Photo { get; set; }
        public IFormFile? Vedio { get; set; }

    }
}
