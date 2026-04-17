namespace Project5Day.WebApi.Entities
{
    public class Match
    {
        public int MatchId { get; set; }

        // Skorlar
        public string FirstHalfScore { get; set; }  // Örn: "1-0"
        public string SecondHalfScore { get; set; } // Örn: "1-2"
        public string FinalScore { get; set; }      // Örn: "2-2"

        // Zaman ve Hafta
        public string MatchTime { get; set; }       // Örn: "16:30" veya "85'"
        public DateTime MatchDate { get; set; }     // Örn: "2024-09-15"
        public int WeekNumber { get; set; }         // Örn: 1, 2, 3, ...

        // Detaylar
        public string RefereeName { get; set; }     // Örn: "John Doe"
        public int SpectatorCount { get; set; }  // Örn: "50,000"

        // Ev Sahibi Takım
        public int HomeTeamId { get; set; }         
        public Team HomeTeam { get; set; }

        // Misafir Takım
        public int AwayTeamId { get; set; }
        public Team AwayTeam { get; set; }

        // Stadyum
        public int StadiumId { get; set; }
        public Stadium Stadium { get; set; }
    }
}