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
      var pokemonData = new Pokemon() {
        Id = 1,
        Name = "Bulbasaur",
        EvolutionLevel = 16,
        LevelingRate = 3,
        CatchRate = 11.9F,
        HatchTime = 5268,
        CreatedAt = DateTime.Now
      };

      var pokemonStatsData = new Stats() {
        HitPoints = 45,
        Attack = 49,
        Defense = 49,
        SpecialAttack = 65,
        SpecialDefense = 65,
        Speed = 45,
        Total = 318,
        PokemonId = 1,
      };

      var data = new CreatePokemonDTO() {
        PokemonData = pokemonData,
        PokemonStatsData = pokemonStatsData,
        AbilityId = 1,
        HiddenAbilityId = 2,
      };

      pokemonRepository.Setup(x => x.ExistsById(1)).ReturnsAsync(false);
      pokemonRepository.Setup(x => x.ExistsByName("Bulbasaur"))
        .ReturnsAsync(false);
      pokemonRepository.Setup(x => x.CreatePokemon(pokemonData))
        .ReturnsAsync(pokemonData);
      statsRepository.Setup(x => x.CreateStats(pokemonStatsData))
        .ReturnsAsync(pokemonStatsData);
      pokemonRepository.Setup(x => x.CreatePokemonAbility(pokemonData.Id, 1))
        .ReturnsAsync(new PokemonAbility() {
          PokemonId = pokemonData.Id,
          AbilityId = 1
        });
      pokemonRepository.Setup(x => x.CreatePokemonAbility(pokemonData.Id, 2))
        .ReturnsAsync(new PokemonAbility() {
          PokemonId = pokemonData.Id,
          AbilityId = 2
        });
      abilitiesRepository.Setup(x => x.FindByPokemonId(pokemonData.Id))
        .ReturnsAsync(new List<Ability>(){
          new Ability() {
            Id = 1,
            Name = "Lorem Ipsum #1",
            Effect = "Some description of the ability effect"
          },
          new Ability() {
            Id = 2,
            Name = "Lorem Ipsum #2",
            Effect = "Some description of the ability effect"
          }
        });

      // Act
      var response = await pokemonServices.CreatePokemon(data);

      // Assert
      Assert.IsType<PokemonViewModel>(response);
      Assert.IsType<StatsViewModel>(response.Stats);
      Assert.IsType<List<AbilityViewModel>>(response.Abilities);
      Assert.NotNull(response);
      Assert.NotNull(response.Stats);
      Assert.NotNull(response.Abilities);
      Assert.Equal(2, response.Abilities.Count);
      Assert.Equal(
        DateTime.Today.ToLocalTime().Date,
        response.CreatedAt.ToLocalTime().Date
      );
    }
  }
}
