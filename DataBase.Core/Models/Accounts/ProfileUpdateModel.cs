using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Core.Models.Accounts
{
    public class ProfileUpdateModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string City { get; set; } 
        public string Country { get; set; } 
        //public string CoverPhoto { get; set; }
        //public string ProfilePohot { get; set; }
    }
}
