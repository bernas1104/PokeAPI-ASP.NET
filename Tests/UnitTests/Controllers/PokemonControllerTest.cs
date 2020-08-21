using System;
using System.IO;
using System.Threading.Tasks;

using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using PokeAPI.Controllers;
using Services.Interfaces;
using Services.ViewModels;
using Tests.Bogus.ViewModel;

namespace Tests.UnitTests.Controllers {
  public class PokemonControllerTest {
    private readonly Mock<PokemonServices> pokemonServices;
    private readonly IFormFile photo;

    public PokemonControllerTest() {
      pokemonServices = new Mock<PokemonServices>();

      var file = File.OpenRead(
        Path.Combine(
          Directory.GetCurrentDirectory(), "Utilities", "Bulbasaur.png"
        )
      );
      photo = new FormFile(file, file.Length, 0, null, Path.GetFileName(file.Name));
    }

    [Fact]
    public async Task Should_Return_201_Status_Code_With_Valid_ViewModel() {
      // Arrange
      var data = BogusViewModel.PokemonViewModelFaker();

      var responseData = data;
      responseData.Abilities = BogusViewModel.AbilityViewModelFaker(2);

      pokemonServices.Setup(x => x.CreatePokemon(data)).ReturnsAsync(
        responseData
      );

      var pokemonController = new PokemonController(pokemonServices.Object);

      // Act
      var response = await pokemonController.Create(data, photo);

      // Assert
      Assert.NotNull(response);
      Assert.IsType<CreatedResult>(response.Result);
    }

    [Fact]
    public async Task Mark_Should_Return_204_Status_Code_With_Valid_Pokemon_Number() {
      // Arrange
      var rnd = new Random();
      var id = rnd.Next(1, 152);

      pokemonServices.Setup(x => x.MarkPokemonAsSeen(id));

      var pokemonController = new PokemonController(pokemonServices.Object);

      // Act
      var response = await pokemonController.Mark(id);

      // Assert
      Assert.NotNull(response);
      Assert.IsType<NoContentResult>(response.Result);
    }

    [Fact]
    public async Task Capture_Should_Return_204_Status_Code_With_Valid_Pokemon_Number() {
      // Arrange
      var rnd = new Random();
      var id = rnd.Next(1, 152);

      pokemonServices.Setup(x => x.MarkPokemonAsCaptured(id));

      var pokemonController = new PokemonController(pokemonServices.Object);

      // Act
      var response = await pokemonController.Capture(id);

      // Assert
      Assert.NotNull(response);
      Assert.IsType<NoContentResult>(response.Result);
    }

    [Fact]
    public async Task AddPreEvolution_Should_Return_200_Status_Code_With_Valid_ViewModel() {
      // Arrange
      var rnd = new Random();
      var id = rnd.Next(1, 151);

      var data = new EvolutionViewModel() {
        pokemonId = id,
        pokemonEvolutionId = id + 1
      };

      var result = BogusViewModel.PokemonViewModelFaker();
      result.Abilities = null;
      result.Stats = null;

      pokemonServices.Setup(x => x.AddPokemonPreEvolution(data)).ReturnsAsync(
        result
      );

      var pokemonController = new PokemonController(pokemonServices.Object);

      // Act
      var response = await pokemonController.AddPreEvolution(data);

      // Assert
      Assert.NotNull(response);
      Assert.IsType<OkObjectResult>(response.Result);
    }

    [Fact]
    public async Task AddEvolution_Should_Return_200_Status_Code_With_Valid_ViewModel() {
      // Arrange
      var rnd = new Random();
      var id = rnd.Next(1, 151);

      var data = new EvolutionViewModel() {
        pokemonId = id,
        pokemonEvolutionId = id + 1
      };

      var result = BogusViewModel.PokemonViewModelFaker();
      result.Abilities = null;
      result.Stats = null;

      pokemonServices.Setup(x => x.AddPokemonEvolution(data)).ReturnsAsync(
        result
      );

      var pokemonController = new PokemonController(pokemonServices.Object);

      // Act
      var response = await pokemonController.AddEvolution(data);

      // Assert
      Assert.NotNull(response);
      Assert.IsType<OkObjectResult>(response.Result);
    }

    [Fact]
    public async Task Should_Return_400_Status_Code_With_Invalid_ViewModel() {
      // Arrange
      var data = new PokemonViewModel();

      var pokemonController = new PokemonController(pokemonServices.Object);

      // Act
      var response = await pokemonController.Create(data, photo);

      // Assert
      Assert.NotNull(response);
      Assert.IsType<BadRequestObjectResult>(response.Result);
    }

    [Fact]
    public async Task Should_Return_400_Status_Code_If_Photo_Not_Uploaded() {
      // Arrange
      var data = BogusViewModel.PokemonViewModelFaker();

      var pokemonController = new PokemonController(pokemonServices.Object);

      // Act
      var response = await pokemonController.Create(data, null);

      // Assert
      Assert.NotNull(response);
      Assert.IsType<BadRequestObjectResult>(response.Result);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(152)]
    public async Task Should_Return_400_Status_Code_If_Pokemon_Number_Invalid(int id) {
      // Arrange
      var pokemonController = new PokemonController(pokemonServices.Object);

      // Act
      var response = await pokemonController.Mark(id);

      // Assert
      Assert.NotNull(response);
      Assert.IsType<BadRequestObjectResult>(response.Result);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(152)]
    public async Task Capture_Should_Return_400_Status_Code_If_Pokemon_Number_Invalid(int id) {
      // Arrange
      var pokemonController = new PokemonController(pokemonServices.Object);

      // Act
      var response = await pokemonController.Capture(id);

      // Assert
      Assert.NotNull(response);
      Assert.IsType<BadRequestObjectResult>(response.Result);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(152, 152)]
    public async Task AddEvolution_Should_Return_400_Status_Code_If_Invalid_ViewModel(
      int pokemonId,
      int pokemonEvolutionId
    ) {
      // Arrange
      var data = new EvolutionViewModel() {
        pokemonId = pokemonId,
        pokemonEvolutionId = pokemonEvolutionId
      };

      var pokemonController = new PokemonController(pokemonServices.Object);

      // Act
      var response = await pokemonController.AddEvolution(data);

      // Assert
      Assert.NotNull(response);
      Assert.IsType<BadRequestObjectResult>(response.Result);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(152, 152)]
    public async Task AddPreEvolution_Should_Return_400_Status_Code_If_Invalid_ViewModel(
      int pokemonId,
      int pokemonEvolutionId
    ) {
      // Arrange
      var data = new EvolutionViewModel() {
        pokemonId = pokemonId,
        pokemonEvolutionId = pokemonEvolutionId
      };

      var pokemonController = new PokemonController(pokemonServices.Object);

      // Act
      var response = await pokemonController.AddPreEvolution(data);

      // Assert
      Assert.NotNull(response);
      Assert.IsType<BadRequestObjectResult>(response.Result);
    }
  }
}
