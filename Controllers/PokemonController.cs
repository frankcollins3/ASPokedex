using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pokemon.Models;

namespace Pokemon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public PokemonController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET api/pokemon/1
        [HttpGet("{id}", Name = "GetPokemon")]
        public IActionResult GetPokemon(int id)
        {
            var pokemon = _dbContext.Pokemon.FirstOrDefault();

            if (pokemon == null)
            {
                return NotFound();
            }

            return Ok(pokemon);
        }
    }
}
