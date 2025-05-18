using ErrorOr;
using Fasor.Domain.Shared.Errors;
using System.Numerics;

namespace Fasor.Domain.Aggregates
{
    public class RideOption
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid QuoteId { get; set; }
        public RideQuote RideQuote { get; set; }
        public decimal Price { get; set; }
        public DateTime TimeRide { get; set; }
        public DateTime EstimatedTime { get; set; }
        public string UrlRedirect { get; set; }
        public Guid AppServiceId { get; set; }
        public AppService AppService { get; set; }
        public CompanyRide CompanyRide { get; set; }

        public RideOption( AppService AppService, DateTime timeRide, RideQuote rideQuote, DateTime estimatedTime, string urlRedirect, decimal price)
        {
            Id = Guid.NewGuid();
            QuoteId = rideQuote.Id; 
            Price = price;
            EstimatedTime = estimatedTime;
            AppService  = AppService;
            AppServiceId = AppService.Id;
            CompanyRide = AppService.CompanyRide;
            UrlRedirect = urlRedirect;
        }

        public RideOption()
        {
             
        }

        public ErrorOr<bool> ValidateRideOptionIsValidByTime(RideQuote rideQuote)
        {
            bool isExpired = DateTime.UtcNow - rideQuote.CreatedAt > TimeSpan.FromMinutes(5);

            if (isExpired) return RideOptionsErrors.InvalidQuoteTime;

            return true;
        }       

    }
}
