using System.Threading.Tasks;

using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;

using Services.ViewModels;
using PokeAPI.Controllers;
using Services.Interfaces;
using Tests.Bogus.ViewModel;

namespace Tests.UnitTests.Controllers {
  public class PokemonControllerTest {
    private readonly Mock<PokemonServices> pokemonServices;

    public PokemonControllerTest() {
      pokemonServices = new Mock<PokemonServices>();
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
      var response = await pokemonController.Create(data);

      // Assert
      Assert.NotNull(response);
      Assert.IsType<CreatedResult>(response.Result);
    }

    [Fact]
    public async Task Should_Return_400_Status_Code_With_Invalid_ViewModel() {
      // Arrange
      var data = new PokemonViewModel();

      var pokemonController = new PokemonController(pokemonServices.Object);

      // Act
      var response = await pokemonController.Create(data);

      // Assert
      Assert.NotNull(response);
      Assert.IsType<BadRequestObjectResult>(response.Result);
    }
  }
}
