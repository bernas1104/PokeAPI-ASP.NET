using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using PokeAPI.Middlewares;

namespace PokeAPI.Configuration {
  public static class ExceptionsMiddlewareConfiguration {
    public static IServiceCollection AddExceptionsMiddleware(
      this IServiceCollection services
    ) {
      return services.AddTransient<ExceptionsHandlerMiddleware>();
    }

    public static void UseExceptionsMiddlewareHandler(
      this IApplicationBuilder app
    ) {
      app.UseMiddleware<ExceptionsHandlerMiddleware>();
    }
  }
}
