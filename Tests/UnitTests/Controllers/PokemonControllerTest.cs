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
  }
}
