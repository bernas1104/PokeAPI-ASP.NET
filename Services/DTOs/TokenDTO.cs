using System;

namespace Services.DTOs {
  public class TokenDTO {
    public string Token { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
  }
}
