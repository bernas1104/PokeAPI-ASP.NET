using AutoMapper;

using Domain;
using Services.ViewModels;

namespace PokeAPI.Configuration {
  public class AutoMapperConfiguration : Profile {
    public AutoMapperConfiguration() {
      CreateMap<Pokemon, PokemonViewModel>().ReverseMap();
      CreateMap<Stats, StatsViewModel>().ReverseMap();
      CreateMap<Ability, AbilityViewModel>().ReverseMap();
    }
  }
}
