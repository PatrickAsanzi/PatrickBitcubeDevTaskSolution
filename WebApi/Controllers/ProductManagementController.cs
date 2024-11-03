using Application.ProductManagement.DTOs;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductManagementController : ControllerBase
    {
        private readonly IProductManagementService _productManagementService;
        public ProductManagementController(IProductManagementService productManagementService)
        {
            {
                _productManagementService = productManagementService;
            }
        }

        [Authorize]
        [HttpPost("addProduct")]
        public async Task<IActionResult> AddProduct([FromBody] AddCheckoutProductDto addCheckoutProductDto)
        {
            await _productManagementService.AddProductToCheckoutAsync(addCheckoutProductDto.ApiKey, addCheckoutProductDto.ProductId, addCheckoutProductDto.Quantity);
            return Ok();
        }

        [Authorize]
        [HttpPost("complete")]
        public async Task<IActionResult> CompleteCheckout([FromQuery] string apiKey)
        {
            var result = await _productManagementService.CompleteCheckoutAsync(apiKey);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("removeProductFromCheckout")]
        public async Task<IActionResult> RemoveProductFromCheckout([FromBody] AddCheckoutProductDto addCheckoutProductDto)
        {
            try
            {
                await _productManagementService.RemoveProductFromCheckoutAsync(addCheckoutProductDto.ApiKey, addCheckoutProductDto.ProductId, addCheckoutProductDto.Quantity);
                return NoContent(); // 204 No Content
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message); // 404 Not Found
            }
        }

        [HttpGet("checkout")]
        public async Task<IActionResult> ViewCheckoutItem(string apiKey)
        {
            try
            {
                var checkoutItemDto = await _productManagementService.GetCheckoutItemAsync(apiKey);
                return Ok(checkoutItemDto);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message); // Return 404 if checkout item not found
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving the checkout item."); // Handle other exceptions
            }
        }
    }
}

