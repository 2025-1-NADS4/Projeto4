using Fasor.Application.Services.Companies.Dtos;
using Fasor.Application.Services.Companies.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fasor.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompaniesController(ICompanyService _companyService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCompanyDto dto)
        {
            var result = await _companyService.CreateCompanyAsync(dto);

            if (result.IsError)
                return BadRequest(result.Errors);

            return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _companyService.GetCompanyByIdAsync(id);

            if (result.IsError)
                return NotFound(result.Errors);

            return Ok(result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var companies = await _companyService.GetAllCompaniesAsync();
            return Ok(companies);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _companyService.DeleteCompanyAsync(id);

            if (result.IsError)
                return NotFound(result.Errors);

            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id)
        {
            var result = await _companyService.UpdateCompanyAsync(id);

            if (result.IsError)
                return NotFound(result.Errors);

            return Ok(result.Value);
        }

        [HttpPatch("{id:guid}/inactivate")]
        public async Task<IActionResult> Inactivate(Guid id)
        {
            var result = await _companyService.CompanyInactiveAsync(id);

            if (result.IsError)
                return NotFound(result.Errors);

            return Ok(result.Value);
        }

        [HttpPatch("{id:guid}/activate")]
        public async Task<IActionResult> Activate(Guid id)
        {
            var result = await _companyService.CompanyActiveAsync(id);

            if (result.IsError)
                return NotFound(result.Errors);

            return Ok(result.Value);
        }
    }
}
