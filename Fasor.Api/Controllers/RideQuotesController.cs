using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fasor.Domain.Aggregates;
using Fasor.Infrastructure.Data;
using Fasor.Application.Services.RideQuotes.Interfaces;
using Fasor.Api.Dtos;

namespace Fasor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RideQuotesController(IRideQuoteService _rideQuoteService) : ControllerBase
    {
     

        [HttpGet("{id}")]
        public async Task<ActionResult<RideQuote>> GetRideQuote(Guid id)
        {
            var rideQuote = await _rideQuoteService.GetRideQuoteByIdAsync(id);

            if (rideQuote == null)
            {
                return NotFound();
            }

            return rideQuote;
        }
        [HttpPost]
        public async Task<ActionResult<RideQuote>> CreateRideQuote([FromBody] CreateRideQuoteRequest request)
        {
            var result = _rideQuoteService.CreateRideQuoteAsync(request.OriginAddress, request.DestinationAddress,
                request.LatitudeOrigin, request.LongitudeOrigin, request.LatitudeDestination, request.LongitudeDestination, request.RideOptions);

            return Ok(result);
        }

   

    }
}
