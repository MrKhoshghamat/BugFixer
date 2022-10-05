using BugFixer.Domain.Entities.Account;
using BugFixer.Domain.Interfeces;
using BugFixer.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace BugFixer.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    #region Ctor

    private readonly BugFixerDbContext _context;

    public UserRepository(BugFixerDbContext context)
    {
        _context = context;
    }

    #endregion

    public async Task<bool> IsExistUserByEmail(string email)
    {
        return await _context.Users.AnyAsync(s=> s.Email.Equals(email));
    }

    public async Task CreateUser(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}