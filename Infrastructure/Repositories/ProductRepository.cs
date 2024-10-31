using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProductRepository: IProductRepository
    {
        private readonly BitcubeDevTaskDbContext _context;
        public ProductRepository(BitcubeDevTaskDbContext context)
        {
            _context = context;
        }
    }
}
