using KennelPro.Models.Dogs;

namespace KennelPro.Models.Reproduction;

public class HeatCycle
{
    public Guid Id { get; set; }


    public Guid DogId { get; set; }

    public Dog Dog { get; set; } = null!;


    // Начало течки
    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Status { get; set; }


    // Следующая предполагаемая течка
    public DateTime? NextCycleDate { get; set; }


    public string? Notes { get; set; }
}
