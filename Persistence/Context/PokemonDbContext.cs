using System;

using Microsoft.EntityFrameworkCore;

using Domain;
using Persistence.Mappings;

namespace Persistence.Context {
  public class PokemonDbContext : DbContext {
    public DbSet<Pokemon> Pokemons { get; set; }
    public DbSet<Ability> Abilities { get; set; }
    public DbSet<Stats> Stats { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<PokemonAbility> PokemonAbilities { get; set; }

    public PokemonDbContext(DbContextOptions<PokemonDbContext> options)
      : base(options) {}

    protected override void OnModelCreating(ModelBuilder builder) {
      builder.ApplyConfiguration(new PokemonMapping());
      builder.ApplyConfiguration(new AbilityMapping());
      builder.ApplyConfiguration(new StatsMapping());
      builder.ApplyConfiguration(new AdminMapping());
      builder.ApplyConfiguration(new PokemonAbilityMapping());


      builder.Entity<Admin>().HasData(new Admin() {
        Id = 1,
        Email = "bernardoc1104@gmail.com",
        Password = "AQAAAAEAACcQAAAAEBwF+KTzyPtq0ReO+PjXxZKOg4WzIB1OD7RC+s8DYkVYeOgumfgoq3K6a6LQ3Th4AQ==",
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now,
      });
    }
  }
}
