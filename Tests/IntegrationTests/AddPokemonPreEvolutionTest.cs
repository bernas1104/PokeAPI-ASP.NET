using System.IO;
using System.Net;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Collections.Generic;

using Xunit;
using Newtonsoft.Json;

using PokeAPI;
using Services.ViewModels;
using Tests.Bogus.ViewModel;

namespace Tests.IntegrationTests {
  public class AddPokemonPreEvolutionTest
    : IClassFixture<CustomWebApplicationFactory<Startup>> {
    private readonly CustomWebApplicationFactory<Startup> factory;

    public AddPokemonPreEvolutionTest(CustomWebApplicationFactory<Startup> factory) {
      this.factory = factory;
    }

    private async Task<HttpResponseMessage> PerformRequest(
      HttpClient client,
      EvolutionViewModel data
    ) {
      return await client.PatchAsync("/v1/pokemon/add-pre-evolution", new StringContent(
        JsonConvert.SerializeObject(data),
        Encoding.UTF8
      ) {
        Headers = {
          ContentType = new MediaTypeHeaderValue("application/json")
        }
      });
    }

    [Fact]
    public async Task Should_Return_200_Status_Code_If_Pre_Evolution_Added() {
      // Arrange
      var client = factory.CreateClient();
      await factory.AuthenticateAsync(client);

      var data = new EvolutionViewModel() {
        pokemonId = 2,
        pokemonEvolutionId = 1,
      };

      // Act
      var response = await PerformRequest(client, data);

      // Assert
      Assert.NotNull(response);
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Should_Return_401_Status_Code_If_Admin_Not_Authenticated() {
      // Arrange
      var client = factory.CreateClient();

      var data = new EvolutionViewModel() {
        pokemonId = 2,
        pokemonEvolutionId = 1,
      };

      // Act
      var response = await PerformRequest(client, data);

      // Assert
      Assert.NotNull(response);
      Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(152, 152)]
    public async Task Should_Return_400_Status_Code_If_Invalid_ViewModel(
      int pokemonId,
      int pokemonEvolutionId
    ) {
      // Arrange
      var client = factory.CreateClient();
      await factory.AuthenticateAsync(client);

      var data = new EvolutionViewModel() {
        pokemonId = pokemonId,
        pokemonEvolutionId = pokemonEvolutionId,
      };

      // Act
      var response = await PerformRequest(client, data);

      // Assert
      Assert.NotNull(response);
      Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Should_Return_404_Status_Code_If_Base_Pokemon_Not_Exist() {
      // Arrange
      var client = factory.CreateClient();
      await factory.AuthenticateAsync(client);

      var data = new EvolutionViewModel() {
        pokemonId = 65,
        pokemonEvolutionId = 1,
      };

      // Act
      var response = await PerformRequest(client, data);

      // Assert
      Assert.NotNull(response);
      Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Should_Return_404_Status_Code_If_Pre_Evolution_Pokemon_Not_Exist() {
      // Arrange
      var client = factory.CreateClient();
      await factory.AuthenticateAsync(client);

      var data = new EvolutionViewModel() {
        pokemonId = 2,
        pokemonEvolutionId = 89,
      };

      // Act
      var response = await PerformRequest(client, data);

      // Assert
      Assert.NotNull(response);
      Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Should_Return_400_Status_Code_If_Base_Pokemon_Already_Has_Pre_Evolution() {
      // Arrange
      var client = factory.CreateClient();
      await factory.AuthenticateAsync(client);

      var data = new EvolutionViewModel() {
        pokemonId = 3,
        pokemonEvolutionId = 2,
      };

      // Act
      var response = await PerformRequest(client, data);

      // Assert
      Assert.NotNull(response);
      Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
  }
}
