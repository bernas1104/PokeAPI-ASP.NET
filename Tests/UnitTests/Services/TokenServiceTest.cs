using System;

using Moq;
using Xunit;
using Microsoft.Extensions.Configuration;

using Domain;
using Services.Implementations;

namespace Tests.UnitTests.Services {
  public class TokenServiceTest {
    private readonly Mock<IConfiguration> configuration;

    public TokenServiceTest() {
      configuration = new Mock<IConfiguration>();
    }

    [Fact]
    public void Should_Generate_A_JWT_Token() {
      // Arrange
      var admin = new Admin() {
        Id = 1,
        Email = "johndoe@example.com",
        Password = "123456",
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now,
      };
      configuration.Setup(obj => obj["JWTSecret"])
        .Returns("fedaf7d8863b48e197b9287d492b708e");

      // Act
      var token = TokenService.GenerateToken(configuration.Object, admin);

      // Assert
      Assert.Equal(
        DateTime.Today.ToLocalTime().Date,
        token.ValidFrom.ToLocalTime().Date
      );
      Assert.Equal(
        DateTime.Today.AddDays(1).ToLocalTime().Date,
        token.ValidTo.ToLocalTime().Date
      );
    }
  }
}
