using Microsoft.AspNetCore.Mvc;

namespace Project5Day.WebUI.ViewComponents.AdminLayoutComponentPartial
{
    public class AdminLayoutHeaderComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
