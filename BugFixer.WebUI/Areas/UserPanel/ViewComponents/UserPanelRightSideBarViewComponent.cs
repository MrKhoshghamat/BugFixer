using BugFixer.Application.Extensions;
using BugFixer.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BugFixer.WebUI.Areas.UserPanel.ViewComponents
{
    public class UserPanelRightSideBarViewComponent : ViewComponent
    {
        #region ctor

        private IUserService _userService;
        public UserPanelRightSideBarViewComponent(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userService.GetUserByIdAsync(HttpContext.User.GetUserId());  

            return View("UserPanelRightSideBar", user);
        }
    }
}
