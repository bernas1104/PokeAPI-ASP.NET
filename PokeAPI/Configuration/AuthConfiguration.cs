using System.Text;

using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace PokeAPI.Configuration {
  public static class AuthConfiguration {
    public static IServiceCollection AddAuthenticationConfiguration(
      this IServiceCollection services,
      IConfiguration configuration
    ) {
      services.AddScoped<IPasswordHasher<Admin>, PasswordHasher<Admin>>();

      var key = Encoding.ASCII.GetBytes(configuration["JWTSecret"]);
      services.AddAuthentication(auth => {
        auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer(jwt => {
        jwt.RequireHttpsMetadata = false;
        jwt.SaveToken = true;
        jwt.TokenValidationParameters = new TokenValidationParameters {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(key),
          ValidateIssuer = false,
          ValidateAudience = false,
        };
      });

      return services;
    }
  }
}
