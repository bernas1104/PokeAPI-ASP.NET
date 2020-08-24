namespace Services.Exceptions {
  public class PokemonException : ServiceException {
    public PokemonException(string message, int statusCode)
      : base(message, statusCode) {}
  }
}
