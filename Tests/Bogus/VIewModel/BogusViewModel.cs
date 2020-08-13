using System.Collections.Generic;

using Bogus;
using AutoBogus;
using Bogus.Extensions;

using Domain;
using Services.ViewModels;

namespace Tests.Bogus.ViewModel {
  public static class BogusViewModel {
    public static PokemonViewModel PokemonViewModelFaker() {
      var pokemonViewModel = new Faker<PokemonViewModel>()
        .RuleFor(x => x.Id, (f) => f.Random.Int(1, 150))
        .RuleFor(x => x.Name, (f) => f.Name.FirstName())
        .RuleFor(
          x => x.EvolutionLevel,
          (f) => f.Random.Int(5, 50).OrNull(f, 0.5F)
        )
        .RuleFor(x => x.LevelingRate, (f) => f.Random.Int(0, 5))
        .RuleFor(x => x.CatchRate, (f) => f.Random.Float(0.0F, 100.0F))
        .RuleFor(x => x.HatchTime, (f) => f.Random.Int(1280, 30720))
        .RuleFor(x => x.Stats, (_, u) => {
          var statsViewModel = new Faker<StatsViewModel>()
            .RuleFor(x => x.HitPoints, (f) => f.Random.Int(1, 200))
            .RuleFor(x => x.Attack, (f) => f.Random.Int(1, 200))
            .RuleFor(x => x.Defense, (f) => f.Random.Int(1, 200))
            .RuleFor(x => x.SpecialAttack, (f) => f.Random.Int(1, 200))
            .RuleFor(x => x.SpecialDefense, (f) => f.Random.Int(1, 200))
            .RuleFor(x => x.Speed, (f) => f.Random.Int(1, 200))
            .RuleFor(x => x.Total, (_, u) =>
              u.HitPoints
              + u.Attack
              + u.Defense
              + u.SpecialAttack
              + u.SpecialDefense
              + u.Speed
            );

          return statsViewModel.Generate();
        })
        .RuleFor(x => x.Abilities, (_, u) => {
          return new List<AbilityViewModel>() {
            new AbilityViewModel() { Id = 1 },
            new AbilityViewModel() { Id = 2 }
          };
        });

      return pokemonViewModel.Generate();
    }

    public static PokemonViewModel PokemonViewModelFaker(
      Pokemon pokemon,
      Stats stats,
      IList<Ability> abilities
    ) => new PokemonViewModel() {
      Id = pokemon.Id,
      Name = pokemon.Name,
      EvolutionLevel = pokemon.EvolutionLevel,
      LevelingRate = pokemon.LevelingRate,
      CatchRate = pokemon.CatchRate,
      HatchTime = pokemon.HatchTime,
      CreatedAt = pokemon.CreatedAt,
      Stats = new StatsViewModel() {
        HitPoints = stats.HitPoints,
        Attack = stats.Attack,
        Defense = stats.Defense,
        SpecialAttack = stats.SpecialAttack,
        SpecialDefense = stats.SpecialDefense,
        Speed = stats.Speed,
        Total = stats.Total
      },
      Abilities = new List<AbilityViewModel>() {
        new AbilityViewModel() { Id = abilities[0].Id },
        new AbilityViewModel() { Id = abilities[1].Id }
      }
    };

    public static StatsViewModel StatsViewModelFaker() {
      var statsViewModel = new Faker<StatsViewModel>()
        .RuleFor(x => x.HitPoints, (f) => f.Random.Int(1, 200))
        .RuleFor(x => x.Attack, (f) => f.Random.Int(1, 200))
        .RuleFor(x => x.Defense, (f) => f.Random.Int(1, 200))
        .RuleFor(x => x.SpecialAttack, (f) => f.Random.Int(1, 200))
        .RuleFor(x => x.SpecialDefense, (f) => f.Random.Int(1, 200))
        .RuleFor(x => x.Speed, (f) => f.Random.Int(1, 200))
        .RuleFor(x => x.Total, (_, u) =>
          u.HitPoints
          + u.Attack
          + u.Defense
          + u.SpecialAttack
          + u.SpecialDefense
          + u.Speed
        );

      return statsViewModel.Generate();
    }

    public static IList<AbilityViewModel> AbilityViewModelFaker() {
      return new List<AbilityViewModel>() {
        new AbilityViewModel() {
          Id = 1,
          Name = "Lorem Ipsum",
          Effect = "Lorem Ipsum",
        },
        new AbilityViewModel() {
          Id = 2,
          Name = "Lorem Ipsum",
          Effect = "Lorem Ipsum",
        }
      };
    }
  }
}
