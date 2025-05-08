using ErrorOr;
using Fasor.Domain.Shared.Errors;

namespace Fasor.Domain.Aggregates
{
    public class CompanyAppService
    {
        public Guid Id { get; private set; }
        public Guid IdCompany { get; private set; }
        public Company Company { get; private set; }
        public string NameService { get; private set; }
        public bool IsActive { get; private set; }

        private CompanyAppService()
        {
            
        }

        private CompanyAppService(Guid idCompany, string nameService)
        {
            IdCompany = idCompany;
            NameService = nameService; 
        }

        public static ErrorOr<CompanyAppService> Create( Guid idCompany, string nameService)
        {
            List<Error>errors = new();

            var companyAppService = new CompanyAppService();

            var nameServiceResult = companyAppService.SetNameService(nameService);
            if (nameServiceResult.IsError)
                errors.AddRange(nameServiceResult.Errors);

            if (errors.Any())
                return errors;

            companyAppService.IdCompany = idCompany;
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
