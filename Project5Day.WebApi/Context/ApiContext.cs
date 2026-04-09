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
    }
}
