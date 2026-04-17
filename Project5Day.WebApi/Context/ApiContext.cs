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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Ev sahibi maçları için silme kuralını kısıtla
            modelBuilder.Entity<Match>()
                .HasOne(m => m.HomeTeam)
                .WithMany(t => t.HomeMatches)
                .HasForeignKey(m => m.HomeTeamId)
                .OnDelete(DeleteBehavior.Restrict); // Takım silinince maçlar silinmesin

            // Misafir maçları için silme kuralını kısıtla
            modelBuilder.Entity<Match>()
                .HasOne(m => m.AwayTeam)
                .WithMany(t => t.AwayMatches)
                .HasForeignKey(m => m.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
