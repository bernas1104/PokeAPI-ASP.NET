using System;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

using Domain;
using Services.DTOs;

namespace Services.Implementations {
  public static class TokenService {
    public static TokenDTO GenerateToken(
      IConfiguration configuration,
      Admin user
    ) {
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(configuration["JWTSecret"]);
      var tokenDescriptor = new SecurityTokenDescriptor() {
        Subject = new ClaimsIdentity(new Claim[] {
          new Claim(ClaimTypes.Email, user.Email),
        }),
        Expires = DateTime.UtcNow.AddDays(1).ToLocalTime(),
        IssuedAt = DateTime.Now.ToLocalTime(),
        SigningCredentials = new SigningCredentials(
          new SymmetricSecurityKey(key),
          SecurityAlgorithms.HmacSha256Signature
        )
      };

      var token = tokenHandler.CreateToken(tokenDescriptor);

      var tokenData = new TokenDTO() {
        Token = tokenHandler.WriteToken(token),
        ValidFrom = token.ValidFrom,
        ValidTo = token.ValidTo
      };

      return tokenData;
    }
  }
}
