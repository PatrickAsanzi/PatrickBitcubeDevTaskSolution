using Application.UserMangement.DTOs;

namespace Domain.Services
{
    public interface IUserManagementService
    {
        public Task<CreateUserResultDto> Create(UserCreateDto userCreateDto); 
    }
}
