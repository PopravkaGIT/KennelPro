using KennelPro.Models.Dogs;

namespace KennelPro.Models.Reproduction;

public class Mating
{
    public Guid Id { get; set; }


    // Сука
    public Guid FemaleDogId { get; set; }

    public Dog FemaleDog { get; set; } = null!;


    // Кобель
    public Guid MaleDogId { get; set; }

    public Dog MaleDog { get; set; } = null!;


    public DateTime Date { get; set; }


    public string? Notes { get; set; }
}