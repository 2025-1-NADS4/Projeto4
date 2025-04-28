using Fasor.Domain.Aggregates;
using Fasor.Infrastructure.Repositories.Users.Dtos;

namespace Fasor.Infrastructure.Repositories.Users.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> CreateUser(string name, string surname, string cpf, string email, DateTime dateBirth, IEnumerable<Company>? preferences);
        Task<bool> UpdateUserAsync(Guid id, UpdateUserDto dto);
        Task<bool> DeleteUserAsync(Guid id);
    }
}
