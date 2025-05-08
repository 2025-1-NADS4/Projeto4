using Fasor.Domain.Aggregates;

namespace Fasor.Application.Services.Companies.Dtos
{
    public class CreateCompanyDto
    {
        public string TradeName { get; set; }
        public string Cnpj { get; set; }
        public List<CompanyAppService>? CompanyAppService { get; set; }

        public static explicit operator CreateCompanyDto(Company domain)
        {
            return new CreateCompanyDto
            {
                TradeName = domain.TradeName,
                Cnpj = domain.Cnpj,
                CompanyAppService = domain.CompanyAppServices
            };
        }
    }
}