using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project5Day.WebApi.Context;
using Project5Day.WebUI.Dtos.MatchWeekDtos;

namespace Project5Day.WebUI.ViewComponents.MatchFixturesComponentPartial
{
    public class MatchFixturesHeaderComponentPartial : ViewComponent
    {
        private readonly ApiContext _context;

        public MatchFixturesHeaderComponentPartial(ApiContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int week = 36)
        {
            // 1. Veritabanından ham verileri çek (Entity tipinde)
            var weeksEntity = await _context.MatchWeeks.ToListAsync();

            // 2. ŞU ANKİ HAFTA İŞLEMLERİ (Count ve Countdown)
            var currentWeek = weeksEntity.FirstOrDefault(x => x.WeekNumber.Contains(week.ToString()));
            if (currentWeek != null)
            {
                var matches = await _context.Matches.Where(x => x.WeekNumber == week).ToListAsync();
                ViewBag.TotalMatchCount = matches.Count;

                var firstMatch = matches.OrderBy(x => x.MatchDate).ThenBy(x => x.MatchTime).FirstOrDefault();
                if (firstMatch != null)
                {
                    ViewBag.FirstMatchDate = firstMatch.MatchDate.ToString("yyyy-MM-dd") + "T" +
                                            (firstMatch.MatchTime.Contains(":") ? firstMatch.MatchTime : "00:00");
                }

                ViewBag.PrevWeek = week - 1;
                ViewBag.NextWeek = week + 1;
                ViewBag.CurrentWeekNum = week;
            }

            // 🔥 KRİTİK NOKTA: Entity listesini DTO listesine dönüştür (Mapping)
            var values = weeksEntity.Select(x => new ResultMatchWeekDto
            {
                MatchWeekId = x.MatchWeekId,
                LeagueName = x.LeagueName,
                WeekNumber = x.WeekNumber,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                LeagueIcon = x.LeagueIcon
            }).ToList();

            // Artık View'a ResultMatchWeekDto listesi gidiyor, hata düzelecek.
            return View(values);
        }
    }
}