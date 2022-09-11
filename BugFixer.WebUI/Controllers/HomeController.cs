using Microsoft.AspNetCore.Mvc;

namespace BugFixer.WebUI.Controllers;

public class HomeController : BaseController
{
    public IActionResult Index()
    {
        return View();
    }
}