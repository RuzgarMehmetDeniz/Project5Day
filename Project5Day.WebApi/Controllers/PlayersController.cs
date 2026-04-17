using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project5Day.WebApi.Context;
using Project5Day.WebApi.Dtos.PlayerDtos;
using Project5Day.WebApi.Entities;

namespace Project5Day.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly ApiContext _context;

        public PlayersController(ApiContext context)
        {
            _context = context;
        }

        // 1. Tüm Oyuncuları Takım Adıyla Listele
        [HttpGet]
        public async Task<IActionResult> PlayerList()
        {
            var values = await _context.Players
                .Include(x => x.Team)
                .Select(x => new ResultPlayerDto
                {
                    PlayerId = x.PlayerId,
                    Name = x.Name,
                    Surname = x.Surname,
                    PlayerImageUrl = x.PlayerImageUrl,
                    KitNumber = x.KitNumber,
                    Position = x.Position,
                    TeamName = x.Team.Name
                }).ToListAsync();

            return Ok(values);
        }

        // 2. Yeni Oyuncu Ekle
        [HttpPost]
        public async Task<IActionResult> CreatePlayer(CreatePlayerDto createPlayerDto)
        {
            var player = new Player
            {
                Name = createPlayerDto.Name,
                Surname = createPlayerDto.Surname,
                PlayerImageUrl = createPlayerDto.PlayerImageUrl,
                KitNumber = createPlayerDto.KitNumber,
                Position = createPlayerDto.Position,
                TeamId = createPlayerDto.TeamId
            };

            await _context.Players.AddAsync(player);
            await _context.SaveChangesAsync();
            return Ok("Oyuncu başarıyla kadroya eklendi.");
        }

        // 3. ID'ye Göre Oyuncu Getir
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlayer(int id)
        {
            var value = await _context.Players.FindAsync(id);
            if (value == null)
            {
                return NotFound("Oyuncu bulunamadı.");
            }

            var playerDto = new GetByIdPlayerDto
            {
                PlayerId = value.PlayerId,
                Name = value.Name,
                Surname = value.Surname,
                PlayerImageUrl = value.PlayerImageUrl,
                KitNumber = value.KitNumber,
                Position = value.Position,
                TeamId = value.TeamId
            };

            return Ok(playerDto);
        }

        // 4. Oyuncu Güncelle
        [HttpPut]
        public async Task<IActionResult> UpdatePlayer(UpdatePlayerDto updatePlayerDto)
        {
            var value = await _context.Players.FindAsync(updatePlayerDto.PlayerId);
            if (value == null)
            {
                return NotFound("Güncellenecek oyuncu bulunamadı.");
            }

            value.Name = updatePlayerDto.Name;
            value.Surname = updatePlayerDto.Surname;
            value.PlayerImageUrl = updatePlayerDto.PlayerImageUrl;
            value.KitNumber = updatePlayerDto.KitNumber;
            value.Position = updatePlayerDto.Position;
            value.TeamId = updatePlayerDto.TeamId;

            await _context.SaveChangesAsync();
            return Ok("Oyuncu bilgileri başarıyla güncellendi.");
        }

        // 5. Oyuncu Sil
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            var value = await _context.Players.FindAsync(id);
            if (value == null)
            {
                return NotFound("Silinecek oyuncu bulunamadı.");
            }

            _context.Players.Remove(value);
            await _context.SaveChangesAsync();
            return Ok("Oyuncu başarıyla silindi.");
        }
    }
}