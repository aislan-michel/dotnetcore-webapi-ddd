using System.Threading.Tasks;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.User
{
     public class WhenGetIsExecuted : TestUsers
     {
          private IUserService _userService;
          private Mock<IUserService> _mockService;


          [Fact(DisplayName = "It's possible execute get method.")]
          public async Task ItsPossibleExecuteGetMethod()
          {
               _mockService = new Mock<IUserService>();
               _mockService.Setup(m => m.GetUserById(FakerId)).ReturnsAsync(FakerUserDto);

               _userService = _mockService.Object;

               var result = await _userService.GetUserById(FakerId);

               Assert.NotNull(result);
               Assert.True(result.Id == FakerId);
               Assert.Equal(FakerName, result.Name);

               _mockService = new Mock<IUserService>();
               _mockService.Setup(m => m.GetUserById(It.IsAny<long>())).Returns(Task.FromResult((UserDto) null));

               _userService = _mockService.Object;

               var _record = await _userService.GetUserById(FakerId);

               Assert.Null(_record);
          }
          
     }
}
