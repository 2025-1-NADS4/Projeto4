  using Fasor.Domain.Aggregates;
using Fasor.Infrastructure.Data;
using Fasor.Infrastructure.Repositories.CompanyServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fasor.Infrastructure.Repositories.CompanyServices
{ 
    public class AppServicesRepository(AppDbContext _context) : IAppServicesRepository
    {
        public async Task<AppService> GetAppServiceByIdAsync(Guid id)
        {
            var companyService = await _context.AppServices
                .Include(c => c.CompanyRide)
                .FirstOrDefaultAsync(c => c.Id == id);

            return companyService;
        }

        public async Task<IEnumerable<AppService>> GetAllAppServicesAsync()
        {
            var companyServices = await _context.AppServices
                .Include(c => c.CompanyRide)
                .ToListAsync();

            return companyServices;
        }

        public async Task<AppService> CreateAppServiceAsync(Guid idCompanyRide, string nameService)
        {
            var companyService = AppService.Create(idCompanyRide, nameService).Value;
            _context.AppServices.Add(companyService);
            await _context.SaveChangesAsync();
            return companyService;
        }

        public async Task<bool> DeleteAppServiceAsync(Guid id)
        {
            var appService = await GetAppServiceByIdAsync(id);
            if (appService == null) return false;

            _context.AppServices.Remove(appService);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<AppService> UpdateAppServiceAsync(AppService AppService)
        {
            _context.AppServices.Update(AppService);
            await _context.SaveChangesAsync();
            return AppService;
        }

        public async Task<AppService> AppServiceInactiveAsync(AppService AppService)
        {
            if (AppService == null) return null;

            AppService.Inactivate();
            await _context.SaveChangesAsync();
            return AppService;
        }

        public async Task<AppService> AppServiceActiveAsync(AppService companyAppService)
        {
            if (companyAppService == null) return null;

            companyAppService.Active();
            await _context.SaveChangesAsync();
            return companyAppService;
        }
    }
}
