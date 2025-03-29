using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fasor.Domain.Aggregates
{
    public class RideOption
    {
        public Guid Id { get; set; }
        public Guid QuoteId { get; set; }
        public decimal Price { get; set; }
        public DateTime TimeRide { get; set; }
        public DateTime EstimatedTime { get; set; }
        public string UrlRedirect { get; set; }
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
    }

}
