using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project5Day.WebApi.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project5Day.WebUI.ViewComponents.MatchStandingsComponentPartial
{
    public class MatchStandingsTopStatCardsComponentPartial : ViewComponent
    {
        private readonly ApiContext _context;

        public MatchStandingsTopStatCardsComponentPartial(ApiContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // 1. Veritabanından tüm takımları ve bitti durumundaki tüm maçları çekiyoruz
            var teams = await _context.Teams.ToListAsync();
            var allMatches = await _context.Matches
                .Include(x => x.HomeTeam)
                .Include(x => x.AwayTeam)
                .Include(x => x.Stadium)
                .Where(x => !string.IsNullOrEmpty(x.FinalScore))
                .ToListAsync();

            if (!allMatches.Any()) return View(null);

            // 2. Tabloyu hesaplayan mantığın aynısı: Her takımı tek tek hesapla
            var leagueStats = teams.Select(t =>
            {
                int gs = 0, gc = 0, w = 0, d = 0, l = 0;

                // Ev sahibi maçları
                var homeMatches = allMatches.Where(m => m.HomeTeamId == t.TeamId);
                foreach (var m in homeMatches)
                {
                    var score = m.FinalScore.Split('-');
                    int h = int.Parse(score[0]), a = int.Parse(score[1]);
                    gs += h; gc += a;
                    if (h > a) w++; else if (h == a) d++; else l++;
                }

                // Deplasman maçları
                var awayMatches = allMatches.Where(m => m.AwayTeamId == t.TeamId);
                foreach (var m in awayMatches)
                {
                    var score = m.FinalScore.Split('-');
                    int h = int.Parse(score[0]), a = int.Parse(score[1]);
                    gs += a; gc += h;
                    if (a > h) w++; else if (a == h) d++; else l++;
                }

                return new
                {
                    Team = t,
                    Points = (w * 3) + d,
                    GF = gs,
                    GA = gc,
                    GD = gs - gc
                };
            }).ToList();

            // 3. İstatistikleri belirle (Tablo sıralamasıyla aynı: Puan -> Averaj)
            var leader = leagueStats.OrderByDescending(x => x.Points).ThenByDescending(x => x.GD).FirstOrDefault();
            var topScorer = leagueStats.OrderByDescending(x => x.GF).FirstOrDefault();
            var bestDefense = leagueStats.OrderBy(x => x.GA).FirstOrDefault();

            // 4. Haftanın Maçı (En yüksek seyircili bitti durumundaki maç)
            var matchOfTheWeek = allMatches.OrderByDescending(x => x.SpectatorCount).FirstOrDefault();

            // 5. ViewBag Tanımlamaları
            ViewBag.LeaderName = leader?.Team.Name ?? "Belli Değil";
            ViewBag.LeaderShort = leader?.Team.Name.Length >= 3 ? leader.Team.Name.Substring(0, 3).ToUpper() : (leader?.Team.Name.ToUpper() ?? "---");
            ViewBag.LeaderPoints = leader?.Points ?? 0;

            ViewBag.MaxGoals = topScorer?.GF ?? 0;
            ViewBag.TopScorerName = topScorer?.Team.Name ?? "Belli Değil";

            ViewBag.MinConceded = bestDefense?.GA ?? 0;
            ViewBag.BestDefenseName = bestDefense?.Team.Name ?? "Belli Değil";

            return View(matchOfTheWeek);
        }
    }
}