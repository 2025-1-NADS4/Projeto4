using Fasor.Domain.Aggregates;

namespace Fasor.Application.Services.RideQuotes.Interfaces
{
    public interface IRideQuoteService
    {
        Task<RideQuote> CreateRideQuoteAsync
           (string origindAddress,
           string destinationAddress,
           float latitudeOrigin,
           float longitudeOrigin,
           float latitudeDestination,
           float longitudeDestination,
           List<RideOption> rideOptions);

        Task<RideQuote> GetRideQuoteByIdAsync(Guid id);

    }  
}
