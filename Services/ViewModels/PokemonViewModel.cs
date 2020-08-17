using System;
using System.Collections.Generic;

using Newtonsoft.Json;
using Flunt.Validations;
using Flunt.Notifications;
using Microsoft.AspNetCore.Http;

namespace Services.ViewModels {
  public class PokemonViewModel : Notifiable, IValidatable {
    public int Id { get; set; }
    public string Name { get; set; }
    public int? EvolutionLevel { get; set; }
    public int LevelingRate { get; set; }
    public float CatchRate { get; set; }
    public int HatchTime { get; set; }
    public string PhotoUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public IList<AbilityViewModel> Abilities { get; set; }
    public StatsViewModel Stats { get; set; }

    [JsonIgnore]
    public IFormFile PokemonPhoto { get; set; }

    public void Validate() {
      var pokemonContract = new Contract()
        .IsNotNull(Id, "Id", "Pokemon number is required")
        .IsGreaterThan(Id, 0, "Id", "Pokemon number must be greater than 0")
        .IsNotNullOrEmpty(Name, "Name", "Pokemon name is required")
        .HasMaxLen(Name, 20, "Name", "Pokemon name must have at most 20 characters")
        .IsNotNull(LevelingRate, "Leveling Rate", "Pokemon leveling rate is required")
        .IsBetween(LevelingRate, 0, 5, "Leveling Rate", "Pokemon leveling rate must be between 0 and 5")
        .IsNotNull(CatchRate, "Catch Rate", "Pokemon catch rate is require")
        .IsBetween(CatchRate, 0, 100, "Catch Rate", "Pokemon catching rate must be betwwen 0 and 100")
        .IsNotNull(HatchTime, "Hatch Time", "Pokemon hatch time is required")
        .IsBetween(HatchTime, 1000, 31000, "Hatch Time", "Pokemon hatch time must be between 1000 and 31000");

      var abilitiesContract = new Contract()
        .IsNotNull(Abilities, "Abilities", "Pokemon must have exactly 2 abilities");
      abilitiesContract.IfNotNull(Abilities, abilitiesContract => (
        abilitiesContract
          .IsBetween(Abilities[0].Id, 1, 260, "Id", "Pokemon ability must be between 1 and 260")
          .IsBetween(Abilities[1].Id, 1, 260, "Id", "Pokemon hidden ability must be betwwen 1 and 260")
      ));

      var statsContract = new Contract()
        .IsNotNull(Stats, "stats", "Pokemon must have stats points");
      statsContract.IfNotNull(Stats, statsContract => (
        statsContract
          .IsBetween(Stats.HitPoints, 1, 200, "HitPoints", "Pokemon hit points stats must be between 1 and 200")
          .IsBetween(Stats.Attack, 1, 200, "Attack", "Pokemon attack stats must be between 1 and 200")
          .IsBetween(Stats.Defense, 1, 200, "Defense", "Pokemon defense stats must be between 1 and 200")
          .IsBetween(Stats.SpecialAttack, 1, 200, "Special Attack", "Pokemon special attack stats must be between 1 and 200")
          .IsBetween(Stats.SpecialDefense, 1, 200, "Special Defense", "Pokemon special defense stats must be between 1 and 200")
          .IsBetween(Stats.Speed, 1, 200, "Speed", "Pokemon speed stats must be between 1 and 200")
          .IsBetween(Stats.Total, 6, 1200, "Total", "Pokemon total stats must be between 6 and 1200")
      ));

      AddNotifications(pokemonContract);
      AddNotifications(abilitiesContract);
      AddNotifications(statsContract);
    }
  }
}
