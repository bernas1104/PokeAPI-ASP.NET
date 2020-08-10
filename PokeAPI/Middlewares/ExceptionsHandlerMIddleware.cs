using System;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

using Services.Exceptions;

namespace PokeAPI.Middlewares {
  public class ExceptionsHandlerMiddleware : IMiddleware {
    private readonly IWebHostEnvironment Env;
    private readonly string ContentType = "application/json";

    public ExceptionsHandlerMiddleware(IWebHostEnvironment env) {
      Env = env;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next) {
      try {
        await next(context);
      } catch (ServiceViewModelException sde) {
        await HandleValidationException(context, sde);
      } catch (ServiceException se) {
        await HandleServiceException(context, se);
      } catch (Exception ex) {
        await HandleUnknownException(context, ex);
      }
    }

    private Task HandleValidationException(
      HttpContext context,
      ServiceViewModelException sdException
    ) {
      var json = JsonConvert.SerializeObject(new {
        sdException.StatusCode,
        message = sdException.Message,
        details = sdException.Validations,
      });

      context.Response.StatusCode = sdException.StatusCode;
      context.Response.ContentType = ContentType;

      return context.Response.WriteAsync(json);
    }

    private Task HandleServiceException(
      HttpContext context,
      ServiceException sException
    ) {
      var json = JsonConvert.SerializeObject(new {
        sException.StatusCode,
        message = sException.Message,
      });

      context.Response.StatusCode = sException.StatusCode;
      context.Response.ContentType = ContentType;

      return context.Response.WriteAsync(json);
    }

    private Task HandleUnknownException(
      HttpContext context,
      Exception exception
    ) {
      const int statusCode = StatusCodes.Status500InternalServerError;

      var json = JsonConvert.SerializeObject(new {
          statusCode,
          message = "An error occurred while processing you request",
          details = Env.IsDevelopment() ? exception : null,
        },
        new JsonSerializerSettings() {
          NullValueHandling = NullValueHandling.Ignore
        }
      );

      context.Response.StatusCode = statusCode;
      context.Response.ContentType = ContentType;

      return context.Response.WriteAsync(json);
    }
  }
}
