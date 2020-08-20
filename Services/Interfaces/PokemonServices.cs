using System.Threading.Tasks;

using Services.ViewModels;

namespace Services.Interfaces {
  public interface PokemonServices {
    public Task<PokemonViewModel> CreatePokemon(PokemonViewModel data);
    public Task<PokemonViewModel> MarkPokemonAsSeen(int id);
    public Task<PokemonViewModel> MarkPokemonAsCaptured(int id);
  }
}
