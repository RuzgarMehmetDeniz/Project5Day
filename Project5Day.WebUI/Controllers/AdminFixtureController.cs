using Microsoft.AspNetCore.Mvc;

namespace Project5Day.WebUI.Controllers
{
    public class AdminFixtureController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
