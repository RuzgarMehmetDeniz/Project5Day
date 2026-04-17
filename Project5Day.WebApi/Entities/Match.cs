namespace Project5Day.WebApi.Entities
{
    public class Match
    {
        public int MatchId { get; set; }

        // Takım İlişkileri
        public int HomeTeamId { get; set; }
        public Team HomeTeam { get; set; }

        public int AwayTeamId { get; set; }
        public Team AwayTeam { get; set; }

        // Skorlar
        public int? HomeScore { get; set; }
        public int? AwayScore { get; set; }

        // Zaman ve Mekan
        public DateTime MatchDate { get; set; }
        public string Stadium { get; set; }

        // Durum (1: Oynanmadı, 2: Canlı, 3: Bitti)
        public int Status { get; set; }

        // Teknik Detaylar
        public int Week { get; set; }
        public int? LiveMinute { get; set; }
        public bool IsFeatured { get; set; }

        // Haberleşme Alanları (Diğer tabloları bu isimlerle çağıracaksın)
        public List<MatchEvent> MatchEvents { get; set; }
        public Statistic Statistic { get; set; }
    }
}