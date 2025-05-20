using Fasor.Domain.Aggregates;

namespace Fasor.Infrastructure.Repositories.RideOptions.Interfaces
{
    public interface IRideOptionRepository
    {
        Task<RideOption> GetRideOptionByIdAsync(Guid id);
        Task<IEnumerable<RideOption>> GetAllRideOptionsByQuoteAsync(Guid idQuote);
        Task<RideOption> CreateRideOptionAsync(AppService companyService, RideQuote rideQuote,
        decimal price);
        Task<bool> DeleteRideOptionAsync(Guid rideOptionId);
    }
}
