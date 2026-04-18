namespace Project5Day.WebApi.Entities
{
    public enum EventType
    {
        Goal = 1,
        YellowCard = 2,
        RedCard = 3,
        Substitution = 4
    }

    public class MatchEvent
    {
        public int MatchEventId { get; set; }

        // --- İLİŞKİ ---
        public int MatchId { get; set; }
        public Match Match { get; set; }

        public int Minute { get; set; }             // Olay dakikası: 12', 78' vb.
        public EventType Type { get; set; }         // Gol mü, Kart mı?
        public bool IsHomeTeam { get; set; }        // true: Ev Sahibi, false: Deplasman

        // --- OYUNCU BİLGİSİ ---
        public string PlayerName { get; set; }      // Olayı yapan oyuncu (Saka, Rice)
        public string? EventDetail { get; set; }    // "Kafa Golü", "Penaltı", "Sert Müdahale"

        // --- DEĞİŞİKLİK ÖZEL ALANLARI ---
        public string? PlayerIn { get; set; }       // Giren Oyuncu (Trossard ↑)
        public string? PlayerOut { get; set; }      // Çıkan Oyuncu (Martinelli ↓)
    }
}