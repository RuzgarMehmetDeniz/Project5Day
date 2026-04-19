using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project5Day.WebApi.Context;
using Project5Day.WebApi.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Project5Day.WebUI.ViewComponents.AdminLayoutDasboardComponentPartial
{
    public class AdminLayoutDashboardStandingsTopScorersComponentPartial : ViewComponent
    {
        private readonly ApiContext _context;

        public AdminLayoutDashboardStandingsTopScorersComponentPartial(ApiContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // 1. ADIM: PUAN DURUMU İÇİN VERİLERİ ÇEK VE HESAPLA
            var teams = await _context.Teams.ToListAsync();
            var matches = await _context.Matches
                .Where(x => x.MatchStatus == "Finished" || !string.IsNullOrEmpty(x.FinalScore))
                .ToListAsync();

            var standings = teams.Select(t =>
            {
                int w = 0, d = 0, l = 0, p = 0;
                // Ev sahibi maçları kontrolü
                var homeMatches = matches.Where(m => m.HomeTeamId == t.TeamId);
                foreach (var m in homeMatches)
                {
                    var score = m.FinalScore.Split('-');
                    int h = int.Parse(score[0]), a = int.Parse(score[1]);
                    p++; if (h > a) w++; else if (h == a) d++; else l++;
                }
                // Deplasman maçları kontrolü
                var awayMatches = matches.Where(m => m.AwayTeamId == t.TeamId);
                foreach (var m in awayMatches)
                {
                    var score = m.FinalScore.Split('-');
                    int h = int.Parse(score[0]), a = int.Parse(score[1]);
                    p++; if (a > h) w++; else if (a == h) d++; else l++;
                }

                return new
                {
                    Team = t,
                    O = p,
                    G = w,
                    B = d,
                    M = l,
                    Puan = (w * 3) + d
                };
            })
            .OrderByDescending(x => x.Puan)
            .Take(5) // Sadece ilk 5 takım
            .ToList();

            // 2. ADIM: GOL KRALLIĞI (Top 5)
            // MatchEvents tablosundan 'Goal' tipindekileri grupluyoruz
            var scorers = await _context.MatchEvents
                .Where(x => x.Type == EventType.Goal)
                .Include(x => x.Match).ThenInclude(m => m.HomeTeam)
                .Include(x => x.Match).ThenInclude(m => m.AwayTeam)
                .GroupBy(x => x.PlayerName)
                .Select(g => new {
                    PlayerName = g.Key,
                    GoalCount = g.Count(),
                    // Oyuncunun takımını bulmak için IsHomeTeam kontrolü
                    TeamName = g.First().IsHomeTeam ? g.First().Match.HomeTeam.Name : g.First().Match.AwayTeam.Name
                })
                .OrderByDescending(x => x.GoalCount)
                .Take(5)
                .ToListAsync();

            // 3. ADIM: VIEW'A GÖNDERİM
            ViewBag.Standings = standings;
            ViewBag.TopScorers = scorers;

            return View();
        }
    }
}