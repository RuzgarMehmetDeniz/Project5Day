using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project5Day.WebUI.Dtos.StadiumDtos;
using System.Text;

namespace Project5Day.WebUI.Controllers
{
    public class AdminStadiumController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminStadiumController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // 1. Stadyum Listesi
        // 1. Stadyum Listesi - Sayfalama Controller İçinde Yapılıyor
        public async Task<IActionResult> Index(int page = 1)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7201/api/Stadiums");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultStadiumDto>>(jsonData);

                // Sayfalama Ayarları
                int pageSize = 5; // Her sayfada 5 kayıt
                int totalCount = values.Count;

                // Veriyi dilimle (Örn: 2. sayfa için ilk 5'i atla, sonraki 5'i al)
                var pagedData = values
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                // View tarafında butonları oluşturmak için gerekli bilgiler
                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                return View(pagedData);
            }
            return View();
        }

        // 2. Yeni Stadyum Ekleme (Sayfayı Açma)
        [HttpGet]
        public IActionResult CreateStadium()
        {
            return View();
        }

        // 2. Yeni Stadyum Ekleme (Kaydetme)
        [HttpPost]
        public async Task<IActionResult> CreateStadium(CreateStadiumDto createStadiumDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createStadiumDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7201/api/Stadiums", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        // 3. Stadyum Silme
        public async Task<IActionResult> DeleteStadium(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7201/api/Stadiums/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        // 4. Stadyum Güncelleme (Verileri Getirme)
        [HttpGet]
        public async Task<IActionResult> UpdateStadium(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7201/api/Stadiums/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateStadiumDto>(jsonData);
                return View(values);
            }
            return View();
        }

        // 4. Stadyum Güncelleme (Değişiklikleri Kaydetme)
        [HttpPost]
        public async Task<IActionResult> UpdateStadium(UpdateStadiumDto updateStadiumDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateStadiumDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7201/api/Stadiums/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}