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

      db.Admins.Add(admin);
      db.Abilities.AddRange(abilities);

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
          Name = "Lorem Ipsum",
          Effect = "Lorem Ipsum"
        },
        new Ability() {
          Name = "Lorem Ipsum",
          Effect = "Lorem Ipsum"
        }
      };
    }
  }
}
