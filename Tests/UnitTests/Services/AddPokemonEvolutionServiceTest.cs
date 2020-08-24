using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Moq;
using Xunit;
using AutoMapper;

using Domain;
using Tests.Bogus.Domain;
using Services.ViewModels;
using Services.Interfaces;
using Services.Exceptions;
using Tests.Bogus.ViewModel;
using Services.Implementations;
using Services.Providers.StorageProvider;
using Persistence.Repositories.Interfaces;

namespace Tests.UnitTests {
  public class AddPokemonEvolutionServiceTest {
    private readonly Mock<PokemonRepository> pokemonRepository;
    private readonly Mock<StatsRepository> statsRepository;
    private readonly Mock<AbilitiesRepository> abilitiesRepository;
    private readonly Mock<StorageProvider> storageProvider;
    private readonly Mock<IMapper> mapper;
    private readonly PokemonServices pokemonServices;

    public AddPokemonEvolutionServiceTest() {
      pokemonRepository = new Mock<PokemonRepository>();
      statsRepository = new Mock<StatsRepository>();
      abilitiesRepository = new Mock<AbilitiesRepository>();
      storageProvider = new Mock<StorageProvider>();
      mapper = new Mock<IMapper>();

      pokemonServices = new PokemonServicesImpl(
        pokemonRepository.Object,
        statsRepository.Object,
        abilitiesRepository.Object,
        storageProvider.Object,
        mapper.Object
      );
    }

    [Fact]
    public async Task Should_Be_Able_To_Add_Evolution_To_Pokemon() {
      // Arrange
      var data = BogusViewModel.EvolutionViewModelFaker();

      var preEvolutionPokemon = BogusDomain.PokemonFaker();
      var evolutionPokemon = BogusDomain.PokemonFaker();
      preEvolutionPokemon.Id = data.pokemonId;
      evolutionPokemon.Id = data.pokemonEvolutionId;

      var responseData = BogusViewModel.PokemonViewModelFaker();
      responseData.Evolution = new List<Pokemon>() {evolutionPokemon};

      pokemonRepository.Setup(x => x.ExistsById(data.pokemonId)).ReturnsAsync(true);
      pokemonRepository.Setup(x => x.ExistsById(data.pokemonEvolutionId))
        .ReturnsAsync(true);
      pokemonRepository.Setup(x => x.FindById(data.pokemonId))
        .ReturnsAsync(preEvolutionPokemon);
      pokemonRepository.Setup(x => x.FindByIdWithEvolutions(data.pokemonEvolutionId))
        .ReturnsAsync(evolutionPokemon);
      pokemonRepository.Setup(x => x.SaveChangesToDatabase());
      mapper.Setup(x => x.Map<PokemonViewModel>(preEvolutionPokemon)).Returns(
        responseData
      );

      // Act
      var response = await pokemonServices.AddPokemonEvolution(data);
      var evolutions = response.Evolution.ToList();

      // Assert
      Assert.IsType<PokemonViewModel>(response);
      Assert.Equal(evolutionPokemon.Id, evolutions[0].Id);
    }

    [Fact]
    public async Task Should_Not_Be_Able_To_Add_Evolution_To_Pokemon_If_Base_Pokemon_Not_Exist() {
      // Arrange
      var data = BogusViewModel.EvolutionViewModelFaker();

      pokemonRepository.Setup(x => x.ExistsById(data.pokemonId)).ReturnsAsync(false);

      // Act

      // Assert
      await Assert.ThrowsAsync<PokemonException>(
        () => pokemonServices.AddPokemonEvolution(data)
      );
    }

    [Fact]
    public async Task Should_Not_Be_Able_To_Add_Evolution_To_Pokemon_If_Evolution_Not_Exist() {
      // Arrange
      var data = BogusViewModel.EvolutionViewModelFaker();

      var preEvolutionPokemon = BogusDomain.PokemonFaker();

      pokemonRepository.Setup(x => x.ExistsById(data.pokemonId)).ReturnsAsync(true);
      pokemonRepository.Setup(x => x.ExistsById(data.pokemonEvolutionId))
        .ReturnsAsync(false);

      // Act

      // Assert
      await Assert.ThrowsAsync<PokemonException>(
        () => pokemonServices.AddPokemonEvolution(data)
      );
    }

    [Fact]
    public async Task Should_Not_Be_Able_To_Add_Evolution_To_Pokemon_If_Evolution_Already_Has_Pre_Evolution() {
      // Arrange
      var data = BogusViewModel.EvolutionViewModelFaker();

      var preEvolutionPokemon = BogusDomain.PokemonFaker();
      var evolutionPokemon = BogusDomain.PokemonFaker();
      preEvolutionPokemon.Id = data.pokemonId;
      evolutionPokemon.Id = data.pokemonEvolutionId;
      evolutionPokemon.PreEvolutionId = 1;

      pokemonRepository.Setup(x => x.ExistsById(data.pokemonId)).ReturnsAsync(true);
      pokemonRepository.Setup(x => x.ExistsById(data.pokemonEvolutionId))
        .ReturnsAsync(true);
      pokemonRepository.Setup(x => x.FindById(data.pokemonId))
        .ReturnsAsync(preEvolutionPokemon);
      pokemonRepository.Setup(x => x.FindByIdWithEvolutions(data.pokemonEvolutionId))
        .ReturnsAsync(evolutionPokemon);

      // Act

      // Assert
      await Assert.ThrowsAsync<PokemonException>(
        () => pokemonServices.AddPokemonEvolution(data)
      );
    }
  }
}
