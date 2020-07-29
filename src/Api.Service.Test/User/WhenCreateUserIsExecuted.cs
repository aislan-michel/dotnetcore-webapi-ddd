using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.User
{
     public class WhenCreateUserIsExecuted : TestUsers
     {
          private IUserService _userService;
          private Mock<IUserService> _mockService;

          [Fact(DisplayName = "It's possible execute createUser method.")]
          public async Task ItsPossibleExecuteCreateUserMethod()
          {
               _mockService = new Mock<IUserService>();
               _mockService.Setup(m => m.CreateUser(FakerUserDtoCreate)).ReturnsAsync(FakerUserDtoCreateResult);

               _userService = _mockService.Object;

               var result = await _userService.CreateUser(FakerUserDtoCreate);

               Assert.NotNull(result);
               Assert.Equal(FakerName, result.Name);
               Assert.Equal(FakerEmail, result.Email);
          }
     }
}
