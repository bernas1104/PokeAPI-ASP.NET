using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain;
using System;

namespace Persistence.Mappings {
  public class PokemonMapping : IEntityTypeConfiguration<Pokemon> {
    public void Configure(EntityTypeBuilder<Pokemon> builder) {
      builder.HasKey(pkmn => pkmn.Id);

      builder
        .Property(pkmn => pkmn.Name)
        .IsRequired()
        .HasMaxLength(50);

      builder
        .Property(pkmn => pkmn.PreEvolutionId)
        .IsRequired(false);
      builder
        .HasOne(pkmn => pkmn.PreEvolution)
        .WithOne(pkmn => pkmn.Evolution)
        .HasForeignKey<Pokemon>(pkmn => pkmn.PreEvolutionId)
        .OnDelete(DeleteBehavior.Cascade);

      builder
        .Property(pkmn => pkmn.EvolutionId)
        .IsRequired(false);
      builder
        .HasOne(pkmn => pkmn.Evolution)
        .WithOne(pkmn => pkmn.PreEvolution)
        .HasForeignKey<Pokemon>(pkmn => pkmn.EvolutionId)
        .OnDelete(DeleteBehavior.Cascade);

      builder
        .Property(pkmn => pkmn.EvolutionLevel)
        .IsRequired(false);

      builder
        .Property(pkmn => pkmn.LevelingRate)
        .IsRequired();

      builder
        .Property(pkmn => pkmn.CatchRate)
        .IsRequired();

      builder
        .Property(pkmn => pkmn.HatchTime)
        .IsRequired();

      builder
        .HasOne(pkmn => pkmn.Stats)
        .WithOne(stats => stats.Pokemon)
        .HasForeignKey<Stats>(stats => stats.PokemonId)
        .OnDelete(DeleteBehavior.Cascade);

      builder
        .Property(pkmn => pkmn.CreatedAt)
        .IsRequired()
        .HasDefaultValue(DateTime.Now);

      builder
        .Property(pkmn => pkmn.UpdatedAt)
        .IsRequired()
        .HasDefaultValue(DateTime.Now);

      builder
        .Property(pkmn => pkmn.DeletedAt)
        .IsRequired(false);
    }
  }
}
