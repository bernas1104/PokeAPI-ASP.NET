using System.Threading.Tasks;

using Domain;

namespace Persistence.Repositories.Interfaces {
  public interface PokemonRepository {
    public Task<bool> ExistsById(int id);
    public Task<bool> ExistsByName(string name);
    public Task<Pokemon> CreatePokemon(Pokemon pokemon);
    public Task<PokemonAbility> CreatePokemonAbility(
      int pokemonId,
      int abilityId
    );
  }
}
