using Microsoft.AspNetCore.Mvc;
using Project5Day.WebApi.Context;
using Project5Day.WebApi.Dtos.TeamDtos;
using Project5Day.WebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace Project5Day.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ApiContext _context;

        public TeamsController(ApiContext context)
        {
            _context = context;
        }

        // 1. Tüm Takımları Getir
        [HttpGet]
        public async Task<IActionResult> TeamList()
        {
            var values = await _context.Teams
                .Select(x => new ResultTeamDto
                {
                    TeamId = x.TeamId,
                    Name = x.Name,
                    Logo = x.Logo
                }).ToListAsync();

            return Ok(values);
        }

        // 2. Yeni Takım Ekle
        [HttpPost]
        public async Task<IActionResult> CreateTeam(CreateTeamDto createTeamDto)
        {
            var team = new Team
            {
                Name = createTeamDto.Name,
                Logo = createTeamDto.Logo
            };

            await _context.Teams.AddAsync(team);
            await _context.SaveChangesAsync();

            return Ok("Takım başarıyla eklendi");
        }

        // 3. ID'ye Göre Takım Getir
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeam(int id)
        {
            var value = await _context.Teams.FindAsync(id);
            if (value == null)
            {
                return NotFound("Takım bulunamadı");
            }

            var teamDto = new GetByIdTeamDto
            {
                TeamId = value.TeamId,
                Name = value.Name,
                Logo = value.Logo
            };

            return Ok(teamDto);
        }

        // 4. Takım Güncelle
        [HttpPut]
        public async Task<IActionResult> UpdateTeam(UpdateTeamDto updateTeamDto)
        {
            var value = await _context.Teams.FindAsync(updateTeamDto.TeamId);
            if (value == null)
            {
                return NotFound("Güncellenecek takım bulunamadı");
            }

            value.Name = updateTeamDto.Name;
            value.Logo = updateTeamDto.Logo;

            await _context.SaveChangesAsync();
            return Ok("Takım başarıyla güncellendi");
        }

        // 5. Takım Sil
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            var value = await _context.Teams.FindAsync(id);
            if (value == null)
            {
                return NotFound("Silinecek takım bulunamadı");
            }

            _context.Teams.Remove(value);
            await _context.SaveChangesAsync();
            return Ok("Takım başarıyla silindi");
        }
    }
}