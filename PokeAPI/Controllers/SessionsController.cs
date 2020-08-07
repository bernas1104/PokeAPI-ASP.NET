using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
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
    private readonly AdminAuthenticationService adminAuthentication;

    public SessionsController(AdminAuthenticationService adminAuthentication) {
      this.adminAuthentication = adminAuthentication;
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

      return Ok(new SessionViewModel() {
        Id = session.Id,
        Email = session.Email,
        Token = session.Token,
        ValidFrom = session.ValidFrom,
        ValidTo = session.ValidTo
      });
    }
  }
}
