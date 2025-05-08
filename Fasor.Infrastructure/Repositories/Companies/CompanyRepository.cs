using ErrorOr;
using Fasor.Domain.Aggregates;
using Fasor.Domain.Shared.Errors;
using Fasor.Infrastructure.Aggregates.Data;
using Fasor.Infrastructure.Repositories.Companies.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fasor.Infrastructure.Repositories.Companies
{
    public class CompanyRepository(AppDbContext _context) : ICompanyRepository
    {
        public async Task<ErrorOr<Company?>> GetCompanyByIdAsync(Guid id)
        {
            var company = await _context.Companies
                .Include(c => c.CompanyAppServices)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (company == null) return CompanyErrors.CompanyNotFound;

            return company;
        }

        public async Task<ErrorOr<Company>> CreateCompanyAsync(string tradeName, string cnpj)
        {
            var result = Company.Create(tradeName, cnpj);

            if (result.IsError) return result.Errors;

            var company = result.Value;

            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
            return company;
        }

        public async Task<IEnumerable<Company>> GetAllCompaniesAsync()
        {
            return await _context.Companies
                .Include(c => c.CompanyAppServices)
                .ToListAsync();
        }

        public async Task<ErrorOr<bool>> DeleteCompanyAsync(Guid id)
        {
            var result = await GetCompanyByIdAsync(id);

            if (result.IsError) return result.Errors;

            var company = result.Value;

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<ErrorOr<Company>> CompanyInactiveAsync(Guid id)
        {
            var result = await GetCompanyByIdAsync(id);

            if (result.IsError) return result.Errors;     

            var company = result.Value;

            company.Inactivate();
            await _context.SaveChangesAsync();
            return company;
        }
        public async Task <ErrorOr<Company>> CompanyActiveAsync(Guid id)
        {
            var result = await GetCompanyByIdAsync(id); 

            if (result.IsError) return result.Errors;

            var company = result.Value;

            company.Activate();
            await _context.SaveChangesAsync();
            return company;
        }

        public async Task<ErrorOr<Company>> UpdateCompanyAsync(Company company)
        { 
           _context.Companies.Update(company);
            await _context.SaveChangesAsync();
            return company;
        }  
    }
}