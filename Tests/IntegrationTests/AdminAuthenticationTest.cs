using System.Net;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;

using Xunit;
using Newtonsoft.Json;

using PokeAPI;
using Services.DTOs;
using System;

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
        var data = new AuthenticationAdminDTO() {
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
        Console.WriteLine(await response.Content.ReadAsStringAsync());
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var session = JsonConvert.DeserializeObject<AuthenticatedAdminDTO>(
          await response.Content.ReadAsStringAsync()
        );

        Assert.Equal("johndoe@example.com", session.Email);
        Assert.Equal(
          DateTime.Today.ToLocalTime().Date,
          session.ValidFrom.Date.ToLocalTime().Date
        );
        Assert.Equal(
          DateTime.Today.AddDays(1).ToLocalTime().Date,
          session.ValidTo.Date.ToLocalTime().Date
        );
      }
    }
}
