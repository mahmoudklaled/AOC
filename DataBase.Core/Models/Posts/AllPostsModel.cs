﻿using DataBase.Core.Enums;
using DataBase.Core.Models.PhotoModels;
using DataBase.Core.Models.VedioModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Core.Models.Posts
{
    public class AllPostsModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime TimeCreated { get; set; }
        public List<IFormFile> Photos { get; set; }
        public List<IFormFile> Vedios { get; set; }
        public List<BasePhoto> Photo { get; set; }
        public List<BaseVedio> Vedio { get; set; }
        public PostsTypes Type { get; set; }
        public string Question { get; set; } // Additional property for QuestionPost
        public string Answer { get; set; } // Additional property for QuestionPost

        
        [BindNever]
        public List<string> PhotosPath { get; set; }

        [BindNever]
        public List<string> VediosPath { get; set; }
    }
    public class UploadPost
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<IFormFile> Photos { get; set; }
        public List<IFormFile> Vedios { get; set; }
        public PostsTypes Type { get; set; }
        public string Question { get; set; } // Additional property for QuestionPost
        public string Answer { get; set; } // Additional property for QuestionPost

    }


}