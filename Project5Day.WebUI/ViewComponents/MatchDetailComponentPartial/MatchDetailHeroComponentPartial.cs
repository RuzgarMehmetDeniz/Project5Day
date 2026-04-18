using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project5Day.WebApi.Context;
using Project5Day.WebUI.Dtos.MatchDtos;

namespace Project5Day.WebUI.ViewComponents.MatchDetailComponentPartial
{
    public class MatchDetailHeroComponentPartial : ViewComponent
    {
        private readonly ApiContext _context;

        public MatchDetailHeroComponentPartial(ApiContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            // 1. Maçı ve bağlı olduğu takımları/stadyumu getir
            var match = await _context.Matches
                .Include(x => x.HomeTeam)
                .Include(x => x.AwayTeam)
                .Include(x => x.Stadium)
                .FirstOrDefaultAsync(x => x.MatchId == id);

            if (match == null) return View();

            // 2. Arada ilişki olmadığı için WeekNumber üzerinden lig bilgilerini bul
            var leagueInfo = await _context.MatchWeeks
                .FirstOrDefaultAsync(x => x.WeekNumber == match.WeekNumber.ToString());

            // 3. DTO Mapping
            var model = new ResultMatchDetailDto
            {
                MatchId = match.MatchId,
                FinalScore = match.FinalScore,
                FirstHalfScore = match.FirstHalfScore,
                MatchTime = match.MatchTime,
                MatchDate = match.MatchDate,
                WeekNumber = match.WeekNumber,
                SpectatorCount = match.SpectatorCount,
                RefereeName = match.RefereeName,
                MatchStatus = match.MatchStatus,

                // Takım ve Stadyum (Entity isimlerine sadık kaldık: Name, Logo)
                HomeTeamName = match.HomeTeam?.Name,
                HomeTeamLogo = match.HomeTeam?.Logo,
                AwayTeamName = match.AwayTeam?.Name,
                AwayTeamLogo = match.AwayTeam?.Logo,
                StadiumName = match.Stadium?.Name,
                StadiumCity = match.Stadium?.City,

                // Lig Bilgileri
                LeagueName = leagueInfo?.LeagueName ?? "Premier League",
                LeagueIcon = leagueInfo?.LeagueIcon ?? "bi bi-trophy-fill"
            };

            return View(model);
        }
    }
}
