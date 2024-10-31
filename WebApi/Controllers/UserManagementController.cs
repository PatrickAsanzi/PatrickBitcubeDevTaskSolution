using Application.UserMangement.DTOs;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserManagementController : ControllerBase
    {
        private IUserManagementService _userManagementService;
        public UserManagementController(IUserManagementService userManagementService) 
        { 
            _userManagementService = userManagementService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello World");
        }

        [HttpPost]
        public ActionResult UpdateBenchmark([FromBody] UserCreateDto userCreateDto)
        {
            var user = _userManagementService.Create(userCreateDto);
            return Ok(user);
        }
    }
}
