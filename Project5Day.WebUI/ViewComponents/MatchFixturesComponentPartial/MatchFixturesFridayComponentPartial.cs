using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project5Day.WebApi.Context;
using Project5Day.WebUI.Dtos.MatchDtos;

namespace Project5Day.WebUI.ViewComponents.MatchFixturesComponentPartial
{
    public class MatchFixturesFridayComponentPartial : ViewComponent
    {
        private readonly ApiContext _context;

        public MatchFixturesFridayComponentPartial(ApiContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            int targetWeek = 36;

            // 36. haftanın maçlarını tarihe göre sıralayıp alıyoruz
            var matches = await _context.Matches
                .Include(x => x.HomeTeam)
                .Include(x => x.AwayTeam)
                .Include(x => x.Stadium)
                .Where(x => x.WeekNumber == targetWeek)
                .OrderBy(x => x.MatchDate)
                .ThenBy(x => x.MatchTime)
                .ToListAsync();

            // İlk günü (Cuma/Hafta Açılışı) buluyoruz
            var firstDate = matches.Select(x => x.MatchDate.Date).FirstOrDefault();
            var fridayMatches = matches.Where(x => x.MatchDate.Date == firstDate).ToList();

            var values = fridayMatches.Select(x => new ResultMatchDto
            {
                MatchId = x.MatchId,
                HomeTeamName = x.HomeTeam.Name,
                AwayTeamName = x.AwayTeam.Name,
                HomeTeamLogo = x.HomeTeam.Logo,
                AwayTeamLogo = x.AwayTeam.Logo,
                StadiumName = x.Stadium.Name,
                MatchTime = x.MatchTime,
                MatchDate = x.MatchDate
            }).ToList();

            return View(values);
        }
    }
}