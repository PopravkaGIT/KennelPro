namespace KennelPro.Services.Authentication;

public class VerificationService
{
    private readonly Random _random = new();


    public string GenerateCode()
    {
        return _random.Next(100000, 999999).ToString();
    }


    public bool VerifyCode(string enteredCode, string realCode)
    {
        return enteredCode == realCode;
    }
}