using Domain.Entities;
using Domain.Repositories;
using System.Reflection.Metadata.Ecma335;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BitcubeDevTaskDbContext _context;

        public UserRepository(BitcubeDevTaskDbContext context)
        {
            _context = context;
        }

        public async Task<ApplicationUser> GetByIdAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public ApplicationUser Add(ApplicationUser user)
        {
           var result = _context.Users.Add(user);
            return result.Entity; 
        }

        public async Task UpdateAsync(ApplicationUser user)
        {
            _context.Users.Update(user);
           
        }

        public async Task DeleteAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
        }
    }
}