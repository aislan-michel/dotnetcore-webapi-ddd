using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Dtos.User;

namespace Api.Domain.Interfaces.Services.User
{
     public interface IUserService
     {
         Task<UserDto> GetUserById(long id);
         Task<IEnumerable<UserDto>> GetAllUsers();
         Task<UserDtoCreateResult> CreateUser(UserDtoCreate user);
         Task<UserDtoUpdateResult> UpdateUser(UserDtoUpdate user);
         Task<bool> DeleteUser(long id);
     }
}
