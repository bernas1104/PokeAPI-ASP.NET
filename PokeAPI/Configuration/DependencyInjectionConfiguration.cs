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
      services.AddScoped<AbilitiesRepository, AbilitiesRepositoryImpl>();
      services.AddScoped<PokemonRepository, PokemonRepositoryImpl>();
      services.AddScoped<StatsRepository, StatsRepositoryImpl>();

      services.AddTransient<
        AdminAuthenticationService,
        AdminAuthenticationServiceImpl
      >();
      services.AddTransient<
        PokemonServices,
        PokemonServicesImpl
      >();
      services.AddTransient<
        AbilityServices,
        AbilityServicesImpl
      >();

      return services;
    }
  }
}
