using Microsoft.AspNetCore.Mvc;

namespace Project5Day.WebUI.Controllers
{
    public class AdminLayoutController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Index";
            return View();
        }
        public IActionResult Dashboard()
        {
            ViewBag.Title = "Dashboard";
            return View();
        }
    }
}
