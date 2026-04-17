using Microsoft.EntityFrameworkCore;
using Project5Day.WebApi.Entities;

namespace Project5Day.WebApi.Context
{
    public class ApiContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=NıTRO-AN515-57;initial Catalog=ApiDbProject5;TrustServerCertificate=True;Integrated Security=True");
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<MatchEvent> MatchEvents { get; set; }
        public DbSet<Statistic> Statistics { get; set; }
        public DbSet<FixtureHeader> FixtureHeaders { get; set; }

        // --- İLİŞKİ YAPILANDIRMASI (FLUENT API) ---
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            base.OnModelCreating(modelBuilder);
        }
    }
}
