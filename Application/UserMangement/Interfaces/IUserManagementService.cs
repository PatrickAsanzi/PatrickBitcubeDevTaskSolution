using Application.UserMangement.DTOs;

namespace Domain.Services
{
    public interface IUserManagementService
    {
        public ViewUserDto Create(UserCreateDto userCreateDto); 
    }
}
