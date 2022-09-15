using BugFixer.Domain.Interfeces;
using BugFixer.Persistence.Context;

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
}