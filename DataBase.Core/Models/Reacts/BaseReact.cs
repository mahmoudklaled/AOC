﻿using BDataBase.Core.Models.Accounts;
using DataBase.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Core.Models.Reacts
{
    public class BaseReact
    {
        [Key]
        public Guid Id { get; set; }
        public ReactsType reacts { get; set; }
        public string UserAccountsId { get; set; }
        public UserAccounts UserAccounts { get; set; }
    }
}
