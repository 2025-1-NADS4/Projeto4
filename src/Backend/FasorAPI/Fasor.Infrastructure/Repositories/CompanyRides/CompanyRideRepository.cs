using Fasor.Infrastructure.Data;
using Fasor.Infrastructure.Repositories.CompanyRides.Interface;
using Microsoft.EntityFrameworkCore;

namespace Fasor.Infrastructure.Repositories.CompanyRides
{
    public class CompanyRideRepository(AppDbContext _context) : ICompanyRideRepository
    {
        public async Task<CompanyRide> GetCompanyRideByIdAsync(Guid id)
        {
            return await _context.CompanyRides
                .Include(c => c.CompanyCompanyRides)
                .Include(c => c.AppServices)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<CompanyRide>> GetAllAsync()
        {
            return await _context.CompanyRides
                .Include(c => c.CompanyCompanyRides)
                .Include(c => c.AppServices)
                .ToListAsync();
        }

        public async Task<CompanyRide> CreateCompanyRideAsync(CompanyRide companyRide)
        {
            _context.CompanyRides.Add(companyRide);
            await _context.SaveChangesAsync();
            return companyRide;
        }

        public async Task<CompanyRide> UpdateCompanyRideAsync(CompanyRide companyRide)
        {
            _context.CompanyRides.Update(companyRide);
            await _context.SaveChangesAsync();
            return companyRide;
        }

        public async Task<bool> DeleteCompanyRideAsync(Guid id)
        {
            var entity = await _context.CompanyRides.FindAsync(id);
            if (entity == null)
                return false;

            _context.CompanyRides.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> InactiveCompanyRideAsync(Guid id)
        {
            var entity = await _context.CompanyRides.FindAsync(id);
            if (entity == null)
                return false;

            entity.Inactivate() ;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ActiveCompanyRideAsync(Guid id)
        {
            var entity = await _context.CompanyRides.FindAsync(id);
            if (entity == null)
                return false;

            entity.Activate();
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
