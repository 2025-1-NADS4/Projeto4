using ErrorOr;
using Fasor.Domain.Aggregates;
using System.Runtime.InteropServices;

namespace Fasor.Infrastructure.Repositories.Companies.Interfaces
{
    public interface ICompanyRepository
    {
        Task <ErrorOr<Company?>> GetCompanyByIdAsync(Guid id);
        Task <ErrorOr<Company>> CreateCompanyAsync(string tradeName, string cnpj);
        Task AddCompanyRideToCompanyAsync(CompanyCompanyRide companyRide);
        Task<IEnumerable<Company>> GetAllCompaniesAsync();
        Task <ErrorOr<bool>> DeleteCompanyAsync(Guid id);
        Task <ErrorOr<Company>> UpdateCompanyAsync(Company company);
        Task <ErrorOr<Company>> CompanyInactiveAsync(Guid id);
        Task <ErrorOr<Company>> CompanyActiveAsync(Guid id);
    }
}
