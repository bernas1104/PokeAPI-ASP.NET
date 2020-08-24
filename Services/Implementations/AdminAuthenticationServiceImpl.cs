using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

using Domain;
using Services.ViewModels;
using Services.Exceptions;
using Services.Interfaces;
using Persistence.Repositories.Interfaces;

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

    public async Task<SessionViewModel> AuthenticateAdmin(
      LoginViewModel auth
    ) {
      var admin = await adminsRepository.FindByEmail(auth.Email);

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

      return new SessionViewModel() {
        Id = admin.Id,
        Email = admin.Email,
        Token = token.Token,
        ValidFrom = token.ValidFrom,
        ValidTo = token.ValidTo,
      };
    }
  }
}
