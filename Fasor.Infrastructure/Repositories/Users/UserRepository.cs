using Fasor.Domain.Aggregates;
using Fasor.Infrastructure.Aggregates.Data;
using Fasor.Infrastructure.Repositories.Users.Dtos;
using Fasor.Infrastructure.Repositories.Users.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fasor.Infrastructure.Repositories.Users
{
    public class UserRepository(AppDbContext _context) : IUserRepository
    {
        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _context.Users
                .Include(u => u.Preferences)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.
                Include(u => u.Preferences)
                .ToListAsync();
        }

        public async Task<User> CreateUser(string name, string surname, string email, string cpf, DateTime dateBirth, IEnumerable<Company>? preferences)
        {
            var user = new User(name, surname, cpf, email, dateBirth, preferences);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UpdateUserAsync(Guid id, UpdateUserDto dto)
        {
            var user = await GetByIdAsync(id);

            if (user == null) return false;

            if (dto.Email != null)
            {
                user.Email = dto.Email;
            }

            if (dto.Preferences != null)
            {
                user.Preferences = dto.Preferences;
            }

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await GetByIdAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
