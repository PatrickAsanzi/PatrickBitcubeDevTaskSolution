using Domain.Entities;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetByIdAsync(int userId);
        Task AddAsync(ApplicationUser user);
        Task UpdateAsync(ApplicationUser user);
        Task DeleteAsync(int userId);
    }
}
