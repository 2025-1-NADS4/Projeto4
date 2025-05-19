using ErrorOr;
using Fasor.Domain.Aggregates;
using Fasor.Infrastructure.Data;
using Fasor.Infrastructure.Repositories.Users.Dtos;
using Fasor.Infrastructure.Repositories.Users.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fasor.Infrastructure.Repositories.Users
{
    public class UserRepository(AppDbContext _context) : IUserRepository
    {
        public async Task<ErrorOr<User>> GetByIdAsync(Guid id)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<dynamic>> GetAllAsync()
        {
            return await _context.Users
                .Select(u => new
                {
                    u.Id,
                    u.Name,
                    u.Surname,
                    u.Cpf,
                    u.Email,
                    u.DateBirth,
                    u.CompanyId,
                    CompanyName = u.Company.NameService,
                    u.RideQuotes
                })
                .AsNoTracking()
                .ToListAsync();
        }


        public async Task<User> CreateUser(string name, string surname, string email, string cpf, DateTime dateBirth, Guid companyId)
        {
            var user = new User(name, surname, cpf, email, dateBirth, companyId);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<ErrorOr<bool>> UpdateUserAsync(Guid id, UpdateUserDto dto)
        {
            var userResult = await GetByIdAsync(id);

            if (userResult.IsError)
                return userResult.Errors;

            var user = userResult.Value;

            if (dto.Email != null)
            {
                user.Email = dto.Email;
            }

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<ErrorOr<bool>> DeleteUserAsync(Guid id)
        {
            var userResult = await GetByIdAsync(id);
            if (userResult.IsError) return userResult.Errors;

            var user = userResult.Value;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
