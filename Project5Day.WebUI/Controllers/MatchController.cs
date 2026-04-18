using Microsoft.AspNetCore.Mvc;
using Project5Day.WebApi.Context;

namespace Project5Day.WebUI.Controllers
{
    public class MatchController : Controller
    {
        private readonly ApiContext _context;

        public MatchController(ApiContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Fixtures()
        {
            return View();
        }
        public async Task<IActionResult> Detail(int id)
        {
            return View();
        }
        public IActionResult Standings()
        {
            return View();
        }
    }
}