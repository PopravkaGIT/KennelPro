using KennelPro.Models.Documents;

namespace KennelPro.Interfaces.Documents;

public interface ITitleRepository
{
    Task<List<Title>> GetAllAsync();

    Task<Title?> GetByIdAsync(Guid id);

    Task AddAsync(Title title);

    Task UpdateAsync(Title title);

    Task DeleteAsync(Guid id);

    Task<bool> ExistsAsync(Guid id);
}