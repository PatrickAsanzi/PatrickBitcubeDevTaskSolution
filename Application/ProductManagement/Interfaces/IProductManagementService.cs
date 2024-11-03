using Application.ProductManagement.DTOs;

namespace Domain.Services
{
    public interface IProductManagementService
    {
        Task AddProductToCheckoutAsync(string apiKey, string productId, int quanity);
        Task<CheckoutItemDto> CompleteCheckoutAsync(string apiKey);
        Task<CheckoutItemDto> GetCheckoutItemAsync(string apiKey); 
        Task RemoveProductFromCheckoutAsync(string apiKey, string productId, int qunatity);
    }
}
