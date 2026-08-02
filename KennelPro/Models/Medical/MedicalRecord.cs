using KennelPro.Models.Dogs;

namespace KennelPro.Models.Medical;

public class MedicalRecord
{
    public Guid Id { get; set; }


    public Guid DogId { get; set; }

    public Dog Dog { get; set; } = null!;


    public string? Notes { get; set; }


    public DateTime Date { get; set; } = DateTime.UtcNow;
}