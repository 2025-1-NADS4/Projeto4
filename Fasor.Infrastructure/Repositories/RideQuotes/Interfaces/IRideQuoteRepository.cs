using Fasor.Domain.Aggregates;

namespace Fasor.Infrastructure.Repositories.RideQuotes.Interfaces
{
    public interface IRideQuoteRepository
    {
        Task<RideQuote> GetRideQuoteByIdAsync(Guid id);
        Task<RideQuote> CreateRideQuoteAsync(string originAddress, string destinationAddress, List<RideOption>rideOptions);


    }
}
