using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class LoginDTO
    {
        [Required]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
