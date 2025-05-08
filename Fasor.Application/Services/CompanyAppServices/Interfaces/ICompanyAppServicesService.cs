using Fasor.Domain.Aggregates;

namespace Fasor.Application.Services.CompanyServices.Interfaces
{
    public interface ICompanyAppServicesService
    {
        Task<IEnumerable<CompanyAppService>> GetAllCompanyAppServicesAsync();
        Task<CompanyAppService> GetCompanyAppServiceByIdAsync(Guid id);
        Task<CompanyAppService> CreateCompanyAppServiceAsync(Guid idCompany, string NameService);
        Task<bool> DeleteCompanyAppServiceAsync(Guid id);
        Task<CompanyAppService> UpdateCompanyAppServiceAsync(Guid idCompanyAppService);
        Task<CompanyAppService> CompanyAppServiceInactiveAsync(Guid id);
        Task<CompanyAppService> CompanyAppServiceActiveAsync(Guid id);
    }
}
