using DataBase.Core.Enums;

namespace DataBase.Core.Models.Accounts
{
    public class ProfileUpdateModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? City { get; set; } 
        public string? Country { get; set; }
        public string? Bio { get; set; }
        public DateTime? Birthdate { get; set; }
        public Gender? gender { get; set; }
    }
}
