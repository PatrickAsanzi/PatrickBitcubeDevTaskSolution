using Domain.Entities;

namespace Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(string productId);
        Task<IEnumerable<Product>> GetAllAsync();
        Task AddAsync(Product product);
        void Update(Product product);
        void Remove(Product product);
        Task<IEnumerable<Product>> GetProductsByUserIdAsync(string userId);
        void Add(CheckoutItem checkoutItem);
        Task<CheckoutItem> GetByApiKeyAsync(string apiKey, bool IsComplete);
        Task<bool> ProductNameExistsAsync(string name); 
    }
}
