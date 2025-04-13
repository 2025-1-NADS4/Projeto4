using Fasor.Domain.Aggregates;
using Fasor.Infrastructure.Aggregates.Data;
using Fasor.Infrastructure.Repositories.Companies.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fasor.Infrastructure.Repositories.Companies
{
    public class CompanyRepository(AppDbContext _context) : ICompanyRepository
    {
        public async Task<Company?> GetCompanyByIdAsync(Guid id)
        {
            return await _context.Companies
                .Include(c => c.CompanyServices)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Company> CreateCompanyAsync(string tradeName, string cnpj, CompanyService? companyService)
        {
            var company = new Company(tradeName, cnpj, companyService);
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
            return company;
        }

        public async Task<IEnumerable<Company>> GetAllCompaniesAsync()
        {
            return await _context.Companies
                .Include(c => c.CompanyServices)
                .ToListAsync();
        }

        public async Task<bool> DeleteCompanyAsync(Guid id)
        {
            var company = await GetCompanyByIdAsync(id);
            if (company == null) return false;

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Company> UpdateCompanyAsync(Company company)
        {
            _context.Companies.Update(company);
            await _context.SaveChangesAsync();
            return company;
        }
        public async Task<Company> CompanyInactiveAsync(Guid id)
        {
            var company = await GetCompanyByIdAsync(id);
            if (company == null) return null;

            company.Inactivate();
            await _context.SaveChangesAsync();
            return company;
        }
        public async Task<Company> CompanyActiveAsync(Guid id)
        {
            var company = await GetCompanyByIdAsync(id);
            if (company == null) return null;

            company.Activate();
            await _context.SaveChangesAsync();
            return company;
        }
    }
}