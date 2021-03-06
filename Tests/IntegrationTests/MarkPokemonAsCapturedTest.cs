using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Xunit;

using PokeAPI;

namespace Tests.IntegrationTests {
  public class MarkPokemonAsCapturedTest :
    IClassFixture<CustomWebApplicationFactory<Startup>> {
    private readonly CustomWebApplicationFactory<Startup> factory;

    public MarkPokemonAsCapturedTest(CustomWebApplicationFactory<Startup> factory) {
      this.factory = factory;
    }

    private async Task<HttpResponseMessage> PerformRequest(
      HttpClient client,
      int pokemonId
    ) {
      return await client.PatchAsync("/v1/pokemon/mark-captured/" + pokemonId, null);
    }

    [Fact]
    public async Task Should_Be_Able_To_Mark_Pokemon_As_Captured() {
      // Arrange
      var client = factory.CreateClient();

      // Act
      var response = await PerformRequest(client, 2);

      // Assert
      Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task Should_Return_A_Not_Found_Status_Code_If_Pokemon_Not_Exists() {
      // Arrange
      var client = factory.CreateClient();

      // Act
      var response = await PerformRequest(client, 4);

      // Assert
      Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Should_Return_A_Bad_Request_Status_Code_If_Pokemon_Already_Captured() {
      // Arrange
      var client = factory.CreateClient();

      // Act
      var response = await PerformRequest(client, 3);

      // Assert
      Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
  }
}
