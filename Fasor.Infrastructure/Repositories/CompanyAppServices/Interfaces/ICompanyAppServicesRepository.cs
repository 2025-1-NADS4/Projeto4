using Fasor.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fasor.Infrastructure.Repositories.CompanyServices.Interfaces
{
    public interface ICompanyAppServicesRepository
    {
        Task<CompanyAppService> GetCompanyAppServiceByIdAsync(Guid id);
        Task<CompanyAppService> CreateCompanyAppServiceAsync(Guid idCompany, string nameService);
        Task<IEnumerable<CompanyAppService>> GetAllCompanyAppServicesAsync();
        Task<bool> DeleteCompanyServiceAsync(Guid id);
        Task<CompanyAppService> UpdateCompanyAppServiceAsync(CompanyAppService companyService);
        Task<CompanyAppService> CompanyAppServiceInactiveAsync(CompanyAppService companyAppService);
        Task<CompanyAppService> CompanyAppServiceActiveAsync(CompanyAppService companyAppService);

    }
}
