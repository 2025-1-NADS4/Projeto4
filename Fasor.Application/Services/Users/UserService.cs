using ErrorOr;
using Fasor.Application.Services.Users.Interfaces;
using Fasor.Domain.Aggregates;
using Fasor.Infrastructure.Repositories.Users.Interfaces;
using Fasor.Infrastructure.Repositories.Users.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fasor.Application.Services.Users
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        public async Task<ErrorOr<User>> GetUserByIdAsync(Guid id)
        {
            var result = await userRepository.GetByIdAsync(id);
            if (result.IsError) return result.Errors;

            return result.Value;
        }


        public async Task<IEnumerable<dynamic>> GetAllUsersAsync()
        {
            return await userRepository.GetAllAsync();
        }

        public async Task<ErrorOr<User>> CreateUser(string name, string surname, string cpf, string email, DateTime dateBirth, Guid companyId)
        {

            var result = await userRepository.CreateUser(name, surname, email, cpf, dateBirth, companyId);

            if (result == null)
                return Error.Failure("Erro ao criar usuário.");

            return result;
        }

        public async Task<ErrorOr<bool>> UpdateUserAsync(Guid id, UpdateUserDto dto)
        {
            var updateResult = await userRepository.UpdateUserAsync(id, dto);
            return updateResult;
        }

        public async Task<ErrorOr<bool>> DeleteUserAsync(Guid id)
        {
            var deleteResult = await userRepository.DeleteUserAsync(id);
            return deleteResult;
        }
    }
}
