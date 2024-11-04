using Microsoft.AspNetCore.Identity;

namespace Application.UserMangement.DTOs
{
    public class CreateUserResultDto
    {
        public bool Succeeded { get; init; }
        public string? ApiKey { get; init; }  
        public string? Email { get; init; }
        public string? UserId { get; init; }
        public IEnumerable<IdentityError> Errors { get; set; }
    }
}
