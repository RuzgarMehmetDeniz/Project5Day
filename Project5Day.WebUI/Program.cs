using Project5Day.WebApi.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApiContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();

var app = builder.Build();

// Hata yönetimi yapýlandýrmasý
if (!app.Environment.IsDevelopment())
{
    // Genel uygulama hatalarý (500 vb.) için yönlendirme
    app.UseExceptionHandler("/Error/Index");
    app.UseHsts();
}
else
{
    // Geliþtirme modunda da hata sayfasýný test etmek istersen üstteki satýrý buraya da alabilirsin
    app.UseExceptionHandler("/Error/Index");
}

// 404 Sayfa Bulunamadý gibi durum kodlarý için yönlendirme
app.UseStatusCodePagesWithReExecute("/Error/Index/{0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Match}/{action=Index}/{id?}"); // Ana sayfa isteðine uygun olarak Match/Index yapýldý

app.Run();