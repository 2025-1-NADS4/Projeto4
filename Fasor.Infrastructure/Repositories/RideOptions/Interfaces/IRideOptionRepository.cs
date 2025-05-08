using Fasor.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fasor.Infrastructure.Repositories.RideOptions.Interfaces
{
    public interface IRideOptionRepository
    {
        Task<RideOption> GetRideOptionByIdAsync(Guid id);
        Task<IEnumerable<RideOption>> GetAllRideOptionsByQuoteAsync(Guid idQuote);
        Task<RideOption> CreateRideOptionAsync(CompanyAppService companyService, DateTime timeRide, RideQuote rideQuote,
            DateTime estimatedTime, string urlRedirect, decimal price);
        Task<bool> DeleteRideOptionAsync(Guid rideOptionId);
    }
}
