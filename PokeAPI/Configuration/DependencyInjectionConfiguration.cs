using Microsoft.Extensions.DependencyInjection;

using Persistence.Context;
using Services.Interfaces;
using Services.Implementations;
using Persistence.Repositories.Interfaces;
using Persistence.Repositories.Implementations;

namespace PokeAPI.Configuration {
  public static class DependencyInjectionConfiguration {
    public static IServiceCollection ResolveDependencies(
      this IServiceCollection services
    ) {
      services.AddScoped<PokemonDbContext>();

      services.AddScoped<AdminsRepository, AdminsRepositoryImpl>();

      services.AddTransient<
        AdminAuthenticationService,
        AdminAuthenticationServiceImpl
      >();

      return services;
    }
  }
}
