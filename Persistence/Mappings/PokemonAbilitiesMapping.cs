using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain;
using System;

namespace Persistence.Mappings {
  public class PokemonAbilitiesMapping
    : IEntityTypeConfiguration<PokemonAbilities> {
    public void Configure(EntityTypeBuilder<PokemonAbilities> builder) {
      builder.HasKey(pkmnabilities => new {
        pkmnabilities.PokemonId,
        pkmnabilities.AbilityId
      });

      builder
        .HasOne(pkmnabilites => pkmnabilites.Pokemon)
        .WithMany(pkmn => pkmn.PokemonAbilities)
        .HasForeignKey(pkmnabilities => pkmnabilities.PokemonId);

      builder
        .HasOne(pkmnabilites => pkmnabilites.Ability)
        .WithMany(abilities => abilities.PokemonAbilities)
        .HasForeignKey(pkmnabilites => pkmnabilites.AbilityId);
    }
  }
}
