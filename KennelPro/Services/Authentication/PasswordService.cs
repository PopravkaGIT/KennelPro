using System.Security.Cryptography;
using System.Text;

namespace KennelPro.Services.Authentication;

public class PasswordService
{
    /// <summary>
    /// Creates SHA-256 hash from password.
    /// </summary>
    public string HashPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password cannot be empty.", nameof(password));

        using SHA256 sha256 = SHA256.Create();

        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        byte[] hashBytes = sha256.ComputeHash(passwordBytes);

        return Convert.ToBase64String(hashBytes);
    }

    /// <summary>
    /// Verifies password against stored hash.
    /// </summary>
    public bool VerifyPassword(string password, string storedHash)
    {
        if (string.IsNullOrWhiteSpace(password))
            return false;

        if (string.IsNullOrWhiteSpace(storedHash))
            return false;

        string passwordHash = HashPassword(password);

        return string.Equals(
            passwordHash,
            storedHash,
            StringComparison.Ordinal);
    }
}