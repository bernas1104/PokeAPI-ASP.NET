using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain;
using System;

namespace Persistence.Mappings {
  public class AdminMapping : IEntityTypeConfiguration<Admin> {
    public void Configure(EntityTypeBuilder<Admin> builder) {
      builder.HasKey(admin => admin.Id);

      builder.Property(admin => admin.Username).IsRequired().HasMaxLength(100);

      builder.Property(admin => admin.Password).IsRequired().HasMaxLength(255);

      builder
        .Property(admin => admin.CreatedAt)
        .IsRequired()
        .HasDefaultValue(DateTime.Now);

      builder
        .Property(admin => admin.UpdatedAt)
        .IsRequired()
        .HasDefaultValue(DateTime.Now);
    }
  }
}
