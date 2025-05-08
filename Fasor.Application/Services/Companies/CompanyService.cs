using ErrorOr;
using Fasor.Application.Services.Companies.Dtos;
using Fasor.Application.Services.Companies.Interfaces;
using Fasor.Domain.Aggregates;
using Fasor.Infrastructure.Repositories.Companies.Interfaces;
using Fasor.Infrastructure.Repositories.CompanyServices.Interfaces;

namespace Fasor.Application.Services.Companies
{
    public class CompanyService(
        ICompanyRepository _CompanyRepository,
        ICompanyAppServicesRepository _CompanyAppServicesRepository) : ICompanyService
    {
        public async Task<ErrorOr<Company>> CreateCompanyAsync(CreateCompanyDto dto)
        {
            var result = await _CompanyRepository.CreateCompanyAsync(dto.TradeName, dto.Cnpj);

            if (result.IsError) return result;

            var company = result.Value;

            if (dto.CompanyAppService is not null && dto.CompanyAppService.Any())
            {
                foreach (var item in dto.CompanyAppService)
                {
                    var serviceResult = CompanyAppService.Create(company.Id, item.NameService);
                    if (serviceResult.IsError) return serviceResult.Errors;
                    await _CompanyAppServicesRepository.CreateCompanyAppServiceAsync(company.Id, item.NameService);
                }
            }

            return company;
        }

        public async Task<ErrorOr<Company>> GetCompanyByIdAsync(Guid id)
        {
            var company = await _CompanyRepository.GetCompanyByIdAsync(id);
            if (company.IsError) return company.Errors;

            return company;
        }

        public async Task<IEnumerable<Company>> GetAllCompaniesAsync()
        {
            var companies = await _CompanyRepository.GetAllCompaniesAsync();
            return companies;
        }

        public async Task<ErrorOr<bool>> DeleteCompanyAsync(Guid id)
        {
            var result = await _CompanyRepository.DeleteCompanyAsync(id);
            if (result.IsError) return result.Errors;

            return result;
        }

        public async Task<ErrorOr<Company>> UpdateCompanyAsync(Guid id)
        {
            var result = await GetCompanyByIdAsync(id);

            if (result.IsError) return result.Errors;

            var company = result.Value;

            company.UpdateDetails(company.TradeName, company.Cnpj);

            await _CompanyRepository.UpdateCompanyAsync(company);

            return company;
        }

        public async Task<ErrorOr<Company>> CompanyInactiveAsync(Guid id)
        {
            var result = await _CompanyRepository.CompanyInactiveAsync(id);
            if (result.IsError) return result.Errors;

            return result;
        }

        public async Task<ErrorOr<Company>> CompanyActiveAsync(Guid id)
        {
            var result = await _CompanyRepository.CompanyActiveAsync(id);
            if (result.IsError) return result.Errors;

            return result;
        }
    }
}
