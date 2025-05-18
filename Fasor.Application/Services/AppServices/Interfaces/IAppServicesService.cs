using Fasor.Domain.Aggregates;

namespace Fasor.Application.Services.CompanyServices.Interfaces
{
    public interface IAppServicesService
    {
        Task<IEnumerable<AppService>> GetAllAppServicesAsync();
        Task<AppService> GetAppServiceByIdAsync(Guid id);
        Task<AppService> CreateAppServiceAsync(Guid idCompanyRide, string nameService);
        Task<bool> DeleteAppServiceAsync(Guid id);
        Task<AppService> UpdateAppServiceAsync(Guid idAppService);
        Task<AppService> AppServiceInactiveAsync(Guid id);
        Task<AppService> AppServiceActiveAsync(Guid id);
    }
}
