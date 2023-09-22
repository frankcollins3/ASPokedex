using Microsoft.EntityFrameworkCore;
using aspokedex.PokemonModel; // Include the namespace where PokemonModel is defined

namespace aspokedex.Data
{

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Pokemon> Pokemons { get; set; } // Use the plural form "Pokemons" for DbSet
}

}
