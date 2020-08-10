using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Moq;
using Xunit;

using AutoMapper;

using Domain;
using Services.DTOs;
using Services.ViewModels;
using Services.Interfaces;
using Services.Exceptions;
using Services.Implementations;
using Persistence.Repositories.Interfaces;

namespace Tests.UnitTests {
  public class CreatePokemonServiceTest {
    private readonly Mock<PokemonRepository> pokemonRepository;
    private readonly Mock<StatsRepository> statsRepository;
    private readonly Mock<AbilitiesRepository> abilitiesRepository;
    private readonly Mock<IMapper> mapper;
    private readonly PokemonServices pokemonServices;

    public CreatePokemonServiceTest() {
      pokemonRepository = new Mock<PokemonRepository>();
      statsRepository = new Mock<StatsRepository>();
      abilitiesRepository = new Mock<AbilitiesRepository>();
      mapper = new Mock<IMapper>();

      pokemonServices = new PokemonServicesImpl(
        pokemonRepository.Object,
        statsRepository.Object,
        abilitiesRepository.Object,
        mapper.Object
      );
    }

    [Fact]
    public async Task Should_Be_Able_To_Create_New_Pokemon() {
      // Arrange
      var pokemon = new Pokemon() {
        Id = 1,
        Name = "Bulbasaur",
        EvolutionLevel = 16,
        LevelingRate = 3,
        CatchRate = 11.9F,
        HatchTime = 5268,
        CreatedAt = DateTime.Now,
      };

      var stats = new Stats() {
        HitPoints = 45,
        Attack = 49,
        Defense = 49,
        SpecialAttack = 65,
        SpecialDefense = 65,
        Speed = 45,
        Total = 318,
      };

      var data = new PokemonViewModel() {
        Id = 1,
        Name = "Bulbasaur",
        EvolutionLevel = 16,
        LevelingRate = 3,
        CatchRate = 11.9F,
        HatchTime = 5268,
        CreatedAt = DateTime.Now,
        Stats = new StatsViewModel() {
          HitPoints = 45,
          Attack = 49,
          Defense = 49,
          SpecialAttack = 65,
          SpecialDefense = 65,
          Speed = 45,
          Total = 318,
        },
        Abilities = new List<AbilityViewModel>() {
          new AbilityViewModel() { Id = 1},
          new AbilityViewModel() { Id = 2},
        },
      };

      var ability = new Ability() {
        Id = 1,
        Name = "Lorem Ipsum #1",
        Effect = "Some description of the ability effect"
      };

      var hiddenAbility = new Ability() {
        Id = 2,
        Name = "Lorem Ipsum #2",
        Effect = "Some description of the ability effect"
      };

      var abilities = new List<Ability>() {ability, hiddenAbility};

      var pokemonViewModel = new PokemonViewModel() {
        Id = 1,
        Name = "Bulbasaur",
        EvolutionLevel = 16,
        LevelingRate = 3,
        CatchRate = 11.9F,
        HatchTime = 5268,
        CreatedAt = DateTime.Now,
      };

      var statsViewModel = data.Stats;

      var abilityViewModel = new AbilityViewModel() {
        Id = ability.Id,
        Name = ability.Name,
        Effect = ability.Effect,
      };

      var hiddenAbilityViewModel = new AbilityViewModel() {
        Id = hiddenAbility.Id,
        Name = hiddenAbility.Name,
        Effect = hiddenAbility.Effect,
      };

      mapper.Setup(x => x.Map<Pokemon>(data)).Returns(pokemon);
      mapper.Setup(x => x.Map<Stats>(data.Stats)).Returns(stats);
      pokemonRepository.Setup(x => x.ExistsById(1)).ReturnsAsync(false);
      pokemonRepository.Setup(x => x.ExistsByName("Bulbasaur"))
        .ReturnsAsync(false);
      pokemonRepository.Setup(x => x.CreatePokemon(pokemon))
        .ReturnsAsync(pokemon);
      statsRepository.Setup(x => x.CreateStats(stats))
        .ReturnsAsync(stats);
      abilitiesRepository.Setup(x => x.ExistsById(
        It.IsInRange<int>(1, 2, Moq.Range.Inclusive)
      )).ReturnsAsync(true);
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
          ability,
          hiddenAbility
        });
      mapper.Setup(x => x.Map<PokemonViewModel>(pokemon)).Returns(
        pokemonViewModel
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
      var response = await pokemonServices.CreatePokemon(data);

      // Assert
      Assert.IsType<PokemonViewModel>(response);
      Assert.IsType<StatsViewModel>(response.Stats);
      Assert.IsType<List<AbilityViewModel>>(response.Abilities);
      Assert.Equal(1, response.Id);
      Assert.Equal(318, response.Stats.Total);
      Assert.Equal(2, response.Abilities.Count);
      Assert.Equal(
        DateTime.Today.ToLocalTime().Date,
        response.CreatedAt.ToLocalTime().Date
      );
    }

    [Fact]
    public async Task Should_Not_Create_Pokemon_If_Number_Not_Unique() {
      // Arrange
      var pokemon = new Pokemon() {
        Id = 1,
        Name = "Bulbasaur",
        EvolutionLevel = 16,
        LevelingRate = 3,
        CatchRate = 11.9F,
        HatchTime = 5268,
        CreatedAt = DateTime.Now
      };

      var data = new PokemonViewModel() {
        Id = 1,
        Name = "Bulbasaur",
        EvolutionLevel = 16,
        LevelingRate = 3,
        CatchRate = 11.9F,
        HatchTime = 5268,
        CreatedAt = DateTime.Now,
        Stats = new StatsViewModel() {
          HitPoints = 45,
          Attack = 49,
          Defense = 49,
          SpecialAttack = 65,
          SpecialDefense = 65,
          Speed = 45,
          Total = 318,
        },
        Abilities = new List<AbilityViewModel>() {
          new AbilityViewModel() { Id = 1},
          new AbilityViewModel() { Id = 2},
        },
      };

      mapper.Setup(x => x.Map<Pokemon>(data)).Returns(pokemon);
      pokemonRepository.Setup(x => x.ExistsById(1)).ReturnsAsync(true);

      // Act

      // Assert
      await Assert.ThrowsAsync<PokemonException>(
        () => pokemonServices.CreatePokemon(data)
      );
    }

    [Fact]
    public async Task Should_Not_Create_Pokemon_If_Name_Not_Unique() {
      // Arrange
      var pokemon = new Pokemon() {
        Id = 1,
        Name = "Bulbasaur",
        EvolutionLevel = 16,
        LevelingRate = 3,
        CatchRate = 11.9F,
        HatchTime = 5268,
        CreatedAt = DateTime.Now
      };

      var pokemonStats = new StatsViewModel() {
        HitPoints = 45,
        Attack = 49,
        Defense = 49,
        SpecialAttack = 65,
        SpecialDefense = 65,
        Speed = 45,
        Total = 318,
      };

      var data = new PokemonViewModel() {
        Id = 1,
        Name = "Bulbasaur",
        EvolutionLevel = 16,
        LevelingRate = 3,
        CatchRate = 11.9F,
        HatchTime = 5268,
        CreatedAt = DateTime.Now,
        Stats = pokemonStats,
        Abilities = new List<AbilityViewModel>() {
          new AbilityViewModel() { Id = 1},
          new AbilityViewModel() { Id = 2},
        },
      };

      mapper.Setup(x => x.Map<Pokemon>(data)).Returns(pokemon);
      pokemonRepository.Setup(x => x.ExistsById(1)).ReturnsAsync(false);
      pokemonRepository.Setup(x => x.ExistsByName("Bulbasaur"))
        .ReturnsAsync(true);

      // Act

      // Assert
      await Assert.ThrowsAsync<PokemonException>(
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
      var pokemon = new Pokemon() {
        Id = 1,
        Name = "Bulbasaur",
        EvolutionLevel = 16,
        LevelingRate = 3,
        CatchRate = 11.9F,
        HatchTime = 5268,
        CreatedAt = DateTime.Now
      };

      var pokemonStats = new StatsViewModel() {
        HitPoints = 45,
        Attack = 49,
        Defense = 49,
        SpecialAttack = 65,
        SpecialDefense = 65,
        Speed = 45,
        Total = 318,
      };

      var stats = new Stats() {
        HitPoints = 45,
        Attack = 49,
        Defense = 49,
        SpecialAttack = 65,
        SpecialDefense = 65,
        Speed = 45,
        Total = 318,
      };

      var data = new PokemonViewModel() {
        Id = 1,
        Name = "Bulbasaur",
        EvolutionLevel = 16,
        LevelingRate = 3,
        CatchRate = 11.9F,
        HatchTime = 5268,
        CreatedAt = DateTime.Now,
        Stats = pokemonStats,
        Abilities = new List<AbilityViewModel>() {
          new AbilityViewModel() { Id = 1},
          new AbilityViewModel() { Id = 2},
        },
      };

      mapper.Setup(x => x.Map<Pokemon>(data)).Returns(pokemon);
      mapper.Setup(x => x.Map<Stats>(data.Stats)).Returns(stats);
      pokemonRepository.Setup(x => x.ExistsById(1)).ReturnsAsync(false);
      pokemonRepository.Setup(x => x.ExistsByName("Bulbasaur"))
        .ReturnsAsync(false);
      pokemonRepository.Setup(x => x.CreatePokemon(pokemon))
        .ReturnsAsync(pokemon);
      statsRepository.Setup(x => x.CreateStats(stats))
        .ReturnsAsync(stats);
      abilitiesRepository.Setup(x => x.ExistsById(1)).ReturnsAsync(
        abilityPresent
      );
      abilitiesRepository.Setup(x => x.ExistsById(2)).ReturnsAsync(
        hiddenAbilityPresent
      );

      // Act

      // Assert
      await Assert.ThrowsAsync<AbilityException>(
        () => pokemonServices.CreatePokemon(data)
      );
    }
  }
}
