using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project5Day.WebApi.Context;
using Project5Day.WebUI.Dtos.MatchDtos;

namespace Project5Day.WebUI.ViewComponents.MatchIndexComponentPartial
{
    public class MatchIndexLiveComponentPartial : ViewComponent
    {
        private readonly ApiContext _apiContext;

        public MatchIndexLiveComponentPartial(ApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // 🔥 Buradaki .Include satırları "null" sorununu çözen kısımdır.
            var values = await _apiContext.Matches
                .Include(x => x.HomeTeam) // Ev sahibi takımı yükle
                .Include(x => x.AwayTeam) // Deplasman takımını yükle
                .Where(x => x.WeekNumber == 35 && x.MatchStatus == "Devam Ediyor")
                .ToListAsync();

            var result = values.Select(x => {
                int currentMinute = 0;
                if (!string.IsNullOrEmpty(x.MatchTime))
                {
                    int.TryParse(new string(x.MatchTime.Where(char.IsDigit).ToArray()), out currentMinute);
                }

                return new ResultMatchDto
                {
                    MatchId = x.MatchId,
                    // ?. operatörü ile güvenli erişim sağlıyoruz
                    HomeTeamName = x.HomeTeam?.Name ?? "Takım Bulunamadı",
                    AwayTeamName = x.AwayTeam?.Name ?? "Takım Bulunamadı",
                    HomeTeamLogo = x.HomeTeam?.Logo,
                    AwayTeamLogo = x.AwayTeam?.Logo,
                    MatchTime = x.MatchTime,
                    FinalScore = currentMinute <= 45 ? x.FirstHalfScore : x.SecondHalfScore,
                    MatchStatus = x.MatchStatus
                };
            }).ToList();

            return View(result);
        }
    }
}