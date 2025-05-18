using Fasor.Domain.Aggregates;

public class CompanyCompanyRide
{
    public Guid CompanyId { get; set; }
    public Company Company { get; set; }
    public Guid CompanyRideId { get; set; }
    public CompanyRide CompanyRide { get; set; }
}
