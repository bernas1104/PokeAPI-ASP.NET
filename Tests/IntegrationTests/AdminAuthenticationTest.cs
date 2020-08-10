using System;
using System.Net;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;

using Xunit;
using Newtonsoft.Json;

using PokeAPI;
using Services.ViewModels;

namespace Tests.IntegrationTests {
  public class AdminAuthenticationTest
    : IClassFixture<CustomWebApplicationFactory<Startup>> {
    private readonly CustomWebApplicationFactory<Startup> factory;

    public AdminAuthenticationTest(
      CustomWebApplicationFactory<Startup> factory
    ) {
      this.factory = factory;
    }

    [Fact]
    public async Task Should_Authenticate_Admin() {
      // Arrange
      var client = factory.CreateClient();
      var data = new LoginViewModel() {
        Email = "johndoe@example.com",
        Password = "123456"
      };

      // Act
      var response = await client.PostAsync("/v1/sessions", new StringContent(
        JsonConvert.SerializeObject(data),
        Encoding.UTF8
      ) {
        Headers = {
          ContentType = new MediaTypeHeaderValue("application/json")
        }
      });

      // Assert
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);

      var session = JsonConvert.DeserializeObject<SessionViewModel>(
        await response.Content.ReadAsStringAsync()
      );

      Assert.Equal("johndoe@example.com", session.Email);
      Assert.Equal(
        DateTime.Today.ToLocalTime().Date,
        session.ValidFrom.Date
      );
      Assert.Equal(
        DateTime.Today.AddDays(1).ToLocalTime().Date,
        session.ValidTo.Date
      );
    }

    [Theory]
    [InlineData((string)null, "123456")]
    [InlineData("", "123456")]
    [InlineData("invalid@email", "123456")]
    [InlineData(
      "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
      "123456"
    )]
    [InlineData("johndoe@example.com", (string)null)]
    [InlineData("johndoe@example.com", "")]
    [InlineData("johndoe@example.com", "12345")]
    [InlineData("johndoe@example.com", "1234567890abc")]
    public async Task Should_Not_Be_Able_To_Authenticate_With_Invalid_Credentials(
      string email,
      string password
    ) {
      // Arrange
      var client = factory.CreateClient();
      var data = new LoginViewModel() {
        Email = email,
        Password = password
      };

      // Act
      var response = await client.PostAsync("/v1/sessions", new StringContent(
        JsonConvert.SerializeObject(data),
        Encoding.UTF8
      ) {
        Headers = {
          ContentType = new MediaTypeHeaderValue("application/json")
        }
      });

      // Assert
      Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Should_Not_Authenticate_Non_Existing_Admin() {
      // Arrange
      var client = factory.CreateClient();
      var data = new LoginViewModel() {
        Email = "johntre@example.com",
        Password = "123456"
      };

      // Act
      var response = await client.PostAsync("/v1/sessions", new StringContent(
        JsonConvert.SerializeObject(data),
        Encoding.UTF8
      ) {
        Headers = {
          ContentType = new MediaTypeHeaderValue("application/json")
        }
      });

      // Assert
      Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Should_Not_Authenticate_With_Wrong_Password() {
      // Arrange
      var client = factory.CreateClient();
      var data = new LoginViewModel() {
        Email = "johndoe@example.com",
        Password = "123455"
      };

      // Act
      var response = await client.PostAsync("/v1/sessions", new StringContent(
        JsonConvert.SerializeObject(data),
        Encoding.UTF8
      ) {
        Headers = {
          ContentType = new MediaTypeHeaderValue("application/json")
        }
      });

      // Assert
      Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
  }
}
