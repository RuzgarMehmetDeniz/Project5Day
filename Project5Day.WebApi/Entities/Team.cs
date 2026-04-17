namespace Project5Day.WebApi.Entities
{
    public class Team
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }

        // Bir takımın birden fazla oyuncusu olabilir
        public List<Player> Players { get; set; }

        // Maçlarla Haberleşme: Bir takım birçok maçta "Ev Sahibi" olabilir
        public List<Match> HomeMatches { get; set; }

        // Maçlarla Haberleşme: Bir takım birçok maçta "Misafir" olabilir
        public List<Match> AwayMatches { get; set; }
    }
}
