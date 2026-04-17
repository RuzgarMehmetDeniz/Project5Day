using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project5Day.WebApi.Context;
using Project5Day.WebUI.Dtos.MatchWeekDtos;

namespace Project5Day.WebUI.ViewComponents.MatchIndexComponentPartial
{
    public class MatchIndexLowerNavbarComponentPartial : ViewComponent
    {
        private readonly ApiContext _context;

        public MatchIndexLowerNavbarComponentPartial(ApiContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var activeWeek = await _context.MatchWeeks
                .OrderBy(x => x.MatchWeekId)
                .FirstOrDefaultAsync();

            if (activeWeek == null)
                return View(new List<ResultMatchWeekDto>());

            // 🔥 STRING İÇİNDEN SAYI ÇEK (EN KRİTİK FIX)
            var numberStr = new string(activeWeek.WeekNumber
                .Where(char.IsDigit)
                .ToArray());

            int weekNum = 0;

            if (int.TryParse(numberStr, out weekNum))
            {
                var matches = await _context.Matches
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

            var values = new List<ResultMatchWeekDto>
            {
                new ResultMatchWeekDto
                {
                    MatchWeekId = activeWeek.MatchWeekId,
                    LeagueIcon = activeWeek.LeagueIcon,
                    LeagueName = activeWeek.LeagueName,
                    WeekNumber = activeWeek.WeekNumber,
                    StartDate = activeWeek.StartDate,
                    EndDate = activeWeek.EndDate
                }
            };

            return View(values);
        }
    }
}