using KennelPro.Models.Dogs;

namespace KennelPro.Models.Documents;

public class Document
{
    public Guid Id { get; set; }


    public Guid DogId { get; set; }

    public Dog Dog { get; set; } = null!;


    public string Name { get; set; }
        = string.Empty;


    // Путь к файлу
    public string FilePath { get; set; } = string.Empty;


    // PDF, Word, Image
    public string FileType { get; set; } = string.Empty;


    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}