using KennelPro.Models.Dogs;

namespace KennelPro.Models.Litters;

public class Litter
{
    public Guid Id { get; set; }


    // Например: A, B, C
    public string Name { get; set; } = string.Empty;


    public DateTime BirthDate { get; set; }

    public string? Notes { get; set; }


    // Мать
    public Guid MotherDogId { get; set; }

    public Dog MotherDog { get; set; } = null!;


    // Отец
    public Guid FatherDogId { get; set; }

    public Dog FatherDog { get; set; } = null!;


    public ICollection<Puppy> Puppies { get; set; } = new List<Puppy>();
}
