using Flunt.Validations;
using Flunt.Notifications;

namespace Services.ViewModels {
  public class AbilityViewModel : Notifiable, IValidatable {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Effect { get; set; }

    public void Validate() {
      var abilityContract = new Contract()
        .IsBetween(Id, 1, 260, "Id", "Ability number must be between 0 and 260")
        .IsNotNullOrEmpty(Name, "Name", "Ability must have a name")
        .IsNotNullOrEmpty(Effect, "Effect", "Ability must have a effect");

      var fieldsNotNullOrEmptyContract = new Contract()
        .HasMaxLengthIfNotNullOrEmpty(Name, 50, "Name", "Ability name must have at most 50 characters")
        .HasMaxLengthIfNotNullOrEmpty(Effect, 255, "Effect", "Effect must have at most 255 characters");

      AddNotifications(abilityContract);
      AddNotifications(fieldsNotNullOrEmptyContract);
    }
  }
}
