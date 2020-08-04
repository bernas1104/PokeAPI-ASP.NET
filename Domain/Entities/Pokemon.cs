using System;
using System.Collections.Generic;

namespace Domain {
  public class Pokemon {
    public int Id { get; set; }
    public string Name { get; set; }
    public int? PreEvolutionId { get; set; }
    public int? EvolutionId { get; set; }
    public int? EvolutionLevel { get; set; }
    public int LevelingRate { get; set; }
    public int CatchRate { get; set; }
    public int HatchTime { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public Pokemon PreEvolution { get; set; }
    public Pokemon Evolution { get; set; }
    public IEnumerable<PokemonAbilities> PokemonAbilities { get; set; }
    public Stats Stats { get; set; }
  }
}
