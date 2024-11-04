using Application.CheckoutProcess.DTOs;

namespace Application.CheckoutProcess
{
    public interface ICheckoutProcessService
    {
        Task AddProductToCheckoutAsync(string apiKey, string productId, int quanity);
        Task<CheckoutItemDto> GetCurrentBasketAsync(string apiKey);
        Task<CheckoutItemDto> CompleteCheckoutAsync(string apiKey);
        Task RemoveProductFromCheckoutAsync(string apiKey, string productId, int qunatity);
    }
}
