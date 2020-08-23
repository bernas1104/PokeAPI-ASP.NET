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
        .HasMany(pkmn => pkmn.Evolution)
        .WithOne(pkmn => pkmn.PreEvolution)
        .OnDelete(DeleteBehavior.SetNull);

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
        .Property(pkmn => pkmn.Seen)
        .IsRequired()
        .HasDefaultValue(false);

      builder
        .Property(pkmn => pkmn.Captured)
        .IsRequired()
        .HasDefaultValue(false);

      builder
        .Property(pkmn => pkmn.Photo)
        .IsRequired()
        .HasMaxLength(255)
        .HasDefaultValue("default.png");

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
