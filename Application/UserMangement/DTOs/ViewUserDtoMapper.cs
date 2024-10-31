using Domain.Entities;

namespace Application.UserMangement.DTOs
{
    public static class ViewUserDtoMapper
    {
        public static ViewUserDto ToViewUserDto(ApplicationUser user)
        {
            if (user == null)
            {
                return null; // or throw an exception if preferred
            }

            return new ViewUserDto
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ApiKey = user.ApiKey,
                UserName = user.UserName,
                Email = user.Email,
            };
        }
    }
}
