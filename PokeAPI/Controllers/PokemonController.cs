using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

using PokeAPI.Utilities;
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
      [ModelBinder(BinderType = typeof(JsonModelBinder))] PokemonViewModel data,
      IFormFile photo
    ) {
      data.Validate();

      if (data.Invalid)
        return BadRequest(data.Notifications);

      if (photo == null)
        return BadRequest("Pokemon must have a photo");

      data.PokemonPhoto = photo;

      var response = await pokemonServices.CreatePokemon(data);

      return Created(nameof(Create), response);
    }

    [HttpPatch]
    [Route("mark-seen/{id:int}")]
    public async Task<ActionResult<bool>> Mark(int id) {
      if (ValidatePokemonNumber(id))
        return BadRequest("Pokemon number must be between 1 and 151");

      await pokemonServices.MarkPokemonAsSeen(id);

      return NoContent();
    }

    [HttpPatch]
    [Route("mark-captured/{id:int}")]
    public async Task<ActionResult<bool>> Capture(int id) {
      if (ValidatePokemonNumber(id))
        return BadRequest("Pokemon number must be between 1 and 151");

      await pokemonServices.MarkPokemonAsCaptured(id);

      return NoContent();
    }

    [HttpPatch]
    [Authorize]
    [Route("add-evolution")]
    public async Task<ActionResult<PokemonViewModel>> AddEvolution(
      [FromBody] EvolutionViewModel data
    ) {
      data.Validate();

      if (data.Invalid)
        return BadRequest(data.Notifications);

      var response = await pokemonServices.AddPokemonEvolution(data);

      return Ok(response);
    }

    private bool ValidatePokemonNumber(int id) {
      return (id <= 0 || id >= 152);
    }
  }
}
