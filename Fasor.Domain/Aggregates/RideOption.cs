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
        public Guid CompanyServiceId { get; set; }
        public CompanyService CompanyService { get; set; }

        public RideOption( CompanyService companyService, DateTime timeRide, RideQuote rideQuote, DateTime estimatedTime, string urlRedirect, decimal price)
        {
            Id = Guid.NewGuid();
            QuoteId = rideQuote.Id; 
            Price = price;
            EstimatedTime = estimatedTime;
            CompanyService = companyService;
            CompanyServiceId = companyService.Id;
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
