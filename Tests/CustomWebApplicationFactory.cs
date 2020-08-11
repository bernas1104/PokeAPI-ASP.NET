using System;
using System.Text;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;

using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

using Domain;
using Persistence.Context;
using Services.ViewModels;
using System.Collections.Generic;

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

            var abilities = new List<Ability>() {
              new Ability() {
                Name = "Lorem Ipsum",
                Effect = "Lorem Ipsum"
              },
              new Ability() {
                Name = "Lorem Ipsum",
                Effect = "Lorem Ipsum"
              }
            };

            db.Abilities.AddRange(abilities);

            db.SaveChanges();
          } catch (Exception ex) {
            logger.LogError(ex, "An error has occurred seeding the " +
              "database with data. Error: {Message}", ex.Message
            );
          }
        }
      });
    }

    public async Task AuthenticateAsync(HttpClient client) {
      var token = await GetJwtAsync(client);

      client.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue("bearer", token);
    }

    private async Task<string> GetJwtAsync(HttpClient client) {
      var response = await client.PostAsync("/v1/sessions",
        new StringContent(
          JsonConvert.SerializeObject(
            new LoginViewModel() {
              Email = "johndoe@example.com",
              Password = "123456"
            }
          ), Encoding.UTF8
        ) {
          Headers = {
            ContentType = new MediaTypeHeaderValue("application/json")
          }
        }
      );

      var session = JsonConvert.DeserializeObject<SessionViewModel>(
        await response.Content.ReadAsStringAsync()
      );

      return session.Token;
    }
  }
}
