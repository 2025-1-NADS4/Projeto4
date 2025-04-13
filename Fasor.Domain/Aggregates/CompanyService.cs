using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fasor.Domain.Aggregates
{
    public class CompanyService
    {
        public Guid Id { get; private set; }
        public Company Company { get; private set; }
        public string NameService { get; private set; }
        public decimal BaseFare { get; private set; }
        public decimal PricePerKm { get; private set; }
        public decimal PricePerMinute { get; private set; }
        public bool IsActive { get; private set; }

        public CompanyService()
        {
            
        }

        public CompanyService(Company company, string nameService, decimal baseFare, decimal pricePerKm, decimal pricePerMinute)
        {
            Company = company;
            NameService = nameService;
            BaseFare = baseFare;
            PricePerKm = pricePerKm;
            PricePerMinute = pricePerMinute;
        }

        public void Inactivate()
        {
            IsActive = false;
        }
        public void Active()
        {
            IsActive = true;
        }

    }

}
