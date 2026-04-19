using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Project5Day.WebUI.Dtos.PlayerDtos;
using Project5Day.WebUI.Dtos.TeamDtos;
using System.Text;

namespace Project5Day.WebUI.Controllers
{
    public class AdminPlayerController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminPlayerController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // 1. Oyuncuları Listeleme
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7201/api/Players");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultPlayerDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        // 2. Yeni Oyuncu Ekleme (Sayfayı Açma)
        [HttpGet]
        public async Task<IActionResult> CreatePlayer()
        {
            // Oyuncu eklerken takımları seçtirebilmek için takım listesini çekiyoruz
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7201/api/Teams");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultTeamDto>>(jsonData);

            List<SelectListItem> teamList = (from x in values
                                             select new SelectListItem
                                             {
                                                 Text = x.Name,
                                                 Value = x.TeamId.ToString()
                                             }).ToList();
            ViewBag.Teams = teamList;
            return View();
        }

        // 2. Yeni Oyuncu Ekleme (Kaydetme)
        [HttpPost]
        public async Task<IActionResult> CreatePlayer(CreatePlayerDto createPlayerDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createPlayerDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7201/api/Players", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        // 3. Oyuncu Silme
        public async Task<IActionResult> DeletePlayer(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7201/api/Players/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        // 4. Oyuncu Güncelleme (Verileri Getirme)
        [HttpGet]
        public async Task<IActionResult> UpdatePlayer(int id)
        {
            // Takım listesini dropdown için çekiyoruz
            var client = _httpClientFactory.CreateClient();
            var teamResponseMessage = await client.GetAsync("https://localhost:7201/api/Teams");
            var teamJsonData = await teamResponseMessage.Content.ReadAsStringAsync();
            var teams = JsonConvert.DeserializeObject<List<ResultTeamDto>>(teamJsonData);

            List<SelectListItem> teamList = (from x in teams
                                             select new SelectListItem
                                             {
                                                 Text = x.Name,
                                                 Value = x.TeamId.ToString()
                                             }).ToList();
            ViewBag.Teams = teamList;

            // Güncellenecek oyuncu verisini çekiyoruz
            var responseMessage = await client.GetAsync($"https://localhost:7201/api/Players/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdatePlayerDto>(jsonData);
                return View(values);
            }
            return View();
        }

        // 5. Oyuncu Güncelleme (Kaydetme)
        [HttpPost]
        public async Task<IActionResult> UpdatePlayer(UpdatePlayerDto updatePlayerDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updatePlayerDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7201/api/Players", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}