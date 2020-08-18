using System.Threading.Tasks;
using System.Collections.Generic;

using AutoMapper;

using Domain;
using Services.Interfaces;
using Services.ViewModels;
using Services.Exceptions;
using Services.Providers.StorageProvider;
using Persistence.Repositories.Interfaces;

namespace Services.Implementations {
  public class PokemonServicesImpl : PokemonServices {
    private readonly PokemonRepository pokemonRepository;
    private readonly StatsRepository statsRepository;
    private readonly AbilitiesRepository abilitiesRepository;
    private readonly StorageProvider storageProvider;
    private readonly IMapper mapper;

    public PokemonServicesImpl(
      PokemonRepository pokemonRepository,
      StatsRepository statsRepository,
      AbilitiesRepository abilitiesRepository,
      StorageProvider storageProvider,
      IMapper mapper
    ) {
      this.pokemonRepository = pokemonRepository;
      this.statsRepository = statsRepository;
      this.abilitiesRepository = abilitiesRepository;
      this.storageProvider = storageProvider;
      this.mapper = mapper;
    }

    public async Task<PokemonViewModel> CreatePokemon(PokemonViewModel data) {
      var pokemon = mapper.Map<Pokemon>(data);

      await CheckUniquePokemonId(pokemon.Id);

      await CheckUniquePokemonName(pokemon.Name);

      CheckAbilitiesAreDiferent(data.Abilities[0].Id, data.Abilities[1].Id);

      await CheckAbilityExists(data.Abilities[0].Id);

      await CheckAbilityExists(data.Abilities[1].Id);

      var photoFileName = storageProvider.SaveFile(data.PokemonPhoto);
      pokemon.Photo = photoFileName;

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

    private async Task CheckUniquePokemonId(int pokemonId) {
      var pokemonNumberRegistered = await pokemonRepository.ExistsById(
        pokemonId
      );

      if (pokemonNumberRegistered) {
        throw new PokemonException(
          "Pokemon number must be unique",
          400
        );
      }
    }

    private async Task CheckUniquePokemonName(string pokemonName) {
      var pokemonNameRegistered = await pokemonRepository.ExistsByName(
        pokemonName
      );

      if (pokemonNameRegistered) {
        throw new PokemonException(
          "Pokemon name must be unique",
          400
        );
      }
    }

    private async Task CheckAbilityExists(int abilityId) {
      var abilityExists = await abilitiesRepository.ExistsById(
        abilityId
      );

      if (!abilityExists) {
        throw new AbilityException(
          "Informed ability does not exist",
          404
        );
      }
    }

    private void CheckAbilitiesAreDiferent(
      int firstAbilityId,
      int secondAbilityId
    ) {
      if (firstAbilityId == secondAbilityId) {
        throw new AbilityException(
          "Informed abilities must be different",
          400
        );
      }
    }
  }
}
