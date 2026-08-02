namespace KennelPro.Models.Dogs;

public class Breed
{
    public Guid Id { get; set; }


    public string Name { get; set; } = string.Empty;


    public ICollection<Dog> Dogs { get; set; } = new List<Dog>();
}