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
using PokeAPI.ViewModels.Exceptions;

namespace PokeAPI.Controllers {
  [ApiController]
  [Route("v1/[controller]")]
  public class SessionsController : ControllerBase {
    // Services!

    public SessionsController() {
      //
    }

    [HttpPost]
    [AllowAnonymous]
    public ActionResult<SessionViewModel> Create(
      [FromBody] AuthenticationAdminDTO auth
    ) {
      auth.Validate();

      if (auth.Invalid) {
        throw new ViewModelException(
          "An error occurred while trying to authenticate the system admin",
          StatusCodes.Status400BadRequest,
          auth.Notifications
        );
      }

      // Calls the auth service!

      return new SessionViewModel();
    }
  }
}
