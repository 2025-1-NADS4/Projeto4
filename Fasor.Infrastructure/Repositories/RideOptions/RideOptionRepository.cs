using Fasor.Domain.Aggregates;
using Fasor.Infrastructure.Aggregates.Data;
using Fasor.Infrastructure.Repositories.RideOptions.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fasor.Infrastructure.Repositories.RideOptions
{
    public class RideOptionRepository(AppDbContext _context) : IRideOptionRepository
    {
        public async Task<RideOption> CreateRideOptionAsync(CompanyAppService companyService, DateTime timeRide,
            RideQuote rideQuote, DateTime estimatedTime, string urlRedirect, decimal price)
        {
            var rideOption = new RideOption(companyService, timeRide,
                 rideQuote, estimatedTime, urlRedirect, price);
            var result = await _context.RideOptions.AddAsync(rideOption);
            await _context.SaveChangesAsync();
            return rideOption;
        }

        public async Task<IEnumerable<RideOption>> GetAllRideOptionsByQuoteAsync(Guid idQuote)
        {
            var rideOptions = await _context.RideOptions
                .Include(r => r.CompanyService)
                .Include(r => r.RideQuote)
                .Where(r => r.QuoteId == idQuote)
                .ToListAsync();

            return rideOptions;
        }

        public async Task<RideOption> GetRideOptionByIdAsync(Guid id)
        {
            var rideOption = await _context.RideOptions
                .Include(r => r.CompanyService)
                .Include(r => r.RideQuote)
                .FirstOrDefaultAsync(r => r.Id == id);

            return rideOption;
        }

        public async Task<bool> DeleteRideOptionAsync(Guid rideOptionId)
        {
            var rideOption = await GetRideOptionByIdAsync(rideOptionId);
            if (rideOption == null) return false;

            _context.RideOptions.Remove(rideOption);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
