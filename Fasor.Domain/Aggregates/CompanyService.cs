using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fasor.Domain.Aggregates
{
    public class CompanyService
    {
        public Guid Id { get; set; }
        public Company CompanyId { get; set; }
        public string NameService { get; set; }
        public decimal BaseFare { get; set; }
        public decimal PricePerKm { get; set; }
        public decimal PricePerMinute { get; set; }

    }
}
