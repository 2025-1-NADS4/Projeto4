using ErrorOr;
using Fasor.Domain.Aggregates;
using Fasor.Infrastructure.Repositories.Users.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fasor.Application.Services.Users.Interfaces
{
    public interface IUserService
    {
        Task<ErrorOr<User>> GetUserByIdAsync(Guid id);
        Task<IEnumerable<dynamic>> GetAllUsersAsync();
        Task<ErrorOr<User>> CreateUser(string name, string surname, string cpf, string email, DateTime dateBirth, Guid companyId);
        Task<ErrorOr<bool>> UpdateUserAsync(Guid id, UpdateUserDto dto);
        Task<ErrorOr<bool>> DeleteUserAsync(Guid id);

    }
}
