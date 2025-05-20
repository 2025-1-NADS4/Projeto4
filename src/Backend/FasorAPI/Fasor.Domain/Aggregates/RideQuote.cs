namespace Fasor.Domain.Aggregates
{
    public class RideQuote
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string OriginAddress { get; set; }
        public string DestinationAddress { get; set; }
        public float LongitudeOrigin { get; set; }
        public float LatitudeOrigin { get; set; }
        public float LongitudeDestination { get; set; }
        public float LatitudeDestination { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<RideOption> RideOptions { get; set; }


        public RideQuote
        (string origindAddress,
         string destinationAddress,
         float latitudeOrigin,
         float longitudeOrigin,
         float latitudeDestination,
         float longitudeDestination,
         List<RideOption> rideOptions)
        {
            Id = Guid.NewGuid();
            OriginAddress = origindAddress;
            DestinationAddress = destinationAddress;
            LongitudeOrigin = longitudeOrigin;
            LatitudeOrigin = latitudeOrigin;
            LongitudeDestination = longitudeDestination;
            LatitudeDestination = latitudeDestination;
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
