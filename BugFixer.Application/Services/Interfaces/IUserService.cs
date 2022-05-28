using BugFixer.Domain.Entities.Account;
using BugFixer.Domain.ViewModels.Account;

namespace BugFixer.Application.Services.Interfaces
{
    public interface IUserService
    {
        #region Register

        Task<RegisterResult> RegisterUserAsync(RegisterViewModel register);

        #endregion

        #region Login

        Task<LoginResult> CheckUserForLoginAsync(LoginViewModel login);
        Task<User> GetUserByEmailAsync(string email);

        #endregion
    }
}
