using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project5Day.WebApi.Context;
using System.Linq;

namespace Project5Day.WebUI.ViewComponents.AdminLayoutDasboardComponentPartial
{
    public class AdminLayoutDashboardStatCardsComponentPartial : ViewComponent
    {
        private readonly ApiContext _context;

        public AdminLayoutDashboardStatCardsComponentPartial(ApiContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.TotalTeam = _context.Teams.Count();
            ViewBag.TotalPlayer = _context.Players.Count();
            ViewBag.TotalMatch = _context.Matches.Count();
            // 1. Verileri veritabanından çekiyoruz
            var matches = _context.Matches.ToList();

            // 2. String skorları parçalayıp topluyoruz
            int toplamGol = matches.Sum(m => {
                if (string.IsNullOrEmpty(m.FinalScore)) return 0;

                // "2-2" stringini '-' karakterinden ikiye bölüyoruz
                var skorlar = m.FinalScore.Split('-');

                // Parçaları sayıya çevirip topluyoruz
                if (skorlar.Length == 2 &&
                    int.TryParse(skorlar[0], out int ev) &&
                    int.TryParse(skorlar[1], out int deplasman))
                {
                    return ev + deplasman;
                }

                return 0;
            });

            // 3. Sonucu ViewBag'e aktarıyoruz
            ViewBag.TotalGoal = toplamGol;
            return View();
        }
    }
}