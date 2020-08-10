using System.Threading.Tasks;
using System.Collections.Generic;

using AutoMapper;

using Domain;
using Services.Interfaces;
using Services.ViewModels;
using Services.Exceptions;
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

    public async Task<PokemonViewModel> CreatePokemon(PokemonViewModel data) {
      var pokemon = mapper.Map<Pokemon>(data);

      var pokemonNumberRegistered = await pokemonRepository.ExistsById(
        pokemon.Id
      );

      if (pokemonNumberRegistered) {
        throw new PokemonException(
          "Pokemon number must be unique",
          400
        );
      }

      var pokemonNameRegistered = await pokemonRepository.ExistsByName(
        pokemon.Name
      );

      if (pokemonNameRegistered) {
        throw new PokemonException(
          "Pokemon name must be unique",
          400
        );
      }

      var abilityExists = await abilitiesRepository.ExistsById(
        data.Abilities[0].Id
      );

      if (!abilityExists) {
        throw new AbilityException(
          "Informed ability does not exist",
          404
        );
      }

      abilityExists = await abilitiesRepository.ExistsById(
        data.Abilities[1].Id
      );

      if (!abilityExists) {
        throw new AbilityException(
          "Informed hidden ability does not exist",
          404
        );
      }

      pokemon = await pokemonRepository.CreatePokemon(pokemon);

      var stats = mapper.Map<Stats>(data.Stats);
      stats.PokemonId = pokemon.Id;
      stats = await statsRepository.CreateStats(stats);

      var ability = await pokemonRepository.CreatePokemonAbility(
        pokemon.Id,
        data.Abilities[0].Id
      );
      var hiddenAbility = await pokemonRepository.CreatePokemonAbility(
        pokemon.Id,
        data.Abilities[1].Id
      );

      var abilities = await abilitiesRepository.FindByPokemonId(pokemon.Id);

      var pokemonViewModel = mapper.Map<PokemonViewModel>(pokemon);
      pokemonViewModel.Stats = mapper.Map<StatsViewModel>(stats);
      pokemonViewModel.Abilities = mapper
        .Map<List<AbilityViewModel>>(abilities);

      return pokemonViewModel;
    }
  }
}
