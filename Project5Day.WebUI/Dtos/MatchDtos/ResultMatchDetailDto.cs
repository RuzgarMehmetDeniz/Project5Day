namespace Project5Day.WebUI.Dtos.MatchDtos
{
    public class ResultMatchDetailDto
    {
        public int MatchId { get; set; }
        public string FinalScore { get; set; }
        public string FirstHalfScore { get; set; }
        public string MatchTime { get; set; }
        public DateTime MatchDate { get; set; }
        public int WeekNumber { get; set; }
        public int SpectatorCount { get; set; }
        public string RefereeName { get; set; }
        public string MatchStatus { get; set; }

        // Takımlar
        public string HomeTeamName { get; set; }
        public string HomeTeamLogo { get; set; }
        public string AwayTeamName { get; set; }
        public string AwayTeamLogo { get; set; }

        // Stadyum
        public string StadiumName { get; set; }
        public string StadiumCity { get; set; }

        // Lig Bilgileri (MatchWeek tablosundan gelecek)
        public string LeagueName { get; set; }
        public string LeagueIcon { get; set; }
    }
}