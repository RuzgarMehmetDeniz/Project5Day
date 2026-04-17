using Microsoft.AspNetCore.Mvc;

namespace Project5Day.WebUI.Controllers
{
    public class AdminLayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
