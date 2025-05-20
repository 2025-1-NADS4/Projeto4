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
        public Guid AppServiceId { get; set; }
        public AppService AppService { get; set; }
        public CompanyRide CompanyRide { get; set; }

        public RideOption( AppService AppService, RideQuote rideQuote, decimal price)
        {
            Id = Guid.NewGuid();
            QuoteId = rideQuote.Id; 
            Price = price;
            AppService  = AppService;
            AppServiceId = AppService.Id;
            CompanyRide = AppService.CompanyRide;
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
