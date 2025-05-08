using Fasor.Application.Services.CompanyServices.Interfaces;
using Fasor.Domain.Aggregates;
using Fasor.Infrastructure.Repositories.CompanyServices.Interfaces;

namespace Fasor.Application.Services.CompanyServices
{
    public class CompanyAppServicesService(
        ICompanyAppServicesRepository _CompanyAppServicesRepository
        ) : ICompanyAppServicesService
    {
        public async Task<IEnumerable<CompanyAppService>> GetAllCompanyAppServicesAsync()
        {
            var companyAppServices = await _CompanyAppServicesRepository.GetAllCompanyAppServicesAsync();
            return companyAppServices;
        }

        public async Task<CompanyAppService> GetCompanyAppServiceByIdAsync(Guid id)
        {
            var companyAppService = await _CompanyAppServicesRepository.GetCompanyAppServiceByIdAsync(id);
            return companyAppService;
        }

        public async Task<CompanyAppService> CreateCompanyAppServiceAsync(Guid idCompany, string nameService)
        {
            var companyAppService = await _CompanyAppServicesRepository.CreateCompanyAppServiceAsync(idCompany, nameService);
            return companyAppService;
        }

        public async Task<bool> DeleteCompanyAppServiceAsync(Guid id)
        {
            var result = await _CompanyAppServicesRepository.DeleteCompanyServiceAsync(id);
            return result;
        }

        public async Task<CompanyAppService> UpdateCompanyAppServiceAsync(Guid idCompanyAppService)
        {
            var companyAppService = await _CompanyAppServicesRepository.GetCompanyAppServiceByIdAsync(idCompanyAppService);
            var result = await _CompanyAppServicesRepository.UpdateCompanyAppServiceAsync(companyAppService);
            return result;
        }

        public async Task<CompanyAppService> CompanyAppServiceInactiveAsync(Guid id)
        {
            var companyAppService = await _CompanyAppServicesRepository.GetCompanyAppServiceByIdAsync(id);
            if (companyAppService == null) return null;
            var result = await _CompanyAppServicesRepository.CompanyAppServiceInactiveAsync(companyAppService);
            return result;
        }

        public async Task<CompanyAppService> CompanyAppServiceActiveAsync(Guid id)
        {
            var companyAppService = await _CompanyAppServicesRepository.GetCompanyAppServiceByIdAsync(id);
            if (companyAppService == null) return null;
            var result = await _CompanyAppServicesRepository.CompanyAppServiceActiveAsync(companyAppService);
            return result;
        }
    }
}
