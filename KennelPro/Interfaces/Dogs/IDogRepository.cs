using KennelPro.Models.Dogs;

namespace KennelPro.Interfaces.Dogs;

public interface IDogRepository
{
    Task<List<Dog>> GetAllAsync();

    Task<List<Dog>> GetByKennelIdAsync(Guid kennelId);

    Task<Dog?> GetByIdAsync(Guid id);

    Task AddAsync(Dog dog);

    Task UpdateAsync(Dog dog);

    Task DeleteAsync(Guid id);

    Task<bool> ExistsAsync(Guid id);
}