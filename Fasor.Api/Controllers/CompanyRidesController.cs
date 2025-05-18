using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CompanyRideController : ControllerBase
{
    private readonly ICompanyRideService _service;

    public CompanyRideController(ICompanyRideService service)
    {
        _service = service;
    }

    // POST: api/CompanyRide
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] string tradeName)
    {
        var result = await _service.CreateCompanyRideAsync(tradeName);

        if (result.IsError)
            return BadRequest(result.Errors);

        return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
    }

    // GET: api/CompanyRide/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var companyRide = await _service.GetCompanyRideByIdAsync(id);
        if (companyRide == null)
            return NotFound();

        return Ok(companyRide);
    }

    // GET: api/CompanyRide
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var list = await _service.GetAllAsync();
        return Ok(list);
    }

    // PUT: api/CompanyRide/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CompanyRide companyRide)
    {
        if (id != companyRide.Id)
            return BadRequest("ID mismatch");

        var updated = await _service.UpdateCompanyRideAsync(companyRide);
        if (updated == null)
            return NotFound();

        return Ok(updated);
    }

    // DELETE: api/CompanyRide/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _service.DeleteCompanyRideAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }

    // PATCH: api/CompanyRide/{id}/inactive
    [HttpPatch("{id}/inactive")]
    public async Task<IActionResult> Inactivate(Guid id)
    {
        var success = await _service.InactiveCompanyRideAsync(id);
        if (!success)
            return NotFound();

        return NoContent();
    }

    // PATCH: api/CompanyRide/{id}/active
    [HttpPatch("{id}/active")]
    public async Task<IActionResult> Activate(Guid id)
    {
        var success = await _service.ActiveCompanyRideAsync(id);
        if (!success)
            return NotFound();

        return NoContent();
    }
}
