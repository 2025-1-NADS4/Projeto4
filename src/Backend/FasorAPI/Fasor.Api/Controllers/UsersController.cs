using Fasor.Application.Services.Users.Interfaces;
using Fasor.Infrastructure.Repositories.Users.Dtos;
using Microsoft.AspNetCore.Mvc;


namespace Fasor.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var result = await _userService.GetUserByIdAsync(id);

            if (result.IsError)
                return NotFound(result.FirstError.Description);

            return Ok(result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userService.GetAllUsersAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
        {
            var result = await _userService.CreateUser(dto.Name, dto.Surname, dto.Cpf, dto.Email, dto.DateBirth, dto.CompanyId);

            if (result.IsError)
                return BadRequest(result.FirstError.Description);

            return CreatedAtAction(nameof(GetUserById), new { id = result.Value.Id }, result.Value);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserDto dto)
        {
            var result = await _userService.UpdateUserAsync(id, dto);

            if (result.IsError)
                return BadRequest(result.FirstError.Description);

            if (!result.Value)
                return BadRequest("Não foi possível atualizar o usuário.");

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var result = await _userService.DeleteUserAsync(id);

            if (result.IsError)
                return BadRequest(result.FirstError.Description);

            if (!result.Value)
                return BadRequest("Não foi possível excluir o usuário.");

            return NoContent();
        }
    }
}
