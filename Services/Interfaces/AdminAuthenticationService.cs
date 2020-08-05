using System.Threading.Tasks;

using Services.DTOs;

namespace Services.Interfaces {
  public interface AdminAuthenticationService {
    public Task<AuthenticatedAdminDTO> AuthenticateAdmin(
      AuthenticationAdminDTO auth
    );
  }
}
