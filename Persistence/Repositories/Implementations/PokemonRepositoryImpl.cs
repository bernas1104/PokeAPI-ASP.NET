using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Domain;
using Persistence.Context;
using Persistence.Repositories.Interfaces;

namespace Persistence.Repositories.Implementations {
  public class PokemonRepositoryImpl : PokemonRepository {
    private readonly PokemonDbContext dbContext;

    public PokemonRepositoryImpl(PokemonDbContext dbContext) {
      this.dbContext = dbContext;
    }

    public async Task<Pokemon> FindById(int id) {
      return await dbContext.Pokemons
        .FirstOrDefaultAsync(pkmn => pkmn.Id == id);
    }

    public async Task<bool> ExistsById(int id) {
      return await dbContext.Pokemons
        .AsNoTracking()
        .AnyAsync(pkmn => pkmn.Id == id);
    }

    public async Task<bool> ExistsByName(string name) {
      return await dbContext.Pokemons
        .AsNoTracking()
        .AnyAsync(pkmn => pkmn.Name == name);
    }

    public async Task<Pokemon> CreatePokemon(Pokemon pokemon) {
      dbContext.Pokemons.Add(pokemon);
      await SaveChangesToDatabase();
      return pokemon;
    }

    public async Task<PokemonAbility> CreatePokemonAbility(
      int pokemonId,
      int abilityId
    ) {
      var pokemonAbility = new PokemonAbility() {
        PokemonId = pokemonId,
        AbilityId = abilityId,
      };

      dbContext.PokemonAbilities.Add(pokemonAbility);

      await SaveChangesToDatabase();

      return pokemonAbility;
    }

    public async Task SaveChangesToDatabase() {
      await dbContext.SaveChangesAsync();
    }
  }
}
