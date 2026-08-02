using KennelPro.Models.Documents;
using KennelPro.Models.Enums;
using KennelPro.Models.Kennels;
using KennelPro.Models.Litters;
using KennelPro.Models.Medical;
using KennelPro.Models.Reproduction;

namespace KennelPro.Models.Dogs;

public class Dog
{
    public Guid Id { get; set; }


    public Guid KennelId { get; set; }

    public Kennel Kennel { get; set; }
        = null!;


    public Guid BreedId { get; set; }

    public Breed Breed { get; set; }
        = null!;


    public string Name { get; set; }
        = string.Empty;


    public Gender Gender { get; set; }


    public DateTime BirthDate { get; set; }


    public string? Color { get; set; }


    public string? ChipNumber { get; set; }


    public string? PedigreeNumber { get; set; }


    public string? Notes { get; set; }


    public DateTime CreatedAt { get; set; }
        = DateTime.UtcNow;



    // Медицинская история
    public ICollection<MedicalRecord> MedicalRecords { get; set; }
        = new List<MedicalRecord>();


    // Документы
    public ICollection<Document> Documents { get; set; }
        = new List<Document>();


    // Титулы
    public ICollection<Title> Titles { get; set; } = new List<Title>();


    // Репродукция
    public ICollection<HeatCycle> HeatCycles { get; set; } = new List<HeatCycle>();


    public ICollection<Mating> Matings { get; set; } = new List<Mating>();


    // Пометы
    public ICollection<Litter> LittersAsMother { get; set; } = new List<Litter>();


    public ICollection<Litter> LittersAsFather { get; set; } = new List<Litter>();
}