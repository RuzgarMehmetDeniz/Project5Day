using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project5Day.WebApi.Context;
using Project5Day.WebUI.Dtos.MatchDtos;

namespace Project5Day.WebUI.ViewComponents.MatchFixturesComponentPartial
{
    public class MatchFixturesSundayComponentPartial : ViewComponent
    {
        private readonly ApiContext _context;

        public MatchFixturesSundayComponentPartial(ApiContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            int targetWeek = 36;

            var allMatches = await _context.Matches
                .Include(x => x.HomeTeam)
                .Include(x => x.AwayTeam)
                .Include(x => x.Stadium)
                .Where(x => x.WeekNumber == targetWeek)
                .OrderBy(x => x.MatchDate)
                .ThenBy(x => x.MatchTime)
                .ToListAsync();

            // Üçüncü günü (Pazartesi) bulmak için tarihleri grupla ve 3. sıradakini (Skip 2) al
            var lastDate = allMatches.Select(x => x.MatchDate.Date).Distinct().Skip(2).FirstOrDefault();

            var mondayMatches = allMatches.Where(x => x.MatchDate.Date == lastDate).ToList();

            var values = mondayMatches.Select(x => new ResultMatchDto
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