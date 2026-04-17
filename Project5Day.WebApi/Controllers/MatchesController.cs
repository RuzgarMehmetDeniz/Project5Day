using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project5Day.WebApi.Context;
using Project5Day.WebApi.Dtos.MatchDtos;
using Project5Day.WebApi.Entities;

namespace Project5Day.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        private readonly ApiContext _context;

        public MatchesController(ApiContext context)
        {
            _context = context;
        }

        // 1. Tüm Maçları İlişkili Tablo Bilgileriyle Listele
        [HttpGet]
        public async Task<IActionResult> MatchList()
        {
            var values = await _context.Matches
                .Include(x => x.HomeTeam)   // Ev sahibi bilgilerini çek
                .Include(x => x.AwayTeam)   // Misafir bilgilerini çek
                .Include(x => x.Stadium)    // Stadyum bilgilerini çek
                .Select(x => new ResultMatchDto
                {
                    MatchId = x.MatchId,
                    HomeTeamName = x.HomeTeam.Name,
                    AwayTeamName = x.AwayTeam.Name,
                    HomeTeamLogo = x.HomeTeam.Logo,
                    AwayTeamLogo = x.AwayTeam.Logo,
                    StadiumName = x.Stadium.Name,
                    FinalScore = x.FinalScore,
                    MatchTime = x.MatchTime,
                    MatchDate = x.MatchDate,
                    WeekNumber = x.WeekNumber
                }).ToListAsync();

            return Ok(values);
        }

        // 2. Yeni Maç Oluştur
        [HttpPost]
        public async Task<IActionResult> CreateMatch(CreateMatchDto createMatchDto)
        {
            var match = new Match
            {
                FirstHalfScore = createMatchDto.FirstHalfScore,
                SecondHalfScore = createMatchDto.SecondHalfScore,
                FinalScore = createMatchDto.FinalScore,
                MatchTime = createMatchDto.MatchTime,
                MatchDate = createMatchDto.MatchDate,
                WeekNumber = createMatchDto.WeekNumber,
                RefereeName = createMatchDto.RefereeName,
                SpectatorCount = createMatchDto.SpectatorCount,
                HomeTeamId = createMatchDto.HomeTeamId,
                AwayTeamId = createMatchDto.AwayTeamId,
                StadiumId = createMatchDto.StadiumId
            };

            await _context.Matches.AddAsync(match);
            await _context.SaveChangesAsync();
            return Ok("Maç başarıyla programa eklendi.");
        }

        // 3. ID'ye Göre Maç Detayı Getir
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMatch(int id)
        {
            var value = await _context.Matches.FindAsync(id);
            if (value == null) return NotFound("Maç bulunamadı.");

            var matchDto = new GetByIdMatchDto
            {
                MatchId = value.MatchId,
                FirstHalfScore = value.FirstHalfScore,
                SecondHalfScore = value.SecondHalfScore,
                FinalScore = value.FinalScore,
                MatchTime = value.MatchTime,
                MatchDate = value.MatchDate,
                WeekNumber = value.WeekNumber,
                RefereeName = value.RefereeName,
                SpectatorCount = value.SpectatorCount,
                HomeTeamId = value.HomeTeamId,
                AwayTeamId = value.AwayTeamId,
                StadiumId = value.StadiumId
            };

            return Ok(matchDto);
        }

        // 4. Maç Bilgilerini/Skoru Güncelle
        [HttpPut]
        public async Task<IActionResult> UpdateMatch(UpdateMatchDto updateMatchDto)
        {
            var value = await _context.Matches.FindAsync(updateMatchDto.MatchId);
            if (value == null) return NotFound("Güncellenecek maç bulunamadı.");

            value.FirstHalfScore = updateMatchDto.FirstHalfScore;
            value.SecondHalfScore = updateMatchDto.SecondHalfScore;
            value.FinalScore = updateMatchDto.FinalScore;
            value.MatchTime = updateMatchDto.MatchTime;
            value.MatchDate = updateMatchDto.MatchDate;
            value.WeekNumber = updateMatchDto.WeekNumber;
            value.RefereeName = updateMatchDto.RefereeName;
            value.SpectatorCount = updateMatchDto.SpectatorCount;
            value.HomeTeamId = updateMatchDto.HomeTeamId;
            value.AwayTeamId = updateMatchDto.AwayTeamId;
            value.StadiumId = updateMatchDto.StadiumId;

            await _context.SaveChangesAsync();
            return Ok("Maç bilgileri/skor güncellendi.");
        }

        // 5. Maç Sil
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatch(int id)
        {
            var value = await _context.Matches.FindAsync(id);
            if (value == null) return NotFound("Silinecek maç bulunamadı.");

            _context.Matches.Remove(value);
            await _context.SaveChangesAsync();
            return Ok("Maç programdan kaldırıldı.");
        }
    }
}