using Domain;
using Domain.Repositories;
using Infrastructure.Repositories;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BitcubeDevTaskDbContext _context;
        private IUserRepository _userRepository;
        private IProductRepository _productRepository;

        public UnitOfWork(BitcubeDevTaskDbContext context)
        {
            _context = context;
        }

        public IUserRepository UserRepository =>
            _userRepository ??= new UserRepository(_context);

        public IProductRepository ProductRepository =>
            _productRepository ??= new ProductRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
