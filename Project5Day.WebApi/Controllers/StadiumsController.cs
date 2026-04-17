using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project5Day.WebApi.Context;
using Project5Day.WebApi.Dtos.StadiumDtos;
using Project5Day.WebApi.Entities;

namespace Project5Day.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StadiumsController : ControllerBase
    {
        private readonly ApiContext _context;

        public StadiumsController(ApiContext context)
        {
            _context = context;
        }

        // 1. Tüm Stadyumları Listele
        [HttpGet]
        public async Task<IActionResult> StadiumList()
        {
            var values = await _context.Stadiums
                .Select(x => new ResultStadiumDto
                {
                    StadiumId = x.StadiumId,
                    Name = x.Name,
                    City = x.City,
                    Capacity = x.Capacity,
                    ImageUrl = x.ImageUrl
                }).ToListAsync();

            return Ok(values);
        }

        // 2. Yeni Stadyum Ekle
        [HttpPost]
        public async Task<IActionResult> CreateStadium(CreateStadiumDto createStadiumDto)
        {
            var stadium = new Stadium
            {
                Name = createStadiumDto.Name,
                City = createStadiumDto.City,
                Capacity = createStadiumDto.Capacity,
                ImageUrl = createStadiumDto.ImageUrl
            };

            await _context.Stadiums.AddAsync(stadium);
            await _context.SaveChangesAsync();
            return Ok("Stadyum başarıyla sisteme kaydedildi.");
        }

        // 3. ID'ye Göre Stadyum Getir
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStadium(int id)
        {
            var value = await _context.Stadiums.FindAsync(id);
            if (value == null)
            {
                return NotFound("Stadyum bulunamadı.");
            }

            var stadiumDto = new GetByIdStadiumDto
            {
                StadiumId = value.StadiumId,
                Name = value.Name,
                City = value.City,
                Capacity = value.Capacity,
                ImageUrl = value.ImageUrl
            };

            return Ok(stadiumDto);
        }

        // 4. Stadyum Bilgilerini Güncelle
        [HttpPut]
        public async Task<IActionResult> UpdateStadium(UpdateStadiumDto updateStadiumDto)
        {
            var value = await _context.Stadiums.FindAsync(updateStadiumDto.StadiumId);
            if (value == null)
            {
                return NotFound("Güncellenecek stadyum bulunamadı.");
            }

            value.Name = updateStadiumDto.Name;
            value.City = updateStadiumDto.City;
            value.Capacity = updateStadiumDto.Capacity;
            value.ImageUrl = updateStadiumDto.ImageUrl;

            await _context.SaveChangesAsync();
            return Ok("Stadyum bilgileri başarıyla güncellendi.");
        }

        // 5. Stadyum Sil
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStadium(int id)
        {
            var value = await _context.Stadiums.FindAsync(id);
            if (value == null)
            {
                return NotFound("Silinecek stadyum bulunamadı.");
            }

            _context.Stadiums.Remove(value);
            await _context.SaveChangesAsync();
            return Ok("Stadyum sistemden silindi.");
        }
    }
}