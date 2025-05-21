using Fasor.Domain.Aggregates;

namespace Fasor.Application.Services.Companies.Dtos
{
    public class UpdateCompanyDto
    {
        public string TradeName { get; set; }
        public string Cnpj { get; set; }
        public List<CompanyCompanyRide>? CompanyCompanyRides { get; set; }

        public static explicit operator UpdateCompanyDto(Company domain)
        {
            return new UpdateCompanyDto
            {
                TradeName = domain.NameService,
                Cnpj = domain.Cnpj,
                CompanyCompanyRides = domain.CompanyCompanyRides
            };
        }
    }
}
