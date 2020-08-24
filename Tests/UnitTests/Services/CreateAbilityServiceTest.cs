using System.Threading.Tasks;

using Moq;
using Xunit;
using AutoMapper;

using Domain;
using Services.ViewModels;
using Services.Interfaces;
using Services.Exceptions;
using Tests.Bogus.ViewModel;
using Services.Implementations;
using Persistence.Repositories.Interfaces;

namespace Tests.UnitTests.Services {
  public class CreateAbilityServiceTest {
    private readonly Mock<AbilitiesRepository> abilitiesRepository;
    private readonly Mock<IMapper> mapper;

    public CreateAbilityServiceTest() {
      abilitiesRepository = new Mock<AbilitiesRepository>();
      mapper = new Mock<IMapper>();
    }

    [Fact]
    public async Task Should_Create_New_Ability() {
      // Arrange
      var data = BogusViewModel.AbilityViewModelFaker();
      var ability = new Ability() {
        Id = data.Id,
        Name = data.Name,
        Effect = data.Effect
      };

      var service = new AbilityServicesImpl(
        abilitiesRepository.Object,
        mapper.Object
      );

      mapper.Setup(x => x.Map<Ability>(data)).Returns(ability);
      abilitiesRepository.Setup(x => x.ExistsById(data.Id)).ReturnsAsync(false);
      abilitiesRepository.Setup(x => x.ExistsByName(data.Name))
        .ReturnsAsync(false);
      abilitiesRepository.Setup(x => x.CreateAbility(ability))
        .ReturnsAsync(ability);
      mapper.Setup(x => x.Map<AbilityViewModel>(ability)).Returns(data);

      // Act
      var response = await service.CreateAbility(data);

      // Assert
      Assert.InRange(response.Id, 1, 260);
      Assert.NotNull(response.Name);
      Assert.NotNull(response.Effect);
    }

    [Fact]
    public async Task Should_Not_Create_Ability_If_Id_Not_Unique() {
      // Arrange
      var data = BogusViewModel.AbilityViewModelFaker();
      var ability = new Ability() {
        Id = data.Id,
        Name = data.Name,
        Effect = data.Effect
      };

      var service = new AbilityServicesImpl(
        abilitiesRepository.Object,
        mapper.Object
      );

      mapper.Setup(x => x.Map<Ability>(data)).Returns(ability);
      abilitiesRepository.Setup(x => x.ExistsById(data.Id)).ReturnsAsync(true);

      // Act

      // Assert
      await Assert.ThrowsAsync<AbilityException>(
        () => service.CreateAbility(data)
      );
    }

    [Fact]
    public async Task Should_Not_Create_Ability_If_Name_Not_Unique() {
      // Arrange
      var data = BogusViewModel.AbilityViewModelFaker();
      var ability = new Ability() {
        Id = data.Id,
        Name = data.Name,
        Effect = data.Effect
      };

      var service = new AbilityServicesImpl(
        abilitiesRepository.Object,
        mapper.Object
      );

      mapper.Setup(x => x.Map<Ability>(data)).Returns(ability);
      abilitiesRepository.Setup(x => x.ExistsById(data.Id)).ReturnsAsync(false);
      abilitiesRepository.Setup(x => x.ExistsByName(data.Name))
        .ReturnsAsync(true);

      // Act

      // Assert
      await Assert.ThrowsAsync<AbilityException>(
        () => service.CreateAbility(data)
      );
    }
  }
}
