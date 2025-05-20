using Fasor.Domain.Aggregates;
using Fasor.Infrastructure.Data;
using Fasor.Infrastructure.Repositories.RideQuotes.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Fasor.Infrastructure.Repositories.RideQuotes
{
    public class RideQuoteRepository(AppDbContext _context) : IRideQuoteRepository
    {
        public async Task<RideQuote> CreateRideQuoteAsync
        (string origindAddress,
         string destinationAddress,
         float latitudeOrigin,
         float longitudeOrigin,
         float latitudeDestination,
         float longitudeDestination,
         List<RideOption> rideOptions)
        {
    
            var rideQuote = new RideQuote(origindAddress, destinationAddress, latitudeOrigin, longitudeOrigin, latitudeDestination, longitudeDestination, rideOptions);
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
