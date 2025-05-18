using Fasor.Domain.Aggregates;

namespace Fasor.Api.Dtos
{
    public class CreateRideQuoteRequest
    {
        public string OriginAddress { get; set; }
        public string DestinationAddress { get; set; }
        public float LatitudeOrigin { get; set; }
        public float LongitudeOrigin { get; set; }
        public float LatitudeDestination { get; set; }
        public float LongitudeDestination { get; set; }
        public List<RideOption> RideOptions { get; set; }
    }
}
