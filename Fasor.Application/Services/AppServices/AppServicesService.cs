using Fasor.Application.Services.CompanyServices.Interfaces;
using Fasor.Domain.Aggregates;
using Fasor.Infrastructure.Repositories.CompanyServices.Interfaces;

namespace Fasor.Application.Services.CompanyServices
{
    public class AppServicesService(
        IAppServicesRepository _AppServicesRepository
        ) : IAppServicesService
    {
        public async Task<IEnumerable<AppService>> GetAllAppServicesAsync()
        {
            var AppServices = await _AppServicesRepository.GetAllAppServicesAsync();
            return AppServices;
        }

        public async Task<AppService> GetAppServiceByIdAsync(Guid id)
        {
            var AppService = await _AppServicesRepository.GetAppServiceByIdAsync(id);
            return AppService;
        }

        public async Task<AppService> CreateAppServiceAsync(Guid idCompanyRide, string nameService)
        {
            var AppService = await _AppServicesRepository.CreateAppServiceAsync(idCompanyRide, nameService);
            return AppService;
        }

        public async Task<bool> DeleteAppServiceAsync(Guid id)
        {
            var result = await _AppServicesRepository.DeleteAppServiceAsync(id);
            return result;
        }

        public async Task<AppService> UpdateAppServiceAsync(Guid idAppService)
        {
            var AppService = await _AppServicesRepository.GetAppServiceByIdAsync(idAppService);
            var result = await _AppServicesRepository.UpdateAppServiceAsync(AppService);
            return result;
        }

        public async Task<AppService> AppServiceInactiveAsync(Guid id)
        {
            var AppService = await _AppServicesRepository.GetAppServiceByIdAsync(id);
            if (AppService == null) return null;
            var result = await _AppServicesRepository.AppServiceInactiveAsync(AppService);
            return result;
        }

        public async Task<AppService> AppServiceActiveAsync(Guid id)
        {
            var AppService = await _AppServicesRepository.GetAppServiceByIdAsync(id);
            if (AppService == null) return null;
            var result = await _AppServicesRepository.AppServiceActiveAsync(AppService);
            return result;
        }
    }
}
