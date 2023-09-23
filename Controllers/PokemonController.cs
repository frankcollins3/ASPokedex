using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging; // Add this using directive for logging
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspokedex.PokemonModel;
using aspokedex.Data;

[ApiController]
[Route("/api/[controller]")]
public class PokemonController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<PokemonController> _logger; // Add a logger field

    public PokemonController(ApplicationDbContext context, ILogger<PokemonController> logger) // Inject the logger
    {
        _context = context;
        _logger = logger; // Initialize the logger
    }

    // GET: api/pokemon
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Pokemon>>> GetPokemons()
    {
        try
        {
            return await _context.Pokemons.ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching Pokemons.");
            return StatusCode(500, "An error occurred while fetching Pokemons.");
        }
    }

    // GET: api/pokemon/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Pokemon>> GetPokemon(int id)
    {
        try
        {
            var pokemon = await _context.Pokemons.FindAsync(id);

            if (pokemon == null)
            {
                return NotFound();
            }

            return pokemon;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching Pokemon by ID.");
            return StatusCode(500, "An error occurred while fetching Pokemon by ID.");
        }
    }

        // POST: api/pokemon
    [HttpPost]
    public async Task<ActionResult<Pokemon>> AddPokemon([FromBody] Pokemon pokemon)
    {
        try
        {
            // Add the new Pokemon to the context
            _context.Pokemons.Add(pokemon);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPokemon), new { id = pokemon.Id }, pokemon);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while adding a Pokemon.");
            return StatusCode(500, "A wild 500 error appears!");
        }
    }

    // GET: api/pokemon/test
    [HttpGet("test")]
    public IActionResult GetTest()
    {
        return Ok("test");
    }
}
