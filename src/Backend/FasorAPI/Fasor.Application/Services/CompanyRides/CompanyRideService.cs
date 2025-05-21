using ErrorOr;
using Fasor.Infrastructure.Repositories.CompanyRides.Interface;

public class CompanyRideService(ICompanyRideRepository _repository) : ICompanyRideService
{
    public async Task<ErrorOr<CompanyRide>> CreateCompanyRideAsync(string tradeName)
    {
        var companyRide = CompanyRide.Create(tradeName);

        if (companyRide.IsError)
            return companyRide.Errors;

        var companyRideResult = companyRide.Value;
        var created = await _repository.CreateCompanyRideAsync(companyRideResult);
        return created;
    }

    public async Task<CompanyRide> GetCompanyRideByIdAsync(Guid id)
    {
        var companyRide = await _repository.GetCompanyRideByIdAsync(id);
        return companyRide;
    }

    public async Task<List<CompanyRide>> GetAllAsync()
    {
        var list = await _repository.GetAllAsync();
        return list;
    }

    public async Task<CompanyRide> UpdateCompanyRideAsync(CompanyRide companyRide)
    {
        var updated = await _repository.UpdateCompanyRideAsync(companyRide);
        return updated;
    }

    public async Task<bool> DeleteCompanyRideAsync(Guid id)
    {
        var result = await _repository.DeleteCompanyRideAsync(id);
        return result;
    }

    public async Task<bool> InactiveCompanyRideAsync(Guid id)
    {
        var result = await _repository.InactiveCompanyRideAsync(id);
        return result;
    }

    public async Task<bool> ActiveCompanyRideAsync(Guid id)
    {
        var result = await _repository.ActiveCompanyRideAsync(id);
        return result;
    }
}
