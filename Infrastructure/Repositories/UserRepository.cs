using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BitcubeDevTaskDbContext _context;

        public UserRepository(BitcubeDevTaskDbContext context)
        {
            _context = context;
        }

        public async Task<ApplicationUser> GetByIdAsync(string userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task DeleteAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
        }

        public ApplicationUser GetByAPIKey(string apiKey)
        {
            return _context.Users.Where(x => x.ApiKey == apiKey).FirstOrDefault();
        }
    }
}