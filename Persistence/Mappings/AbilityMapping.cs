using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain;
using System;

namespace Persistence.Mappings {
  public class AbilityMapping : IEntityTypeConfiguration<Ability> {
    public void Configure(EntityTypeBuilder<Ability> builder) {
      builder.HasKey(ability => ability.Id);

      builder
        .Property(ability => ability.Effect)
        .IsRequired()
        .HasMaxLength(255);

      builder
        .Property(ability => ability.Name)
        .IsRequired()
        .HasMaxLength(50);

      builder
        .Property(ability => ability.CreatedAt)
        .IsRequired()
        .HasDefaultValue(DateTime.Now);

      builder
        .Property(ability => ability.UpdatedAt)
        .IsRequired()
        .HasDefaultValue(DateTime.Now);

      builder
        .Property(ability => ability.DeletedAt)
        .IsRequired(false);
    }
  }
}
