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
            var weeksEntity = await _context.MatchWeeks.ToListAsync();
            var currentWeek = weeksEntity.FirstOrDefault(x => x.WeekNumber.Contains(week.ToString()));

            if (currentWeek != null)
            {
                var matches = await _context.Matches.Where(x => x.WeekNumber == week).ToListAsync();
                ViewBag.TotalMatchCount = matches.Count;

                // Tarihe göre sıralayıp en erken maçı alıyoruz
                var firstMatch = matches.OrderBy(x => x.MatchDate).ThenBy(x => x.MatchTime).FirstOrDefault();

                if (firstMatch != null)
                {
                    // Format: 2026-05-02
                    string formattedDate = firstMatch.MatchDate.ToString("yyyy-MM-dd");

                    // Format: 15:30 (Noktayı iki noktaya çeviriyoruz)
                    string formattedTime = firstMatch.MatchTime.Replace(".", ":");
                    if (formattedTime.Length == 5) formattedTime += ":00"; // Saniye ekle

                    // Sonuç: 2026-05-02T15:30:00
                    ViewBag.FirstMatchDate = $"{formattedDate}T{formattedTime}";
                }

                ViewBag.PrevWeek = week - 1;
                ViewBag.NextWeek = week + 1;
                ViewBag.CurrentWeekNum = week;
            }

            var values = weeksEntity.Select(x => new ResultMatchWeekDto
            {
                MatchWeekId = x.MatchWeekId,
                LeagueName = x.LeagueName,
                WeekNumber = x.WeekNumber,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                LeagueIcon = x.LeagueIcon
            }).ToList();

            return View(values);
        }
    }
}