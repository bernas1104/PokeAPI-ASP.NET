using System;
using System.Collections.Generic;

using Flunt.Notifications;

namespace Services.Exceptions {
  public class ServiceViewModelException : Exception {
    public int StatusCode { get; }
    public IReadOnlyCollection<Notification> Validations { get; }

    public ServiceViewModelException(string message) : base(message) {}

    public ServiceViewModelException(
      string message,
      int statusCode,
      IReadOnlyCollection<Notification> validations
    ) : base(message) {
      StatusCode = statusCode;
      Validations = validations;
    }
  }
}
