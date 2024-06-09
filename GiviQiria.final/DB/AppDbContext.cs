using GiviQiria.final.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GiviQiria.final.DB
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public AppDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cat>()
                .HasMany(cat => cat.CatToys)
                .WithOne(toy => toy.ToyOwner)
                .HasForeignKey(toy => toy.ToyOwnerId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Cat> Cats { get; set; }
        public DbSet<Toy> Toys { get; set; }
    }
}
