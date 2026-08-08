using KennelPro.Models.Dogs;

namespace KennelPro.Models.Medical;

public class Disease
{
    public Guid Id { get; set; }


    public Guid DogId { get; set; }

    public Dog Dog { get; set; } = null!;


    public string Name { get; set; } = string.Empty;


    public string? AllergyInfo { get; set; }


    public string? Notes { get; set; }

    public string? Symptoms { get; set; }

    public DateTime StartDate { get; set; } = DateTime.UtcNow;

    public DateTime? RecoveryDate { get; set; }

    public string Status { get; set; } = "Active";
}
