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
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDto userCreateDto)
        {
             var result = await _userManagementService.Create(userCreateDto);

            if (result.Succeeded)
            {
                return Ok(new CreateUserResultDto() { ApiKey = result.ApiKey, Email = result.Email, UserName = result.UserName, Succeeded = result.Succeeded}); 
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description); 
            }
            return BadRequest(ModelState);
        }
    }
}
