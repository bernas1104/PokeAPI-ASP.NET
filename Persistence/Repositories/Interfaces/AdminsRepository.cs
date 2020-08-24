using System.Threading.Tasks;

using Domain;

namespace Persistence.Repositories.Interfaces {
  public interface AdminsRepository {
    public Task<Admin> FindByEmail(string email);
  }
}
