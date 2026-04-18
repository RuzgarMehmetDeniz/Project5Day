using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project5Day.WebApi.Context;
using Project5Day.WebUI.Dtos.MatchDtos; // UI Katmanındaki DTO

namespace Project5Day.WebUI.ViewComponents.MatchIndexComponentPartial
{
    public class MatchIndexFinishedComponentPartial : ViewComponent
    {
        private readonly ApiContext _apiContext;

        public MatchIndexFinishedComponentPartial(ApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _apiContext.Matches
                .Include(x => x.HomeTeam)
                .Include(x => x.AwayTeam)
                .Where(x => x.WeekNumber == 35 && x.MatchStatus == "Bitti")
                .Select(x => new ResultMatchDto
                {
                    MatchId = x.MatchId,
                    HomeTeamName = x.HomeTeam.Name,
                    AwayTeamName = x.AwayTeam.Name,
                    HomeTeamLogo = x.HomeTeam.Logo,
                    AwayTeamLogo = x.AwayTeam.Logo,
                    FinalScore = x.FinalScore, // Maç bittiği için direkt final skoru
                    MatchTime = x.MatchTime,   // Başlangıç saati (16:30 gibi)
                    MatchStatus = x.MatchStatus
                })
                .ToListAsync();

            return View(values);
        }
    }
}