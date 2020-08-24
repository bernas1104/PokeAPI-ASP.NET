using System.Threading.Tasks;

using Services.ViewModels;

namespace Services.Interfaces {
  public interface AdminAuthenticationService {
    public Task<SessionViewModel> AuthenticateAdmin(
      LoginViewModel auth
    );
  }
}
