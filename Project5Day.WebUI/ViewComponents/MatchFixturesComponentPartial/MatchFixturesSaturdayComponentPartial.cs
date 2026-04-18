using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project5Day.WebApi.Context;
using Project5Day.WebUI.Dtos.MatchDtos;

namespace Project5Day.WebUI.ViewComponents.MatchFixturesComponentPartial
{
    public class MatchFixturesSaturdayComponentPartial : ViewComponent
    {
        private readonly ApiContext _context;

        public MatchFixturesSaturdayComponentPartial(ApiContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            int targetWeek = 36;

            // 36. haftanın tüm maçlarını getir
            var allMatches = await _context.Matches
                .Include(x => x.HomeTeam)
                .Include(x => x.AwayTeam)
                .Include(x => x.Stadium)
                .Where(x => x.WeekNumber == targetWeek)
                .OrderBy(x => x.MatchDate)
                .ThenBy(x => x.MatchTime)
                .ToListAsync();

            // İkinci günü (Pazar) bulmak için tarihleri grupla ve 2. sıradakini al
            var secondDate = allMatches.Select(x => x.MatchDate.Date).Distinct().Skip(1).FirstOrDefault();

            var saturdayMatches = allMatches.Where(x => x.MatchDate.Date == secondDate).ToList();

            var values = saturdayMatches.Select(x => new ResultMatchDto
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