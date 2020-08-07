using System;

namespace Services.Exceptions {
  public class ServiceException : Exception {
    public int StatusCode { get; set ; }

    public ServiceException(string message, int statusCode) : base(message) {
      StatusCode = statusCode;
    }
  }
}
