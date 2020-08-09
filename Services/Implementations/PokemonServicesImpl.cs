using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using AutoMapper;

using Domain;
using Services.DTOs;
using Services.Interfaces;
using Services.ViewModels;
using Persistence.Repositories.Interfaces;

namespace Services.Implementations {
  public class PokemonServicesImpl : PokemonServices {
    private readonly PokemonRepository pokemonRepository;
    private readonly StatsRepository statsRepository;
    private readonly AbilitiesRepository abilitiesRepository;
    private readonly IMapper mapper;

    public PokemonServicesImpl(
      PokemonRepository pokemonRepository,
      StatsRepository statsRepository,
      AbilitiesRepository abilitiesRepository,
      IMapper mapper
    ) {
      this.pokemonRepository = pokemonRepository;
      this.statsRepository = statsRepository;
      this.abilitiesRepository = abilitiesRepository;
      this.mapper = mapper;
    }

    public async Task<PokemonViewModel> CreatePokemon(CreatePokemonDTO data) {
      var pokemonData = data.PokemonData;

      var pokemon = await pokemonRepository.CreatePokemon(pokemonData);

      var statsData = data.PokemonStatsData;

      var stats = await statsRepository.CreateStats(statsData);

      var ability = await pokemonRepository.CreatePokemonAbility(
        pokemon.Id,
        data.AbilityId
      );
      var hiddenAbility = await pokemonRepository.CreatePokemonAbility(
        pokemon.Id,
        data.HiddenAbilityId
      );

      var abilities = await abilitiesRepository.FindByPokemonId(pokemon.Id);

      return new PokemonViewModel() {
        Id = pokemon.Id,
        Name = pokemon.Name,
        EvolutionLevel = pokemon.EvolutionLevel,
        LevelingRate = pokemon.LevelingRate,
        CatchRate = pokemon.CatchRate,
        HatchTime = pokemon.HatchTime,
        Stats = new StatsViewModel() {
          HitPoints = stats.HitPoints,
          Attack = stats.Attack,
          Defense = stats.Defense,
          SpecialAttack = stats.SpecialAttack,
          SpecialDefense = stats.SpecialDefense,
          Speed = stats.Speed,
          Total = stats.Total,
        },
        Abilities = new List<AbilityViewModel>() {
          new AbilityViewModel() {
            Id = abilities[0].Id,
            Name = abilities[0].Name,
            Effect = abilities[0].Effect
          },
          new AbilityViewModel() {
            Id = abilities[1].Id,
            Name = abilities[1].Name,
            Effect = abilities[1].Effect
          },
        },
        CreatedAt = pokemon.CreatedAt
      };
    }
  }
}
