using System;
using System.Text;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;

using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

using Tests.Utilities;
using Persistence.Context;
using Services.ViewModels;

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
            DatabaseSeeder.InitializeDatabase(db);
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
