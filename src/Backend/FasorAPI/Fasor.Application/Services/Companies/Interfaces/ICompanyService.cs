using ErrorOr;
using Fasor.Application.Services.Companies.Dtos;
using Fasor.Domain.Aggregates;

namespace Fasor.Application.Services.Companies.Interfaces
{
    public interface ICompanyService
    {
        Task<ErrorOr<Company>> CreateCompanyAsync(CreateCompanyDto dto);
        Task<ErrorOr<Company>> GetCompanyByIdAsync(Guid id);
        Task<IEnumerable<Company>> GetAllCompaniesAsync();
        Task<ErrorOr<bool>> DeleteCompanyAsync(Guid id);
        Task<ErrorOr<Company>> UpdateCompanyAsync(Guid id);
        Task<ErrorOr<Company>> CompanyInactiveAsync(Guid id);
        Task<ErrorOr<Company>> CompanyActiveAsync(Guid id);
    }
}
