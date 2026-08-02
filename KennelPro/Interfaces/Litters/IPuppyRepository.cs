using KennelPro.Models.Litters;

namespace KennelPro.Interfaces.Litters;

public interface IPuppyRepository
{
    Task<List<Puppy>> GetAllAsync();

    Task<Puppy?> GetByIdAsync(Guid id);

    Task AddAsync(Puppy puppy);

    Task UpdateAsync(Puppy puppy);

    Task DeleteAsync(Guid id);

    Task<bool> ExistsAsync(Guid id);
}