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
        public DateTime CreatedAt { get; set; }
        public ICollection<RideOption> RideOptions { get; set; }


        public RideQuote(User user, ICollection<RideOption> rideOptions)
        {
            Id = Guid.NewGuid();
            UserId = user.Id;
            User = user;
            CreatedAt = DateTime.UtcNow;
            RideOptions = rideOptions;
        }
        public RideQuote()
        {

        }
        public void AddRideOption(RideOption rideOption)
        {
            if (RideOptions == null)
            {
                RideOptions = new List<RideOption>();
            }
            RideOptions.Add(rideOption);
        }

    }
}
