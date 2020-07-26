using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Models;
using AutoMapper;

namespace Api.Service.Services
{
     public class UserService : IUserService
     {
          private IRepository<UserEntity> _repository;
          private readonly IMapper _mapper;

          public UserService(IRepository<UserEntity> repository, IMapper mapper)
          {
              _repository = repository;
              _mapper = mapper;
          }

          public async Task<UserDtoCreateResult> CreateUser(UserDtoCreate user)
          {
               var model = _mapper.Map<UserModel>(user);

               var entity = _mapper.Map<UserEntity>(model);

               var result = await _repository.InsertAsync(entity);

               return _mapper.Map<UserDtoCreateResult>(result);
          }

          public async Task<bool> DeleteUser(long id)
          {
               return await _repository.DeleteAsync(id);
          }

          public async Task<IEnumerable<UserDto>> GetAllUsers()
          {
               var entities = await _repository.SelectAsync();

               return _mapper.Map<IEnumerable<UserDto>>(entities);
          }

          public async Task<UserDto> GetUserById(long id)
          {
               var entity = await _repository.SelectAsync(id);

               return _mapper.Map<UserDto>(entity);
          }

          public async Task<UserDtoUpdateResult> UpdateUser(UserDtoUpdate user)
          {
               var model = _mapper.Map<UserModel>(user);

               var entity = _mapper.Map<UserEntity>(model);

               var result = await _repository.UpdateAsync(entity);

               return _mapper.Map<UserDtoUpdateResult>(result);
          }
     }
}
