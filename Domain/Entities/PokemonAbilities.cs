namespace Domain {
  public class PokemonAbilities {
    public int PokemonId { get; set; }
    public int AbilityId { get; set; }
    public bool Hidden { get; set; }

    public Pokemon Pokemon { get; set; }
    public Ability Ability { get; set; }
  }
}
