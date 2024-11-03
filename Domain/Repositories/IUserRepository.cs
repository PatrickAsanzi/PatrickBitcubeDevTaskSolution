using Domain.Entities;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetByIdAsync(string userId);
        Task DeleteAsync(int userId);
    }
}
