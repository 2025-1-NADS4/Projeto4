using ErrorOr;
using Fasor.Domain.Aggregates;

public class CompanyRide
{
    public Guid Id { get; private set; }
    public string TradeName { get; private set; }
    public bool IsActive { get; private set; }
    public List<CompanyCompanyRide> CompanyCompanyRides { get; private set; } = new();
    public List<AppService> AppServices { get; private set; } = new();

    private CompanyRide() { }

    private CompanyRide(string tradeName)
    {
        Id = Guid.NewGuid();
        TradeName = tradeName;
        IsActive = true;
    }

    public static ErrorOr<CompanyRide> Create(string tradeName)
    {
        if (string.IsNullOrWhiteSpace(tradeName))
            return CompanyRideErrors.TradeNameIsRequired;

        var companyRide = new CompanyRide(tradeName);
        return companyRide;
    }

    public void Inactivate() => IsActive = false;
    public void Activate() => IsActive = true;
}