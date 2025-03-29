using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fasor.Domain.Aggregates
{
    public class RideQuote
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid RideOptionId { get; set; }
        public ICollection<RideOption> RideOptions { get; set; }

    }
}
