using Flunt.Validations;
using Flunt.Notifications;

namespace Services.ViewModels {
  public class EvolutionViewModel : Notifiable, IValidatable {
    public int pokemonId { get; set; }
    public int pokemonEvolutionId { get; set; }

    public void Validate() {
      AddNotifications(
        new Contract()
          .IsBetween(pokemonId, 1, 151, "Pokemon ID", "Pokemon number must be between 1 and 151")
          .IsBetween(pokemonEvolutionId, 1, 151, "Pokemon Evolution ID", "Pokemon number must be between 1 and 151")
      );
    }
  }
}
