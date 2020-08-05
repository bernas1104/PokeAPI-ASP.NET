using System;
using System.Collections.Generic;

using Flunt.Notifications;

namespace PokeAPI.ViewModels.Exceptions {
  public class ViewModelException : Exception {
    public int StatusCode { get; }
    public IReadOnlyCollection<Notification> Validations { get; }

    public ViewModelException(string message) : base(message) {}

    public ViewModelException(
      string message,
      int statusCode,
      IReadOnlyCollection<Notification> validations
    ) : base(message) {
      StatusCode = statusCode;
      Validations = validations;
    }
  }
}
