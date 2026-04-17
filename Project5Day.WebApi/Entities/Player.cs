namespace Project5Day.WebApi.Entities
{
    public class Player
    {
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; } 
        public string PlayerImageUrl { get; set; } // Oyuncu fotoğrafı
        public int KitNumber { get; set; } // Forma numarası
        public string Position { get; set; } // Oyuncu pozisyonu (örneğin: Forvet, Orta Saha, Defans, Kaleci)

        // Her oyuncunun bir takımı olur (Foreign Key)
        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}
