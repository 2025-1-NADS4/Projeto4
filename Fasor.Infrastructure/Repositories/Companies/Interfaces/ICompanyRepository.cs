using Fasor.Domain.Aggregates;
using System.Runtime.InteropServices;

namespace Fasor.Infrastructure.Repositories.Companies.Interfaces
{
    public interface ICompanyRepository
    {
        Task<Company?> GetCompanyByIdAsync(Guid id);
        Task<Company> CreateCompanyAsync(string tradeName, string cnpj, CompanyService? companyService);
        Task<IEnumerable<Company>> GetAllCompaniesAsync();
        Task<bool> DeleteCompanyAsync(Guid id);
        Task<Company> UpdateCompanyAsync(Company company);

        Task<Company> CompanyInactiveAsync(Guid id);
        Task<Company> CompanyActiveAsync(Guid id);
    }
}
