using Fasor.Domain.Aggregates;

namespace Fasor.Application.Services.RideOptions.Interfaces
{
    public interface IRideOptionService
    {
        Task<RideOption> CreateRideOptionAsync(AppService AppService, DateTime timeRide,
            RideQuote rideQuote, DateTime estimatedTime, string urlRedirect, decimal price);

        Task<IEnumerable<RideOption>> GetAllRideOptionsByQuoteAsync(Guid idQuote);

        Task<RideOption> GetRideOptionByIdAsync(Guid idRideOption);
        Task<bool> DeleteRideOptionByIdAsync(Guid idRideOption);
    }
}
