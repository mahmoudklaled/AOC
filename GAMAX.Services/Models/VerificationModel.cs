using System.ComponentModel.DataAnnotations;

namespace GAMAX.Services.Models
{
    public class VerificationModel
    {
        [EmailAddress]
        public string Email { get; set; }
        public string VerificationCode { get; set; }
    }

}
