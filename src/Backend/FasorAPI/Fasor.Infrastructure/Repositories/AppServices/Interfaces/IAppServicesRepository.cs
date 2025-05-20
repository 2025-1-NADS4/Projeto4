using Fasor.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fasor.Infrastructure.Repositories.CompanyServices.Interfaces
{
    public interface IAppServicesRepository
    {
        Task<AppService> GetAppServiceByIdAsync(Guid id);
        Task<AppService> CreateAppServiceAsync(Guid idCompanyRide, string nameService);
        Task<IEnumerable<AppService>> GetAllAppServicesAsync();
        Task<bool> DeleteAppServiceAsync(Guid id);
        Task<AppService> UpdateAppServiceAsync(AppService companyService);
        Task<AppService> AppServiceInactiveAsync(AppService AppService);
        Task<AppService> AppServiceActiveAsync(AppService AppService);

    }
}
