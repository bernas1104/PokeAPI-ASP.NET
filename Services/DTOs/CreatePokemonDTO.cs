using Domain;

namespace Services.DTOs {
  public class CreatePokemonDTO {
    public Pokemon PokemonData { get; set; }
    public Stats PokemonStatsData { get; set; }
    public int AbilityId { get; set; }
    public int HiddenAbilityId { get; set; }
  }
}
