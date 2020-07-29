using System.Linq;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Api.Data.Test
{
     public class UserCrud : BaseTest, IClassFixture<DbTest>
     {
          private ServiceProvider _serviceProvider;

          public UserCrud(DbTest dbTest)
          {
              _serviceProvider = dbTest.ServiceProvider;
          }

          [Fact(DisplayName = "Crud de Usuário")]
          public async Task Crud()
          {
               using(var context = _serviceProvider.GetService<MyContext>())
               {
                   UserImplementation _repository = new UserImplementation(context);

                   UserEntity _entity = new UserEntity()
                   {
                        Email = Faker.Internet.Email(),
                        Name = Faker.Name.FullName()
                   };

                   var _createEntity = await _repository.InsertAsync(_entity);

                    Assert.NotNull(_createEntity);
                    Assert.Equal(_entity.Email, _createEntity.Email);
                    Assert.Equal(_entity.Name, _createEntity.Name);
                    Assert.True(_createEntity.Id > 0);

                    _entity.Name = Faker.Name.First();

                    var _updatedEntity = await _repository.UpdateAsync(_entity);

                    Assert.NotNull(_updatedEntity);
                    Assert.Equal(_entity.Email, _updatedEntity.Email);
                    Assert.Equal(_entity.Name, _updatedEntity.Name);

                    var _existEntity = await _repository.ExistAsync(_updatedEntity.Id);

                    Assert.True(_existEntity);

                    var _selectedEntity = await _repository.SelectAsync(_updatedEntity.Id);

                    Assert.NotNull(_selectedEntity);
                    Assert.Equal(_updatedEntity.Email, _selectedEntity.Email);
                    Assert.Equal(_updatedEntity.Name, _selectedEntity.Name);

                    var _allEntities = await _repository.SelectAsync();

                    Assert.NotNull(_allEntities);
                    Assert.True(_allEntities.Count() > 0);

                    var _removeEntity = await _repository.DeleteAsync(_selectedEntity.Id);

                    Assert.True(_removeEntity);

                    var _defaultUser = await _repository.FindByLogin("aislan@teste.com.br");

                    Assert.NotNull(_defaultUser);
                    Assert.Equal("aislan@teste.com.br", _defaultUser.Email);
                    Assert.Equal("Aislan Michel", _defaultUser.Name);

               }
          }

     }
}
