namespace KennelPro.Models.Authentication;

public class VerificationCode
{
    public Guid Id { get; set; }


    public string Email { get; set; } = string.Empty;


    public string Code { get; set; } = string.Empty;


    public DateTime ExpirationDate { get; set; }
}