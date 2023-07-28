using DataBase.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Core.Models.Reacts
{
    public class ReactUpdateRequest
    {
        public Guid ReactId { get; set; }
        public Guid ObjectId { get; set; }
        public ReactsType ReactType { get; set; }
    }
    public class AddReactRequest
    {
        public Guid ObjectId { get; set; }
        public ReactsType ReactType { get; set; }
    }
}
