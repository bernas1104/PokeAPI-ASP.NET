using System;

namespace Services.DTOs {
  public class AuthenticatedAdminDTO {
    public int Id { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
  }
}