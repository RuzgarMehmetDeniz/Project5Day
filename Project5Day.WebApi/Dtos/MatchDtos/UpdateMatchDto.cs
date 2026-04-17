namespace Project5Day.WebApi.Dtos.MatchDtos
{
    public class UpdateMatchDto
    {
        public int MatchId { get; set; }
        public string FirstHalfScore { get; set; }
        public string SecondHalfScore { get; set; }
        public string FinalScore { get; set; }
        public string MatchTime { get; set; }
        public DateTime MatchDate { get; set; }
        public int WeekNumber { get; set; }
        public string RefereeName { get; set; }
        public int SpectatorCount { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public int StadiumId { get; set; }
        public string MatchStatus { get; set; }
    }
}
