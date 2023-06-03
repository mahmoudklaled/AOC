using System.ComponentModel.DataAnnotations;

namespace GAMAX.Services.Models
{
    public class TokenCode
    {
        [Key]
        public string Token { get; set; }
        public string Code { get; set; }
    }
}
