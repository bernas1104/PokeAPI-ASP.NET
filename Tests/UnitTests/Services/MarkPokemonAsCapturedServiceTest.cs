using System;
using System.Threading.Tasks;

using Moq;
using Xunit;
using AutoMapper;

using Tests.Bogus.Domain;
using Services.ViewModels;
using Services.Interfaces;
using Services.Exceptions;
using Services.Implementations;
using Services.Providers.StorageProvider;
using Persistence.Repositories.Interfaces;

namespace Tests.UnitTests.Services {
  public class MarkPokemonAsCaptured {
    private readonly Mock<PokemonRepository> pokemonRepository;
    private readonly Mock<StatsRepository> statsRepository;
    private readonly Mock<AbilitiesRepository> abilitiesRepository;
    private readonly Mock<StorageProvider> storageProvider;
    private readonly Mock<IMapper> mapper;
    private readonly PokemonServices pokemonServices;

    public MarkPokemonAsCaptured() {
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
    public async Task Should_Be_Able_To_Mark_Pokemon_As_Captured() {
      // Arrange
      var rnd = new Random();
      var id = rnd.Next(1, 152);

      var fakePokemon = BogusDomain.PokemonFaker();

      pokemonRepository.Setup(x => x.ExistsById(id)).ReturnsAsync(true);
      pokemonRepository.Setup(x => x.FindById(id)).ReturnsAsync(fakePokemon);
      pokemonRepository.Setup(x => x.SaveChangesToDatabase());
      mapper.Setup(x => x.Map<PokemonViewModel>(fakePokemon))
        .Returns(() => new PokemonViewModel() {
          Id = fakePokemon.Id,
          Name = fakePokemon.Name,
          EvolutionLevel = fakePokemon.EvolutionLevel,
          LevelingRate = fakePokemon.LevelingRate,
          CatchRate = fakePokemon.CatchRate,
          HatchTime = fakePokemon.HatchTime,
          Seen = fakePokemon.Seen,
          Captured = fakePokemon.Captured,
        });

      // Act
      var response = await pokemonServices.MarkPokemonAsCaptured(id);

      // Assert
      Assert.NotNull(response);
      Assert.True(response.Captured);
    }

    [Fact]
    public async Task Should_Not_Be_Able_To_Mark_Pokemon_As_Captured_If_It_Not_Exists() {
      // Arrange
      var rnd = new Random();
      var id = rnd.Next(1, 152);

      pokemonRepository.Setup(x => x.ExistsById(id)).ReturnsAsync(false);

      // Act

      // Assert
      await Assert.ThrowsAsync<PokemonException>(
        () => pokemonServices.MarkPokemonAsCaptured(id)
      );
    }

    [Fact]
    public async Task Should_Not_Be_Able_To_Mark_Pokemon_As_Captured_If_Already_Captured() {
      // Arrange
      var rnd = new Random();
      var id = rnd.Next(1, 152);

      var fakePokemon = BogusDomain.PokemonFaker();
      fakePokemon.Seen = true;

      pokemonRepository.Setup(x => x.ExistsById(id)).ReturnsAsync(false);
      pokemonRepository.Setup(x => x.FindById(id)).ReturnsAsync(fakePokemon);

      // Act

      // Assert
      await Assert.ThrowsAsync<PokemonException>(
        () => pokemonServices.MarkPokemonAsCaptured(id)
      );
    }
  }
}
