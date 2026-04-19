using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project5Day.WebApi.Context;
using System.Linq;

namespace Project5Day.WebUI.ViewComponents.AdminLayoutDasboardComponentPartial
{
    public class AdminLayoutDashboardChartsRecentMatchesComponentPartial : ViewComponent
    {
        private readonly ApiContext _context;

        public AdminLayoutDashboardChartsRecentMatchesComponentPartial(ApiContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            // 1. Veri çekme ve İlişkili tabloları dahil etme
            var recentMatches = _context.Matches
                .Include(x => x.HomeTeam)
                .Include(x => x.AwayTeam)
                .AsEnumerable() // İşlemi bellek üzerinde devam ettir (Trim hatalarını önler)
                .Where(x =>
                    (x.MatchStatus != null && x.MatchStatus.Trim() == "2") ||
                    (!string.IsNullOrEmpty(x.FinalScore) && x.FinalScore.Contains("-"))
                )
                .OrderByDescending(x => x.MatchDate)
                .Take(3)
                .ToList();

            // 2. Grafik Verileri
            var allMatches = _context.Matches.ToList();
            var weeklyStats = allMatches
                .GroupBy(x => x.WeekNumber)
                .OrderBy(x => x.Key)
                .Select(g => new {
                    WeekName = "H" + g.Key,
                    TotalGoals = g.Sum(m => {
                        if (string.IsNullOrEmpty(m.FinalScore) || !m.FinalScore.Contains("-")) return 0;
                        var scores = m.FinalScore.Split('-');
                        if (scores.Length == 2 && int.TryParse(scores[0], out int h) && int.TryParse(scores[1], out int a))
                            return h + a;
                        return 0;
                    })
                }).ToList();

            ViewBag.ChartLabels = weeklyStats.Select(x => x.WeekName).ToList();
            ViewBag.ChartData = weeklyStats.Select(x => x.TotalGoals).ToList();

            return View(recentMatches);
        }
    }
}