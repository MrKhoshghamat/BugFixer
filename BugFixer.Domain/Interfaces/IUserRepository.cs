using BugFixer.Domain.Entities.Account;

namespace BugFixer.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> IsExistUserByEmailAsync(string email);
        Task CreateUserAsync(User user);
        Task<User> GetUserByEmailAsync(string email);
        Task SaveAsync();
    }
}
