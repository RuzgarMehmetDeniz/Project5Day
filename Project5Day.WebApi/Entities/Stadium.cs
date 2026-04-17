namespace Project5Day.WebApi.Entities
{
    public class Stadium
    {
        public int StadiumId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public int Capacity { get; set; }
        public string ImageUrl { get; set; }
        public List<Match> Matches { get; set; }
    }
}
