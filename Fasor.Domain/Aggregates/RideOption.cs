﻿namespace Fasor.Domain.Aggregates
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
        public string NameService { get; set; }
        public CompanyService CompanyService { get; set; }

        public RideOption( CompanyService nameService, DateTime timeRide, RideQuote rideQuote, DateTime estimatedTime, CompanyService companyService, string urlRedirect, decimal price)
        {
            Id = Guid.NewGuid();
            NameService = nameService.NameService;
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

    }
}
