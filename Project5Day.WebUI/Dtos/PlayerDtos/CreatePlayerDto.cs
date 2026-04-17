namespace Project5Day.WebUI.Dtos.PlayerDtos
{
    public class CreatePlayerDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PlayerImageUrl { get; set; }
        public int KitNumber { get; set; }
        public string Position { get; set; }
        public int TeamId { get; set; }
    }
}
