using System;
using System.Threading.Tasks;

using Moq;
using Xunit;
using AutoMapper;

using Tests.Bogus.Domain;
using Services.ViewModels;
using Services.Interfaces;
using Services.Exceptions;
using Services.Implementations;
using Services.Providers.StorageProvider;
using Persistence.Repositories.Interfaces;

namespace Tests.UnitTests.Services {
  public class AddPreEvolutionServiceTest {
    private readonly Mock<PokemonRepository> pokemonRepository;
    private readonly Mock<StatsRepository> statsRepository;
    private readonly Mock<AbilitiesRepository> abilitiesRepository;
    private readonly Mock<StorageProvider> storageProvider;
    private readonly Mock<IMapper> mapper;
    private readonly PokemonServices pokemonServices;

    public AddPreEvolutionServiceTest() {
      pokemonRepository = new Mock<PokemonRepository>();
      statsRepository = new Mock<StatsRepository>();
      abilitiesRepository = new Mock<AbilitiesRepository>();
      storageProvider = new Mock<StorageProvider>();
      mapper = new Mock<IMapper>();

      pokemonServices = new PokemonServicesImpl(
        pokemonRepository.Object,
        statsRepository.Object,
        abilitiesRepository.Object,
        storageProvider.Object,
        mapper.Object
      );
    }

    //
  }
}
