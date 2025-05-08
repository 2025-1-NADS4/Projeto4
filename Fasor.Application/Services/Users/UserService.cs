using ErrorOr;
using Fasor.Application.Services.Users.Interfaces;
using Fasor.Domain.Aggregates;
using Fasor.Infrastructure.Repositories.Users.Interfaces;

namespace Fasor.Application.Services.Users
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        public async Task<ErrorOr<User>> GetUserByIdAsync(Guid id)
        {
            var result = await userRepository.GetByIdAsync(id);
            if (result.IsError) return result.Errors;

            var user = result.Value;

            return user;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var users = await userRepository.GetAllAsync();
            return users;
        }

        public async Task<User> CreateUser(string name, string surname, string cpf, string email, DateTime dateBirth, IEnumerable<Company>? preferences)
        {
            var result = await userRepository.CreateUser(name, surname, cpf, email, dateBirth, preferences);

            return result;
        }
    }
}