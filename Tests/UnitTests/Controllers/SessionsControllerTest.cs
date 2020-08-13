using System;
using System.Threading.Tasks;

using Moq;
using Xunit;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using PokeAPI.Controllers;
using Services.ViewModels;
using Services.Interfaces;

namespace Tests.UnitTests.Controllers {
  public class SessionsControllerTest {
    private readonly Mock<IMapper> mapper;
    private readonly Mock<AdminAuthenticationService> adminAuthentication;

    public SessionsControllerTest() {
      mapper = new Mock<IMapper>();
      adminAuthentication = new Mock<AdminAuthenticationService>();
    }

    [Fact]
    public async Task Should_Return_200_Status_Code_With_Valid_ViewModel() {
      // Arrange
      var data = new LoginViewModel() {
        Email = "johndoe@example.com",
        Password = "123456"
      };

      var session = new SessionViewModel() {
        Id = 1,
        Email = "johndoe@example.com",
        Token = "JWTToken",
        ValidFrom = DateTime.Now,
        ValidTo = DateTime.Today.AddDays(1),
      };

      adminAuthentication.Setup(obj => obj.AuthenticateAdmin(data))
        .ReturnsAsync(session);

      mapper.Setup(x => x.Map<SessionViewModel>(session))
        .Returns(new SessionViewModel() {
          Id = session.Id,
          Email = session.Email,
          Token = session.Token,
          ValidFrom = session.ValidFrom,
          ValidTo = session.ValidTo
        });

      var sessionsController = new SessionsController(
        adminAuthentication.Object,
        mapper.Object
      );

      // Act
      var response = await sessionsController.Create(data);

      // Assert
      Assert.NotNull(response);
      Assert.IsType<OkObjectResult>(response.Result);
    }

    [Fact]
    public async Task Should_Throw_Exception_With_Invalid_ViewModel() {
      // Arrange
      var data = new LoginViewModel() {
        Email = null,
        Password = null
      };

      var sessionsController = new SessionsController(
        adminAuthentication.Object,
        mapper.Object
      );

      // Act
      var response = await sessionsController.Create(data);

      // Assert
      Assert.NotNull(response);
      Assert.IsType<BadRequestObjectResult>(response.Result);
    }
  }
}
