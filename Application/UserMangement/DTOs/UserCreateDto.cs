using System.ComponentModel.DataAnnotations;

namespace Application.UserMangement.DTOs
{
    public class UserCreateDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
