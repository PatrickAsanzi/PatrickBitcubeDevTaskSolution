using Application.UserMangement.DTOs;
using Domain.Entities;

namespace Domain.Services
{
    public interface IUserManagementService
    {
        Task<CreateUserResultDto> Create(UserCreateDto userCreateDto);
        Task<IEnumerable<Product>> GetAllProductsAsync(); 
        Task<ViewProductDto> AddProductAsync(CreateProductDto createProductDto, string userId);
        Task<ViewProductDto> UpdateProductAsync(string productId, CreateProductDto createProductDto, string userId);
        Task DeleteProductAsync(string productId, string userId); 
    }
}
