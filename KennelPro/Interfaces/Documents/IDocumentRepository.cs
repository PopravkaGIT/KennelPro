using KennelPro.Models.Documents;

namespace KennelPro.Interfaces.Documents;

public interface IDocumentRepository
{
    Task<List<Document>> GetAllAsync();

    Task<Document?> GetByIdAsync(Guid id);

    Task AddAsync(Document document);

    Task UpdateAsync(Document document);

    Task DeleteAsync(Guid id);

    Task<bool> ExistsAsync(Guid id);
}