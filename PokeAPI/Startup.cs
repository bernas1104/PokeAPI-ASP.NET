using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistence.Context;

using PokeAPI.Configuration;

namespace PokeAPI {
    public class Startup {
      public Startup(IConfiguration configuration) {
        Configuration = configuration;
      }

      public IConfiguration Configuration { get; }

      // This method gets called by the runtime. Use this method to add services to the container.
      public void ConfigureServices(IServiceCollection services) {
        services.AddDbContext<PokemonDbContext>(options => {
          options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddCors();

        services.AddControllers();
        services.AddScoped<PokemonDbContext>();

        services.AddAuthenticationConfiguration(Configuration);
        services.AddExceptionsMiddleware();
        services.ResolveDependencies();
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseCors(x => x
          .AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader()
        );

        app.UseRouting();

        app.UseAuthorization();
        app.UseAuthentication();

        app.UseExceptionsMiddlewareHandler();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
      }
    }
}
