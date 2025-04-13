using Fasor.Domain.Aggregates;
using Fasor.Infrastructure.Aggregates.Data;
using Fasor.Infrastructure.Repositories.CompanyServices.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fasor.Infrastructure.Repositories.CompanyServices
{
    public class CompanyServicesRepository(AppDbContext _context) : ICompanyServicesRepository
    {
        public async Task<CompanyService> GetCompanyServiceByIdAsync(Guid id)
        {
            var companyService = await _context.CompanyServices
                .Include(c => c.Company)
                .FirstOrDefaultAsync(c => c.Id == id);

            return companyService;
        }

        public async Task<IEnumerable<CompanyService>> GetAllCompanyServicesAsync()
        {
            var companyServices = await _context.CompanyServices
                .Include(c => c.Company)
                .ToListAsync();

            return companyServices;
        }

        public async Task<CompanyService> CreateCompanyServiceAsync(Company company, string nameService, decimal baseFare, decimal pricePerKm, decimal pricePerMinute)
        {
            var companyService = new CompanyService(company, nameService, baseFare, pricePerKm, pricePerMinute);
            _context.CompanyServices.Add(companyService);
            await _context.SaveChangesAsync();
            return companyService;
        }

        public async Task<bool> DeleteCompanyServiceAsync(Guid id)
        {
            var companyService = await GetCompanyServiceByIdAsync(id);
            if (companyService == null) return false;

            _context.CompanyServices.Remove(companyService);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<CompanyService> UpdateCompanyServiceAsync(CompanyService companyService)
        {
            _context.CompanyServices.Update(companyService);
            await _context.SaveChangesAsync();
            return companyService;
        }

        public async Task<CompanyService> CompanyServiceInactiveAsync(Guid id)
        {
            var companyService = await GetCompanyServiceByIdAsync(id);
            if (companyService == null) return null;

            companyService.Inactivate();
            await _context.SaveChangesAsync();
            return companyService;
        }

        public async Task<CompanyService> CompanyServiceActiveAsync(Guid id)
        {
            var companyService = await GetCompanyServiceByIdAsync(id);
            if (companyService == null) return null;

            companyService.Active();
            await _context.SaveChangesAsync();
            return companyService;
        }
    }
}
