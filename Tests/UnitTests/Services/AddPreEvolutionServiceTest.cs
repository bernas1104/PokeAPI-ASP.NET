using System;
using System.Threading.Tasks;

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

namespace Tests.UnitTests.Services {
  public class AddPreEvolutionServiceTest {
    private readonly Mock<PokemonRepository> pokemonRepository;
    private readonly Mock<StatsRepository> statsRepository;
    private readonly Mock<AbilitiesRepository> abilitiesRepository;
    private readonly Mock<StorageProvider> storageProvider;
    private readonly Mock<IMapper> mapper;
    private readonly PokemonServices pokemonServices;

    public AddPreEvolutionServiceTest() {
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
    public async Task Should_Be_Able_To_Add_Pre_Evolution_Pokemon_To_Pokemon() {
      // Arrange
      var data = BogusViewModel.EvolutionViewModelFaker();
      var mappedPokemon = BogusViewModel.PokemonViewModelFaker();
      mappedPokemon.Abilities = null;
      mappedPokemon.Stats = null;

      var pokemon = BogusDomain.PokemonFaker();
      var preEvolutionPokemon = BogusDomain.PokemonFaker();
      pokemon.Id = data.pokemonId;
      preEvolutionPokemon.Id = data.pokemonEvolutionId - 2;

      pokemonRepository.Setup(x => x.ExistsById(data.pokemonId)).ReturnsAsync(true);
      pokemonRepository.Setup(x => x.ExistsById(data.pokemonEvolutionId)).ReturnsAsync(true);
      pokemonRepository.Setup(x => x.FindById(data.pokemonId)).ReturnsAsync(pokemon);
      pokemonRepository.Setup(x => x.FindById(data.pokemonEvolutionId)).ReturnsAsync(
        preEvolutionPokemon
      );
      pokemonRepository.Setup(x => x.SaveChangesToDatabase());

      mapper.Setup(x => x.Map<PokemonViewModel>(pokemon)).Returns(() => {
        mappedPokemon.Id = data.pokemonId;
        mappedPokemon.PreEvolutionId = data.pokemonEvolutionId;

        return mappedPokemon;
      });

      // Act
      var response = await pokemonServices.AddPokemonPreEvolution(data);

      // Assert
      Assert.Equal(data.pokemonId, response.Id);
      Assert.Equal(data.pokemonEvolutionId, response.PreEvolutionId);
    }

    [Fact]
    public async Task Should_Not_Be_Able_To_Add_Pre_Evolution_If_Base_Pokemon_Not_Exist() {
      // Arrange
      var data = BogusViewModel.EvolutionViewModelFaker();

      pokemonRepository.Setup(x => x.FindById(data.pokemonId)).ReturnsAsync((Pokemon)null);

      // Act

      // Assert
      await Assert.ThrowsAsync<PokemonException>(
        () => pokemonServices.AddPokemonPreEvolution(data)
      );
    }

    [Fact]
    public async Task Should_Not_Be_Able_To_Add_Pre_Evolution_If_Pre_Evolution_Pokemon_Not_Exist() {
      // Arrange
      var data = BogusViewModel.EvolutionViewModelFaker();

      var pokemon = BogusDomain.PokemonFaker();

      pokemonRepository.Setup(x => x.ExistsById(data.pokemonId)).ReturnsAsync(true);
      pokemonRepository.Setup(x => x.ExistsById(data.pokemonEvolutionId)).ReturnsAsync(false);
      pokemonRepository.Setup(x => x.FindById(data.pokemonId)).ReturnsAsync(pokemon);

      // Act

      // Asert
      await Assert.ThrowsAsync<PokemonException>(
        () => pokemonServices.AddPokemonPreEvolution(data)
      );
    }

    [Fact]
    public async Task Should_Not_Be_Able_To_Add_Pre_Evolution_If_Base_Pokemon_Already_Has_Pre_Evolution() {
      // Arrange
      var rnd = new Random();
      var data = BogusViewModel.EvolutionViewModelFaker();
      var mappedPokemon = BogusViewModel.PokemonViewModelFaker();
      mappedPokemon.Abilities = null;
      mappedPokemon.Stats = null;

      var pokemon = BogusDomain.PokemonFaker();
      var preEvolutionPokemon = BogusDomain.PokemonFaker();
      pokemon.Id = data.pokemonId;
      pokemon.PreEvolutionId = rnd.Next(1, 152);
      preEvolutionPokemon.Id = data.pokemonEvolutionId - 2;

      pokemonRepository.Setup(x => x.ExistsById(data.pokemonId)).ReturnsAsync(true);
      pokemonRepository.Setup(x => x.ExistsById(data.pokemonEvolutionId)).ReturnsAsync(true);
      pokemonRepository.Setup(x => x.FindById(data.pokemonId)).ReturnsAsync(pokemon);
      pokemonRepository.Setup(x => x.FindById(data.pokemonEvolutionId)).ReturnsAsync(
        preEvolutionPokemon
      );

      // Act

      // Assert
      await Assert.ThrowsAsync<PokemonException>(
        () => pokemonServices.AddPokemonPreEvolution(data)
      );
    }
  }
}
