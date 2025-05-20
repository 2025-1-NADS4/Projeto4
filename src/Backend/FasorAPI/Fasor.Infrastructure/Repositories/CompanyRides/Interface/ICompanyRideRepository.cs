using ErrorOr;
using Fasor.Domain.Aggregates;

namespace Fasor.Infrastructure.Repositories.CompanyRides.Interface
{
    public interface ICompanyRideRepository
    {
        public Task<CompanyRide> GetCompanyRideByIdAsync(Guid id);
        public Task<List<CompanyRide>> GetAllAsync();
        public Task<CompanyRide> CreateCompanyRideAsync(CompanyRide companyRide);
        public Task<CompanyRide> UpdateCompanyRideAsync(CompanyRide companyRide);
        public Task<bool> DeleteCompanyRideAsync(Guid id);
        public Task<bool> InactiveCompanyRideAsync(Guid id);
        public Task<bool> ActiveCompanyRideAsync(Guid id);
    }
}
