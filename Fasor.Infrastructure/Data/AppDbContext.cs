using Fasor.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Fasor.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<AppService> AppServices { get; set; }
        public DbSet<CompanyRide> CompanyRides { get; set; }
        public DbSet<RideOption> RideOptions { get; set; }
        public DbSet<RideQuote> RideQuotes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User - Company (muitos para 1)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Company)
                .WithMany(c => c.Users)
                .HasForeignKey(u => u.CompanyId);

            // Company - CompanyCompanyRide (muitos para muitos via entidade de junção)
            modelBuilder.Entity<CompanyCompanyRide>()
                .HasKey(ccr => new { ccr.CompanyId, ccr.CompanyRideId });

            modelBuilder.Entity<CompanyCompanyRide>()
                .HasOne(ccr => ccr.Company)
                .WithMany(c => c.CompanyCompanyRides)
                .HasForeignKey(ccr => ccr.CompanyId);

            modelBuilder.Entity<CompanyCompanyRide>()
                .HasOne(ccr => ccr.CompanyRide)
                .WithMany(cr => cr.CompanyCompanyRides)
                .HasForeignKey(ccr => ccr.CompanyRideId);

            // CompanyRide - AppService (1 para muitos)
            modelBuilder.Entity<AppService>()
                .HasOne(a => a.CompanyRide)
                .WithMany(cr => cr.AppServices)
                .HasForeignKey(a => a.CompanyRideId);

            // RideOption chave primária
            modelBuilder.Entity<RideOption>()
                .HasKey(ro => ro.Id);

            // RideQuote - User (muitos para 1)
            modelBuilder.Entity<RideQuote>()
                .HasOne(rq => rq.User)
                .WithMany(u => u.RideQuotes)
                .HasForeignKey(rq => rq.UserId);

            // RideQuote - RideOptions (1 para muitos)
            modelBuilder.Entity<RideQuote>()
                .HasMany(rq => rq.RideOptions)
                .WithOne(ro => ro.RideQuote)
                .HasForeignKey(ro => ro.QuoteId);

            // Company chave primária
            modelBuilder.Entity<Company>()
                .HasKey(c => c.Id);

            // CompanyRide chave primária
            modelBuilder.Entity<CompanyRide>()
                .HasKey(cr => cr.Id);
        }

    }
}
