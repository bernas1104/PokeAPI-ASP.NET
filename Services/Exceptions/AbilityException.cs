namespace Services.Exceptions {
  public class AbilityException : ServiceException {
    public AbilityException(string message, int statusCode)
      : base(message, statusCode) {}
  }
}
