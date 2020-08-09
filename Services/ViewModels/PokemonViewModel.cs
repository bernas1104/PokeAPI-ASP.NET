using System;
using System.Collections.Generic;

using Domain;

namespace Services.ViewModels {
  public class PokemonViewModel {
    public int Id { get; set; }
    public string Name { get; set; }
    public int? EvolutionLevel { get; set; }
    public int LevelingRate { get; set; }
    public float CatchRate { get; set; }
    public int HatchTime { get; set; }
    public IList<AbilityViewModel> Abilities { get; set; }
    public StatsViewModel Stats { get; set; }
    public DateTime CreatedAt { get; set; }
  }
}
