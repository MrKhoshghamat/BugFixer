using Microsoft.AspNetCore.Mvc;

namespace BugFixer.WebUI.Areas.UserPanel.Controllers
{
    public class HomeController : UserPanelBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
