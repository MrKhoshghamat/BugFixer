using BugFixer.Application.Generators;
using BugFixer.Application.Security;
using BugFixer.Application.Services.Interfaces;
using BugFixer.Application.Statics;
using BugFixer.Domain.Entities.Account;
using BugFixer.Domain.Interfeces;
using BugFixer.Domain.ViewModels.Account;

namespace BugFixer.Application.Services.Implementations;

public class UserService : IUserService
{
    #region Ctor

    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    #endregion

    #region Register

    public async Task<RegisterResult> RegisterUser(RegisterViewModel registerViewModel)
    {
        if (await _userRepository.IsExistUserByEmail(registerViewModel.Email.Trim().ToLower()))
            return RegisterResult.EmailExisted;

        var password = PasswordHelper.EncodePasswordMd5(registerViewModel.Password);

        var user = new User()
        {
            Avatar = PathTools.DefaultUserAvatar,
            Email = registerViewModel.Email.Trim().ToLower(),
            Password = password,
            EmailActivationCode = CodeGenerator.CreateActivationCode()
        };

        await _userRepository.CreateUser(user);
        await _userRepository.Save();

        return RegisterResult.Success;
    }

    #endregion
}