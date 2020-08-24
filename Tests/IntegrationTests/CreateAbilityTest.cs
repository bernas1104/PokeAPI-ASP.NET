using System.Net;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;

using Xunit;
using Newtonsoft.Json;

using PokeAPI;
using Services.ViewModels;
using Tests.Bogus.ViewModel;

namespace Tests.IntegrationTests {
  public class CreateAbilityTest :
    IClassFixture<CustomWebApplicationFactory<Startup>> {
    private readonly CustomWebApplicationFactory<Startup> factory;

    public CreateAbilityTest(CustomWebApplicationFactory<Startup> factory) {
      this.factory = factory;
    }

    private async Task<HttpResponseMessage> PerformRequest(
      HttpClient client,
      AbilityViewModel data
    ) {
      return await client.PostAsync("/v1/abilities", new StringContent(
        JsonConvert.SerializeObject(data),
        Encoding.UTF8
      ) {
        Headers = {
          ContentType = new MediaTypeHeaderValue("application/json")
        }
      });
    }

    [Fact]
    public async Task Should_Return_201_Status_Code_If_Ability_Created() {
      // Arrange
      var client = factory.CreateClient();
      await factory.AuthenticateAsync(client);

      var data = BogusViewModel.AbilityViewModelFaker();
      data.Id = 3;

      // Act
      var response = await PerformRequest(client, data);

      // Assert
      Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task Should_Return_401_Status_Code_If_Admin_Not_Authenticated() {
      // Arrange
      var client = factory.CreateClient();

      var data = BogusViewModel.AbilityViewModelFaker();

      // Act
      var response = await PerformRequest(client, data);

      // Assert
      Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Should_Return_400_Status_Code_If_ViewModel_Invalid() {
      // Arrange
      var client = factory.CreateClient();
      await factory.AuthenticateAsync(client);

      var data = new AbilityViewModel();

      // Act
      var response = await PerformRequest(client, data);

      // Assert
      Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Should_Return_400_Status_Code_If_Ability_Id_Not_Unique() {
      // Arrange
      var client = factory.CreateClient();
      await factory.AuthenticateAsync(client);

      var data = BogusViewModel.AbilityViewModelFaker();
      data.Id = 1;

      // Act
      var response = await PerformRequest(client, data);

      // Assert
      Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Should_Return_400_Status_Code_If_Ability_Name_Not_Unique() {
      // Arrange
      var client = factory.CreateClient();
      await factory.AuthenticateAsync(client);

      var data = BogusViewModel.AbilityViewModelFaker();
      data.Name = "Lorem Ipsum 1";

      // Act
      var response = await PerformRequest(client, data);

      // Assert
      Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
  }
}
