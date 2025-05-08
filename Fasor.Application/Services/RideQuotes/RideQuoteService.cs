using Fasor.Application.Services.RideQuotes.Interfaces;
using Fasor.Domain.Aggregates;
using Fasor.Infrastructure.Repositories.RideQuotes.Interfaces;

namespace Fasor.Application.Services.RideQuotes
{
    public class RideQuoteService(
        IRideQuoteRepository _RideQuoteRepository
        ) : IRideQuoteService
    {
        public async Task<RideQuote> CreateRideQuoteAsync(string originAddress, string destinationAddress, List<RideOption> rideOptions)
        {
            var rideQuote = await _RideQuoteRepository.CreateRideQuoteAsync(originAddress, destinationAddress, rideOptions);
            return rideQuote;
        }

        public async Task<RideQuote> GetRideQuoteByIdAsync(Guid id)
        {
            var rideQuote = await _RideQuoteRepository.GetRideQuoteByIdAsync(id);
            return rideQuote;
        }
    }
}
