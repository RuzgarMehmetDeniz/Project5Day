namespace Project5Day.WebApi.Dtos.MatchEventDtos
{
    public class CreateMatchEventDto
    {
        public int MatchId { get; set; }
        public int Minute { get; set; }
        public int Type { get; set; } // Enum değeri (1, 2, 3...) olarak gönderilir
        public bool IsHomeTeam { get; set; }
        public string PlayerName { get; set; }
        public string? EventDetail { get; set; }
        public string? PlayerIn { get; set; }
        public string? PlayerOut { get; set; }
    }
}