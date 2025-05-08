  using Fasor.Domain.Aggregates;
using Fasor.Infrastructure.Aggregates.Data;
using Fasor.Infrastructure.Repositories.CompanyServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fasor.Infrastructure.Repositories.CompanyServices
{ 
    public class CompanyAppServicesRepository(AppDbContext _context) : ICompanyAppServicesRepository
    {
        public async Task<CompanyAppService> GetCompanyAppServiceByIdAsync(Guid id)
        {
            var companyService = await _context.CompanyServices
                .Include(c => c.Company)
                .FirstOrDefaultAsync(c => c.Id == id);

            return companyService;
        }

        public async Task<IEnumerable<CompanyAppService>> GetAllCompanyAppServicesAsync()
        {
            var companyServices = await _context.CompanyServices
                .Include(c => c.Company)
                .ToListAsync();

            return companyServices;
        }

        public async Task<CompanyAppService> CreateCompanyAppServiceAsync(Guid idCompany, string nameService)
        {
            var companyService = CompanyAppService.Create(idCompany, nameService).Value;
            _context.CompanyServices.Add(companyService);
            await _context.SaveChangesAsync();
            return companyService;
        }

        public async Task<bool> DeleteCompanyServiceAsync(Guid id)
        {
            var companyService = await GetCompanyAppServiceByIdAsync(id);
            if (companyService == null) return false;

            _context.CompanyServices.Remove(companyService);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<CompanyAppService> UpdateCompanyAppServiceAsync(CompanyAppService companyService)
        {
            _context.CompanyServices.Update(companyService);
            await _context.SaveChangesAsync();
            return companyService;
        }

        public async Task<CompanyAppService> CompanyAppServiceInactiveAsync(CompanyAppService companyAppService)
        {
            if (companyAppService == null) return null;

            companyAppService.Inactivate();
            await _context.SaveChangesAsync();
            return companyAppService;
        }

        public async Task<CompanyAppService> CompanyAppServiceActiveAsync(CompanyAppService companyAppService)
        {
            if (companyAppService == null) return null;

            companyAppService.Active();
            await _context.SaveChangesAsync();
            return companyAppService;
        }
    }
}
