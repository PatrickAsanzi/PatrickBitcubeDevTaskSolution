using Application.UserMangement.DTOs;
using Domain.Entities;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


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
                return Ok(new CreateUserResultDto() { ApiKey = result.ApiKey, Email = result.Email, UserName = result.UserName, Succeeded = result.Succeeded });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }
            return BadRequest(ModelState);
        }
        [Authorize]
        [HttpPost("addProductToUser")]
        public async Task<IActionResult> AddProduct([FromBody] CreateProductDto createProductDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           var result = await _userManagementService.AddProductAsync(createProductDto, userId);
            return Ok(result);
        }
        [Authorize]
        [HttpPut("updateProduct/{productId}")]
        public async Task<IActionResult> UpdateProduct([FromQuery] string productId, [FromBody] CreateProductDto updateProductDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            try
            {
                var result = await _userManagementService.UpdateProductAsync(productId, updateProductDto, userId);
                return Ok(result); // Return the updated product details
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message); // 404 Not Found
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message); // 403 Forbidden
            }
        }

        [Authorize]
        [HttpDelete("deleteProduct/{productId}")]
        public async Task<IActionResult> DeleteProduct([FromQuery] string productId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            try
            {
                await _userManagementService.DeleteProductAsync(productId, userId);
                return NoContent(); // 204 No Content
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message); // 404 Not Found
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message); // 403 Forbidden
            }
        }

        [Authorize]
        [HttpGet("products")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _userManagementService.GetAllProductsAsync();
            return Ok(products); // Returns 200 OK with the list of products
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginInputModel loginInputModel)
        {

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginInputModel.UserName, loginInputModel.Password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {

                    return Ok(new { message = "Login successfull"});
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
    }
}
