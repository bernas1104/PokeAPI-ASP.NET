using Flunt.Validations;
using Flunt.Notifications;

namespace Services.DTOs {
  public class AuthenticationAdminDTO : Notifiable, IValidatable {
    public string Email { get; set; }
    public string Password { get; set; }

    public void Validate() {
      AddNotifications(
        new Contract()
        .IsEmail(Email, "Email", "Should be a valid e-mail")
        .IsNotNullOrEmpty(Email, "Email", "E-mail is required for login")
        .HasMaxLen(
          Email,
          100,
          "Email",
          "E-mail should be at most 100 characters"
        )
        .IsNotNullOrEmpty(
          Password,
          "Password",
          "Password is required for login"
        )
        .HasMinLen(
          Password,
          6,
          "Password",
          "Password should have at least 6 characters"
        )
        .HasMaxLen(
          Password,
          12,
          "Password",
          "Password should have at most 12 characters"
        )
      );
    }
  }
}
