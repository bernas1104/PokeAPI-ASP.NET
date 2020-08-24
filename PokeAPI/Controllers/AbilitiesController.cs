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
  public class AbilitiesController : ControllerBase {
    private readonly AbilityServices abilityServices;

    public AbilitiesController(AbilityServices abilityServices) {
      this.abilityServices = abilityServices;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<AbilityViewModel>> Create (
      [FromBody] AbilityViewModel data
    ) {
      data.Validate();

      if (data.Invalid)
        return BadRequest(data.Notifications);

      var response = await abilityServices.CreateAbility(data);

      return Created(nameof(Create), response);
    }
  }
}
