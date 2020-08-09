using Flunt.Validations;
using Flunt.Notifications;

namespace Services.ViewModels {
  public class CreatePokemonViewModel : Notifiable, IValidatable {
    public int Id { get; set; }
    public string Name { get; set; }
    public int EvolutionLevel { get; set; }
    public int LevelingRate { get; set; }
    public float CatchRate { get; set; }
    public int HatchTime { get; set; }
    public int AbilityId { get; set; }
    public int HiddenAbilityId { get; set; }
    public int HitPoints { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int SpecialAttack { get; set; }
    public int SpecialDefense { get; set; }
    public int Speed { get; set; }
    public int Total { get; set; }

    public void Validate() {
      AddNotifications(
        new Contract()
          .IsGreaterThan(Id, 1, "Id", "Pokemon number must be greater than 0")
          .IsNotNullOrEmpty(Name, "Name", "Pokemon name is required")
          .HasMaxLen(Name, 20, "Name", "Pokemon name must have at most 20 characters")
          .IsBetween(LevelingRate, 0, 5, "LevelingRate", "Pokemon leveling rate must be between 0 and 5")
          .IsBetween(CatchRate, 0, 100, "CatchRate", "Pokemon catching rate must be betwwen 0 and 100")
          .IsBetween(HatchTime, 1000, 31000, "HatchTime", "Pokemon hatch time must be between 1000 and 31000")
          .IsBetween(AbilityId, 1, 260, "AbilityId", "Pokemon ability must be between 1 and 260")
          .IsBetween(HiddenAbilityId, 1, 260, "HiddenAbility", "Pokemon hidden ability must be betwwen 1 and 260")
          .IsBetween(HitPoints, 1, 200, "HitPoints", "Pokemon hit points stats must be between 1 and 200")
          .IsBetween(Attack, 1, 200, "Attack", "Pokemon hit points stats must be between 1 and 200")
          .IsBetween(Defense, 1, 200, "Defense", "Pokemon hit points stats must be between 1 and 200")
          .IsBetween(SpecialAttack, 1, 200, "SpecialAttack", "Pokemon hit points stats must be between 1 and 200")
          .IsBetween(SpecialDefense, 1, 200, "SpecialDefense", "Pokemon hit points stats must be between 1 and 200")
          .IsBetween(Speed, 1, 200, "Speed", "Pokemon hit points stats must be between 1 and 200")
          .IsBetween(Total, 6, 1200, "Total", "Pokemon hit points stats must be between 1 and 200")
      );
    }
  }
}
