using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project5Day.WebApi.Context;

namespace Project5Day.WebUI.ViewComponents.MatchStandingsComponentPartial
{
    public class MatchStandingsScriptComponentPartial:ViewComponent
    {
        private readonly ApiContext _context;

        public MatchStandingsScriptComponentPartial(ApiContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Veritabanından takımları ve maçları çekiyoruz
            var teams = await _context.Teams.ToListAsync();
            var matches = await _context.Matches.Where(x => !string.IsNullOrEmpty(x.FinalScore)).ToListAsync();

            var dynamicTeams = teams.Select(t =>
            {
                var homeMatches = matches.Where(m => m.HomeTeamId == t.TeamId);
                var awayMatches = matches.Where(m => m.AwayTeamId == t.TeamId);

                int gs = 0, gc = 0, w = 0, d = 0, l = 0;

                foreach (var m in homeMatches)
                {
                    var score = m.FinalScore.Split('-');
                    int h = int.Parse(score[0]), a = int.Parse(score[1]);
                    gs += h; gc += a;
                    if (h > a) w++; else if (h == a) d++; else l++;
                }
                foreach (var m in awayMatches)
                {
                    var score = m.FinalScore.Split('-');
                    int h = int.Parse(score[0]), a = int.Parse(score[1]);
                    gs += a; gc += h;
                    if (a > h) w++; else if (a == h) d++; else l++;
                }

                return new
                {
                    name = t.Name,
                    shortName = t.Name.Length > 3 ? t.Name.Substring(0, 3).ToUpper() : t.Name,
                    logo = t.Logo,
                    mp = w + d + l,
                    w = w,
                    d = d,
                    l = l,
                    gf = gs,
                    ga = gc,
                    pts = (w * 3) + d
                };
            })
            .OrderByDescending(x => x.pts)
            .ThenByDescending(x => (x.gf - x.ga))
            .ToList();

            return View(dynamicTeams);
        }
    }
}
