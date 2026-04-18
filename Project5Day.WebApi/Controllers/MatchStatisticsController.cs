using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project5Day.WebApi.Context;
using Project5Day.WebApi.Dtos.MatchStatisticDtos;
using Project5Day.WebApi.Entities;

namespace Project5Day.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchStatisticsController : ControllerBase
    {
        private readonly ApiContext _context;

        public MatchStatisticsController(ApiContext context)
        {
            _context = context;
        }

        // 1. Tüm İstatistikleri Listele
        [HttpGet]
        public async Task<IActionResult> MatchStatisticList()
        {
            var values = await _context.MatchStatistics
                .Select(x => new ResultMatchStatisticDto
                {
                    MatchStatisticId = x.MatchStatisticId,
                    MatchId = x.MatchId,
                    HomePossession = x.HomePossession,
                    HomeShots = x.HomeShots,
                    HomeShotsOnTarget = x.HomeShotsOnTarget,
                    HomePasses = x.HomePasses,
                    HomePassAccuracy = x.HomePassAccuracy,
                    HomeCorners = x.HomeCorners,
                    HomeFouls = x.HomeFouls,
                    HomeOffsides = x.HomeOffsides,
                    AwayPossession = x.AwayPossession,
                    AwayShots = x.AwayShots,
                    AwayShotsOnTarget = x.AwayShotsOnTarget,
                    AwayPasses = x.AwayPasses,
                    AwayPassAccuracy = x.AwayPassAccuracy,
                    AwayCorners = x.AwayCorners,
                    AwayFouls = x.AwayFouls,
                    AwayOffsides = x.AwayOffsides
                }).ToListAsync();

            return Ok(values);
        }

        // 2. Yeni İstatistik Girişi Yap
        [HttpPost]
        public async Task<IActionResult> CreateMatchStatistic(CreateMatchStatisticDto createDto)
        {
            var stats = new MatchStatistic
            {
                MatchId = createDto.MatchId,
                HomePossession = createDto.HomePossession,
                HomeShots = createDto.HomeShots,
                HomeShotsOnTarget = createDto.HomeShotsOnTarget,
                HomePasses = createDto.HomePasses,
                HomePassAccuracy = createDto.HomePassAccuracy,
                HomeCorners = createDto.HomeCorners,
                HomeFouls = createDto.HomeFouls,
                HomeOffsides = createDto.HomeOffsides,
                AwayPossession = createDto.AwayPossession,
                AwayShots = createDto.AwayShots,
                AwayShotsOnTarget = createDto.AwayShotsOnTarget,
                AwayPasses = createDto.AwayPasses,
                AwayPassAccuracy = createDto.AwayPassAccuracy,
                AwayCorners = createDto.AwayCorners,
                AwayFouls = createDto.AwayFouls,
                AwayOffsides = createDto.AwayOffsides
            };

            await _context.MatchStatistics.AddAsync(stats);
            await _context.SaveChangesAsync();
            return Ok("Maç istatistikleri kaydedildi.");
        }

        // 3. Maç ID'sine veya Kendi ID'sine Göre İstatistik Getir
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMatchStatistic(int id)
        {
            var value = await _context.MatchStatistics.FindAsync(id);
            if (value == null) return NotFound("İstatistik bulunamadı.");

            var dto = new GetByIdMatchStatisticDto
            {
                MatchStatisticId = value.MatchStatisticId,
                MatchId = value.MatchId,
                HomePossession = value.HomePossession,
                HomeShots = value.HomeShots,
                HomeShotsOnTarget = value.HomeShotsOnTarget,
                HomePasses = value.HomePasses,
                HomePassAccuracy = value.HomePassAccuracy,
                HomeCorners = value.HomeCorners,
                HomeFouls = value.HomeFouls,
                HomeOffsides = value.HomeOffsides,
                AwayPossession = value.AwayPossession,
                AwayShots = value.AwayShots,
                AwayShotsOnTarget = value.AwayShotsOnTarget,
                AwayPasses = value.AwayPasses,
                AwayPassAccuracy = value.AwayPassAccuracy,
                AwayCorners = value.AwayCorners,
                AwayFouls = value.AwayFouls,
                AwayOffsides = value.AwayOffsides
            };

            return Ok(dto);
        }

        // 4. İstatistik Güncelle
        [HttpPut]
        public async Task<IActionResult> UpdateMatchStatistic(UpdateMatchStatisticDto updateDto)
        {
            var value = await _context.MatchStatistics.FindAsync(updateDto.MatchStatisticId);
            if (value == null) return NotFound("Güncellenecek istatistik bulunamadı.");

            value.HomePossession = updateDto.HomePossession;
            value.HomeShots = updateDto.HomeShots;
            value.HomeShotsOnTarget = updateDto.HomeShotsOnTarget;
            value.HomePasses = updateDto.HomePasses;
            value.HomePassAccuracy = updateDto.HomePassAccuracy;
            value.HomeCorners = updateDto.HomeCorners;
            value.HomeFouls = updateDto.HomeFouls;
            value.HomeOffsides = updateDto.HomeOffsides;
            value.AwayPossession = updateDto.AwayPossession;
            value.AwayShots = updateDto.AwayShots;
            value.AwayShotsOnTarget = updateDto.AwayShotsOnTarget;
            value.AwayPasses = updateDto.AwayPasses;
            value.AwayPassAccuracy = updateDto.AwayPassAccuracy;
            value.AwayCorners = updateDto.AwayCorners;
            value.AwayFouls = updateDto.AwayFouls;
            value.AwayOffsides = updateDto.AwayOffsides;

            await _context.SaveChangesAsync();
            return Ok("İstatistikler güncellendi.");
        }
    }
}