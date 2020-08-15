using System;
using System.Threading.Tasks;

using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;

using PokeAPI.Controllers;
using Services.ViewModels;
using Services.Interfaces;
using Tests.Bogus.ViewModel;

namespace Tests.UnitTests.Controllers {
  public class AbilitiesControllerTest {
    private readonly Mock<AbilityServices> abilityServices;

    public AbilitiesControllerTest() {
      abilityServices = new Mock<AbilityServices>();
    }

    [Fact]
    public async Task Should_Return_200_Status_Code_With_Valid_ViewModel() {
      // var actionValue = Assert.IsAssignableFrom<IEnumerable<CustomerAddressViewModel>>(actionResult.Value);
      // Arrange
      var data = BogusViewModel.AbilityViewModelFaker();

      abilityServices.Setup(x => x.CreateAbility(data)).ReturnsAsync(data);

      var abilitiesController = new AbilitiesController(abilityServices.Object);

      // Act
      var response = await abilitiesController.Create(data);

      // Assert
      Assert.NotNull(response);
      Assert.IsType<CreatedResult>(response.Result);
    }

    [Fact]
    public async Task Should_Return_400_Status_Code_With_Invalid_ViewModel() {
      // Arrange
      var data = new AbilityViewModel();

      var abilitiesController = new AbilitiesController(abilityServices.Object);

      // Act
      var response = await abilitiesController.Create(data);

      // Assert
      Assert.NotNull(response);
      Assert.IsType<BadRequestObjectResult>(response.Result);
    }
  }
}
