using BugFixer.Domain.Entities.Account;

namespace BugFixer.Domain.Interfeces;

public interface IUserRepository
{
    Task<bool> IsExistUserByEmail(string email);
    
    Task CreateUser(User user);
    
    Task Save();
}