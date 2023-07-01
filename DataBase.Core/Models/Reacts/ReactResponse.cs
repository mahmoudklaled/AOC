using DataBase.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Core.Models.Reacts
{
    public class ReactResponse
    {
        public Guid Id { get; set; }
        public ReactsType Type { get; set; }
    }
}
