using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain;
using System;

namespace Persistence.Mappings {
  public class StatsMapping : IEntityTypeConfiguration<Stats> {
    public void Configure(EntityTypeBuilder<Stats> builder) {
      builder.HasKey(stats => stats.Id);

      builder.Property(stats => stats.HitPoints).IsRequired();

      builder.Property(stats => stats.Attack).IsRequired();

      builder.Property(stats => stats.Defense).IsRequired();

      builder.Property(stats => stats.SpecialAttack).IsRequired();

      builder.Property(stats => stats.SpecialDefense).IsRequired();

      builder.Property(stats => stats.Speed).IsRequired();

      builder.Property(stats => stats.Total).IsRequired();

      builder
        .Property(stats => stats.CreatedAt)
        .IsRequired()
        .HasDefaultValue(DateTime.Now);

      builder
        .Property(stats => stats.UpdatedAt)
        .IsRequired()
        .HasDefaultValue(DateTime.Now);

      builder
        .Property(stats => stats.DeletedAt)
        .IsRequired(false);
    }
  }
}
