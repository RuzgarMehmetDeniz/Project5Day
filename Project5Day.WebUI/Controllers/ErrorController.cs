using Microsoft.AspNetCore.Mvc;

namespace Project5Day.WebUI.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
