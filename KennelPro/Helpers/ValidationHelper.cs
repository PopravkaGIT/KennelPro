using System.Text.RegularExpressions;

namespace KennelPro.Helpers;

public static class ValidationHelper
{
    public static bool IsEmail(string email)
    {
        return Regex.IsMatch(
            email,
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }

    public static bool IsPhone(string phone)
    {
        return Regex.IsMatch(
            phone,
            @"^\+?[0-9]{9,15}$");
    }

    public static bool HasText(string? text)
    {
        return !string.IsNullOrWhiteSpace(text);
    }
}