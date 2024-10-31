using Application.UserMangement.DTOs;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserManagementController : ControllerBase
    {
        private readonly IUserManagementService _userManagementService;
        public UserManagementController(IUserManagementService userManagementService) 
        { 
            _userManagementService = userManagementService;
        }

        [HttpPost("CreateUser")]
        public ActionResult<ViewUserDto> CreateUser([FromBody] UserCreateDto userCreateDto)
        {
            var user = _userManagementService.Create(userCreateDto);

            if (user == null) // Check if user creation failed
            {
                return BadRequest("User creation failed.");
            }
            return Ok(user);
        }
    }
}
