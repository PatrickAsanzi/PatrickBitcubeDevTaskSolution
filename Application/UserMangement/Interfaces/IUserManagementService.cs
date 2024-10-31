using Application.UserMangement.DTOs;

namespace Domain.Services
{
    public interface IUserManagementService
    {
        public Task Create(UserCreateDto userCreateDto); 
    }
}
