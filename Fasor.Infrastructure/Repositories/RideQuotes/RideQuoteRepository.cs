using Fasor.Domain.Aggregates;
using Fasor.Infrastructure.Aggregates.Data;
using Fasor.Infrastructure.Repositories.RideQuotes.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fasor.Infrastructure.Repositories.RideQuotes
{
    public class RideQuoteRepository(AppDbContext _context) : IRideQuoteRepository
    {
        public async Task<RideQuote> CreateRideQuoteAsync(string originAddress, string destinationAddress, List<RideOption> rideOptions)
        {
            var rideQuote = new RideQuote(originAddress, destinationAddress, rideOptions);
            await _context.RideQuotes.AddAsync(rideQuote);
            await _context.SaveChangesAsync();
            return rideQuote;
        }

        public async Task<RideQuote>GetRideQuoteByIdAsync(Guid id)
        {
            return await _context.RideQuotes
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
