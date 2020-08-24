using System.Threading.Tasks;

using Services.ViewModels;

namespace Services.Interfaces {
  public interface AbilityServices {
    public Task<AbilityViewModel> CreateAbility(AbilityViewModel data);
  }
}
