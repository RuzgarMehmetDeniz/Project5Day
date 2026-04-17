namespace Project5Day.WebApi.Entities
{
    public class MatchWeek
    {
        public int MatchWeekId { get; set; }
        public string LeagueIcon { get; set; }
        public string LeagueName { get; set; }
        public string WeekNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
