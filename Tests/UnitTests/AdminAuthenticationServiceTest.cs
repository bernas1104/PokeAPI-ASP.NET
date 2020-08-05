using System;

using Moq;
using Xunit;

using Microsoft.AspNetCore.Identity;

using Domain;
using Services.DTOs;
using Services.Exceptions;
using Services.Interfaces;
using Services.Implementations;
using Persistence.Repositories.Interfaces;

namespace Tests.UnitTests {
  public class AdminAuthenticationServiceTest {
    private Mock<AdminsRepository> adminsRepository;
    private Mock<IPasswordHasher<Admin>> passwordHasher;
    private AdminAuthenticationService adminAuthenticationService;

    public AdminAuthenticationServiceTest() {
      adminsRepository = new Mock<AdminsRepository>();
      passwordHasher = new Mock<IPasswordHasher<Admin>>();
      adminAuthenticationService = new AdminAuthenticationServiceImpl(
        adminsRepository.Object,
        passwordHasher.Object
      );
    }

    [Fact]
    public async void Should_Be_Able_To_Return_Session_Information() {
      // Arrange
      adminsRepository.Setup(
        obj => obj.FindByEmail("johndoe@example.com")
      )
      .ReturnsAsync(new Admin() {
        Id = 1,
        Email = "johndoe@example.com",
        Password = "123456",
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now,
      });
      passwordHasher.Setup(
        obj => obj.VerifyHashedPassword(
          It.IsAny<Admin>(),
          "123456",
          It.IsAny<string>()
        )
      )
      .Returns(PasswordVerificationResult.Success);

      var auth = new AuthenticationAdminDTO() {
        Email = "johndoe@example.com",
        Password = "123456"
      };

      // Act
      var response = await adminAuthenticationService.AuthenticateAdmin(auth);

      // Assert
      Assert.Equal(1, response.Id);
      Assert.Equal("johndoe@example.com", response.Email);
    }

    [Fact]
    public async void Should_Not_Be_Able_To_Authenticate_If_Admin_Does_Not_Exist() {
      // Arrange
      adminsRepository.Setup(
        obj => obj.FindByEmail("johndoe@example.com")
      )
      .ReturnsAsync((Admin)null);
      passwordHasher.Setup(
        obj => obj.VerifyHashedPassword(
          It.IsAny<Admin>(),
          "123456",
          It.IsAny<string>()
        )
      )
      .Returns(PasswordVerificationResult.Success);

      var auth = new AuthenticationAdminDTO() {
        Email = "johndoe@example.com",
        Password = "123456"
      };

      // Act

      // Assert
      await Assert.ThrowsAsync<AdminException>(
        () => adminAuthenticationService.AuthenticateAdmin(auth)
      );
    }

    [Fact]
    public async void Should_Not_Be_Able_To_Authenticate_If_Admin_Password_Invalid() {
      // Arrange
      adminsRepository.Setup(
        obj => obj.FindByEmail("johndoe@example.com")
      )
      .ReturnsAsync(new Admin() {
        Id = 1,
        Email = "johndoe@example.com",
        Password = "123456",
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now,
      });
      passwordHasher.Setup(
        obj => obj.VerifyHashedPassword(
          It.IsAny<Admin>(),
          "123456",
          It.IsAny<string>()
        )
      )
      .Returns(PasswordVerificationResult.Failed);

      // Act

      // Assert
      var auth = new AuthenticationAdminDTO() {
        Email = "johndoe@example.com",
        Password = "123456"
      };

      // Act

      // Assert
      await Assert.ThrowsAsync<AdminException>(
        () => adminAuthenticationService.AuthenticateAdmin(auth)
      );
    }
  }
}
