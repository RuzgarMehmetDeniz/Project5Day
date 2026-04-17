namespace Project5Day.WebApi.Dtos.PlayerDtos
{
    public class UpdatePlayerDto
    {
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PlayerImageUrl { get; set; }
        public int KitNumber { get; set; }
        public string Position { get; set; }
        public int TeamId { get; set; }
    }
}
