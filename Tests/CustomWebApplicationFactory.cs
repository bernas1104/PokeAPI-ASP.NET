using System;
using System.Linq;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

using Domain;
using Persistence.Context;

namespace Tests {
  public class CustomWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup> where TStartup : class {
    protected override void ConfigureWebHost(IWebHostBuilder builder) {
      builder.ConfigureServices(services => {
        var descriptor = services.SingleOrDefault(
          d => d.ServiceType ==
            typeof(DbContextOptions<PokemonDbContext>)
        );

        services.Remove(descriptor);

        services.AddDbContext<PokemonDbContext>(options => {
          options.UseInMemoryDatabase("InMemoryDbForTesting");
        });

        var sp = services.BuildServiceProvider();

        using (var scope = sp.CreateScope()) {
          var scopedServices = scope.ServiceProvider;
          var db = scopedServices.GetRequiredService<PokemonDbContext>();
          var logger = scopedServices
            .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

          db.Database.EnsureCreated();

          try {
            // Seed!
            // Export to a Utilities class!
            var admin = new Admin() {
              Email = "johndoe@example.com",
              CreatedAt = DateTime.Now,
              UpdatedAt = DateTime.Now,
            };

            IPasswordHasher<Admin> passwordHasher = new PasswordHasher<Admin>();
            admin.Password = passwordHasher.HashPassword(admin, "123456");

            db.Admins.Add(admin);
            db.SaveChanges();
          } catch (Exception ex) {
            logger.LogError(ex, "An error has occurred seeding the " +
              "database with data. Error: {Message}", ex.Message
            );
          }
        }
      });
    }
  }
}
