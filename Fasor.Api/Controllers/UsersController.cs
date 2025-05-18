using Fasor.Application.Services.Users.Interfaces;
using Fasor.Infrastructure.Repositories.Users.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Fasor.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserDto dto)
        {
            var result = await _userService.UpdateUserAsync(id, dto);

            if (result.IsError)
                return BadRequest(result.Errors);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var result = await _userService.DeleteUserAsync(id);

            if (result.IsError)
                return BadRequest(result.Errors);

            return NoContent();
        }
    }
}
