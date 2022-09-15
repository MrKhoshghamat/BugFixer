using BugFixer.Domain.Entities.Account;
using Microsoft.EntityFrameworkCore;

namespace BugFixer.Persistence.Context;

public class BugFixerDbContext : DbContext
{
    #region Ctor

    public BugFixerDbContext(DbContextOptions<BugFixerDbContext> options) : base(options)
    {
        
    }

    #endregion

    #region DbSet

    public DbSet<User> Users { get; set; }

    #endregion
}