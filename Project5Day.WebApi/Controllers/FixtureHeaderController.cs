using Microsoft.AspNetCore.Mvc;
using Project5Day.WebApi.Context;
using Project5Day.WebApi.Entities;
using Project5Day.WebApi.Dtos.FixtureHeaderDtos; // DTO'ların olduğu namespace

namespace Project5Day.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FixtureHeaderController : ControllerBase
    {
        private readonly ApiContext _context;

        public FixtureHeaderController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult FixtureHeaderList()
        {
            var values = _context.FixtureHeaders.ToList();

            // Entity'den Result DTO'ya Manuel Mapping
            var result = values.Select(x => new ResultFixtureHeaderDto
            {
                FixtureHeaderId = x.FixtureHeaderId,
                LeagueName = x.LeagueName,
                LeagueIcon = x.LeagueIcon,
                Title = x.Title,
                CurrentWeek = x.CurrentWeek,
                Title1 = x.Title1,
                StartDate = x.StartDate,
                EndDate = x.EndDate
            }).ToList();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateFixtureHeader(CreateFixtureHeaderDto createDto)
        {
            // DTO'dan gelen veriyi Entity'ye çeviriyoruz
            var fixtureHeader = new FixtureHeader
            {
                LeagueName = createDto.LeagueName,
                LeagueIcon = createDto.LeagueIcon,
                Title = createDto.Title,
                CurrentWeek = createDto.CurrentWeek,
                Title1 = createDto.Title1,
                StartDate = createDto.StartDate,
                EndDate = createDto.EndDate
            };

            _context.FixtureHeaders.Add(fixtureHeader);
            _context.SaveChanges();
            return Ok("Başlık Bilgisi Eklendi");
        }

        [HttpDelete]
        public IActionResult DeleteFixtureHeader(int id)
        {
            var value = _context.FixtureHeaders.Find(id);
            if (value == null) return NotFound("Veri bulunamadı.");

            _context.FixtureHeaders.Remove(value);
            _context.SaveChanges();
            return Ok("Başlık Bilgisi Silindi");
        }

        [HttpPut]
        public IActionResult UpdateFixtureHeader(UpdateFixtureHeaderDto updateDto)
        {
            var value = _context.FixtureHeaders.Find(updateDto.FixtureHeaderId);
            if (value == null) return NotFound("Güncellenecek veri bulunamadı.");

            // Entity alanlarını DTO'dan gelenlerle güncelliyoruz
            value.LeagueName = updateDto.LeagueName;
            value.LeagueIcon = updateDto.LeagueIcon;
            value.Title = updateDto.Title;
            value.CurrentWeek = updateDto.CurrentWeek;
            value.Title1 = updateDto.Title1;
            value.StartDate = updateDto.StartDate;
            value.EndDate = updateDto.EndDate;

            _context.Update(value);
            _context.SaveChanges();
            return Ok("Başlık Bilgisi Güncellendi");
        }

        [HttpGet("GetFixtureHeader")]
        public IActionResult GetFixtureHeader(int id)
        {
            var value = _context.FixtureHeaders.Find(id);
            if (value == null) return NotFound();

            // Entity'den GetById DTO'ya çevirme
            var result = new GetByIdFixtureHeaderDto
            {
                FixtureHeaderId = value.FixtureHeaderId,
                LeagueName = value.LeagueName,
                LeagueIcon = value.LeagueIcon,
                Title = value.Title,
                CurrentWeek = value.CurrentWeek,
                Title1 = value.Title1,
                StartDate = value.StartDate,
                EndDate = value.EndDate
            };

            return Ok(result);
        }
    }
}