namespace Services.Exceptions {
  public class AdminException : ServiceException {
    public AdminException(string message, int statusCode)
      : base(message, statusCode) {}
  }
}
