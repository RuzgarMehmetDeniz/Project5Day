namespace Project5Day.WebApi.Dtos.FixtureHeaderDtos
{
    public class CreateFixtureHeaderDto
    {
        public string LeagueName { get; set; }
        public string LeagueIcon { get; set; }
        public string Title { get; set; }
        public int CurrentWeek { get; set; }
        public string Title1 { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
