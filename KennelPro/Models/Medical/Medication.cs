using KennelPro.Models.Dogs;

namespace KennelPro.Models.Medical;

public class Medication
{
    public Guid Id { get; set; }


    public Guid DogId { get; set; }

    public Dog Dog { get; set; } = null!;


    public string Name { get; set; } = string.Empty;


    public string? Description { get; set; }


    public DateTime Date { get; set; }
}