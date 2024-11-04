using Application.ProductManagement.DTOs;

namespace Application.ProductManagement
{
    public interface IProductManagementService
    {
        Task<ViewCreateProductDto> CreateProductAsync(CreateProductDto createProductDto, string userId);
        IEnumerable<ViewCreateProductDto> GetAllUserProducts(string userId);
        IEnumerable<ViewProductDto> GetAllProducts();
        Task<ViewCreateProductDto> UpdateProductAsync(string productId, CreateProductDto createProductDto, string userId);
        Task DeleteProductAsync(string productId, string userId);
       
    }
}
