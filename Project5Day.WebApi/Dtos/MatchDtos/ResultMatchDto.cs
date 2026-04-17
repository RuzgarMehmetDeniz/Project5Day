namespace Project5Day.WebApi.Dtos.MatchDtos
{
    public class ResultMatchDto
    {
        public int MatchId { get; set; }
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
        public string HomeTeamLogo { get; set; }
        public string AwayTeamLogo { get; set; }
        public string StadiumName { get; set; }
        public string FinalScore { get; set; }
        public string MatchTime { get; set; }
        public DateTime MatchDate { get; set; }
        public int WeekNumber { get; set; }
        public string MatchStatus { get; set; }
    }
}
