using KennelPro.Models.Kennels;

namespace KennelPro.Models.Authentication;

public class User
{
    public Guid Id { get; set; }


    public string Email { get; set; } = string.Empty;


    public string PasswordHash { get; set; } = string.Empty;


    public bool EmailConfirmed { get; set; }


    public Guid KennelId { get; set; }

    public Kennel Kennel { get; set; } = null!;


    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}