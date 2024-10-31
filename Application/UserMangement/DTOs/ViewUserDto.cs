using System.ComponentModel.DataAnnotations;

namespace Application.UserMangement.DTOs
{
    public class ViewUserDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string? ApiKey { get; set; }  
        public string? UserName { get; set; }  
        public string? Email { get; set; }  
    }
}
