using Microsoft.Extensions.DependencyInjection;

using Persistence.Context;
using Services.Interfaces;
using Services.Implementations;
using Services.Providers.StorageProvider;
using Persistence.Repositories.Interfaces;
using Persistence.Repositories.Implementations;
using Services.Providers.StorageProvider.Implementations;

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
      services.AddScoped<StorageProvider, DiskStorageProvider>();

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
