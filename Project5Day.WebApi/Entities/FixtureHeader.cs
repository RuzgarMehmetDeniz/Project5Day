namespace Project5Day.WebApi.Entities
{
    public class FixtureHeader
    {
        public int FixtureHeaderId { get; set; }

        // 1. Sarı Rozet Alanı
        public string LeagueName { get; set; }  // "PREMİER LEAGUE"
        public string LeagueIcon { get; set; }  // Kupa ikonunun dosya yolu

        // 2. Ana Başlık Alanı
        public string Title { get; set; }
        public int CurrentWeek { get; set; }    // 33 (Yeşil Rakam)
        public string Title1 { get; set; }       // "Hafta Sonuçları" (Statik kelimeler)

        // 3. Alt Bilgi Satırı
        public DateTime StartDate { get; set; } // 11 Nisan
        public DateTime EndDate { get; set; }   // 13 Nisan
    }
}
