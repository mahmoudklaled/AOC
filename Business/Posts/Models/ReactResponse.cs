using Business.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Posts.Models
{
    public class ReactResponse
    {
        public Guid Id { get; set; }
        public ReactsType Type { get; set; }
    }
}
