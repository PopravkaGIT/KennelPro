using KennelPro.Models.Dogs;

namespace KennelPro.Models.Medical;

public class Medication
{
    public Guid Id { get; set; }


    public Guid DogId { get; set; }

    public Dog Dog { get; set; } = null!;


    public string Name { get; set; } = string.Empty;


    public string? Description { get; set; }


    public string? Dosage { get; set; }

    public string? Frequency { get; set; }

    public DateTime StartDate { get; set; } = DateTime.UtcNow;

    public DateTime? EndDate { get; set; }

    // Kept for compatibility with databases created before Phase 3.
    public DateTime Date { get; set; }
}
