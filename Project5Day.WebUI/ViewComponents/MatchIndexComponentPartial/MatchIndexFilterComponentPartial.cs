using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Project5Day.WebApi.Context;

namespace Project5Day.WebUI.ViewComponents.MatchIndexComponentPartial
{
    public class MatchIndexFilterComponentPartial : ViewComponent
    {
        private readonly ApiContext _apiContext;

        public MatchIndexFilterComponentPartial(ApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var activeWeek = await _apiContext.MatchWeeks
                 .OrderBy(x => x.MatchWeekId)
                 .FirstOrDefaultAsync();

            // 🔥 STRING İÇİNDEN SAYI ÇEK (EN KRİTİK FIX)
            var numberStr = new string(activeWeek.WeekNumber
                .Where(char.IsDigit)
                .ToArray());

            int weekNum = 0;

            if (int.TryParse(numberStr, out weekNum))
            {
                var matches = await _apiContext.Matches
                    .Where(x => x.WeekNumber == weekNum)
                    .ToListAsync();

                ViewBag.LiveCount = matches.Count(x => x.MatchStatus == "Devam Ediyor");
                ViewBag.FinishedCount = matches.Count(x => x.MatchStatus == "Bitti");
                ViewBag.UpcomingCount = matches.Count(x => x.MatchStatus == "Gelecek");
            }
            else
            {
                // 🔴 fallback (boş kalmasın diye)
                ViewBag.LiveCount = 0;
                ViewBag.FinishedCount = 0;
                ViewBag.UpcomingCount = 0;
            }
            return View();
        }
    }
}
