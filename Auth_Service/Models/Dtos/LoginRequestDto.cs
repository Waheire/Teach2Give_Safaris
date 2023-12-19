using System.ComponentModel.DataAnnotations;

namespace Auth_Service.Models.Dtos
{
    public class LoginRequestDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
