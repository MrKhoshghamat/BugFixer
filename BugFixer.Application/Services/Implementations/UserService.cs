using BugFixer.Application.Generators;
using BugFixer.Application.Security;
using BugFixer.Application.Services.Interfaces;
using BugFixer.Application.Statics;
using BugFixer.Domain.Entities.Account;
using BugFixer.Domain.Interfaces;
using BugFixer.Domain.ViewModels.Account;
using Ganss.XSS;

namespace BugFixer.Application.Services.Implementations
{
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

        public async Task<RegisterResult> RegisterUserAsync(RegisterViewModel register)
        {
            // Check Email Exist
            if (await _userRepository.IsExistUserByEmailAsync(register.Email.SanitizeText().Trim().ToLower()))
            {
                return RegisterResult.EmailExist;
            }

            // Hash Password
            var password = PasswordHelper.EncodePasswordMd5(register.Password.SanitizeText());

            // Create User
            var user = new User
            {
                Avatar = PathTools.DefaultUserAvatar,
                Email = register.Email.SanitizeText().Trim().ToLower(),
                Password = password,
                EmailActivationCode = CodeGenerator.CreateActivationCode()
            };

            // Send Activation Email
            #region Send Activation Email

            // TODO Send Email Activation Code

            #endregion

            // Add To Database
            await _userRepository.CreateUserAsync(user);
            await _userRepository.SaveAsync();

            return RegisterResult.Success;
        }

        #endregion

        #region Login

        public async Task<LoginResult> CheckUserForLoginAsync(LoginViewModel login)
        {
            var user = await _userRepository.GetUserByEmailAsync(login.Email.SanitizeText().Trim().ToLower());

            if (user == null) return LoginResult.UserNotFound;

            var hashedPassword = PasswordHelper.EncodePasswordMd5(login.Password.SanitizeText());
            if (hashedPassword != user.Password)
            {
                return LoginResult.UserNotFound;
            }

            if (user.IsDeleted) return LoginResult.UserNotFound;

            if (user.IsBanned) return LoginResult.UserIsBanned;

            if (!user.IsEmailConfirmed) return LoginResult.EmailNotActivated;

            return LoginResult.Success;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
        }

        #endregion

        #region EmailActivation

        public async Task<bool> ActivateUserEmail(string activationCode)
        {
            var user = await _userRepository.GetUserByActivationCodeAsync(activationCode);

            if (user == null) return false;
            if (user.IsBanned || user.IsDeleted) return false;

            user.IsEmailConfirmed = true;
            user.EmailActivationCode = CodeGenerator.CreateActivationCode();

            await _userRepository.UpdateUserAsync(user);
            await _userRepository.SaveAsync();

            return true;
        }

        #endregion
    }
}
