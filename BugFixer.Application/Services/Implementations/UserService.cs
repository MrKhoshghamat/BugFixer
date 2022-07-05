using BugFixer.Application.Generators;
using BugFixer.Application.Security;
using BugFixer.Application.Services.Interfaces;
using BugFixer.Application.Statics;
using BugFixer.Domain.Entities.Account;
using BugFixer.Domain.Interfaces;
using BugFixer.Domain.ViewModels.Account;

namespace BugFixer.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        #region Ctor

        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        public UserService(IUserRepository userRepository, IEmailService emailService)
        {
            _userRepository = userRepository;
            _emailService = emailService;
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

            var body = $@"
                <div> برای فعالسازی حساب کاربری خود روی لینک زیر کلیک کنید </div>
                <a href='{PathTools.SiteAddress}/Activate-Email/{user.EmailActivationCode}'>فعالسازی حساب کاربری</a>
                ";

            await _emailService.SendEmail(user.Email, "فعالسازی حساب کاربری", body);

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

        #region Forgot Password

        public async Task<ForgotPasswordResult> ForgotPasswordAsync(ForgotPasswordViewModel forgotPassword)
        {
            var email = forgotPassword.Email.SanitizeText().Trim().ToLower();
            var user = await _userRepository.GetUserByEmailAsync(forgotPassword.Email);

            if (user == null || user.IsDeleted) return ForgotPasswordResult.UserNotFound;
            if (user.IsBanned) return ForgotPasswordResult.UserBanned;

            #region Send Activation Email

            var body = $@"
                <div> برای فراموشی کلمه عبور بر روی لینک زیر کلیک کنید </div>
                <a href='{PathTools.SiteAddress}/Reset-Password/{user.EmailActivationCode}'>فراموشی کلمه عبور</a>
                ";

            await _emailService.SendEmail(user.Email, "فعالسازی حساب کاربری", body);

            #endregion

            return ForgotPasswordResult.Success;
        }

        #endregion

        #region Reset Password

        public async Task<ResetPasswordResult> ResetPasswordAsync(ResetPasswordViewModel resetPassword)
        {
            var user = await _userRepository.GetUserByActivationCodeAsync(
                resetPassword.EmailActivationCode.SanitizeText());

            if (user == null || user.IsDeleted) return ResetPasswordResult.UserNotFound;
            if (user.IsBanned) return ResetPasswordResult.UserIsBanned;

            var password = PasswordHelper.EncodePasswordMd5(resetPassword.Password.SanitizeText());
            user.Password = password;
            user.IsEmailConfirmed = true;
            user.EmailActivationCode = CodeGenerator.CreateActivationCode();

            await _userRepository.UpdateUserAsync(user);
            await _userRepository.SaveAsync();

            return ResetPasswordResult.Success;
        }

        public async Task<User> GetUserByActivationCode(string activationCode)
        {
            return await _userRepository.GetUserByActivationCodeAsync(activationCode.SanitizeText());
        }

        #endregion

        #region User Panel

        public async Task<User?> GetUserByIdAsync(long userId)
        {
            return await _userRepository.GetUserByIdAsync(userId);
        }

        #endregion
    }
}
