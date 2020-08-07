using System.Threading.Tasks;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

using Services.DTOs;
using PokeAPI.ViewModels;
using Services.Interfaces;
using Services.Exceptions;

namespace PokeAPI.Controllers {
  [ApiController]
  [Route("v1/[controller]")]
  [Produces("application/json")]
  public class SessionsController : ControllerBase {
    private readonly IMapper mapper;
    private readonly AdminAuthenticationService adminAuthentication;

    public SessionsController(
      AdminAuthenticationService adminAuthentication,
      IMapper mapper
    ) {
      this.adminAuthentication = adminAuthentication;
      this.mapper = mapper;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<SessionViewModel>> Create(
      [FromBody] AuthenticationAdminDTO auth
    ) {
      auth.Validate();

      if (auth.Invalid) {
        throw new ServiceDTOException(
          "An error occurred while trying to authenticate the system admin",
          StatusCodes.Status400BadRequest,
          auth.Notifications
        );
      }

      var session = await adminAuthentication.AuthenticateAdmin(auth);
      var sessionViewModel = mapper.Map<SessionViewModel>(session);

      return Ok(session);
    }
  }
}
