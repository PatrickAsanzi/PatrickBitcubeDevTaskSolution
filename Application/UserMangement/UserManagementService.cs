using Application.UserMangement.DTOs;
using Domain.Entities;
using Domain.Repositories;
using Domain.Services;

namespace Application.UserMangement
{
    public class UserManagementService: IUserManagementService
    {
        private readonly IUserRepository _userRepository;
        public UserManagementService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Create(UserCreateDto userCreateDto)
        {
            var user = new ApplicationUser(userCreateDto.FirstName, userCreateDto.LastName);
            await _userRepository.AddAsync(user);
        }
    }
}
