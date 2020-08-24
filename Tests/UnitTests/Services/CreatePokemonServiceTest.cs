using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;

using Moq;
using Xunit;
using AutoMapper;
using Microsoft.AspNetCore.Http;

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
  public class CreatePokemonServiceTest {
    private readonly Mock<PokemonRepository> pokemonRepository;
    private readonly Mock<StatsRepository> statsRepository;
    private readonly Mock<AbilitiesRepository> abilitiesRepository;
    private readonly Mock<StorageProvider> storageProvider;
    private readonly Mock<IMapper> mapper;
    private readonly PokemonServices pokemonServices;

    public CreatePokemonServiceTest() {
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

    private IFormFile LoadPokemonPhoto() {
      var file = File.OpenRead(
        Path.Combine(
          Directory.GetCurrentDirectory(),
          "Utilities",
          "Bulbasaur.png"
        )
      );

      return new FormFile(file, 0, file.Length, null, Path.GetFileName(file.Name));
    }

    [Fact]
    public async Task Should_Be_Able_To_Create_New_Pokemon() {
      // Arrange
      var pokemon = BogusDomain.PokemonFaker();

      var stats = BogusDomain.StatsFaker();

      var abilities = BogusDomain.AbilitiesFaker();

      var data = BogusViewModel.PokemonViewModelFaker(pokemon, stats, abilities);
      var photo = LoadPokemonPhoto();
      data.PokemonPhoto = photo;

      var response = new PokemonViewModel() {
        Id = pokemon.Id,
        Name = pokemon.Name,
        EvolutionLevel = pokemon.EvolutionLevel,
        LevelingRate = pokemon.LevelingRate,
        CatchRate = pokemon.CatchRate,
        HatchTime = pokemon.HatchTime,
        CreatedAt = pokemon.CreatedAt,
      };

      var statsViewModel = data.Stats;

      var abilityViewModel = new AbilityViewModel() {
        Id = abilities[0].Id,
        Name = abilities[0].Name,
        Effect = abilities[0].Effect,
      };

      var hiddenAbilityViewModel = new AbilityViewModel() {
        Id = abilities[1].Id,
        Name = abilities[1].Name,
        Effect = abilities[1].Effect,
      };

      mapper.Setup(x => x.Map<Pokemon>(data)).Returns(pokemon);
      mapper.Setup(x => x.Map<Stats>(data.Stats)).Returns(stats);
      pokemonRepository.Setup(x => x.ExistsById(pokemon.Id)).ReturnsAsync(false);
      pokemonRepository.Setup(x => x.ExistsByName(pokemon.Name))
        .ReturnsAsync(false);
      abilitiesRepository.Setup(x => x.ExistsById(
        abilities[0].Id
      )).ReturnsAsync(true);
      abilitiesRepository.Setup(x => x.ExistsById(
        abilities[1].Id
      )).ReturnsAsync(true);
      storageProvider.Setup(x => x.SaveFile(photo)).Returns(Guid.NewGuid().ToString());
      pokemonRepository.Setup(x => x.CreatePokemon(pokemon))
        .ReturnsAsync(pokemon);
      statsRepository.Setup(x => x.CreateStats(stats))
        .ReturnsAsync(stats);
      pokemonRepository.Setup(x => x.CreatePokemonAbility(
        data.Id, data.Abilities[0].Id
      )).ReturnsAsync(new PokemonAbility() {
        PokemonId = data.Id,
        AbilityId = data.Abilities[0].Id
      });
      pokemonRepository.Setup(x => x.CreatePokemonAbility(
        data.Id, data.Abilities[1].Id
      )).ReturnsAsync(new PokemonAbility() {
        PokemonId = data.Id,
        AbilityId = data.Abilities[1].Id
      });
      abilitiesRepository.Setup(x => x.FindByPokemonId(data.Id))
        .ReturnsAsync(new List<Ability>(){
          abilities[0],
          abilities[1]
        });
      mapper.Setup(x => x.Map<PokemonViewModel>(pokemon)).Returns(
        response
      );
      mapper.Setup(x => x.Map<StatsViewModel>(stats)).Returns(
        statsViewModel
      );
      mapper.Setup(x => x.Map<List<AbilityViewModel>>(abilities)).Returns(
        new List<AbilityViewModel>() {
          abilityViewModel,
          hiddenAbilityViewModel
        }
      );

      // Act
      var result = await pokemonServices.CreatePokemon(data);

      // Assert
      Assert.IsType<PokemonViewModel>(result);
      Assert.IsType<StatsViewModel>(result.Stats);
      Assert.IsType<List<AbilityViewModel>>(result.Abilities);
      Assert.Equal(pokemon.Id, result.Id);
      Assert.Equal(stats.Total, result.Stats.Total);
      Assert.Equal(2, result.Abilities.Count);
      Assert.Equal(
        DateTime.Today.ToLocalTime().Date,
        result.CreatedAt.ToLocalTime().Date
      );
    }

    [Fact]
    public async Task Should_Not_Create_Pokemon_If_Number_Not_Unique() {
      // Arrange
      var pokemon = BogusDomain.PokemonFaker();

      var stats = BogusDomain.StatsFaker();

      var abilities = BogusDomain.AbilitiesFaker();

      var data = BogusViewModel.PokemonViewModelFaker(pokemon, stats, abilities);

      mapper.Setup(x => x.Map<Pokemon>(data)).Returns(pokemon);
      pokemonRepository.Setup(x => x.ExistsById(pokemon.Id)).ReturnsAsync(true);

      // Act

      // Assert
      await Assert.ThrowsAsync<PokemonException>(
        () => pokemonServices.CreatePokemon(data)
      );
    }

    [Fact]
    public async Task Should_Not_Create_Pokemon_If_Name_Not_Unique() {
      // Arrange
      var pokemon = BogusDomain.PokemonFaker();

      var stats = BogusDomain.StatsFaker();

      var abilities = BogusDomain.AbilitiesFaker();

      var data = BogusViewModel.PokemonViewModelFaker(pokemon, stats, abilities);

      mapper.Setup(x => x.Map<Pokemon>(data)).Returns(pokemon);
      pokemonRepository.Setup(x => x.ExistsById(pokemon.Id)).ReturnsAsync(false);
      pokemonRepository.Setup(x => x.ExistsByName(pokemon.Name))
        .ReturnsAsync(true);

      // Act

      // Assert
      await Assert.ThrowsAsync<PokemonException>(
        () => pokemonServices.CreatePokemon(data)
      );
    }

    [Fact]
    public async Task Should_Not_Create_Pokemon_If_Abilities_Are_Equal() {
      // Arrange
      var pokemon = BogusDomain.PokemonFaker();

      var stats = BogusDomain.StatsFaker();

      var abilities = BogusDomain.AbilitiesFaker();
      abilities[1].Id = abilities[0].Id;

      var data = BogusViewModel.PokemonViewModelFaker(pokemon, stats, abilities);

      mapper.Setup(x => x.Map<Pokemon>(data)).Returns(pokemon);
      mapper.Setup(x => x.Map<Stats>(data.Stats)).Returns(stats);
      pokemonRepository.Setup(x => x.ExistsById(pokemon.Id)).ReturnsAsync(false);
      pokemonRepository.Setup(x => x.ExistsByName(pokemon.Name))
        .ReturnsAsync(false);
      pokemonRepository.Setup(x => x.CreatePokemon(pokemon))
        .ReturnsAsync(pokemon);
      statsRepository.Setup(x => x.CreateStats(stats))
        .ReturnsAsync(stats);

      // Act

      // Assert
      await Assert.ThrowsAsync<AbilityException>(
        () => pokemonServices.CreatePokemon(data)
      );
    }

    [Theory]
    [InlineData(false, true)]
    [InlineData(true, false)]
    public async Task Should_Not_Create_Pokemon_If_Any_Abilities_Not_Exist(
      bool abilityPresent, bool hiddenAbilityPresent
    ) {
      // Arrange
      var pokemon = BogusDomain.PokemonFaker();

      var stats = BogusDomain.StatsFaker();

      var abilities = BogusDomain.AbilitiesFaker();

      var data = BogusViewModel.PokemonViewModelFaker(pokemon, stats, abilities);

      mapper.Setup(x => x.Map<Pokemon>(data)).Returns(pokemon);
      mapper.Setup(x => x.Map<Stats>(data.Stats)).Returns(stats);
      pokemonRepository.Setup(x => x.ExistsById(pokemon.Id)).ReturnsAsync(false);
      pokemonRepository.Setup(x => x.ExistsByName(pokemon.Name))
        .ReturnsAsync(false);
      pokemonRepository.Setup(x => x.CreatePokemon(pokemon))
        .ReturnsAsync(pokemon);
      statsRepository.Setup(x => x.CreateStats(stats))
        .ReturnsAsync(stats);
      abilitiesRepository.Setup(x => x.ExistsById(abilities[0].Id))
        .ReturnsAsync(abilityPresent);
      abilitiesRepository.Setup(x => x.ExistsById(abilities[1].Id))
        .ReturnsAsync(hiddenAbilityPresent);

      // Act

      // Assert
      await Assert.ThrowsAsync<AbilityException>(
        () => pokemonServices.CreatePokemon(data)
      );
    }
  }
}
