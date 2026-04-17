namespace Project5Day.WebApi.Dtos.MatchWeekDtos
{
    public class CreateMatchWeekDto
    {
        public string LeagueIcon { get; set; }
        public string LeagueName { get; set; }
        public string WeekNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
