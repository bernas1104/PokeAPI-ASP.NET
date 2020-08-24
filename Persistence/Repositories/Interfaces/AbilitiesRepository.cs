using System.Threading.Tasks;
using System.Collections.Generic;

using Domain;

namespace Persistence.Repositories.Interfaces {
  public interface AbilitiesRepository {
    public Task<Ability> CreateAbility(Ability ability);
    public Task<bool> ExistsById(int abilityId);
    public Task<bool> ExistsByName(string abilityName);
    public Task<IList<Ability>> FindByPokemonId(int pokemonId);
  }
}
