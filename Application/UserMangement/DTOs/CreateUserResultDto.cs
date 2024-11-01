using Microsoft.AspNetCore.Identity;

namespace Application.UserMangement.DTOs
{
    public class CreateUserResultDto
    {
        public bool Succeeded { get; set; }
        public string? ApiKey { get; set; }  
        public string? UserName { get; set; }  
        public string? Email { get; set; }

        public IEnumerable<IdentityError> Errors { get; set; }
    }
}
