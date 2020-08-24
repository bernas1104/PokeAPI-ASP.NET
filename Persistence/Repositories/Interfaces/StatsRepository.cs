using System.Threading.Tasks;

using Domain;

namespace Persistence.Repositories.Interfaces {
  public interface StatsRepository {
    public Task<Stats> CreateStats(Stats stats);
  }
}
