using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project5Day.WebApi.Context;
using Project5Day.WebUI.Dtos.MatchDtos;

namespace Project5Day.WebUI.ViewComponents.MatchIndexComponentPartial
{
    public class MatchIndexUpcomingComponentPartial : ViewComponent
    {
        private readonly ApiContext _apiContext;

        public MatchIndexUpcomingComponentPartial(ApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _apiContext.Matches
                .Include(x => x.HomeTeam)
                .Include(x => x.AwayTeam)
                .Where(x => x.WeekNumber == 35 && x.MatchStatus == "Gelecek")
                .Select(x => new ResultMatchDto
                {
                    MatchId = x.MatchId,
                    HomeTeamName = x.HomeTeam.Name,
                    AwayTeamName = x.AwayTeam.Name,
                    HomeTeamLogo = x.HomeTeam.Logo,
                    AwayTeamLogo = x.AwayTeam.Logo,
                    MatchTime = x.MatchTime,
                    MatchDate = x.MatchDate,
                    MatchStatus = x.MatchStatus
                })
                .ToListAsync();

            return View(values);
        }
    }
}