using BugFixer.Data.Context;
using BugFixer.Domain.Entities.Account;
using BugFixer.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BugFixer.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        #region Ctor

        private readonly BugFixerDbContext _context;

        public UserRepository(BugFixerDbContext context)
        {
            _context = context;
        }

        #endregion

        public async Task<bool> IsExistUserByEmailAsync(string email)
        {
            return await _context.Users.AnyAsync(e => e.Email == email);
        }

        public async Task CreateUserAsync(User user)
        {
            await _context.AddAsync(user);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(e => e.Email.Equals(email));
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
