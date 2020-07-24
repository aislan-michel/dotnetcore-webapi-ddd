using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services.User;

namespace Api.Service.Services
{
     public class UserService : IUserService
     {
          private IRepository<UserEntity> _repository;

          public UserService(IRepository<UserEntity> repository)
          {
              _repository = repository;
          }

          public async Task<UserEntity> CreateUser(UserEntity user)
          {
               return await _repository.InsertAsync(user);
          }

          public async Task<bool> DeleteUser(long id)
          {
               return await _repository.DeleteAsync(id);
          }

          public async Task<IEnumerable<UserEntity>> GetAllUsers()
          {
               return await _repository.SelectAsync();
          }

          public async Task<UserEntity> GetUser(long id)
          {
               return await _repository.SelectAsync(id);
          }

          public async Task<UserEntity> UpdateUser(UserEntity user)
          {
               return await _repository.UpdateAsync(user);
          }
     }
}
