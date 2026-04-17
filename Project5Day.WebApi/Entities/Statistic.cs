namespace Project5Day.WebApi.Entities
{
    public class Statistic
    {
        public int StatisticId { get; set; }
        public int MatchId { get; set; }
        public Match Match { get; set; }

        public string HalfTimeScore { get; set; } // "İY: 1-1" (Görseldeki alan)
        public int HomePossession { get; set; } // Topla oynama %
        public int AwayPossession { get; set; }
    }
}
