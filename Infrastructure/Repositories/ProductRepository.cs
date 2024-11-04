using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly BitcubeDevTaskDbContext _context;
        public ProductRepository(BitcubeDevTaskDbContext context)
        {
            _context = context;
        }

        public async Task<Product> GetByIdAsync(string productId)
        {
            return await _context.Products.FindAsync(productId);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }

        public void Remove(Product product)
        {
            _context.Products.Remove(product);
        }

        public async Task<IEnumerable<Product>> GetProductsByUserIdAsync(string userId)
        {
            return await _context.Products
                .Where(p => p.UserId == userId)
                .ToListAsync();
        }

        public async Task<bool> ProductNameExistsAsync(string name)
        {
            return await _context.Products
                .AnyAsync(p => p.Name == name);
        }

        public void Add(CheckoutItem checkoutItem)
        {
            _context.CheckoutItems.Add(checkoutItem);
        }

        public async Task<CheckoutItem?> GetByApiKeyAsync(string apiKey, bool IsComplete)
        {
             return await _context.CheckoutItems
                  .Include(i => i.CheckoutItemProducts)
                  .ThenInclude (i => i.Product)
                  .FirstOrDefaultAsync(i => i.ApiKey == apiKey && i.IsComplete == IsComplete);
        }
    }
}
