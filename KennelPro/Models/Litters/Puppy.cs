using KennelPro.Models.Enums;

namespace KennelPro.Models.Litters;

public class Puppy
{
    public Guid Id { get; set; }


    public Guid LitterId { get; set; }

    public Litter Litter { get; set; } = null!;


    // Номер внутри помета
    public int Number { get; set; }

    public string Name { get; set; } = string.Empty;


    public Gender Gender { get; set; }


    public double Weight { get; set; }


    public string? Status { get; set; }
    // например:
    // Available
    // Reserved
    // Sold


    public DateTime BirthDate { get; set; }

    public string? ChipNumber { get; set; }

    public string? PedigreeNumber { get; set; }

    public string? Notes { get; set; }
}
