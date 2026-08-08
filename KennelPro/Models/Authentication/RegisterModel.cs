namespace KennelPro.Models.Authentication;

public class RegisterModel
{
    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string ConfirmPassword { get; set; } = string.Empty;

    public string KennelName { get; set; } = string.Empty;

    public bool AcceptTerms { get; set; }
}