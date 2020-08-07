using System;
using System.Threading.Tasks;

using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;

using Services.DTOs;
// using PokeAPI.ViewModels;
using PokeAPI.Controllers;
using Services.Interfaces;
using Services.Exceptions;

namespace Tests.UnitTests.Controllers {
  public class SessionsControllerTest {
    private readonly Mock<AdminAuthenticationService> adminAuthentication;

    public SessionsControllerTest() {
      adminAuthentication = new Mock<AdminAuthenticationService>();
    }

    [Fact]
    public async Task Should_Return_200_Status_Code_With_Valid_DTO() {
      // Arrange
      var data = new AuthenticationAdminDTO() {
        Email = "johndoe@example.com",
        Password = "123456"
      };

      adminAuthentication.Setup(obj => obj.AuthenticateAdmin(data))
        .ReturnsAsync(new AuthenticatedAdminDTO() {
        Id = 1,
        Email = "johndoe@example.com",
        Token = "JWTToken",
        ValidFrom = DateTime.Now,
        ValidTo = DateTime.Today.AddDays(1),
      });

      var sessionsController = new SessionsController(
        adminAuthentication.Object
      );

      // Act
      var response = await sessionsController.Create(data);

      // Assert
      Assert.NotNull(response);
      Assert.IsType<OkObjectResult>(response.Result);
    }

    [Fact]
    public async Task Should_Throw_Exception_With_Invalid_DTO() {
      // Arrange
      var data = new AuthenticationAdminDTO() {
        Email = null,
        Password = null
      };

      var sessionsController = new SessionsController(
        adminAuthentication.Object
      );

      // Act

      // Assert
      await Assert.ThrowsAsync<ServiceDTOException>(
        () => sessionsController.Create(data)
      );
    }
  }
}
