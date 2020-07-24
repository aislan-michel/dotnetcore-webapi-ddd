using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Services.User
{
     public interface IUserService
     {
         Task<UserEntity> GetUserById(long id);
         Task<IEnumerable<UserEntity>> GetAllUsers();
         Task<UserEntity> CreateUser(UserEntity user);
         Task<UserEntity> UpdateUser(UserEntity user);
         Task<bool> DeleteUser(long id);
     }
}
