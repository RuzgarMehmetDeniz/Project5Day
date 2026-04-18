using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project5Day.WebApi.Context;

namespace Project5Day.WebUI.ViewComponents.MatchDetailComponentPartial
{
    public class MatchDetailContentComponentPartial : ViewComponent
    {
        private readonly ApiContext _context;

        public MatchDetailContentComponentPartial(ApiContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var match = await _context.Matches
                .Include(x => x.MatchStatistic)
                .Include(x => x.MatchEvents)
                .Include(x => x.HomeTeam)
                .Include(x => x.AwayTeam)
                .Include(x => x.Stadium)
                .FirstOrDefaultAsync(x => x.MatchId == id);

            if (match == null) return View();

            return View(match);
        }
    }
}