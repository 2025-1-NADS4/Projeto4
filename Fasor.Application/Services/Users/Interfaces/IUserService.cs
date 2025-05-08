using ErrorOr;
using Fasor.Domain.Aggregates;
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
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> CreateUser(string name, string surname, string cpf, string email, DateTime dateBirth, IEnumerable<Company>? preferences);
        
    }
}
