namespace Project5Day.WebUI.Dtos.MatchEventDtos
{
    public class GetByIdMatchEventDto
    {
        public int MatchEventId { get; set; }
        public int MatchId { get; set; }
        public int Minute { get; set; }
        public int Type { get; set; }
        public bool IsHomeTeam { get; set; }
        public string PlayerName { get; set; }
        public string? EventDetail { get; set; }
        public string? PlayerIn { get; set; }
        public string? PlayerOut { get; set; }
    }
}