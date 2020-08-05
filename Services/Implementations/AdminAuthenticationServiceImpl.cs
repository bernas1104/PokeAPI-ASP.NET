using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

using Domain;
using Services.DTOs;
using Services.Interfaces;
using Persistence.Repositories.Interfaces;
using Services.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Services.Implementations {
  public class AdminAuthenticationServiceImpl : AdminAuthenticationService {
    private readonly AdminsRepository adminsRepository;
    private readonly IPasswordHasher<Admin> passwordHasher;

    public AdminAuthenticationServiceImpl(
      AdminsRepository adminsRepository,
      IPasswordHasher<Admin> passwordHasher
    ) {
      this.passwordHasher = passwordHasher;
      this.adminsRepository = adminsRepository;
    }

    public async Task<AuthenticatedAdminDTO> AuthenticateAdmin(
      AuthenticationAdminDTO auth
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

      // GENERATE TOKEN!!

      return new AuthenticatedAdminDTO() {
        Id = admin.Id,
        Email = admin.Email,
      };
    }
  }
}
