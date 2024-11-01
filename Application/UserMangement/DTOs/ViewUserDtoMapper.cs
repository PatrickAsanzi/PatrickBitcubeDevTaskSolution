using Domain.Entities;

namespace Application.UserMangement.DTOs
{
    public static class ViewUserDtoMapper
    {
        public static CreateUserResultDto ToViewUserDto(ApplicationUser user)
        {
            if (user == null)
            {
                return null; // or throw an exception if preferred
            }

            return new CreateUserResultDto
            {
                ApiKey = user.ApiKey,
                UserName = user.UserName,
                Email = user.Email,
            };
        }
    }
}
