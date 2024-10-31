using Domain.Repositories;

namespace Domain
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IProductRepository ProductRepository { get; }
        Task<int> CommitAsync();
        public int Commit(); 
    }
}
