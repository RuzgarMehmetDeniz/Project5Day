using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project5Day.WebApi.Context;
using System.Linq;
using System.Threading.Tasks;

namespace Project5Day.WebUI.ViewComponents.MatchIndexComponentPartial
{
    public class MatchIndexFeaturedComponentPartial : ViewComponent
    {
        private readonly ApiContext _context;

        public MatchIndexFeaturedComponentPartial(ApiContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // 35. haftadaki bitmiş maçlardan en yüksek seyircili olanı (Haftanın Maçı) getir
            var featuredMatch = await _context.Matches
                .Include(x => x.HomeTeam)
                .Include(x => x.AwayTeam)
                .Include(x => x.Stadium)
                .Where(x => x.WeekNumber == 35 && x.MatchStatus == "Bitti")
                .OrderByDescending(x => x.SpectatorCount)
                .FirstOrDefaultAsync();

            return View(featuredMatch);
        }
    }
}