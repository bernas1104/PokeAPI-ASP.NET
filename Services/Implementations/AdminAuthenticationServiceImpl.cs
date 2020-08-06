using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

using Domain;
using Services.DTOs;
using Services.Exceptions;
using Services.Interfaces;
using Persistence.Repositories.Interfaces;
using System;

namespace Services.Implementations {
  public class AdminAuthenticationServiceImpl : AdminAuthenticationService {
    private readonly IConfiguration configuration;
    private readonly AdminsRepository adminsRepository;
    private readonly IPasswordHasher<Admin> passwordHasher;

    public AdminAuthenticationServiceImpl(
      IConfiguration configuration,
      AdminsRepository adminsRepository,
      IPasswordHasher<Admin> passwordHasher

    ) {
      this.configuration = configuration;
      this.passwordHasher = passwordHasher;
      this.adminsRepository = adminsRepository;
    }

    public async Task<AuthenticatedAdminDTO> AuthenticateAdmin(
      AuthenticationAdminDTO auth
    ) {
      var admin = await adminsRepository.FindByEmail(auth.Email);

      Console.WriteLine(admin == null);

      if (admin == null) {
        throw new AdminException(
          "Invalid e-mail/password combination",
          StatusCodes.Status401Unauthorized
        );
      }

      var verifyPassword = passwordHasher.VerifyHashedPassword(
        admin,
        admin.Password,
        auth.Password
      );

      if (verifyPassword != PasswordVerificationResult.Success) {
        throw new AdminException(
          "Invalid e-mail/password combination",
          StatusCodes.Status401Unauthorized
        );
      }

      var token = TokenService.GenerateToken(configuration, admin);

      return new AuthenticatedAdminDTO() {
        Id = admin.Id,
        Email = admin.Email,
        Token = token.Token,
        ValidFrom = token.ValidFrom,
        ExpiresIn = token.ValidTo,
      };
    }
  }
}
