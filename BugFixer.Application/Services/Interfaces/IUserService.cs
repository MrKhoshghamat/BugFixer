using BugFixer.Domain.ViewModels.Account;

namespace BugFixer.Application.Services.Interfaces;

public interface IUserService
{
    Task<RegisterResult> RegisterUser(RegisterViewModel registerViewModel);
}