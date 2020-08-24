using System.Threading.Tasks;

using AutoMapper;

using Domain;
using Services.ViewModels;
using Services.Interfaces;
using Services.Exceptions;
using Persistence.Repositories.Interfaces;

namespace Services.Implementations {
  public class AbilityServicesImpl : AbilityServices {
    private readonly AbilitiesRepository abilitiesRepository;
    private readonly IMapper mapper;

    public AbilityServicesImpl(
      AbilitiesRepository abilitiesRepository,
      IMapper mapper
    ) {
      this.abilitiesRepository = abilitiesRepository;
      this.mapper = mapper;
    }

    public async Task<AbilityViewModel> CreateAbility(AbilityViewModel data) {
      var ability = mapper.Map<Ability>(data);

      var abilityExists = await abilitiesRepository.ExistsById(data.Id);
      if (abilityExists)
        throw new AbilityException("Ability number must be unique", 400);

      abilityExists = await abilitiesRepository.ExistsByName(data.Name);
      if (abilityExists)
        throw new AbilityException("Ability name must be unique", 400);

      ability = await abilitiesRepository.CreateAbility(ability);

      return mapper.Map<AbilityViewModel>(ability);
    }
  }
}
