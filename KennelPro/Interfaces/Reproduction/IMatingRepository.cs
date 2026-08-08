using KennelPro.Models.Reproduction;

namespace KennelPro.Interfaces.Reproduction;

public interface IMatingRepository
{
    Task<List<Mating>> GetAllAsync();
    Task<IEnumerable<Mating>> GetByKennelIdAsync(Guid kennelId);

    Task<Mating?> GetByIdAsync(Guid id);

    Task AddAsync(Mating mating);

    Task UpdateAsync(Mating mating);

    Task DeleteAsync(Guid id);

    Task<bool> ExistsAsync(Guid id);
}
