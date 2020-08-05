using System;

namespace Services.Exceptions {
  public class AdminException : Exception {
    public int StatusCode { get; set; }

    public AdminException(string message, int statusCode) : base(message) {
      StatusCode = statusCode;
    }
  }
}
