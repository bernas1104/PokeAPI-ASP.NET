using System;
using System.Linq;
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

    public async Task<bool> ExistsById(int id) {
      return await dbContext.Pokemons.AnyAsync(pkmn => pkmn.Id == id);
    }

    public async Task<bool> ExistsByName(string name) {
      return await dbContext.Pokemons.AnyAsync(pkmn => pkmn.Name == name);
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
      throw new NotImplementedException();
    }

    private async Task SaveChangesToDatabase() {
      await dbContext.SaveChangesAsync();
    }
  }
}
