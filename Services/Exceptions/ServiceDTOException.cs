using System;
using System.Collections.Generic;

using Flunt.Notifications;

namespace Services.Exceptions {
  public class ServiceDTOException : Exception {
    public int StatusCode { get; }
    public IReadOnlyCollection<Notification> Validations { get; }

    public ServiceDTOException(string message) : base(message) {}

    public ServiceDTOException(
      string message,
      int statusCode,
      IReadOnlyCollection<Notification> validations
    ) : base(message) {
      StatusCode = statusCode;
      Validations = validations;
    }
  }
}
