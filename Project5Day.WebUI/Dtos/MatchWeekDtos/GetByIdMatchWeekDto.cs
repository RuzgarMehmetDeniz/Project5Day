namespace Project5Day.WebUI.Dtos.MatchWeekDtos
{
    public class GetByIdMatchWeekDto
    {
        public int MatchWeekId { get; set; }
        public string LeagueIcon { get; set; }
        public string LeagueName { get; set; }
        public string WeekNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
