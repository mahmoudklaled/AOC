﻿using Business.Authentication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Enums;

namespace Business.Accounts.Models
{
    public class ProfileAccounts
    {
        [Key]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public ProfileTypes Type { get; set; }
        public string CoverPhoto { get; set; }
        public string ProfilePohot { get; set; }

    }
}
