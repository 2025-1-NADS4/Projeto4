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
using Microsoft.AspNetCore.Identity;
using Fasor.Application.Services.Users.Interfaces;
using Fasor.Application.Services.Companies.Interfaces;
using Fasor.Application.Services.RideQuotes;
using System.Text.Json;
using System.Text;

namespace Fasor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RideQuotesController(IRideQuoteService _rideQuoteService,
        IUserService userService, 
        ICompanyService companyService,
        IRideQuoteService rideQuoteService) : ControllerBase
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

            var userResult = await userService.GetUserByIdAsync(request.UserId);
            if (userResult.IsError)
                return NotFound("Usuário não encontrado");

            var user = userResult.Value;

            var companyResult = await companyService.GetCompanyByIdAsync(user.CompanyId);
            if (companyResult.IsError)
                return NotFound("Empresa não encontrada");

            var company = companyResult.Value;

            var appServices = company.CompanyCompanyRides
                .SelectMany(ccr => ccr.CompanyRide.AppServices)
                .ToList();


            var previsaoRequest = new
            {
                lat_origem = request.LatitudeOrigin,
                lng_origem = request.LongitudeOrigin,
                lat_destino = request.LatitudeDestination,
                lng_destino = request.LongitudeDestination,
                ano = request.ano,
                mes = request.mes,
                hora = request.hora,
                tipo_dia = request.tipodia,
                trafego_estimado = request.tipohorario,
                Company_services = appServices.Select(s => s.NameService).ToList()
            };

            var httpClient = new HttpClient();
            var externalApiUrl = "http://localhost:8000/prever_precos";

            var jsonContent = JsonSerializer.Serialize(previsaoRequest);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                var response = await httpClient.PostAsync(externalApiUrl, content);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                var previsoes = JsonSerializer.Deserialize<Dictionary<string, decimal>>(responseBody, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var rideOptions = new List<RideOption>();


                var rideQuote = await rideQuoteService.CreateRideQuoteAsync(
                    request.OriginAddress,
                    request.DestinationAddress,
                    request.LatitudeOrigin,
                    request.LongitudeOrigin,
                    request.LatitudeDestination,
                    request.LongitudeDestination,
                    rideOptions
                );

                return Ok(rideQuote);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao chamar a API externa: {ex.Message}");
            }
        }



    }
}
