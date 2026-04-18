using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project5Day.WebApi.Context;

namespace Project5Day.WebUI.ViewComponents.MatchFixturesComponentPartial
{
    public class MatchFixturesSummaryComponentPartial : ViewComponent
    {
        private readonly ApiContext _context;

        public MatchFixturesSummaryComponentPartial(ApiContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            int targetWeek = 36;

            // 1. Toplam Maç Sayısı
            var totalMatches = await _context.Matches
                .CountAsync(x => x.WeekNumber == targetWeek);

            // 2. Hafta Bilgileri (Tarih aralığı ve gün isimleri için)
            var weekInfo = await _context.MatchWeeks
                .FirstOrDefaultAsync(x => x.WeekNumber.Contains(targetWeek.ToString()));

            ViewBag.TotalMatches = totalMatches;

            if (weekInfo != null)
            {
                // "Cum-Paz" gibi gün aralığı formatı
                string startDay = weekInfo.StartDate.ToString("ddd"); // Örn: Cmt
                string endDay = weekInfo.EndDate.ToString("ddd");     // Örn: Pzt
                ViewBag.DayRange = $"{startDay}–{endDay}";

                // Ay ve Yıl bilgisi
                ViewBag.Day = weekInfo.StartDate.Day;
                ViewBag.MonthYear = weekInfo.StartDate.ToString("MMMM yyyy");
            }

            // Statik "Zirve Derbisi" sayısı (Şimdilik el ile 3 verdik, veritabanında derbi işaretli değilse)
            ViewBag.DerbyCount = 3;

            return View();
        }
    }
}