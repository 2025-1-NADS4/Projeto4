namespace Fasor.Domain.Aggregates
{
    public class RideQuote
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string OriginAddress{ get; set; }
        public string DestinationAddress { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<RideOption> RideOptions { get; set; }


        public RideQuote(string origindAddress, string destinationAddress, List<RideOption> rideOptions)
        {
            Id = Guid.NewGuid();
            OriginAddress = origindAddress;
            DestinationAddress = destinationAddress;
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
