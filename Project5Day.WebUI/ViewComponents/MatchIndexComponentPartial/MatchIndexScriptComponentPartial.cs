using Microsoft.AspNetCore.Mvc;

namespace Project5Day.WebUI.ViewComponents.MatchIndexComponentPartial
{
    public class MatchIndexScriptComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
