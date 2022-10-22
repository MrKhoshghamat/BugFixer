using BugFixer.Application.Services.Interfaces;
using BugFixer.Domain.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;

namespace BugFixer.WebUI.Controllers
{
    public class AccountController : BaseController
    {
        #region Ctor

        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        #region Register

        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
                return View(registerViewModel);

            var result = await _userService.RegisterUser(registerViewModel);

            switch (result)
            {
                case RegisterResult.EmailExisted:
                    TempData[ErrorMessage] = "ایمیل وارد شده از قبل موجود میباشد";
                    break;
                case RegisterResult.Success:
                    TempData[SuccessMessage] = "عملیات ثبت نام با موفقیت انجام شد";
                    return RedirectToAction("Login", "Account");
            }

            return View(registerViewModel);
        }

        #endregion

        #region Login

        [HttpGet("Login")]
        public IActionResult Login()
        {
            return View();
        }

        #endregion
    }
}