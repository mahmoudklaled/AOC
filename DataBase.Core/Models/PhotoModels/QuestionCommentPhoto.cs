﻿using DataBase.Core.Models.CommentModels;
using DataBase.Core.Models.VedioModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Core.Models.PhotoModels
{
    public class QuestionCommentPhoto:BasePhoto
    {
        public Guid QuestionCommentId { get; set; }
        public QuestionComment QuestionComment { get; set; }
    }
}
