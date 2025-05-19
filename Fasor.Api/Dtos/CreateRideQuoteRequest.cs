using Fasor.Domain.Aggregates;

namespace Fasor.Api.Dtos
{
    public class CreateRideQuoteRequest
    {
        public Guid UserId { get; set; }
        public string OriginAddress { get; set; }
        public string DestinationAddress { get; set; }
        public float LatitudeOrigin { get; set; }
        public float LongitudeOrigin { get; set; }
        public float LatitudeDestination { get; set; }
        public float LongitudeDestination { get; set; }
        public string tipodia { get; set; }
        public string tipohorario { get; set; }
        public string ano { get; set; }
        public string mes { get; set; }
        public string hora { get; set; }
        public List<RideOption> RideOptions { get; set; }
    }
}
