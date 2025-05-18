using ErrorOr;
using Fasor.Domain.Shared.Errors;

namespace Fasor.Domain.Aggregates
{
    public class AppService
    {
        public Guid Id { get; private set; }
        public Guid CompanyRideId { get; private set; }
        public CompanyRide CompanyRide { get; private set; }
        public string NameService { get; private set; }
        public bool IsActive { get; private set; }

        private AppService()
        {
            
        }

        private AppService(string nameService)
        {
            Id = Guid.NewGuid();
            NameService = nameService; 
        }

        public static ErrorOr<AppService> Create(Guid companyRideId, string nameService)
        {
            List<Error> errors = new();

            var companyAppService = new AppService();

            var nameServiceResult = companyAppService.SetNameService(nameService);
            if (nameServiceResult.IsError)
                errors.AddRange(nameServiceResult.Errors);

            if (errors.Any())
                return errors;

            companyAppService.Id = Guid.NewGuid();
            companyAppService.CompanyRideId = companyRideId;
            companyAppService.IsActive = true;

            return companyAppService;
        }


        public void Inactivate()
        {
            IsActive = false;
        }
        public void Active()
        {
            IsActive = true;
        }

        public ErrorOr<string> SetNameService(string nameService)
        {
            if (string.IsNullOrWhiteSpace(nameService))
                return CompanyAddServiceErrors.NameServiceIsRequired;

            return NameService = nameService;
        }

    }

}
