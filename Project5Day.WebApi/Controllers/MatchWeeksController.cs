using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project5Day.WebApi.Context;
using Project5Day.WebApi.Dtos.MatchWeekDtos;
using Project5Day.WebApi.Entities;

namespace Project5Day.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchWeeksController : ControllerBase
    {
        private readonly ApiContext _context;

        public MatchWeeksController(ApiContext context)
        {
            _context = context;
        }

        // 1. Tüm Haftaları/Ligleri Listele
        [HttpGet]
        public async Task<IActionResult> MatchWeekList()
        {
            var values = await _context.MatchWeeks
                .Select(x => new ResultMatchWeekDto
                {
                    MatchWeekId = x.MatchWeekId,
                    LeagueIcon = x.LeagueIcon,
                    LeagueName = x.LeagueName,
                    WeekNumber = x.WeekNumber,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate
                }).ToListAsync();

            return Ok(values);
        }

        // 2. Yeni Hafta/Lig Tanımı Oluştur
        [HttpPost]
        public async Task<IActionResult> CreateMatchWeek(CreateMatchWeekDto createMatchWeekDto)
        {
            var matchWeek = new MatchWeek
            {
                LeagueIcon = createMatchWeekDto.LeagueIcon,
                LeagueName = createMatchWeekDto.LeagueName,
                WeekNumber = createMatchWeekDto.WeekNumber,
                StartDate = createMatchWeekDto.StartDate,
                EndDate = createMatchWeekDto.EndDate
            };

            await _context.MatchWeeks.AddAsync(matchWeek);
            await _context.SaveChangesAsync();
            return Ok("Hafta bilgisi başarıyla sisteme eklendi.");
        }

        // 3. ID'ye Göre Hafta Detayı Getir
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMatchWeek(int id)
        {
            var value = await _context.MatchWeeks.FindAsync(id);
            if (value == null)
            {
                return NotFound("İlgili hafta bilgisi bulunamadı.");
            }

            var matchWeekDto = new GetByIdMatchWeekDto
            {
                MatchWeekId = value.MatchWeekId,
                LeagueIcon = value.LeagueIcon,
                LeagueName = value.LeagueName,
                WeekNumber = value.WeekNumber,
                StartDate = value.StartDate,
                EndDate = value.EndDate
            };

            return Ok(matchWeekDto);
        }

        // 4. Hafta Bilgilerini Güncelle
        [HttpPut]
        public async Task<IActionResult> UpdateMatchWeek(UpdateMatchWeekDto updateMatchWeekDto)
        {
            var value = await _context.MatchWeeks.FindAsync(updateMatchWeekDto.MatchWeekId);
            if (value == null)
            {
                return NotFound("Güncellenecek kayıt bulunamadı.");
            }

            value.LeagueIcon = updateMatchWeekDto.LeagueIcon;
            value.LeagueName = updateMatchWeekDto.LeagueName;
            value.WeekNumber = updateMatchWeekDto.WeekNumber;
            value.StartDate = updateMatchWeekDto.StartDate;
            value.EndDate = updateMatchWeekDto.EndDate;

            await _context.SaveChangesAsync();
            return Ok("Hafta bilgileri başarıyla güncellendi.");
        }

        // 5. Hafta Bilgisini Sil
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatchWeek(int id)
        {
            var value = await _context.MatchWeeks.FindAsync(id);
            if (value == null)
            {
                return NotFound("Silinecek kayıt bulunamadı.");
            }

            _context.MatchWeeks.Remove(value);
            await _context.SaveChangesAsync();
            return Ok("Hafta bilgisi sistemden silindi.");
        }
    }
}