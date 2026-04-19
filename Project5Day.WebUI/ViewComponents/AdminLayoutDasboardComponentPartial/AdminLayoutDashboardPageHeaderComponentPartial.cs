using Microsoft.AspNetCore.Mvc;

namespace Project5Day.WebUI.ViewComponents.AdminLayoutDasboardComponentPartial
{
    public class AdminLayoutDashboardPageHeaderComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
