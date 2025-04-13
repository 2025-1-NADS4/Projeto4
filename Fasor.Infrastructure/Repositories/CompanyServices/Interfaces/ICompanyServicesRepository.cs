using Fasor.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fasor.Infrastructure.Repositories.CompanyServices.Interfaces
{
    public interface ICompanyServicesRepository
    {
        Task<CompanyService> GetCompanyServiceByIdAsync(Guid id);
        Task<CompanyService> CreateCompanyServiceAsync(Company company, string nameService, decimal baseFare, decimal pricePerKm, decimal pricePerMinute);
        Task<IEnumerable<CompanyService>> GetAllCompanyServicesAsync();
        Task<bool> DeleteCompanyServiceAsync(Guid id);
        Task<CompanyService> UpdateCompanyServiceAsync(CompanyService companyService);
        Task<CompanyService> CompanyServiceInactiveAsync(Guid id);
        Task<CompanyService> CompanyServiceActiveAsync(Guid id);

    }
}
