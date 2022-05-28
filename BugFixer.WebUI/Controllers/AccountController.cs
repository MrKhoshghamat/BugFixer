using System.Security.Claims;
using BugFixer.Application.Services.Interfaces;
using BugFixer.Domain.ViewModels.Account;
using BugFixer.WebUI.ActionFilters;
using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace BugFixer.Web.Controllers
{
    public class AccountController : BaseController
    {
        #region Ctor

        private readonly IUserService _userService;
        private readonly ICaptchaValidator _captchaValidator;
        public AccountController(IUserService userService, ICaptchaValidator captchaValidator)
        {
            _userService = userService;
            _captchaValidator = captchaValidator;
        }

        #endregion

        #region Login

        [HttpGet("login")]
        [RedirectHomeIfLoggedInActionFilter]
        public IActionResult Login(string ReturnUrl = "")
        {
            var result = new LoginViewModel();
            if (!string.IsNullOrEmpty(ReturnUrl))
            {
                result.ReturnUrl = ReturnUrl;
            }
            return View();
        }

        [HttpPost("login"), ValidateAntiForgeryToken]
        [RedirectHomeIfLoggedInActionFilter]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (!await _captchaValidator.IsCaptchaPassedAsync(login.Captcha))
            {
                TempData[ErrorMessage] = "اعتبارسنجی Captcha با مشکل مواجه شد. لطفا مجددا تلاش کنید.";
                return View(login);
            }

            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var result = await _userService.CheckUserForLoginAsync(login);
            switch (result)
            {
                case LoginResult.UserIsBanned:
                    TempData[ErrorMessage] = "دسترسی شما به سایت محدود میباشد.";
                    break;
                case LoginResult.UserNotFound:
                    TempData[ErrorMessage] = "کاربر مورد نظر یافت نشد.";
                    break;
                case LoginResult.EmailNotActivated:
                    TempData[ErrorMessage] = "برای ورود به حساب کاربری ابتدا ایمیل خود را فعال کنید.";
                    break;
                case LoginResult.Success:
                    var user = await _userService.GetUserByEmailAsync(login.Email);

                    #region Login User

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    var properties = new AuthenticationProperties { IsPersistent = login.RememberMe };
                    await HttpContext.SignInAsync(principal,properties);

                    #endregion

                    TempData[SuccessMessage] = "خوش آمدید";

                    if (!string.IsNullOrEmpty(login.ReturnUrl))
                    {
                        return Redirect(login.ReturnUrl);
                    }

                    return Redirect("/");
            }

            return View(login);
        }

        #endregion

        #region Register

        [HttpGet("register")]
        [RedirectHomeIfLoggedInActionFilter]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("register"), ValidateAntiForgeryToken]
        [RedirectHomeIfLoggedInActionFilter]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if (!await _captchaValidator.IsCaptchaPassedAsync(register.Captcha))
            {
                TempData[ErrorMessage] = "اعتبارسنجی Captcha با مشکل مواجه شد. لطفا مجددا تلاش کنید.";
                return View(register);
            }

            if (!ModelState.IsValid)
            {
                return View(register);
            }

            var result = await _userService.RegisterUserAsync(register);
            
            switch (result)
            {
                case RegisterResult.EmailExist:
                    TempData[ErrorMessage] = "ایمیل وارده شده موجود میباشد";
                    break;
                case RegisterResult.Success:
                    TempData[SuccessMessage] = "عملیات با موفقیت انجام شد";
                    return RedirectToAction("Login","Account");
            }

            return View(register);
        }
        #endregion

        #region Logout

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }

        #endregion

        #region Email Activation

        [HttpGet("activate-email/{activationCode}")]
        [RedirectHomeIfLoggedInActionFilter]
        public async Task<IActionResult> ActivationUserEmail(string activationCode)
        {
            var result = await _userService.ActivateUserEmail(activationCode);
            if (result)
            {
                TempData[SuccessMessage] = "حساب کاربری شما با موفقیت فعال شد";
            }
            else
            {
                TempData[ErrorMessage] = "فعالسازی حساب کاربری با خطا مواجه شد";
            }

            return RedirectToAction("Login", "Account");
        }

        #endregion
    }
}
