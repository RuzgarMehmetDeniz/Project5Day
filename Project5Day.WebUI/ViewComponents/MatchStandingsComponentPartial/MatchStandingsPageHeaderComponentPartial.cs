using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project5Day.WebApi.Context;
using System.Linq;
using System.Threading.Tasks;

namespace Project5Day.WebUI.ViewComponents.MatchStandingsComponentPartial
{
    public class MatchStandingsPageHeaderComponentPartial : ViewComponent
    {
        private readonly ApiContext _context;

        public MatchStandingsPageHeaderComponentPartial(ApiContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var weeks = await _context.MatchWeeks.ToListAsync(); // Veriyi önce çekiyoruz

            var latestWeek = weeks
                .Where(x => int.TryParse(x.WeekNumber, out int wn) && wn <= 36)
                .OrderByDescending(x => int.Parse(x.WeekNumber))
                .FirstOrDefault();

            return View(latestWeek);
        }
    }
}