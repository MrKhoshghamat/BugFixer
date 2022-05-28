using BugFixer.Domain.Entities.Account;

namespace BugFixer.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> IsExistUserByEmailAsync(string email);
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByActivationCodeAsync(string activationCode);
        Task SaveAsync();
    }
}
