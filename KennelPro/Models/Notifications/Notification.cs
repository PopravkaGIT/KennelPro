using KennelPro.Models.Authentication;

namespace KennelPro.Models.Notifications;

public class Notification
{
    public Guid Id { get; set; }


    public Guid UserId { get; set; }

    public User User { get; set; } = null!;


    public string Title { get; set; } = string.Empty;


    public string Message { get; set; } = string.Empty;


    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    public bool IsRead { get; set; }
}