using Microsoft.EntityFrameworkCore;
using Project5Day.WebApi.Entities;

namespace Project5Day.WebApi.Context
{
    public class ApiContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=NıTRO-AN515-57;initial Catalog=ApiMatchDb;TrustServerCertificate=True;Integrated Security=True");
        }
        // Tablolarımızı tanımlıyoruz
        public DbSet<Team> Teams { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Stadium> Stadiums { get; set; }
        public DbSet<MatchWeek> MatchWeeks { get; set; }
        public DbSet<MatchStatistic> MatchStatistics { get; set; }
        public DbSet<MatchEvent> MatchEvents { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1. Ev Sahibi ve Misafir Takım İlişkisi (Kısıtlamalı Silme)
            modelBuilder.Entity<Match>()
                .HasOne(m => m.HomeTeam)
                .WithMany(t => t.HomeMatches)
                .HasForeignKey(m => m.HomeTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.AwayTeam)
                .WithMany(t => t.AwayMatches)
                .HasForeignKey(m => m.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            // 2. Maç ve İstatistik İlişkisi (Bire-Bir)
            modelBuilder.Entity<Match>()
                .HasOne(m => m.MatchStatistic)
                .WithOne(s => s.Match)
                .HasForeignKey<MatchStatistic>(s => s.MatchId);

            // 3. Maç ve Olaylar İlişkisi (Bire-Çok)
            modelBuilder.Entity<MatchEvent>()
                .HasOne(me => me.Match)
                .WithMany(m => m.MatchEvents)
                .HasForeignKey(me => me.MatchId);
        }
    }
}
