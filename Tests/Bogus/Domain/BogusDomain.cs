using System.Collections.Generic;

using Bogus;
using Bogus.Extensions;

using Domain;
using System;

namespace Tests.Bogus.Domain {
  public static class BogusDomain {
    public static Pokemon PokemonFaker() {
      var pokemon = new Faker<Pokemon>()
        .RuleFor(x => x.Id, (f) => f.Random.Int(1, 150))
        .RuleFor(x => x.Name, (f) => f.Name.FirstName())
        .RuleFor(x => x.EvolutionLevel, (f) =>
          f.Random.Int(5, 50).OrNull(f, .5F)
        )
        .RuleFor(x => x.LevelingRate, (f) => f.Random.Int(0, 5))
        .RuleFor(x => x.CatchRate, (f) => f.Random.Float(0.0F, 100.0F))
        .RuleFor(x => x.HatchTime, (f) => f.Random.Int(1280, 30720))
        .RuleFor(x => x.Seen, () => false)
        .RuleFor(x => x.Captured, () => false)
        .RuleFor(x => x.CreatedAt, (_) => DateTime.Now)
        .RuleFor(x => x.UpdatedAt, (_) => DateTime.Now)
        .RuleFor(x => x.DeletedAt, (f) => null);

      return pokemon.Generate();
    }

    public static IList<Ability> AbilitiesFaker() {
      var abilities = new Faker<Ability>()
        .RuleFor(x => x.Id, (f) => f.Random.Int(1, 260))
        .RuleFor(x => x.Name, (f) => f.Name.FirstName())
        .RuleFor(x => x.Effect, (f) => f.Lorem.Sentences(5))
        .RuleFor(x => x.CreatedAt, (_) => DateTime.Now)
        .RuleFor(x => x.UpdatedAt, (_) => DateTime.Now)
        .RuleFor(x => x.DeletedAt, (f) => null);

      return abilities.Generate(2);
    }

    public static Stats StatsFaker() {
      var stats = new Faker<Stats>()
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

      return stats.Generate();
    }
  }
}
