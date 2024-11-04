using Application.ProductManagement;
using Application.ProductManagement.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [HttpPost("create")]
        public async Task<IActionResult> CreateProductAsync([FromBody] CreateProductDto createProductDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _productManagementService.CreateProductAsync(createProductDto, userId);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("get-all/{userId}")]
        public IActionResult GetAllUserProducts(string userId)
        {
            var products = _productManagementService.GetAllUserProducts(userId);
            return Ok(products);
        }

        [Authorize]
        [HttpGet("get-all")]
        public IActionResult GetAllProducts()
        {
            var products = _productManagementService.GetAllProducts();
            return Ok(products);
        }

        [Authorize]
        [HttpPut("update/{productId}")]
        public async Task<IActionResult> UpdateProductAsync(string productId, [FromBody] CreateProductDto updateProductDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            try
            {
                var result = await _productManagementService.UpdateProductAsync(productId, updateProductDto, userId);
                return Ok(result); 
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message); 
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("delete/{productId}")]
        public async Task<IActionResult> DeleteProductAsync(string productId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            try
            {
                await _productManagementService.DeleteProductAsync(productId, userId);
                return NoContent(); 
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message); 
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message); 
            }
        }
    }
}

