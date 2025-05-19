using ErrorOr;
using Fasor.Application.Services.Companies.Dtos;
using Fasor.Application.Services.Companies.Interfaces;
using Fasor.Domain.Aggregates;
using Fasor.Infrastructure.Repositories.Companies;
using Fasor.Infrastructure.Repositories.Companies.Interfaces;
using Fasor.Infrastructure.Repositories.CompanyServices.Interfaces;

namespace Fasor.Application.Services.Companies
{
    public class CompanyService(
        ICompanyRepository _CompanyRepository,
        IAppServicesRepository _CompanyAppServicesRepository) : ICompanyService
    {
        public async Task<ErrorOr<Company>> CreateCompanyAsync(CreateCompanyDto dto)
        {
            var result = await _CompanyRepository.CreateCompanyAsync(dto.NameService, dto.Cnpj);

            if (result.IsError) return result;

            var company = result.Value;

            foreach (var rideId in dto.CompanyRideIds)
            {
                var pivot = new CompanyCompanyRide
                {
                    CompanyId = company.Id,
                    CompanyRideId = rideId
                };

                await _CompanyRepository.AddCompanyRideToCompanyAsync(pivot);
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

            company.UpdateDetails(company.NameService, company.Cnpj);

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
