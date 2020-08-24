using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Domain;
using Persistence.Context;
using Persistence.Repositories.Interfaces;

namespace Persistence.Repositories.Implementations {
  public class AdminsRepositoryImpl : AdminsRepository {
    private readonly PokemonDbContext dbContext;

    public AdminsRepositoryImpl(PokemonDbContext dbContext) {
      this.dbContext = dbContext;
    }

    public async Task<Admin> FindByEmail(string email) {
      var admin = await dbContext.Admins
        .Where(admin => admin.Email == email)
        .FirstOrDefaultAsync();

      return admin;
    }
  }
}
