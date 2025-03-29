using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fasor.Domain.Aggregates
{
    public class Company
    {
        public Guid Id { get; set; }
        public string TradeName { get; set; }
        public string Cnpj { get; set; }
        public CompanyService CompanyServices { get; set; }
    }
}
