using Fasor.Domain.Aggregates;

namespace Fasor.Application.Services.Companies.Dtos
{
    public class UpdateCompanyDto
    {
        public string TradeName { get; set; }
        public string Cnpj { get; set; }
        public List<CompanyAppService>? CompanyAppService { get; set; }

        public static explicit operator UpdateCompanyDto(Company domain)
        {
            return new UpdateCompanyDto
            {
                TradeName = domain.TradeName,
                Cnpj = domain.Cnpj,
                CompanyAppService = domain.CompanyAppServices
            };
        }
    }
}
