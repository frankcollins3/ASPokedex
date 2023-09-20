using Microsoft.EntityFrameworkCore;
using Pokemon.Models;


public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<PokemonModel> Pokemon { get; set; }
}
