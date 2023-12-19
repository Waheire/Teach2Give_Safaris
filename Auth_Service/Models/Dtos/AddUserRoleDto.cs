using System.ComponentModel.DataAnnotations;

namespace Auth_Service.Models.Dtos
{
    public class AddUserRoleDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string RoleName { get; set; } = "User";
    }
}
