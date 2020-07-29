using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.User
{
     public class WhenGetAllUsersIsExecuted : TestUsers
     {
          private IUserService _userService;
          private Mock<IUserService> _mockService;

          [Fact(DisplayName = "It's possible execute getAll method.")]
          public async Task ItsPossibleExecuteGetAllMethod()
          {
               _mockService = new Mock<IUserService>();
               _mockService.Setup(m => m.GetAllUsers()).ReturnsAsync(FakerUserDtoList);

               _userService = _mockService.Object;

               var result = await _userService.GetAllUsers();

               Assert.NotNull(result);
               Assert.True(result.Count() == 10);

               var _listResult = new List<UserDto>();
               _mockService = new Mock<IUserService>();
               _mockService.Setup(m => m.GetAllUsers()).ReturnsAsync(_listResult.AsEnumerable());

               _userService = _mockService.Object;

               var _resultEmpty = await _userService.GetAllUsers();

               Assert.Empty(_resultEmpty);
               Assert.True(_resultEmpty.Count() == 0);

          }
     }
}
