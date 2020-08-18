using AutoMapper;

using Domain;
using Services.ViewModels;

namespace PokeAPI.Configuration {
  public class AutoMapperConfiguration : Profile {
    public AutoMapperConfiguration() {
      CreateMap<Pokemon, PokemonViewModel>().ForMember(
        x => x.PhotoUrl, y => y.MapFrom(z => z.Photo)
      ).ReverseMap();
      CreateMap<Stats, StatsViewModel>().ReverseMap();
      CreateMap<Ability, AbilityViewModel>().ReverseMap();
    }
  }
}
