using Domain.Entities;

namespace Application.UserMangement.DTOs
{
    public static class ViewUserDtoMapper
    {
        public static CreateUserResultDto ToViewUserDto(ApplicationUser user)
        {
            if (user == null)
            {
                return null; 
            }

            return new CreateUserResultDto
            {
                ApiKey = user.ApiKey,
                Email = user.Email,
                UserId = user.Id,
            };
        }
    }
}
