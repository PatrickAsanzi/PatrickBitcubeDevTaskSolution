using Application.CheckoutProcess;
using Application.CheckoutProcess.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CheckoutProcessController : ControllerBase
    {
        private readonly ICheckoutProcessService _checkoutProcessService;

        public CheckoutProcessController(ICheckoutProcessService checkoutProcessService)
        {
            _checkoutProcessService = checkoutProcessService;

        }

        [Authorize]
        [HttpPost("add-product")]
        public async Task<IActionResult> AddProductToCheckoutAsyn([FromBody] AddCheckoutProductDto addCheckoutProductDto)
        {
            try
            {
                await _checkoutProcessService.AddProductToCheckoutAsync(addCheckoutProductDto.ApiKey, addCheckoutProductDto.ProductId, addCheckoutProductDto.Quantity);
                return Ok(new { message = "Product added to checkout successfully." });
            }
            catch (ArgumentException ex) when (ex.ParamName == nameof(addCheckoutProductDto.ProductId))
            {
                return BadRequest(new { message = "The specified product could not be found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("current-basket")]
        public async Task<IActionResult> GetCurrentBasketAsync([FromQuery] string apiKey)
        {
            try
            {
                var checkoutItemDto = await _checkoutProcessService.GetCurrentBasketAsync(apiKey);
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

        [Authorize]
        [HttpPost("complete")]
        public async Task<IActionResult> CompleteCheckout([FromQuery] string apiKey)
        {
            var result = await _checkoutProcessService.CompleteCheckoutAsync(apiKey);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("remove-product")]
        public async Task<IActionResult> RemoveProductFromCheckoutAsync([FromBody] AddCheckoutProductDto addCheckoutProductDto)
        {
            try
            {
                await _checkoutProcessService.RemoveProductFromCheckoutAsync(addCheckoutProductDto.ApiKey, addCheckoutProductDto.ProductId, addCheckoutProductDto.Quantity);
                return NoContent(); // 204 No Content
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message); // 404 Not Found
            }
        }
    }
}
