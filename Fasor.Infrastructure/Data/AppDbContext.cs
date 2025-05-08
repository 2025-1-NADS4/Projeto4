using Fasor.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Fasor.Infrastructure.Aggregates.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyAppService> CompanyServices { get; set; }
        public DbSet<RideOption> RideOptions { get; set; }
        public DbSet<RideQuote> RideQuotes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<Company>().HasKey(u => u.Id);
            modelBuilder.Entity<CompanyAppService>().HasKey(u => u.Id);
            modelBuilder.Entity<RideOption>().HasKey(u => u.Id);
            modelBuilder.Entity<RideQuote>().HasKey(u => u.Id);

        }
    }
}
