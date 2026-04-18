namespace Project5Day.WebApi.Entities
{
    public class MatchStatistic
    {
        public int MatchStatisticId { get; set; }

        // --- İLİŞKİ (Bire-Bir) ---
        public int MatchId { get; set; }
        public Match Match { get; set; }

        // --- EV SAHİBİ (Home) VERİLERİ ---
        public int HomePossession { get; set; }     // Örn: 55
        public int HomeShots { get; set; }          // Örn: 14
        public int HomeShotsOnTarget { get; set; }
        public int HomePasses { get; set; }
        public int HomePassAccuracy { get; set; }
        public int HomeCorners { get; set; }
        public int HomeFouls { get; set; }
        public int HomeOffsides { get; set; }

        // --- DEPLASMAN (Away) VERİLERİ ---
        public int AwayPossession { get; set; }     // Örn: 45
        public int AwayShots { get; set; }          // Örn: 10
        public int AwayShotsOnTarget { get; set; }
        public int AwayPasses { get; set; }
        public int AwayPassAccuracy { get; set; }
        public int AwayCorners { get; set; }
        public int AwayFouls { get; set; }
        public int AwayOffsides { get; set; }
    }
}