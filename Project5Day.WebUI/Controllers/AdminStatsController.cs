using Microsoft.AspNetCore.Mvc;

namespace Project5Day.WebUI.Controllers
{
    public class AdminStatsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
