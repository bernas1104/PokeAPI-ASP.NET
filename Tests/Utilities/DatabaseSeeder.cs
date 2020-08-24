using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Identity;

using Domain;
using Persistence.Context;

namespace Tests.Utilities {
  public static class DatabaseSeeder {
    public static void InitializeDatabase(PokemonDbContext db) {
      var admin = InitializeAdmin();

      var abilities = InitializeAbilities();

      var pokemon = InitializePokemon();

      db.Admins.Add(admin);
      db.Abilities.AddRange(abilities);
      db.Pokemons.AddRange(pokemon);

      db.SaveChanges();
    }

    private static Admin InitializeAdmin() {
      var admin = new Admin() {
        Email = "johndoe@example.com",
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now,
      };

      IPasswordHasher<Admin> passwordHasher = new PasswordHasher<Admin>();
      admin.Password = passwordHasher.HashPassword(admin, "123456");

      return admin;
    }

    private static IList<Ability> InitializeAbilities() {
      return new List<Ability>() {
        new Ability() {
          Id = 1,
          Name = "Lorem Ipsum 1",
          Effect = "Lorem Ipsum"
        },
        new Ability() {
          Id = 2,
          Name = "Lorem Ipsum",
          Effect = "Lorem Ipsum 2"
        }
      };
    }

    private static IList<Pokemon> InitializePokemon() {
      return new List<Pokemon>() {
        new Pokemon() {
          Id = 1,
          Name = "Bulbasaur",
          EvolutionLevel = 16,
          LevelingRate = 2,
          CatchRate = 11.90F,
          HatchTime = 3170,
          Seen = false,
          Captured = false,
        },
        new Pokemon() {
          Id = 2,
          Name = "Ivysaur",
          EvolutionLevel = 32,
          LevelingRate = 2,
          CatchRate = 11.90F,
          HatchTime = 3170,
          Seen = true,
          Captured = false,
        },
        new Pokemon() {
          Id = 3,
          Name = "Venosaur",
          EvolutionLevel = null,
          PreEvolutionId = 2,
          LevelingRate = 2,
          CatchRate = 11.90F,
          HatchTime = 3170,
          Seen = true,
          Captured = true,
        },
      };
    }
  }
}
