using KennelPro.Models.Authentication;
using KennelPro.Models.Dogs;

namespace KennelPro.Models.Kennels;

public class Kennel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    // Логотип питомника
    public string? LogoPath { get; set; }

    public string? Country { get; set; }

    public string? City { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    // Пользователи питомника
    public ICollection<User> Users { get; set; }
        = new List<User>();


    // Собаки питомника
    public ICollection<Dog> Dogs { get; set; }
        = new List<Dog>();
}