namespace Project5Day.WebApi.Entities
{
    public class Team
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
        public string LogoUrl { get; set; }

        public List<Match> HomeMatches { get; set; }
        public List<Match> AwayMatches { get; set; }
    }
}