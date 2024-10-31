using Domain.Entities;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetByIdAsync(int userId);
        public ApplicationUser Add(ApplicationUser user); 
        Task UpdateAsync(ApplicationUser user);
        Task DeleteAsync(int userId);
    }
}
