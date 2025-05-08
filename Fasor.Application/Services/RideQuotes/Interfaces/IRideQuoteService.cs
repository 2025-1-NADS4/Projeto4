using Fasor.Domain.Aggregates;

namespace Fasor.Application.Services.RideQuotes.Interfaces
{
    public interface IRideQuoteService
    {
        Task<RideQuote> CreateRideQuoteAsync(string originAddress, string destinationAddress, List<RideOption> rideOptions);

        Task<RideQuote> GetRideQuoteByIdAsync(Guid id);

    }  
}
