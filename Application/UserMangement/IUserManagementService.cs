using Application.UserMangement.DTOs;

namespace Application.UserMangement
{
    public interface IUserManagementService
    {
        Task<CreateUserResultDto> Create(UserCreateDto userCreateDto);
        CreateUserResultDto GetUserDetailsByUserId(string userId);
    }
}
