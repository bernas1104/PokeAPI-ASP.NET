using System.Threading.Tasks;

using Domain;
using Persistence.Context;
using Persistence.Repositories.Interfaces;

namespace Persistence.Repositories.Implementations {
  public class StatsRepositoryImpl : StatsRepository {
    private readonly PokemonDbContext dbContext;

    public StatsRepositoryImpl(PokemonDbContext dbContext) {
      this.dbContext = dbContext;
    }

    public async Task<Stats> CreateStats(Stats stats) {
      dbContext.Stats.Add(stats);

      await SaveChangesToDatabase();

      return stats;
    }

    private async Task SaveChangesToDatabase() {
      await dbContext.SaveChangesAsync();
    }
  }
}
