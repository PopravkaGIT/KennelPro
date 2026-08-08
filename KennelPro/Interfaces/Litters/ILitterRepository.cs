using KennelPro.Models.Litters;

namespace KennelPro.Interfaces.Litters;

public interface ILitterRepository
{
    Task<List<Litter>> GetAllAsync();
    Task<IEnumerable<Litter>> GetByKennelIdAsync(Guid kennelId);

    Task<Litter?> GetByIdAsync(Guid id);

    Task AddAsync(Litter litter);

    Task UpdateAsync(Litter litter);

    Task DeleteAsync(Guid id);

    Task<bool> ExistsAsync(Guid id);
}
