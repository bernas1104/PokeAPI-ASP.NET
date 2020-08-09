using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain;
using System;

namespace Persistence.Mappings {
  public class PokemonAbilityMapping
    : IEntityTypeConfiguration<PokemonAbility> {
    public void Configure(EntityTypeBuilder<PokemonAbility> builder) {
      builder.HasKey(pkmnability => new {
        pkmnability.PokemonId,
        pkmnability.AbilityId
      });

      builder
        .HasOne(pkmnabilites => pkmnabilites.Pokemon)
        .WithMany(pkmn => pkmn.PokemonAbilities)
        .HasForeignKey(pkmnability => pkmnability.PokemonId);

      builder
        .HasOne(pkmnabilites => pkmnabilites.Ability)
        .WithMany(Ability => Ability.PokemonAbilities)
        .HasForeignKey(pkmnabilites => pkmnabilites.AbilityId);
    }
  }
}
