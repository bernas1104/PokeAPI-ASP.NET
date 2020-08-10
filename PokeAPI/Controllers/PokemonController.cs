using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

using Services.ViewModels;
using Services.Interfaces;

namespace PokeAPI.Controllers {
  [ApiController]
  [Route("v1/[controller]")]
  [Produces("application/json")]
  [ProducesResponseType(StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public class PokemonController : ControllerBase {
    private readonly PokemonServices pokemonServices;

    public PokemonController(PokemonServices pokemonServices) {
      this.pokemonServices = pokemonServices;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<PokemonViewModel>> Create(
      [FromBody] PokemonViewModel data
    ) {
      data.Validate();

      if (data.Invalid)
        return BadRequest(data.Notifications);

      var response = await pokemonServices.CreatePokemon(data);

      return Ok(response);
    }
  }
}
