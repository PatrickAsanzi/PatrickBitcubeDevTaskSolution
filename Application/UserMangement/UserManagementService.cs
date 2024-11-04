using Application.UserMangement.DTOs;
using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.UserMangement
{
    public class UserManagementService : IUserManagementService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserManagementService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<CreateUserResultDto> Create(UserCreateDto userCreateDto)
        {
            var user = new ApplicationUser(userCreateDto.FirstName, userCreateDto.LastName, userCreateDto?.Email);

            var result = await _userManager.CreateAsync(user, userCreateDto.Password);
            await _unitOfWork.CommitAsync();

            if (result.Succeeded)
            {
                return new CreateUserResultDto
                {
                    Succeeded = true,
                    ApiKey = user.ApiKey,
                    Email = user.Email, 
                    UserId = user.Id,
                };
            }
            return new CreateUserResultDto
            {
                Succeeded = false,
                Errors = result.Errors
            };
        }

        public CreateUserResultDto GetUserDetailsByUserId(string userId)
        {
            var user = _unitOfWork.UserRepository.GetByIdAsync(userId).Result;

            if (user == null)
            {
                throw new ArgumentException("User Could not be found");
            }

            return ViewUserDtoMapper.ToViewUserDto(user);

        }
    }
}
