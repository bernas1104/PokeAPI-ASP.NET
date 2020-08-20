using System.IO;
using System.Net;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

using Xunit;
using Newtonsoft.Json;

using PokeAPI;
using Services.ViewModels;
using Tests.Bogus.ViewModel;
using System;

namespace Tests.IntegrationTests {
  public class CreatePokemonTest :
    IClassFixture<CustomWebApplicationFactory<Startup>> {
    private readonly CustomWebApplicationFactory<Startup> factory;

    public CreatePokemonTest(CustomWebApplicationFactory<Startup> factory) {
      this.factory = factory;
    }

    private async Task<HttpResponseMessage> PerformRequest(
      HttpClient client,
      PokemonViewModel data,
      bool uploadPhoto = true
    ) {
      var formData = new MultipartFormDataContent();

      formData.Add(new StringContent(
        JsonConvert.SerializeObject(data), Encoding.UTF8
      ));

      if (uploadPhoto) {
        var file = File.OpenRead(
          Path.Combine(Directory.GetCurrentDirectory(), "Utilities", "Bulbasaur.png")
        );

        formData.Add(new StreamContent(file), "photo", Path.GetFileName(file.Name));
      }

      return await client.PostAsync("/v1/pokemon", formData);
    }

    [Fact]
    public async Task Should_Return_201_Status_Code_If_Pokemon_Created() {
      // Arrange
      var client = factory.CreateClient();
      await factory.AuthenticateAsync(client);

      var data = BogusViewModel.PokemonViewModelFaker();

      // Act
      var response = await PerformRequest(client, data);
      var pokemonViewModel = JsonConvert.DeserializeObject<PokemonViewModel>(
        await response.Content.ReadAsStringAsync()
      );

      // Assert
      Assert.Equal(HttpStatusCode.Created, response.StatusCode);

      File.Delete(
        Path.Combine(
          Directory.GetCurrentDirectory(),
          "..",
          "..",
          "..",
          "..",
          "PokeAPI",
          "wwwroot",
          "images",
          pokemonViewModel.PhotoUrl
        )
      );
    }

    [Fact]
    public async Task Should_Return_401_Status_Code_If_Admin_Not_Authenticated() {
      // Arrange
      var client = factory.CreateClient();

      var data = BogusViewModel.PokemonViewModelFaker();

      // Act
      var response = await PerformRequest(client, data);

      // Assert
      Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Should_Return_400_If_Pokemo_ID_Invalid() {
      // Arrange
      var client = factory.CreateClient();
      await factory.AuthenticateAsync(client);

      var data = BogusViewModel.PokemonViewModelFaker();
      data.Id = 0;

      // Act
      var response = await PerformRequest(client, data);

      // Assert
      Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Theory]
    [InlineData((string)null)]
    [InlineData("")]
    [InlineData("aaaaaaaaaaaaaaaaaaaaa")]
    public async Task Should_Return_400_If_Pokemon_Name_Invalid(string name) {
      // Arrange
      var client = factory.CreateClient();
      await factory.AuthenticateAsync(client);

      var data = BogusViewModel.PokemonViewModelFaker();
      data.Name = name;

      // Act
      var response = await PerformRequest(client, data);

      // Assert
      Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(6)]
    public async Task Should_Return_400_If_Pokemon_Leveling_Rate_Invalid(
      int levelingRate
    ) {
      // Arrange
      var client = factory.CreateClient();
      await factory.AuthenticateAsync(client);

      var data = BogusViewModel.PokemonViewModelFaker();
      data.LevelingRate = levelingRate;

      // Act
      var response = await PerformRequest(client, data);

      // Assert
      Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Theory]
    [InlineData(-0.1F)]
    [InlineData(100.1F)]
    public async Task Should_Return_400_If_Pokemon_Catch_Rate_Invalid(
      float catchRate
    ) {
      // Arrange
      var client = factory.CreateClient();
      await factory.AuthenticateAsync(client);

      var data = BogusViewModel.PokemonViewModelFaker();
      data.CatchRate = catchRate;

      // Act
      var response = await PerformRequest(client, data);

      // Assert
      Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Theory]
    [InlineData(999)]
    [InlineData(31001)]
    public async Task Should_Return_400_If_Pokemon_Hatch_Time_Invalid(
      int hatchTime
    ) {
      // Arrange
      var client = factory.CreateClient();
      await factory.AuthenticateAsync(client);

      var data = BogusViewModel.PokemonViewModelFaker();
      data.HatchTime = hatchTime;

      // Act
      var response = await PerformRequest(client, data);

      // Assert
      Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(0, 261)]
    [InlineData(1, 0)]
    [InlineData(1, 261)]
    public async Task Should_Return_400_If_Pokemon_Abilities_Invalid(
      int ability,
      int id
    ) {
      // Arrange
      var client = factory.CreateClient();
      await factory.AuthenticateAsync(client);

      var data = BogusViewModel.PokemonViewModelFaker();
      data.Abilities[ability].Id = id;

      // Act
      var response = await PerformRequest(client, data);

      // Assert
      Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Theory]
    [InlineData(0, 5)]
    [InlineData(201, 1201)]
    public async Task Should_Return_400_If_Pokemon_Stats_Invalid(
      int stats,
      int total
    ) {
      // Arrange
      var client = factory.CreateClient();
      await factory.AuthenticateAsync(client);

      var data = BogusViewModel.PokemonViewModelFaker();
      data.Stats.HitPoints = stats;
      data.Stats.Attack = stats;
      data.Stats.Defense = stats;
      data.Stats.SpecialAttack = stats;
      data.Stats.SpecialDefense = stats;
      data.Stats.Speed = stats;
      data.Stats.Total = total;

      // Act
      var response = await PerformRequest(client, data);
      var errors = JsonConvert.DeserializeObject<IList<dynamic>>(
        await response.Content.ReadAsStringAsync()
      );

      // Assert
      Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
      Assert.Equal(7, errors.Count);
    }
  }
}
