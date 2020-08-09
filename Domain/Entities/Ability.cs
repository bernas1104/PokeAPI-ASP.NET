using System;
using System.Collections.Generic;

namespace Domain {
  public class Ability {
    public int Id { get; set; }
    public string Effect { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public IEnumerable<PokemonAbility> PokemonAbilities { get; set; }
  }
}
