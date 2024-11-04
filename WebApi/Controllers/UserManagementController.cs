using Application.UserMangement;
using Application.UserMangement.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserManagementController : ControllerBase
    {
        private readonly IUserManagementService _userManagementService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UserManagementController(IUserManagementService userManagementService, SignInManager<ApplicationUser> signInManager)
        {
            _userManagementService = userManagementService;
            _signInManager = signInManager;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDto userCreateDto)
        {
            var result = await _userManagementService.Create(userCreateDto);

            if (result.Succeeded)
            {
                return Ok(result);
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }
            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpGet("view/{userId}")]
        public IActionResult GetUserDetails(string userId)
        {
            var userDetails = _userManagementService.GetUserDetailsByUserId(userId);

            if (userDetails == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            return Ok(userDetails);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginInputModel loginInputModel)
        {

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginInputModel.UserEmail, loginInputModel.Password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {

                    return Ok(new { message = "Login successfull" });
                }
                if (result.IsLockedOut)
                {
                    return BadRequest(new { message = "Access denied." });
                }
                else
                {
                    return BadRequest(new { message = "Invalid login attempt." });
                }
            }

            return BadRequest(new { message = "Login was not successfull, try again" });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new { message = "Logout successful" });
        }
    }
}
