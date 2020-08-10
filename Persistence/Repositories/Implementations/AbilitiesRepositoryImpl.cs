using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using Domain;
using Persistence.Context;
using Persistence.Repositories.Interfaces;

namespace Persistence.Repositories.Implementations {
  public class AbilitiesRepositoryImpl : AbilitiesRepository {
    private readonly PokemonDbContext dbContext;

    public AbilitiesRepositoryImpl(PokemonDbContext dbContext) {
      this.dbContext = dbContext;
    }

    public async Task<bool> ExistsById(int abilityId) {
      return await dbContext.Abilities
        .AsNoTracking()
        .AnyAsync(ability => ability.Id == abilityId);
    }

    public async Task<IList<Ability>> FindByPokemonId(int pokemonId) {
      var pokemonAbilities = await dbContext.PokemonAbilities
        .AsNoTracking()
        .Include(pokemonAbility => pokemonAbility.Ability)
        .Where(pokemonAbility => pokemonAbility.PokemonId == pokemonId)
        .ToListAsync();

      List<Ability> abilities = new List<Ability>();
      pokemonAbilities.ForEach(x => {
        abilities.Add(x.Ability);
      });

      return abilities;
    }
  }
}
