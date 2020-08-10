using System;

namespace Services.ViewModels {
  public class SessionViewModel {
    public int Id { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
  }
}
