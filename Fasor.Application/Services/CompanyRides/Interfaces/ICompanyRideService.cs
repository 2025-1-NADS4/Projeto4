using ErrorOr;

public interface ICompanyRideService
{
    Task<ErrorOr<CompanyRide>> CreateCompanyRideAsync(string tradeName);
    Task<CompanyRide> GetCompanyRideByIdAsync(Guid id);
    Task<List<CompanyRide>> GetAllAsync();
    Task<CompanyRide> UpdateCompanyRideAsync(CompanyRide companyRide);
    Task<bool> DeleteCompanyRideAsync(Guid id);
    Task<bool> InactiveCompanyRideAsync(Guid id);
    Task<bool> ActiveCompanyRideAsync(Guid id);
}
