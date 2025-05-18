using ErrorOr;
using Fasor.Domain.Aggregates;
using Fasor.Infrastructure.Repositories.Users.Dtos;

namespace Fasor.Infrastructure.Repositories.Users.Interfaces
{
    public interface IUserRepository
    {
        Task<ErrorOr<User>> GetByIdAsync(Guid id);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> CreateUser(string name, string surname, string cpf, string email, DateTime dateBirth);
        Task<ErrorOr<bool>> UpdateUserAsync(Guid id, UpdateUserDto dto);
        Task<ErrorOr<bool>> DeleteUserAsync(Guid id);
    }
}
