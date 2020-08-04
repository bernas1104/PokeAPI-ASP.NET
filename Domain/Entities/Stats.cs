using System;

namespace Domain {
  public class Stats {
    public int Id { get; set; }
    public int HitPoints { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int SpecialAttack { get; set; }
    public int SpecialDefense { get; set; }
    public int Speed { get; set; }
    public int Total { get; set; }
    public int PokemonId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public Pokemon Pokemon { get; set; }
  }
}
