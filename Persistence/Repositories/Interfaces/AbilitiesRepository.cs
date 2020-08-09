using System.Threading.Tasks;
using System.Collections.Generic;

using Domain;

namespace Persistence.Repositories.Interfaces {
  public interface AbilitiesRepository {
    public Task<IList<Ability>> FindByPokemonId(int pokemonId);
  }
}
