using KennelPro.Models.Dogs;

namespace KennelPro.Models.Documents;

public class Title
{
    public Guid Id { get; set; }


    public string Name { get; set; } = string.Empty;


    public DateTime AwardedDate { get; set; }


    public ICollection<Dog> Dogs { get; set; } = new List<Dog>();
}