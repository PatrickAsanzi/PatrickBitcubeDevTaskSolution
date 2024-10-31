using Application.UserMangement.DTOs;
using Domain;
using Domain.Entities;
using Domain.Repositories;
using Domain.Services;

namespace Application.UserMangement
{
    public class UserManagementService: IUserManagementService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserManagementService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public ViewUserDto Create(UserCreateDto userCreateDto)
        {
            var user = new ApplicationUser(userCreateDto.FirstName, userCreateDto.LastName);
            var result = _unitOfWork.UserRepository.Add(user);
            _unitOfWork.Commit();
            return ViewUserDtoMapper.ToViewUserDto(result);

        }
    }
}
