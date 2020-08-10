using System.Threading.Tasks;

using Services.ViewModels;

namespace Services.Interfaces {
  public interface PokemonServices {
    public Task<PokemonViewModel> CreatePokemon(PokemonViewModel data);
  }
}
