using Fasor.Domain.Aggregates;
using System.ComponentModel.DataAnnotations;

namespace Fasor.Application.Services.Companies.Dtos
{
    public class CreateCompanyDto
    {
        [Required]
        public string NameService{ get; set; }
        [Required]
        public string Cnpj { get; set; }
        public List<Guid> CompanyRideIds { get; set; }

        public static explicit operator CreateCompanyDto(Company domain)
        {
            return new CreateCompanyDto
            {
                NameService = domain.NameService,
                Cnpj = domain.Cnpj,
                CompanyRideIds = domain.CompanyCompanyRides?.Select(x => x.CompanyRideId).ToList() ?? new()
            };
        }
    }
}
