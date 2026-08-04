using KennelPro.Interfaces.Documents;
using KennelPro.Models.Documents;

namespace KennelPro.Services.Documents;

public class DocumentService
{
    private readonly IDocumentRepository _documentRepository;

    public DocumentService(IDocumentRepository documentRepository)
    {
        _documentRepository = documentRepository;
    }

    public async Task<List<Document>> GetDocumentsAsync()
    {
        return await _documentRepository.GetAllAsync();
    }

    public async Task<Document?> GetDocumentAsync(Guid id)
    {
        return await _documentRepository.GetByIdAsync(id);
    }

    public async Task AddDocumentAsync(Document document)
    {
        await _documentRepository.AddAsync(document);
    }

    public async Task UpdateDocumentAsync(Document document)
    {
        await _documentRepository.UpdateAsync(document);
    }

    public async Task DeleteDocumentAsync(Guid id)
    {
        await _documentRepository.DeleteAsync(id);
    }
}