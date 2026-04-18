using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project5Day.WebApi.Context;
using Project5Day.WebApi.Dtos.MatchEventDtos;
using Project5Day.WebApi.Entities;

namespace Project5Day.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchEventsController : ControllerBase
    {
        private readonly ApiContext _context;

        public MatchEventsController(ApiContext context)
        {
            _context = context;
        }

        // 1. Tüm Olayları Listele (Veya Belirli Bir Maça Ait Olayları)
        [HttpGet]
        public async Task<IActionResult> MatchEventList()
        {
            var values = await _context.MatchEvents
                .Select(x => new ResultMatchEventDto
                {
                    MatchEventId = x.MatchEventId,
                    MatchId = x.MatchId,
                    Minute = x.Minute,
                    Type = x.Type.ToString(), // Enum'ı string'e çeviriyoruz
                    IsHomeTeam = x.IsHomeTeam,
                    PlayerName = x.PlayerName,
                    EventDetail = x.EventDetail,
                    PlayerIn = x.PlayerIn,
                    PlayerOut = x.PlayerOut
                }).ToListAsync();

            return Ok(values);
        }

        // 2. Yeni Maç Olayı (Gol, Kart vb.) Oluştur
        [HttpPost]
        public async Task<IActionResult> CreateMatchEvent(CreateMatchEventDto createMatchEventDto)
        {
            var matchEvent = new MatchEvent
            {
                MatchId = createMatchEventDto.MatchId,
                Minute = createMatchEventDto.Minute,
                Type = (EventType)createMatchEventDto.Type, // int'ten Enum'a cast ediyoruz
                IsHomeTeam = createMatchEventDto.IsHomeTeam,
                PlayerName = createMatchEventDto.PlayerName,
                EventDetail = createMatchEventDto.EventDetail,
                PlayerIn = createMatchEventDto.PlayerIn,
                PlayerOut = createMatchEventDto.PlayerOut
            };

            await _context.MatchEvents.AddAsync(matchEvent);
            await _context.SaveChangesAsync();
            return Ok("Maç olayı başarıyla eklendi.");
        }

        // 3. ID'ye Göre Olay Detayı Getir
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMatchEvent(int id)
        {
            var value = await _context.MatchEvents.FindAsync(id);
            if (value == null) return NotFound("Olay bulunamadı.");

            var dto = new GetByIdMatchEventDto
            {
                MatchEventId = value.MatchEventId,
                MatchId = value.MatchId,
                Minute = value.Minute,
                Type = (int)value.Type,
                IsHomeTeam = value.IsHomeTeam,
                PlayerName = value.PlayerName,
                EventDetail = value.EventDetail,
                PlayerIn = value.PlayerIn,
                PlayerOut = value.PlayerOut
            };

            return Ok(dto);
        }

        // 4. Olay Güncelle
        [HttpPut]
        public async Task<IActionResult> UpdateMatchEvent(UpdateMatchEventDto updateMatchEventDto)
        {
            var value = await _context.MatchEvents.FindAsync(updateMatchEventDto.MatchEventId);
            if (value == null) return NotFound("Güncellenecek olay bulunamadı.");

            value.Minute = updateMatchEventDto.Minute;
            value.Type = (EventType)updateMatchEventDto.Type;
            value.IsHomeTeam = updateMatchEventDto.IsHomeTeam;
            value.PlayerName = updateMatchEventDto.PlayerName;
            value.EventDetail = updateMatchEventDto.EventDetail;
            value.PlayerIn = updateMatchEventDto.PlayerIn;
            value.PlayerOut = updateMatchEventDto.PlayerOut;

            await _context.SaveChangesAsync();
            return Ok("Maç olayı güncellendi.");
        }

        // 5. Olay Sil
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatchEvent(int id)
        {
            var value = await _context.MatchEvents.FindAsync(id);
            if (value == null) return NotFound("Silinecek olay bulunamadı.");

            _context.MatchEvents.Remove(value);
            await _context.SaveChangesAsync();
            return Ok("Maç olayı kaldırıldı.");
        }
    }
}