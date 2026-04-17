using Microsoft.AspNetCore.Mvc;
using Project5Day.WebApi.Context;
using Project5Day.WebApi.Entities;
using Project5Day.WebApi.Dtos.TeamDtos; // DTO'ların bulunduğu namespace

namespace Project5Day.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ApiContext _context;

        public TeamController(ApiContext context)
        {
            _context = context;
        }

        // 1. Tüm Takımları Listele
        [HttpGet]
        public IActionResult TeamList()
        {
            var values = _context.Teams.ToList();

            // Entity listesini Result DTO listesine çeviriyoruz
            var result = values.Select(x => new ResultTeamDto
            {
                TeamId = x.TeamId,
                Name = x.Name,
                LogoUrl = x.LogoUrl
            }).ToList();

            return Ok(result);
        }

        // 2. Yeni Takım Ekle
        [HttpPost]
        public IActionResult CreateTeam(CreateTeamDto createTeamDto)
        {
            var team = new Team
            {
                Name = createTeamDto.Name,
                LogoUrl = createTeamDto.LogoUrl
            };

            _context.Teams.Add(team);
            _context.SaveChanges();
            return Ok("Takım Başarıyla Eklendi");
        }

        // 3. Takımı Sil
        [HttpDelete("{id}")]
        public IActionResult DeleteTeam(int id)
        {
            var value = _context.Teams.Find(id);
            if (value == null) return NotFound("Takım bulunamadı.");

            _context.Teams.Remove(value);
            _context.SaveChanges();
            return Ok("Takım Silindi");
        }

        // 4. Takımı Güncelle
        [HttpPut]
        public IActionResult UpdateTeam(UpdateTeamDto updateTeamDto)
        {
            var value = _context.Teams.Find(updateTeamDto.TeamId);
            if (value == null) return NotFound("Güncellenecek takım bulunamadı.");

            value.Name = updateTeamDto.Name;
            value.LogoUrl = updateTeamDto.LogoUrl;

            _context.SaveChanges();
            return Ok("Takım Bilgisi Güncellendi");
        }

        // 5. ID'ye Göre Takım Getir
        [HttpGet("GetTeam")]
        public IActionResult GetTeam(int id)
        {
            var value = _context.Teams.Find(id);
            if (value == null) return NotFound();

            var result = new GetByIdTeamDto
            {
                TeamId = value.TeamId,
                Name = value.Name,
                LogoUrl = value.LogoUrl
            };

            return Ok(result);
        }
    }
}