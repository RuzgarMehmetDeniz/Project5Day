namespace Project5Day.WebApi.Dtos.MatchEventDtos
{
    public class ResultMatchEventDto
    {
        public int MatchEventId { get; set; }
        public int MatchId { get; set; }
        public int Minute { get; set; }
        public string Type { get; set; } // UI tarafında "Goal", "YellowCard" gibi metin basmak için
        public bool IsHomeTeam { get; set; } // Ev sahibi/Deplasman ayrımı için
        public string PlayerName { get; set; } // Olayı gerçekleştiren oyuncu
        public string? EventDetail { get; set; } // "Kafa Golü", "Foul" vb.
        public string? PlayerIn { get; set; } // Oyuncu değişikliğinde giren
        public string? PlayerOut { get; set; } // Oyuncu değişikliğinde çıkan
    }
}