using System.Threading.Tasks;

using Services.DTOs;
using Services.ViewModels;

namespace Services.Interfaces {
  public interface PokemonServices {
    public Task<PokemonViewModel> CreatePokemon(CreatePokemonDTO data);
  }
}
