using Microsoft.AspNetCore.Mvc;

namespace Project5Day.WebUI.ViewComponents.MatchStandingsComponentPartial
{
    public class MatchStandingsScriptComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
