using AutoMapper;

using Services.DTOs;
using PokeAPI.ViewModels;

namespace PokeAPI.Configuration {
  public class AutoMapperConfiguration : Profile {
    public AutoMapperConfiguration() {
      CreateMap<AuthenticatedAdminDTO, SessionViewModel>();
    }
  }
}
