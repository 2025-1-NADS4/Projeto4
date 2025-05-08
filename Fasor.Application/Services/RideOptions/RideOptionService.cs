using Fasor.Application.Services.RideOptions.Interfaces;
using Fasor.Domain.Aggregates;
using Fasor.Infrastructure.Repositories.RideOptions.Interfaces;

namespace Fasor.Application.Services.RideOptions
{
    public class RideOptionService(
        IRideOptionRepository _RideOptionRepository
        ) : IRideOptionService
    {

        public async Task<RideOption> GetRideOptionByIdAsync(Guid id)
        {
            var rideOption = await _RideOptionRepository.GetRideOptionByIdAsync(id);
            return rideOption;
        }
        public async Task<RideOption> CreateRideOptionAsync(CompanyAppService companyService, DateTime timeRide,
            RideQuote rideQuote, DateTime estimatedTime, string urlRedirect, decimal price)
        {
            var rideOption = await _RideOptionRepository.CreateRideOptionAsync(companyService, timeRide, rideQuote,
                estimatedTime, urlRedirect, price);
            return rideOption;
        }

        public async Task<IEnumerable<RideOption>> GetAllRideOptionsByQuoteAsync(Guid idQuote)
        {
            var rideOptions = await _RideOptionRepository.GetAllRideOptionsByQuoteAsync(idQuote);
            return rideOptions;
        }

        public async Task<bool> DeleteRideOptionByIdAsync(Guid idRideOption)
        {
            var result = await _RideOptionRepository.DeleteRideOptionAsync(idRideOption);
            return result;
        }
    }
}
