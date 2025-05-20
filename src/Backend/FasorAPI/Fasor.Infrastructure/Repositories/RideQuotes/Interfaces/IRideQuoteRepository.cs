using Fasor.Domain.Aggregates;

namespace Fasor.Infrastructure.Repositories.RideQuotes.Interfaces
{
    public interface IRideQuoteRepository
    {
        Task<RideQuote> GetRideQuoteByIdAsync(Guid id);
        Task<RideQuote> CreateRideQuoteAsync
        (string origindAddress,
         string destinationAddress,
         float latitudeOrigin,
         float longitudeOrigin,
         float latitudeDestination,
         float longitudeDestination,
         List<RideOption> rideOptions);


    }
}
