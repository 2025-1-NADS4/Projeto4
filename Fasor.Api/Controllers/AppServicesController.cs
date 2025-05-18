using Microsoft.AspNetCore.Mvc;
using Fasor.Domain.Aggregates;
using Fasor.Application.Services.CompanyServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class AppServicesController : ControllerBase
{
    private readonly IAppServicesService _service;

    public AppServicesController(IAppServicesService service)
    {
        _service = service;
    }

    // GET: api/AppServices
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var list = await _service.GetAllAppServicesAsync();
        return Ok(list);
    }

    // GET: api/AppServices/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var appService = await _service.GetAppServiceByIdAsync(id);
        if (appService == null)
            return NotFound();

        return Ok(appService);
    }

    // POST: api/AppServices
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAppServiceDto dto)
    {
        // dto deve conter: Guid IdCompanyRide, string NameService
        var created = await _service.CreateAppServiceAsync(dto.IdCompanyRide, dto.NameService);
        if (created == null)
            return BadRequest("Não foi possível criar o AppService.");

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT: api/AppServices/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id)
    {
        var updated = await _service.UpdateAppServiceAsync(id);
        if (updated == null)
            return NotFound();

        return Ok(updated);
    }

    // DELETE: api/AppServices/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _service.DeleteAppServiceAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }

    // PATCH: api/AppServices/{id}/inactive
    [HttpPatch("{id}/inactive")]
    public async Task<IActionResult> Inactivate(Guid id)
    {
        var inactivated = await _service.AppServiceInactiveAsync(id);
        if (inactivated == null)
            return NotFound();

        return Ok(inactivated);
    }

    // PATCH: api/AppServices/{id}/active
    [HttpPatch("{id}/active")]
    public async Task<IActionResult> Activate(Guid id)
    {
        var activated = await _service.AppServiceActiveAsync(id);
        if (activated == null)
            return NotFound();

        return Ok(activated);
    }
}

// DTO para criação
public class CreateAppServiceDto
{
    public Guid IdCompanyRide { get; set; }
    public string NameService { get; set; }
}
