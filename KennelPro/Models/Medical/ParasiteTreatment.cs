using KennelPro.Models.Dogs;

namespace KennelPro.Models.Medical;

public class ParasiteTreatment
{
    public Guid Id { get; set; }


    public Guid DogId { get; set; }

    public Dog Dog { get; set; }
        = null!;


    public string ProductName { get; set; } = string.Empty;


    public DateTime Date { get; set; }


    public DateTime? NextTreatmentDate { get; set; }


    public string? Notes { get; set; }
}