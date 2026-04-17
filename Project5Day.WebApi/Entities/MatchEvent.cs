namespace Project5Day.WebApi.Entities
{
    public class MatchEvent
    {
        public int MatchEventId { get; set; }
        public int MatchId { get; set; } // Hangi maçta oldu?
        public Match Match { get; set; }

        public string ActionType { get; set; } // Gol, Sarı Kart, Kırmızı Kart
        public string Description { get; set; } // "Golü atan oyuncu: Bukayo Saka"
        public int Minute { get; set; } // Kaçıncı dakika?
    }
}
